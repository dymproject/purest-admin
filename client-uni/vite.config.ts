import { defineConfig } from "vite";
import uni from "@dcloudio/vite-plugin-uni";
import path from 'path' // 引入 path 模块
// https://vitejs.dev/config/
export default defineConfig({
  plugins: [uni()],
  resolve: {
    alias: {
      // 设置别名
      '@': path.resolve(__dirname, 'src'), // 将 @ 指向 src 目录
    },
  },
  // 服务端渲染
  server: {
    // 端口号
    port: 5173,
    host: "0.0.0.0",
    // 本地跨域代理 https://cn.vitejs.dev/config/server-options.html#server-proxy
    proxy: {
      '/signalr-hubs': {
        target: 'http://localhost:5062', // 替换为你的后端API服务器地址
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
        target: "http://localhost:5062",
        changeOrigin: true,
        bypass(req, res, options: any) {
          //这段代码可以看到代理后的地址
          const proxyURL = options.target + req.url;
          console.log("proxyURL", proxyURL);
          res.setHeader("x-req-proxyURL", proxyURL); // 设置响应头可以看到
        }
      }
    },
  }
});
