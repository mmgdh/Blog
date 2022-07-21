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
let content =ref('');
ArticleId=router.query.ArticleId as string;
console.log(ArticleId);
ArticleService.prototype.GetArticleById(ArticleId).then(ret=>{
content.value=ret.content;
});


</script>
<style scoped lang='less'>

</style>