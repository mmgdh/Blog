<template>
  <div >
    <BlogList></BlogList>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, toRefs, onBeforeMount, onMounted, watchEffect, computed } from 'vue';
import BlogList from './Article/BlogList.vue'

</script>
<style scoped>

</style>