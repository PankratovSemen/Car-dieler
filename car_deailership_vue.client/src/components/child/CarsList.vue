<template>

        <div class="row">
            <div class="col col-lg-3 back text-center " v-for="i in cars">
                <sliders :id="i.Номер_машины" class="gran"></sliders>
                <br>
                <h2 @click="moveCar(i.Номер_машины)" class="linkCar">
                    {{ i.Заголовок }}</h2>
                <p id="descr">{{ i.Описание.substring(0,200) }}</p>
                <span class="coast">{{ i.Цена }} RUB</span>
                <br>
                <br>
            </div>
        </div>

</template>

<script>
import axios from "axios";
import Sliders from '@/components/child/sliderCar.vue'

const API_URL = "https://localhost:7046/"
export default {

    data(){
        return{
            cars:[]
        }
    },
    components:{
        Sliders,

    },
    methods:{
        async refreshData(){
            axios.get(API_URL + "api/Cars/GetCar").then(
                (response) => {
                    const data = response.data;
                    this.cars = data;


                }
            )
        },
        moveCar(id){
            this.$router.push({
                path:'/car/'+id
            })
        }
    },
    created() {
        this.refreshData();
        this.timer = setInterval(this.refreshData, 10000);
    },
    mounted: function () {
        this.refreshData();


    },

}
</script>

<style scoped>

</style>