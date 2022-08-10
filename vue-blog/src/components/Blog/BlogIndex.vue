<template>
  <div>
    <div class="IndexText">
      <h1>Welcome To My Website</h1>
      <p>Picture,Techonolege...</p>
    </div>

    <BlogList></BlogList>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, toRefs, onBeforeMount, onMounted, watchEffect, computed } from 'vue';
import BlogList from './Article/BlogList.vue'

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
</style>