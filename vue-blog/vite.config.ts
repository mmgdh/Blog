import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import importToCDN from 'vite-plugin-cdn-import'


// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    importToCDN({
      modules: [
        {
          name:'tocbot',
          var:'tocbot',
          path:'https://cdnjs.cloudflare.com/ajax/libs/tocbot/4.5.0/tocbot.css'
        }
      ]
    })],
})
