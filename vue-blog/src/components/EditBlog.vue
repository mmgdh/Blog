<template>
   <div>
       4444444444444444
   </div>
   <Md v-model="content"></Md>
</template>

<script setup lang='ts'>
import {ref} from 'vue'
import Md from 'md-editor-v3'
import 'md-editor-v3/lib/style.css'

const content=ref<string>('');

</script>

<style>

</style>