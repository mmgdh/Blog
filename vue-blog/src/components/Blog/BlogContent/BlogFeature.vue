<template>
    <div class="Feature">
        <HorizontalArticleVue :articledata="refStore.CurPageArticles.value[0]"></HorizontalArticleVue>
        <BlogFeatureList></BlogFeatureList>
    </div>
</template>

<script setup lang='ts'>
import HorizontalArticleVue from '../Article/HorizontalArticle.vue';
import { useArticleStore } from '../../../Store/ArticleStore'
import { storeToRefs } from 'pinia';
import BlogFeatureList from './BlogFeatureList.vue';
let ArticleStore = useArticleStore();
let refStore = storeToRefs(ArticleStore);
</script>

<style>

</style>