/**
 * 该文件可自行根据业务逻辑进行调整
 */
import { useAppConfig } from '@vben/hooks';
import { preferences } from '@vben/preferences';
import {
  errorMessageResponseInterceptor,
  HttpStatusCode,
  RequestClient,
} from '@vben/request';
import { useAccessStore } from '@vben/stores';

import { ElMessage } from 'element-plus';

import { useAuthStore } from '#/store';

// import { refreshTokenApi } from './core';

const { apiURL } = useAppConfig(import.meta.env, import.meta.env.PROD);

function createRequestClient(baseURL: string) {
  const client = new RequestClient({
    baseURL,
  });

  /**
   * 重新认证逻辑
   */
  // async function doReAuthenticate() {
  //   console.warn('Access token or refresh token is invalid or expired. ');
  //   const accessStore = useAccessStore();
  //   const authStore = useAuthStore();
  //   accessStore.setAccessToken(null);
  //   if (
  //     preferences.app.loginExpiredMode === 'modal' &&
  //     accessStore.isAccessChecked
  //   ) {
  //     accessStore.setLoginExpired(true);
  //   } else {
  //     await authStore.logout();
  //   }
  // }

  /**
   * 刷新token逻辑
   */
  // async function doRefreshToken() {
  //   const accessStore = useAccessStore();
  //   const resp = await refreshTokenApi();
  //   const newToken = resp.data;
  //   accessStore.setAccessToken(newToken);
  //   return newToken;
  // }

  function formatToken(token: null | string) {
    return token ? `Bearer ${token}` : null;
  }

  // 请求头处理
  client.addRequestInterceptor({
    fulfilled: async (config) => {
      const accessStore = useAccessStore();
      config.headers.Authorization = formatToken(accessStore.accessToken);
      config.headers['Accept-Language'] = preferences.app.locale;
      return config;
    },
  });

  // response数据解构
  client.addResponseInterceptor({
    fulfilled: (response) => {
      const { data, status, headers, config } = response;
      const accessStore = useAccessStore();
      if (headers.accesstoken) {
        accessStore.setAccessToken(response.headers.accesstoken);
      }
      if (config.responseType == 'blob') {
        return response;
      }
      if (status == HttpStatusCode.NoContent) {
        ElMessage.success('操作成功');
        return;
      }
      if (status == HttpStatusCode.Ok) {
        return data;
      }
      throw Object.assign({}, response, { response });
    },
  });

  // token过期的处理
  // client.addResponseInterceptor(
  //   authenticateResponseInterceptor({
  //     client,
  //     doReAuthenticate,
  //     doRefreshToken,
  //     enableRefreshToken: preferences.app.enableRefreshToken,
  //     formatToken,
  //   }),
  // );

  // 通用的错误处理,如果没有进入上面的错误处理逻辑，就会进入这里
  client.addResponseInterceptor(
    errorMessageResponseInterceptor((msg: string, error) => {
      const { code } = error;
      if (code == 'ECONNABORTED' || code == 'ERR_NETWORK') {
        ElMessage.warning(msg);
        return;
      }
      const {
        response: { data },
        status,
      } = error;
      switch (status) {
        case HttpStatusCode.Unauthorized:
          const authStore = useAuthStore();
          authStore.logout();
          break;
        case HttpStatusCode.BadRequest:
          ElMessage.warning(data.error?.message || msg);
          break;
        case HttpStatusCode.Forbidden:
          ElMessage.warning(data.error?.message || msg);
          break;
        default:
          ElMessage.error(data.error?.message || msg);
          break;
      }
    }),
  );

  return client;
}

export const requestClient = createRequestClient(apiURL);

export const baseRequestClient = new RequestClient({ baseURL: apiURL });
