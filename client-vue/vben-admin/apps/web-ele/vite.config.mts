import { defineConfig } from '@vben/vite-config';

import ElementPlus from 'unplugin-element-plus/vite';

export default defineConfig(async () => {
  return {
    application: {},
    vite: {
      plugins: [
        ElementPlus({
          format: 'esm',
        }),
      ],
      server: {
        proxy: {
          '/signalr-hubs': {
            target: 'http://localhost:5062', // 替换为你的后端API服务器地址
            changeOrigin: true,
            ws: true, // 确保启用WebSocket代理支持
            bypass(req, res: any, options: any) {
              //这段代码可以看到代理后的地址
              const proxyURL = options.target + req.url;
              console.log('proxyURL', proxyURL);
              // res.setHeader('x-req-proxyURL', proxyURL); // 设置响应头可以看到
            },
            // rewrite: (path) => path.replace(/^\/signalr/, ''),
          },
          '/api': {
            // 这里填写后端地址
            target: 'http://localhost:5062',
            changeOrigin: true,
            bypass(req, res: any, options: any) {
              //这段代码可以看到代理后的地址
              const proxyURL = options.target + req.url;
              console.log('proxyURL', proxyURL);
              res.setHeader('x-req-proxyURL', proxyURL); // 设置响应头可以看到
            },
          },
        },
      },
    },
  };
});
