<template>

    <div class="d-flex justify-content-center">
        <div class="row">
            <div class="col col-sm-12 reg-box">
                <p>Фамилия</p>
                <input v-model="surname">
                <p>Имя</p>
                <input v-model="name">
                <p>Отчество</p>
                <input v-model="middlename">
                <p>Логин</p>
                <input v-model="login">
                <p>Пароль</p>
                <input v-model="password">

                <button class="btn-register" @click="register">Зарегестироваться</button>
            </div>
        </div>
    </div>

</template>

<script>
import axios from "axios";
import {createRouter as $router} from "vue-router";

const API_URL = "https://localhost:7046/"
export default {
    data(){
        return{
            surname:'',
            name:'',
            login:'',
            middlename:'',
            password:'',
            role:'Пользователь',
            id:''
        }
    },
    methods:{
        async register(){
            axios.post(API_URL + "api/Account/Register?surname=" + this.surname +"&name=" + this.name + "&middlename="+this.middlename+ "&login="+this.login + "&password="+ this.password+"&role=" + this.role)
            .then(
                (response) => {
                    const data = response.data;
                    this.id = data;
                    this.$router.push({
                        path:'/login'
                    })
                }
            );
        }
    }

}
</script>

