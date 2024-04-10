import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'

import VueLazyload from 'vue-lazyload'
import store from './store/index'
import appRouter from '@/appRouter.js'


import loadimage from '@/assets/img/load.gif';





const app = createApp(App);
app.use(appRouter);
app.use(VueLazyload, {
    preLoad: 1.3,
    loading: loadimage,
    attempt: 1,
    listenEvents: [ 'scroll' ]
});
app.use(store);
app.mount("#app");
