<template>
    <div class="Feature">
        <HorizontalArticleVue :articledata="refStore.CurPageArticles.value[0]"></HorizontalArticleVue>
    </div>
</template>

<script setup lang='ts'>
import HorizontalArticleVue from '../Article/HorizontalArticle.vue';
import { useArticleStore } from '../../../Store/ArticleStore'
import { storeToRefs } from 'pinia';
import { Article } from '../../../Entities/E_Article';
import { onBeforeMount, watch } from 'vue';
let ArticleStore = useArticleStore();
let refStore = storeToRefs(ArticleStore);
</script>

<style>

</style>