import { createApp } from 'vue'
import { VueQueryPlugin } from '@tanstack/vue-query'
import '@fontsource-variable/inter'
import '@/styles/main.css'
import App from './App.vue'
import router from '@/router'

createApp(App).use(router).use(VueQueryPlugin).mount('#app')
