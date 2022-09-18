<template>
  <div>
    <BlogFeatureVue></BlogFeatureVue>
    <div class="IndexContainer">
      <div class="IndexBlogList">
        <BlogList></BlogList>
      </div>
      <div class="IndexRightContent">
        <Introduction></Introduction>
        <ArticleClssifyList></ArticleClssifyList>
      </div>
    </div>


  </div>
</template>

<script setup lang="ts">
import BlogList from '../Article/BlogList.vue'
import Introduction from './IndexContent/Introduction.vue'
import ArticleClssifyList from '../Article/ArticleClassifyList.vue';
import BlogFeatureVue from './BlogFeature.vue';

</script>
<style scoped>
.IndexContainer{
  width: 100%;

}
</style>