import Axios, {
  type AxiosInstance,
  type AxiosRequestConfig,
  type CustomParamsSerializer,
  HttpStatusCode
} from "axios";
import type {
  PureHttpError,
  RequestMethods,
  PureHttpResponse,
  PureHttpRequestConfig
} from "./types.d";
import { stringify } from "qs";
// import NProgress from "../progress";
import { message } from "@/utils/message";
import { useUserStoreHook } from "@/store/modules/user";
import { ElLoading, ElMessage } from "element-plus";

// 相关配置请参考：www.axios-js.com/zh-cn/docs/#axios-request-config-1
const defaultConfig: AxiosRequestConfig = {
  // 请求超时时间
  timeout: 10000,
  headers: {
    Accept: "application/json, text/plain, */*",
    "Content-Type": "application/json",
    "X-Requested-With": "XMLHttpRequest"
  },
  // 数组格式参数序列化（https://github.com/axios/axios/issues/5142）
  paramsSerializer: {
    serialize: stringify as unknown as CustomParamsSerializer
  }
};

class PureHttp {
  constructor() {
    this.httpInterceptorsRequest();
    this.httpInterceptorsResponse();
  }

  /** token过期后，暂存待执行的请求 */
  private static requests = [];

  /** 防止重复刷新token */
  private static isRefreshing = false;

  /** 初始化配置对象 */
  private static initConfig: PureHttpRequestConfig = {};

  /** 保存当前Axios实例对象 */
  private static axiosInstance: AxiosInstance = Axios.create(defaultConfig);

  /** 重连原始请求 */
  private static retryOriginalRequest(config: PureHttpRequestConfig) {
    return new Promise(resolve => {
      PureHttp.requests.push((token: string) => {
        config.headers["Authorization"] = `Bearer ${token}`;
        resolve(config);
      });
    });
  }

  /** 请求拦截 */
  private httpInterceptorsRequest(): void {
    PureHttp.axiosInstance.interceptors.request.use(
      async (config: PureHttpRequestConfig): Promise<any> => {
        // 开启进度条动画
        // NProgress.start();
        // 优先判断post/get等方法是否传入回调，否则执行初始化设置等回调
        if (typeof config.beforeRequestCallback === "function") {
          config.beforeRequestCallback(config);
          return config;
        }
        if (PureHttp.initConfig.beforeRequestCallback) {
          PureHttp.initConfig.beforeRequestCallback(config);
          return config;
        }
        /** 请求白名单，放置一些不需要token的接口（通过设置请求白名单，防止token过期后再请求造成的死循环问题） */
        // const whiteList = ["/login"];
        if (config.url.endsWith("/login")) {
          return Promise.resolve(config);
        } else {
          config.headers.Authorization =
            "Bearer " + useUserStoreHook().getToken;
        }
        return Promise.resolve(config);
      },
      error => {
        return Promise.reject(error);
      }
    );
  }

  /** 响应拦截 */
  private httpInterceptorsResponse(): void {
    const instance = PureHttp.axiosInstance;
    instance.interceptors.response.use(
      (response: PureHttpResponse) => {
        const $config = response.config;
        // 关闭进度条动画
        // NProgress.done();
        // 优先判断post/get等方法是否传入回调，否则执行初始化设置等回调
        if ($config.responseType == 'blob') {
          return response;
        }
        if (typeof $config.beforeResponseCallback === "function") {
          $config.beforeResponseCallback(response);
          return response.data;
        }
        if (PureHttp.initConfig.beforeResponseCallback) {
          PureHttp.initConfig.beforeResponseCallback(response);
          return response.data;
        }
        if (response.headers.accesstoken) {
          useUserStoreHook().setToken(response.headers.accesstoken);
        }
        if (response.status == HttpStatusCode.NoContent) {
          message("操作成功", { type: "success" });
        }
        return response.data;
      },
      (error: PureHttpError) => {
        const $error = error;
        const { response } = error;
        const data = response.data as any;
        switch (response.status) {
          case HttpStatusCode.Unauthorized:
            ElMessage({
              message: "登陆超时，5秒后返回登录页面",
              type: "warning"
            });
            setTimeout(() => {
              useUserStoreHook().logOut();
            }, 5000);
            break;
          case HttpStatusCode.Forbidden:
            message("未授权", {
              type: "error"
            });
            break;
          case HttpStatusCode.BadRequest:
            if (data.error) {
              message(data.error.message + "\r\n" + data.error.details, {
                type: "warning"
              });
            }
            break;
          case HttpStatusCode.InternalServerError:
            console.log(data);
            if (data.error) {
              message(data.error.message, {
                type: "warning"
              });
            }
            break;
          default:
            message("接口异常，请反馈详细浮现流程给管理员", {
              type: "error"
            });
        }
        $error.isCancelRequest = Axios.isCancel($error);
        // 关闭进度条动画
        // NProgress.done();
        return Promise.reject($error);
      }
    );
  }

  /** 通用请求工具函数 */
  public request<T>(
    method: RequestMethods,
    url: string,
    param?: AxiosRequestConfig,
    axiosConfig?: PureHttpRequestConfig
  ): Promise<T> {
    const config = {
      method,
      url,
      ...param,
      ...axiosConfig
    } as PureHttpRequestConfig;
    config.baseURL = "/api/v1";
    // 单独处理自定义请求/响应回调
    return new Promise((resolve, reject) => {
      const loading = ElLoading.service({
        lock: true,
        text: "加载中……",
        background: "rgba(0, 0, 0, 0)"
      });
      PureHttp.axiosInstance
        .request(config)
        .then((response: undefined) => {
          resolve(response);
        })
        .catch(error => {
          reject(error);
        }).finally(() => loading.close());
    });
  }

  /** 单独抽离的post工具函数 */
  public post<T, P>(
    url: string,
    params?: AxiosRequestConfig<T>,
    config?: PureHttpRequestConfig
  ): Promise<P> {
    return this.request<P>("post", url, params, config);
  }

  /** 单独抽离的get工具函数 */
  public get<T, P>(
    url: string,
    params?: AxiosRequestConfig<T>,
    config?: PureHttpRequestConfig
  ): Promise<P> {
    return this.request<P>("get", url, params, config);
  }
}

export const http = new PureHttp();
