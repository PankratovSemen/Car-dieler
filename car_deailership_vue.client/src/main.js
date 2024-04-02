import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import {createWebHistory,createRouter} from "vue-router";
import VueLazyload from 'vue-lazyload'


import loadimage from '@/assets/img/load.gif';

const car = () => import('./components/Car.vue');

const routes = [
    { path: '/', component: () => import('./components/Home.vue') },
    {path: '/car/:id',component: car}];
const router = createRouter({
    history:createWebHistory(),
    routes
})
const app = createApp(App);
app.use(router);
app.use(VueLazyload, {
    preLoad: 1.3,
    loading: loadimage,
    attempt: 1,
    listenEvents: [ 'scroll' ]
});

app.mount("#app");
