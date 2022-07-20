<template>
  <div >
    <Md :preview-only="true" v-model="content"></Md>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, toRefs, onBeforeMount, onMounted, watchEffect, computed } from 'vue';
import Md from 'md-editor-v3'
import 'md-editor-v3/lib/style.css'
import { useRoute } from 'vue-router'
import ArticleService from '../../Services/ArticleService'
import {Article} from "../../Entities/E_Article"

let router =useRoute();
let ArticleId:string;
ArticleId=router.query.ArticleId as string;
console.log(ArticleId);
var aa=await ArticleService.prototype.GetArticleById(ArticleId) 
let _Article:Article =aa as Article;
let content =ref('');
content.value=_Article.content;

</script>
<style scoped lang='less'>

</style>