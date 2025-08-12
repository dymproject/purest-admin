import { getPluginsList } from "./build/plugins";
import { include, exclude } from "./build/optimize";
import { type UserConfigExport, type ConfigEnv, loadEnv } from "vite";
import {
  root,
  alias,
  wrapperEnv,
  pathResolve,
  __APP_INFO__
} from "./build/utils";

export default ({ mode }: ConfigEnv): UserConfigExport => {
  const { VITE_CDN, VITE_PORT, VITE_COMPRESSION, VITE_PUBLIC_PATH } =
    wrapperEnv(loadEnv(mode, root));
  return {
    base: VITE_PUBLIC_PATH,
    root,
    resolve: {
      alias
    },
    // 服务端渲染
    server: {
      // 端口号
      port: VITE_PORT,
      host: "0.0.0.0",
      // 本地跨域代理 https://cn.vitejs.dev/config/server-options.html#server-proxy
      proxy: {
        '/signalr-hubs': {
          target: 'http://pure.purestadmin.com', // 替换为你的后端API服务器地址
          changeOrigin: true,
          ws: true, // 确保启用WebSocket代理支持
          bypass(req, res, options: any) {
            //这段代码可以看到代理后的地址
            const proxyURL = options.target + req.url;
            console.log("proxyURL", proxyURL);
            // res.setHeader("x-req-proxyURL", proxyURL); // 设置响应头可以看到
          }
          // rewrite: (path) => path.replace(/^\/signalr/, ''),
        },
        "/api": {
          // 这里填写后端地址
          target: "http://pure.purestadmin.com",
          changeOrigin: true,
          bypass(req, res, options: any) {
            //这段代码可以看到代理后的地址
            const proxyURL = options.target + req.url;
            console.log("proxyURL", proxyURL);
            res.setHeader("x-req-proxyURL", proxyURL); // 设置响应头可以看到
          }
        }
      },
      // 预热文件以提前转换和缓存结果，降低启动期间的初始页面加载时长并防止转换瀑布
      warmup: {
        clientFiles: ["./index.html", "./src/{views,components}/*"]
      }
    },
    plugins: getPluginsList(VITE_CDN, VITE_COMPRESSION),
    // https://cn.vitejs.dev/config/dep-optimization-options.html#dep-optimization-options
    optimizeDeps: {
      include,
      exclude
    },
    build: {
      // https://cn.vitejs.dev/guide/build.html#browser-compatibility
      target: "es2015",
      sourcemap: false,
      // 消除打包大小超过500kb警告
      chunkSizeWarningLimit: 4000,
      rollupOptions: {
        input: {
          index: pathResolve("./index.html", import.meta.url)
        },
        // 静态资源分类打包
        output: {
          chunkFileNames: "static/js/[name]-[hash].js",
          entryFileNames: "static/js/[name]-[hash].js",
          assetFileNames: "static/[ext]/[name]-[hash].[ext]"
        }
      }
    },
    define: {
      __INTLIFY_PROD_DEVTOOLS__: false,
      __APP_INFO__: JSON.stringify(__APP_INFO__)
    }
  };
};
