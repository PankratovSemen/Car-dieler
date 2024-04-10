<template>
<div class="container" id="car-cont">
    <div class="row">
        <div class="col col-lg-6 col-sm-12 im-car">
            <sliders :id="$route.params.id"/>

        </div>
        <div class="col col-lg-5 col-sm-10 title" v-for="item in cars">
            <span>{{item.Заголовок}}</span>
            <br>
            <span class="coast-car">
               {{item.Цена}} RUB
            </span>

            <button class="btn-buy" @click="buyclick">Купить</button>
            <br>
            <br>
            <div class="descriptionCar">
                <div> Двигатель:{{item.Объем_двигателя}}</div>
                <div> Мощность:{{item.Мощность}}</div>
                <div> Коробка передач:{{item.Коробка}}</div>
                <div> Привод: {{item.Привод}}</div>
                <div> Руль:{{item.Тип_руля}}</div>
                <div> Тип автомобиля:{{item.Тип_автомобиля}}</div>
                <div>VIN:{{item.VIN}}</div>
                <div>Дополнительно:{{item.Дополнительные_услуги}}
                <div>Описание</div>
                <div class="ds">{{item.Описание}}</div>
            </div>
        </div>
            </div>

    </div>


</div>
</template>

<script>
import Sliders from "@/components/child/sliderCar.vue";
import axios from "axios";
const API_URL = "https://localhost:7046/"
export default {

    data(){
        return{
            cars:[]
        }
    },

    components:{
        Sliders
    },
    methods:{
        async refreshData(id){
            axios.get(API_URL + "api/Cars/CarFind?id=" + id).then(
                (response) => {
                    const data = response.data;
                    this.cars = data;


                }
            )
        },
        buyclick(){
            this.$router.push({
                path:'/AddCar/' + this.$route.params.id
            })
        }
    },
    mounted: function () {
        this.refreshData(this.$route.params.id);


    },
}
</script>

<style scoped>

</style>