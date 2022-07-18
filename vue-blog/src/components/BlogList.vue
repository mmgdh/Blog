<template>
  <div v-for="_Article in Ref_ArticleList">
    <div>{{ _Article.Title }}</div>
  </div>
  123
</template>

<script setup lang='ts'>
import { ref, onBeforeMount } from 'vue'
import { number } from 'vue-types';
import ArticleService from '../Services/ArticleService';
import { Article } from '../Entities/E_Article'
interface requestData {
  page: number,
  pageSize: number
}
const pageRequest: requestData = {
  page: 0,
  pageSize: 10
}

let ArticleList: Array<Article> = []
let Ref_ArticleList = ref(ArticleList)

onBeforeMount(() =>
  ArticleService.prototype.GetArticleByPage(pageRequest)
    .then(ret =>
      Ref_ArticleList.value = ret
    )
);





</script>

<style>
</style>