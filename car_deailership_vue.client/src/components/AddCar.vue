<template>
    <div class="spec">
        {{username=gettersAuthData.userName}}
        {{addCar(users)}}


    </div>
{{users}}

CarAdd
</template>

<script>
import {mapGetters} from "vuex";
import axios from "axios";
const API_URL = "https://localhost:7046/"
export default {
    name: "AddCar",
    computed: {
        ...mapGetters('auth', {
            gettersAuthData: 'getAuthData'
        }),

    },
    data(){
        return{
            username:'',
            users:''
        }
    },

    methods:{
        async addCar(id){
            this.userIdmet();
            axios.post(API_URL + "api/Order/SetOrder?userId="+ id+ "&carId=" + this.$route.params.id + "&Status=Оформлен");
            /*this.$router.push({
                path:'/dashboard'
            })*/
            if(this.users!=undefined){
                this.$router.push({
                    path:'/dashboard'
                })
            }
        },
         userIdmet() {

            axios.get(API_URL + "api/Account/GetUserId?Login=" + this.username).then(
                (response) => {

                    const data = response.data;
                    this.users = data;

                }
            );
    },

},
    mounted() {
        this.userIdmet();

    }
}
</script>

<style scoped>

</style>