<template>
  <div class="MdContainerStyle">
    <Md :preview-only="true" :show-code-row-number="true" v-model="content" class="mdStyle hvr-float-shadow" ></Md>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, toRefs, onBeforeMount, onMounted, watchEffect, computed } from 'vue';
import Md from 'md-editor-v3'
import 'md-editor-v3/lib/style.css'
import { useRoute } from 'vue-router'
import ArticleService from '../../../Services/ArticleService'

let router = useRoute();
let ArticleId: string;
let content = ref('');
ArticleId = router.query.ArticleId as string;
ArticleService.prototype.GetArticleById(ArticleId).then(ret => {
  content.value = ret.content;
});


</script>
<style scoped>
.MdContainerStyle {
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}
.mdStyle{
  width: 80%;
  border-radius: 10px;
  box-shadow: 10px 10px 20px rgba(33, 44, 55, .3);
}
</style>