<template>
<div class="container-fluid align-center ">

    <div class="row">
        <div class="col col-lg-5 col-sm-10 centobj">
            <h1 class="text-center titleAuth">Авторизация</h1>
            <div class="container-fluid">
                <p class="text-left">Логин</p>
                <input class="inp100" v-model="username">
                <br>
                <br>
                <p class="text-left">Пароль</p>
                <input class="inp100" v-model="password">
                <br>
                <br>
                <br>
                <div class="row">
                    <div class="col col-sm-12">

                        <div class="inner"><a class="text-center btn-register" @click="this.$router.push('/Register')">Регистрация</a></div>
                        <div class="inner"><button class="btn-sign" @click="login">Войти</button></div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</div>
</template>

<script>

import {mapActions, mapGetters} from 'vuex';
export default {
    data(){
        return{
            username:'',
            password:''
        }
    },
    computed:{
        ...mapGetters('auth',{
            getterLoginStatus:'getLoginStatus'
        })
    },
    methods:{
        ...mapActions('auth',{
            actionLogin:'login'
        }),
        async login(){
            await this.actionLogin([this.username,this.password]);
            if(this.getterLoginStatus === 'success'){
                this.$router.push("/dashboard");
            }else{
                alert('failed to login')
            }
        }
    }
};
</script>

<style scoped>

</style>