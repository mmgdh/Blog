<template>
  <div class="MdContainerStyle ">
    <div class="BlogTitle">
      {{ CurArticle.title }}
    </div>

    <Md :preview-only="true" :show-code-row-number="true" v-model="content"
      class="DivCSS mdStyle hvr-float-shadow"></Md>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, toRefs, onBeforeMount, onMounted, watchEffect, computed } from 'vue';
import Md from 'md-editor-v3'
import 'md-editor-v3/lib/style.css'
import { useRoute } from 'vue-router'
import{Article} from '../../../Entities/E_Article'
import ArticleService from '../../../Services/ArticleService'

let router = useRoute();
let ArticleId: string;
let content = ref('');
let _Article: Article = {

} as Article;
let CurArticle = ref(_Article)
ArticleId = router.query.ArticleId as string;
ArticleService.prototype.GetArticleById(ArticleId).then(ret => {
  CurArticle.value = ret;
  content.value = ret.content;
});


</script>
<style scoped>
.MdContainerStyle {
  width: 100%;
  height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
}
.BlogTitle{
  font-size: xx-large;
  font-family: 'Courier New', Courier, monospace;
  
}
.mdStyle {
  width: 100%;
  height: 1500px;
  background-color: #2c3e50;
  padding: 20px;
}
</style>