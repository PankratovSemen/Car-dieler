<template>

    <div class="container-fluid" >
        <ul class="nav nav-tabs">
            <li class="nav-item">
                <a class="nav-link" aria-current="page" @click="$router.push( {path:'/dashboard'})">Заказы</a>
            </li>
            <li class="nav-item">
                <a class="nav-link active" @click="$router.push( {path:'/dashboard/createUser'})">Добавить пользователя</a>
            </li>

        </ul>
    </div>
    <div className="d-flex justify-content-center">

        <div className="row">

            <div className="col col-sm-12 reg-box">
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
                <p>Роль</p>
                <select class="form-select" aria-label="Default select example" v-model="role">
                    <option selected>Выберите роль</option>
                    <option value="Пользователь">Пользователь</option>
                    <option value="Администратор">Администратор</option>
                    <option value="Менеджер">Менеджер</option>
                </select>

                <button className="btn-register" @click="register">Создать пользователя</button>
            </div>
        </div>
    </div>

</template>

<script>
import axios from "axios";
import {createRouter as $router} from "vue-router";

const API_URL = "https://localhost:7046/"
export default {
    data() {
        return {
            surname: '',
            name: '',
            login: '',
            middlename: '',
            password: '',
            role: 'Пользователь',
            id: ''
        }
    },
    methods: {
        async register() {
            axios.post(API_URL + "api/Account/Register?surname=" + this.surname + "&name=" + this.name + "&middlename=" + this.middlename + "&login=" + this.login + "&password=" + this.password + "&role=" + this.role)
                .then(
                    (response) => {
                        const data = response.data;
                        this.id = data;
                        this.$router.push({
                            path: '/dashboard'
                        })
                    }
                );
        }
    }

}
</script>

