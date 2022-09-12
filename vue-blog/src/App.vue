<template>
  <router-view></router-view>
  <!-- <Main></Main> -->
</template>
<script setup lang="ts">
import { onBeforeMount } from 'vue'
import { useArticleStore } from './Store/Store'
import { useParamStore } from './Store/BlogParameterStore'
import { useRouter } from 'vue-router'

const router = useRouter();
router.push('./BlogMain');

const ArticleStore = useArticleStore();
const ParamStore = useParamStore();

onBeforeMount(() => {
  ArticleStore.GetTags();
  ArticleStore.GetClassifies();
  ParamStore.GetAllParameter();
  // ArticleStore.GetArticleCount();
})

</script>
<style>
#components-layout-demo-side .logo {
  height: 32px;
  margin: 16px;
  background: rgba(255, 255, 255, 0.3);
}

.site-layout .site-layout-background {
  background: #fff;
}

[data-theme='dark'] .site-layout .site-layout-background {
  background: #141414;
}

#md-editor-v3 {
  height: 600px;
}
</style>
