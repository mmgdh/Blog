<template>
  <div class="Comtainer DivCSS hvr-grow">
    <div class="Title">博客分类</div>
    <div class="ClassifyDiv" v-for="classify in ArticleClassifies">
        {{classify.classifyName}}
        <span class="ArticleCount">
          {{classify.articleCount}}
        </span>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, toRefs, onBeforeMount, onMounted, watchEffect, computed } from 'vue';
import { ArticleClassify } from '../../../Entities/E_Article';
import { useArticleStore } from '../../../Store/Store'
import { storeToRefs } from 'pinia';
const ArticleStore=useArticleStore();

let ArticleClassifies=ref({} as Array<ArticleClassify>);
ArticleClassifies=storeToRefs(ArticleStore).Classifies;
console.log(ArticleClassifies.value)
</script>
<style scoped>
.Comtainer{
    width: 100%;
}
.Title{
  font-size: large;
  background-color: antiquewhite;
}
.ClassifyDiv{
  padding: 3px;
  width: 100%;
}
.ClassifyDiv:hover{
  background-color: rgb(66, 104, 137);
  color: pink;
}
.ArticleCount{
    float: right;
    margin-right: 5px;
}
</style>