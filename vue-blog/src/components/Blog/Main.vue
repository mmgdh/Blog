<template>

    <div class="BlogBody">
        <BlogMenu class="BlogMenu"></BlogMenu>
        <div class="Container">
            <router-view class="BlogView"></router-view>
        </div>
    </div>
</template>

<script setup lang="ts">
import BlogMenu from './BlogMenu.vue'
import { useRouter } from 'vue-router'

let router = useRouter()
router.push('/BlogIndex')
</script>
<style scoped>
.BlogBody {
    background-image: url('../../Img/sky.jpg');
    background-repeat: no-repeat;
    background-size: cover;
    background-position: 100% 100%;
    background-attachment: fixed;
    position: absolute;
    color: #fff;
    /* z-index: -2; */
    display: flex;
    justify-content: center;
    width: 100%;
    align-items: center;

}



.BlogBody::before {
    content: '';
    position: absolute;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    z-index: -3;
}

.Container {
    width: 80%;
    /* z-index: 1; */
    /* height: 3000px; */
}

.BlogView {
    /* z-index: 2; */
}
</style>