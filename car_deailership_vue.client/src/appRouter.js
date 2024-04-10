
import { createRouter, createWebHistory } from 'vue-router';

import store from "@/store/index.js";
const car = () => import('./components/Car.vue');
const routes = [
    {path: '/Register',component:()=>import('./components/Register.vue'),meta: {requiredAuth: false}},
    {path: '/AddCar/:id',component:()=>import('./components/AddCar.vue'),meta: {requiredAuth: true}},
    { path: '/', component: () => import('./components/Home.vue'), meta: {
            requiredAuth: true
        }},
    {path: '/car/:id',component: car,meta: {
            requiredAuth: true
        }},
    {path: '/Login',component: ()=> import('./components/Login.vue'),meta: {
            requiredAuth: false
        }},
    {path: '/dashboard',component: ()=> import('./components/dashbord.vue'),meta: {
            requiredAuth: true
        }},
    { path: '/404', component: () => import('./components/Home.vue') },
    { path: '/:pathMatch(.*)*', redirect: '/404' },

];




export const routeConfig = createRouter({
    history: createWebHistory(),
    routes: routes
});
export default routeConfig;

routeConfig.beforeEach((to,from, next) => {
    console.log(store.getters["auth/getAuthData"].token);
    if(!store.getters["auth/getAuthData"].token){
        const access_token = localStorage.getItem("access_token");

        if(access_token){
            const data = {
                access_token:access_token,

            };
            store.commit('auth/saveTokenData',data);
        }
    }
    const auth = store.getters["auth/isTokenActive"];

    if(to.fullPath == "/"){
        return next();
    }
    else if(auth && !to.meta.requiredAuth && to.fullPath=="/Login"){
        return next({path:"/dashboard"});
    }
    else if(!auth && to.meta.requiredAuth){
        return next({path: "/login"});
    }

    return next();
});