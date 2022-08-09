<template>
    <BlogMenu></BlogMenu>
    <div class="Content">
        <router-view></router-view>
    </div>
</template>

<script setup lang="ts">
import BlogMenu from './BlogMenu.vue'
import { useRouter } from 'vue-router'

let router = useRouter()
router.push('/BlogIndex')
</script>
<style scoped>
.Content {
    width: 100%;
    height: 100%;
    position: absolute;
}
</style>