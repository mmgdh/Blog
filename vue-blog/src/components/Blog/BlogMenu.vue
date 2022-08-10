<template>
    <div class="navBody">
        <nav class="nav">
            <div class="container">
                <h1 class="logo"><a @click="topage('/BlogIndex')">主页</a></h1>
                <ul>
                    <li><a href="#" class="current">Home</a> </li>
                    <li><a href="#">T1</a> </li>
                    <li><a href="#">T2</a> </li>
                    <li><a @click="topage('/Login')">登录</a> </li>
                </ul>
            </div>
        </nav>
    </div>



</template>
<script setup lang="ts">
import { defineComponent, ref } from 'vue';
import { MailOutlined, AppstoreOutlined, SettingOutlined } from '@ant-design/icons-vue';
import { useRouter } from 'vue-router'

let router = useRouter()
let topage = (url: string) => {
    router.push(url)
}

</script>
<style>
* {
    box-sizing: border-box;
    margin: 0;
    padding: 0;
}

.navBody {
    font-family: 'Open Sans', sans-serif;
    color: #FFF;
}



.nav {
    position: fixed;
    background-color: #222;
    top: 0;
    left: 0;
    right: 0;
    transition: all 0.3s ease-in-out;
}

.nav .container {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px 0;
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

/* .nav.active {
    background-color: #fff;
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.3);
}

.nav.active a {
    color: #000
}

.nav.active .container {
    padding: 10px 0;
}

.nav a.current,
.nav a:hover {
    color: #c0392b;
    font-weight: bold;
} */
</style>