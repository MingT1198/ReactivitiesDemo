import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vueDevTools from 'vite-plugin-vue-devtools'
import legacy from '@vitejs/plugin-legacy'
import { viteSingleFile } from 'vite-plugin-singlefile'

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    // vueDevTools(),
    // viteSingleFile(),
    legacy({
      targets: ['defaults', 'not IE 11']
    })
  ],
  base: '/ReactivitiesDemo/',
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    },
  },
  server:{
    port: 3000,
    proxy: {
      '/api': { // 將所有以 /api 開頭的請求代理到目標伺服器
        target: 'https://localhost:5001/api', // 目標伺服器位址
        changeOrigin: true, // 必須設定，用於模擬同源請求
        secure: false,
        rewrite: (path) => path.replace(/^\/api/, '') // 可選，重寫路徑，移除 /api 前綴
      }
    }
  }
})
