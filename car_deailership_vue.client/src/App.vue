<template xmlns="http://www.w3.org/1999/html">
    <header class="container-fluid">
        <div class="row">
            <div class="col-6">

                <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="currentColor" viewBox="0 0 16 16"
                     id="logo" class="pu" data-bs-toggle="offcanvas" data-bs-target="#offcanvasScrolling"
                     aria-controls="offcanvasScrolling">
                    <path d="M0 1.5A1.5 1.5 0 0 1 1.5 0h8A1.5 1.5 0 0 1 11 1.5v2A1.5 1.5 0 0 1 9.5 5h-8A1.5 1.5 0 0 1 0 3.5zM1.5 1a.5.5 0 0 0-.5.5v2a.5.5 0 0 0 .5.5h8a.5.5 0 0 0 .5-.5v-2a.5.5 0 0 0-.5-.5z"/>
                    <path d="m7.823 2.823-.396-.396A.25.25 0 0 1 7.604 2h.792a.25.25 0 0 1 .177.427l-.396.396a.25.25 0 0 1-.354 0zM0 8a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v5a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2zm1 3v2a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1v-2zm14-1V8a1 1 0 0 0-1-1H2a1 1 0 0 0-1 1v2zM2 8.5a.5.5 0 0 1 .5-.5h9a.5.5 0 0 1 0 1h-9a.5.5 0 0 1-.5-.5m0 4a.5.5 0 0 1 .5-.5h6a.5.5 0 0 1 0 1h-6a.5.5 0 0 1-.5-.5"/>
                </svg>
            </div>
            <div class="col-6">

            </div>
        </div>
    </header>
    <div class="offcanvas offcanvas-start" data-bs-scroll="true" data-bs-backdrop="false" tabindex="-1"
         id="offcanvasScrolling" aria-labelledby="offcanvasScrollingLabel">
        <div class="offcanvas-header">
            <h5 class="offcanvas-title" id="offcanvasScrollingLabel">Offcanvas с прокруткой тела</h5>
            <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Закрыть"></button>
        </div>
        <div class="offcanvas-body">
            <a class="white" @click="main">Главная</a>
            <br>

            <a class="white" @click="lk">Личный кабинет</a>
        </div>
    </div>
  <keep-alive>
      <router-view></router-view>
  </keep-alive>
</template>


<script>
export default {
    data() {
        return {
            isOpen: false
        }
    },
    methods: {
        menu() {
            if (this.isOpen == false) {
                this.isOpen = true;
            } else {
                this.isOpen = false;
            }
        },
        main(){
            this.$router.push({
                path:'/'
            })
        },
        lk(){
            this.$router.push({
                path:'/dashboard'
            })
        }
    }
}
</script>