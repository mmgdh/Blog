<template>
  <Main></Main>
</template>
<script setup lang="ts">
import Manage from "./components/Background/Manage.vue"
import Main from "./components/Blog/Main.vue"
import {onBeforeMount} from 'vue'
import {useArticleStore} from './Store/Store'

const ArticleStore=useArticleStore();

onBeforeMount(() => {
  ArticleStore.GetTags();
  ArticleStore.GetClassifies();
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
