<template>
  <div class="Comtainer DivCSS hvr-grow">
    <div class="ClassifyDiv" v-for="classify in ArticleClassifies">
        {{classify.classifyName}}
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, toRefs, onBeforeMount, onMounted, watchEffect, computed } from 'vue';
import { ArticleClassify } from '../../../Entities/E_Article';
import { useArticleStore } from '../../../Store/Store'
const ArticleStore=useArticleStore();

let ArticleClassifies=ref({} as Array<ArticleClassify>);
ArticleClassifies.value=ArticleStore.$state.Classifies;
console.log(ArticleClassifies.value)
</script>
<style scoped>
.Comtainer{
    width: 100%;
}
</style>