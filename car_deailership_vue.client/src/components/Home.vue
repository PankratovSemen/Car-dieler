<template>

  <!--    <div class="container-fluid" v-if="isOpen==true">
          <div class="row">
              <div class="col-3" id="openMenu">
                  dhdh
              </div>
          </div>-->



  <!--Content-->
    <br>
    <div class="container">
        <div class="container">
            <div class="row" id="search">
                <div class="col-12">
                    <button @click="menu" id="all">Все</button>
                    <button @click="menu" id="old">б/у</button>
                    <button @click="menu" id="new">Новые</button>
                    <br>
                    <br>

                </div>
                <div class="col col-lg-4 col-sm-4">
                    <div class="block-1">
                        <span>Марка</span>
                        <select id="inp" name="mark">
                            <option value="iphone 6s">iPhone 6S</option>
                            <option value="lumia 950">Lumia 950</option>
                        </select>

                        <span>Модель</span>
                        <br>

                        <select id="inp" name="phone">
                            <option value="iphone 6s">iPhone 6S</option>
                            <option value="lumia 950">Lumia 950</option>
                        </select>

                        <span>Объем двигателя</span>
                        <br>

                        <input id="inp">

                        <span>Мощность</span>
                        <br>

                        <input id="inp">
                    </div>
                </div>
                <div class="col col-lg-4 col-sm-4">

                    <div class="block-2">
                        <span>Цена</span>
                        <div id="inline">
                            <label>
                                От
                                <input>
                            </label>
                            <label>
                                до
                                <input>
                            </label>
                        </div>
                        <span>Коробка</span>
                        <input id="inp">
                        <span>Привод</span>
                        <input id="inp">
                        <span>Тип руля</span>
                        <input id="inp">
                    </div>
                </div>
                <div class="col col-lg-4 col-sm-4">

                    <div class="block-2">
                        <span>Год</span>
                        <input id="inp">
                        <div class="d-flex justify-content-end">
                            <button class="btn-search">Найти</button>
                        </div>


                    </div>
                </div>
            </div>
        </div>
        <Cars ></Cars>
    </div>

</template>

<script>
import Cars from './child/CarsList.vue'
export default {

    data(){
        return{

        }
    },
    components:{

        Cars
    },
    methods:{

    },

    mounted: function () {


    },



}
</script>

<style scoped>

</style>