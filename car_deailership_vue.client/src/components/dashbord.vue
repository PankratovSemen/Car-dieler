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
{{userId}}

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
            userId: '',
            username:'',
            role:'',
            isAdmin:false
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
        }
    },
    mounted() {
        this.userIdmet();

    }
}

</script>