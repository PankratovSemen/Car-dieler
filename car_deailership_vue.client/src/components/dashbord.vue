<template>
    <div class="spec">
        {{username=gettersAuthData.userName}}
        {{getOrder()}}
        {{role=gettersAuthData.role}}
        {{getRole()}}
    </div>

      <div class="row">


        <div class="container-fluid">
            <table class="table" v-if="isAdmin==false">
                <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Название машины</th>
                    <th scope="col">Цена</th>
                    <th scope="col">Статус закааз</th>
                </tr>
                </thead>
                <tbody>
                <tr v-for="item in order">
                    <th scope="row">{{item.Номер_заказа}}</th>
                    <td>{{item.Название_машины}}</td>
                    <td>{{item.Цена}} РУБ</td>
                    <td>{{item.Статус}}</td>
                </tr>
                </tbody>
            </table>
        </div>
          <div class="container-fluid" v-if="isAdmin==true">
              <ul class="nav nav-tabs">
                  <li class="nav-item">
                      <a class="nav-link active" aria-current="page" @click="$router.push( {path:'/dashboard'})">Заказы</a>
                  </li>
                  <li class="nav-item">
                      <a class="nav-link" @click="$router.push( {path:'/dashboard/createUser'})">Добавить пользователя</a>
                  </li>

              </ul>


              <div class="col col-sm-12">
                  <table class="table">
                      <thead>
                      <tr>
                          <th scope="col">#</th>
                          <th scope="col">Название машины</th>
                          <th scope="col">Цена</th>
                          <th scope="col">Статус закааз</th>
                          <th scope="col">Изменение</th>
                      </tr>
                      </thead>
                      <tbody>
                      <tr v-for="item in orderAll">
                          <th scope="row">{{item.Номер_заказа}}</th>
                          <td>{{item.Название_машины}}</td>
                          <td>{{item.Цена}} РУБ</td>
                          <td>{{item.Статус}}</td>
                          <td><button class="btn-primary" @click="editBtn(item.Номер_заказа)">Изменить</button> </td>
                      </tr>
                      </tbody>
                  </table>
              </div>
          </div>


    </div>


    <div class="container-fluid" v-if="isManager==true">



        <div class="col col-sm-12">
            <table class="table">
                <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Название машины</th>
                    <th scope="col">Цена</th>
                    <th scope="col">Статус закааз</th>
                    <th scope="col">Изменение</th>
                </tr>
                </thead>
                <tbody>
                <tr v-for="item in orderAll">
                    <th scope="row">{{item.Номер_заказа}}</th>
                    <td>{{item.Название_машины}}</td>
                    <td>{{item.Цена}} РУБ</td>
                    <td>{{item.Статус}}</td>
                    <td><button class="btn-primary" @click="editBtn(item.Номер_заказа)">Изменить</button> </td>
                </tr>
                </tbody>
            </table>
        </div>
    </div>



</template>
<script>
import { mapGetters } from 'vuex'
import axios from "axios";
const API_URL = "https://localhost:7046/"
export default {
    computed: {
        ...mapGetters('auth', {
            gettersAuthData: 'getAuthData'
        }),

    },
    data() {
        return {
            order: [],
            orderAll: [],
            userId: '',
            username:'',
            role:'',
            isAdmin:false,
            isManager:false,
        }
    },
    methods: {
      getOrder() {
            axios.get(API_URL + "api/Order/GetOrder?userId=" + this.userId).then(
                (response) => {
                    const data = response.data;
                    this.order = data;

                }
            );

          axios.get(API_URL + "api/Order/GetOrderAll").then(
              (response) => {
                  const data = response.data;
                  this.orderAll = data;

              }
          );
        },
        async userIdmet() {

            axios.get(API_URL + "api/Account/GetUserId?Login=" + this.username).then(
                (response) => {

                    const data = response.data;
                    this.userId = data;

                }
            );

        },
        async getRole(){
          if(this.role=="Администратор"){
              this.isAdmin = true;
          }
            if(this.role=="Менеджер"){
                this.isManager = true;
            }
        },
        editBtn(id){
            this.$router.push({
                path:'dashboard/editorder/' + id
            })
        }
    },
    mounted() {
        this.userIdmet();

    }
}

</script>