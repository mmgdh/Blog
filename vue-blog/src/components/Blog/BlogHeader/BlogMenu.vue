<template>
    <div class="menuBody">
        <nav class="nav">
            <div class="container">
                <div class="logo" @click="topage('/BlogIndex')" >
                    <span>零柒贰</span>
                    <span class="logoBottom" @click="topage('/BlogIndex')">LINQIER</span>
                </div>

                <ul>
                    <li><a href="#" class="current">Home</a> </li>
                    <li><a @click="topage('/BlogManage')">管理</a> </li>
                </ul>
            </div>
        </nav>
        <OperationBar>

        </OperationBar>
    </div>



</template>
<script setup lang="ts">
import { useRouter } from 'vue-router'
import OperationBar from './OperationBar.vue'

let router = useRouter()
let topage = (url: string) => {
    router.push(url)
}

</script>
<style scoped lang="less">

.menuBody {
    display: flex;
    flex-grow: 1;
    font-family: 'Open Sans', sans-serif;
    color: #FFF;
    height: 100px;
    width: 100%;
    justify-content: space-between;
    align-items: center;
}

.logo {
    display: flex;
    flex-direction: column;
    align-items: center;
    font-size: 30px;
    cursor: pointer;

    .logoBottom {
        font-size: 10px;
    }
}

.nav {
    transition: all 0.3s ease-in-out;
}

.nav .container {
    display: flex;
    justify-content: space-between;
    align-items: center;
    transition: all 0.3s ease-in-out;
}

.nav ul {
    display: flex;
    list-style-type: none;
    align-items: center;
    justify-content: center;
}

.nav a {
    color: #fff;
    text-decoration: none;
    padding: 7px 15px;
    transition: all 0.3s ease-in-out;
}
</style>