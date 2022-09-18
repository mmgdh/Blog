<template>
  <router-view></router-view>
  <!-- <Main></Main> -->
</template>
<script setup lang="ts">
import { onBeforeMount } from 'vue'
import { useArticleStore } from './Store/ArticleStore'
import { useAppStore } from './Store/AppStore'
import { useRouter } from 'vue-router'

const router = useRouter();
router.push('./BlogMain');

const ArticleStore = useArticleStore();
const ParamStore = useAppStore();

onBeforeMount(() => {
  ArticleStore.GetTags();
  ArticleStore.GetClassifies();
  ParamStore.GetAllParameter();
  // ArticleStore.GetArticleCount();

})

</script>
<style lang="less">
</style>
