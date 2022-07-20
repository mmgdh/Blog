<template>
  <div class="ArticleStyle"  v-for="_Article in Ref_ArticleList" :key="_Article.id" @click="router.push({path:'ShowArticle',query:{'ArticleId':_Article.id}})">
    <div class="TitleStyle">{{ _Article.title }}</div>
    <span class="TagStyle" v-for="_Tag in _Article.tags">
      {{ _Tag.tagName }}
    </span>
    <!-- <div>{{ _Article.title }}</div> -->
  </div>
</template>

<script setup lang='ts'>
import { ref, onBeforeMount } from 'vue'
import { number } from 'vue-types';
import ArticleService from '../../Services/ArticleService';
import { Article } from '../../Entities/E_Article'
import { useRouter } from 'vue-router'
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

onBeforeMount(() => {
  ArticleService.prototype.GetArticleByPage(pageRequest)
    .then(ret =>
      Ref_ArticleList.value = ret
    );
}
);
let router = useRouter()





</script>

<style>
.ArticleStyle{
  border: 3px;
  border-style: solid;
  border-color: aqua;
  margin: 10px;
  width: 300px;
}

.TitleStyle{
  font-size: large;
  color:cornflowerblue;
}

.TagStyle {
  color: red;
  width: 30px;
  margin: 10px;
}
</style>