<template>
    <Carousel class="gran">
        <Slide v-for="slide in images.slice(0,15)" :key="slide" >

            <img v-lazy="slide.Изображение"  class="carIMG"/>


        </Slide>

        <template #addons>
            <Navigation />
            <Pagination/>
        </template>
    </Carousel>


</template>
<script>
import axios from "axios";
import 'vue3-carousel/dist/carousel.css'
import { Carousel, Slide, Pagination, Navigation } from 'vue3-carousel'




const API_URL = "https://localhost:7046/"
export default {

    props: ['id'],
    components:{
        Carousel,
        Slide,
        Pagination,
        Navigation,

    },
    data() {

        return {
            images: [],


        }
    },
    methods: {
        getImage(id) {


            axios.get(API_URL + "api/Cars/GetCarImages?car=" + id).then(
                (response) => {
                    const data = response.data;
                    this.images = data;

                }
            );


        },





    },
    mounted: function () {
        this.getImage(this.id);
    },

}
</script>

<style scoped>

</style>