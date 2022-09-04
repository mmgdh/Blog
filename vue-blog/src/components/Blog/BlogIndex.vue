<template>
  <div>
    <div class="IndexText">
      <h1>Welcome To My Website</h1>
      <p>Picture,Techonolege...</p>
    </div>
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
import BlogList from './Article/BlogList.vue'
import Introduction from './IndexContent/Introduction.vue'
import ArticleClssifyList from './Article/ArticleClassifyList.vue';
import ArticleCardVue from './Article/ArticleCard.vue';
import ArticleCard from './Article/ArticleCard.vue';

</script>
<style scoped>
.IndexText {
  display: flex;
  height: 100vh;
  justify-content: center;
  align-items: center;
  text-align: center;
  position: relative;
  flex-direction: column;
  margin-bottom: 20px;

}
.IndexText h1 {
  color: #fff;
  font-size: 46px;
  margin: -20px 0 20px;
}

.IndexText p {
  font-size: 20px;
  letter-spacing: 1px;
}
.IndexContainer{
  width: 100%;
  display: flex;
  
}
.IndexBlogList{
  flex: 4;
}
.IndexRightContent{
  display: flex;
  flex-direction: column;
  /* justify-content: center; */
  flex: 1;
}
  .IndexContainer  div{
    margin-bottom: 20px;
}
</style>