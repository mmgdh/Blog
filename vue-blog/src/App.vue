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

const ParamStore = useAppStore();
const ArticleStore = useArticleStore();


onBeforeMount(async () => {
  await ParamStore.GetAllParameter();
  await ArticleStore.GetTags();
  await ArticleStore.GetClassifies();
  await ArticleStore.GetArticleCount();

})

</script>
<style lang="less">

</style>
