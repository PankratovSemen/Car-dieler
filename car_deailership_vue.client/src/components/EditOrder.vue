<template>

<div class="container-fluid">
    <div class="row" v-for="item in order">
        <p>Название машины:{{item.Название_машины}}</p>
        <p>VIN:{{item.VIN}}</p>
        <p>Цена:{{item.Цена}}</p>
        <p>Статус</p>
        <select class="form-select" aria-label="Default select example" v-model="Status">
            <option selected>Выберите статус</option>
            <option value="Оформлен">Оформлен</option>
            <option value="Отказ">Отказ</option>
            <option value="Оплачено">Оплачено</option>
            <option value="Готов к выдаче">Готов к выдаче</option>
        </select>
        <button class="btn-register" @click="edit">Изменить статус</button>
    </div>
</div>
</template>

<script>
import axios from "axios";
const API_URL = "https://localhost:7046/";
export default {

    name: "EditOrder",
    data(){
        return{
            Status:'',
            order:[]
        }
    },
    methods:{
        edit(){
            alert(this.Status);
            axios.post(API_URL + "api/Order/SetStatus?OrderId=" + this.$route.params.id + "&Status="+this.Status);
            this.$router.push({
                path:'/dashboard'
            })

        },
        async getInfo(){
            axios.get(API_URL + "api/Order/GetOrderId?idorder=" + this.$route.params.id).then(
                (response) => {
                    const data = response.data;
                    this.order = data;

                }
            );
        }
    },
    mounted() {
        this.getInfo();
    }

}
</script>

<style scoped>

</style>