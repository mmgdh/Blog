<template>
    <div>
        4444444444444444
    </div>
    <Md v-model="content"></Md>
    <div>
        <a-button shape="round" type="primary" @click="showDrawer">提交</a-button>
        <a-button>草稿</a-button>
    </div>



    <a-drawer v-model:visible="visible" class="custom-class" style="color: red" title="Basic Drawer" placement="right"
        @after-visible-change="afterVisibleChange">
        <p>Some contents...</p>
        <p>Some contents...</p>
        <p>Some contents...</p>
    </a-drawer>
</template>


<script setup lang='ts'>
import { ref } from 'vue'
import Md from 'md-editor-v3'
import 'md-editor-v3/lib/style.css'

const content = ref<string>('');

const visible = ref<boolean>(false);

const afterVisibleChange = (bool: boolean) => {
    console.log('visible', bool);
};

const showDrawer = () => {
    visible.value = true;
};

</script>

<style>
</style>