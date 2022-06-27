<template>
  <a-layout style="min-height: 100vh">
    <a-layout-sider v-model:collapsed="collapsed" collapsible>
      <div class="logo" />
      <a-menu v-model:selectedKeys="selectedKeys" theme="dark" mode="inline">
        <a-sub-menu key="sub1">
          <template #title>
            <span>
              <user-outlined />
              <span>文章</span>
            </span>
          </template>
          <a-menu-item key="3" @click="topage('/EditBlog')" >新增文章</a-menu-item>
          <a-menu-item key="4" @click="AxiosFunc">Bill</a-menu-item>
          <a-menu-item key="5" @click="topage('/b')">Alex</a-menu-item>
        </a-sub-menu>
        <a-sub-menu key="sub2">
          <template #title>
            <span>
              <team-outlined />
              <span>Team</span>
            </span>
          </template>
          <a-menu-item key="6">Team 1</a-menu-item>
          <a-menu-item key="8">Team 2</a-menu-item>
        </a-sub-menu>
        <a-menu-item key="9">
          <file-outlined />
          <span>File</span>
        </a-menu-item>
      </a-menu>
    </a-layout-sider>
    <a-layout>
      <a-layout-header style="background: #fff; padding: 0" />
      <a-layout-content style="margin: 0 16px">
        <a-breadcrumb style="margin: 16px 0">
          <a-breadcrumb-item>User</a-breadcrumb-item>
          <a-breadcrumb-item>Bill</a-breadcrumb-item>
        </a-breadcrumb>
        <!-- <div :style="{ padding: '24px', background: '#fff', minHeight: '360px' }">
          
        </div> -->
        <router-view></router-view>
      </a-layout-content>
      <a-layout-footer style="text-align: center">
        Ant Design ©2018 Created by Ant UED
      </a-layout-footer>
    </a-layout>
  </a-layout>
</template>
<script setup lang="ts">
import {
  PieChartOutlined,
  DesktopOutlined,
  UserOutlined,
  TeamOutlined,
  FileOutlined,
} from '@ant-design/icons-vue';
import { defineComponent, ref } from 'vue';
import {get,post} from './axiosInstance'
import {ArticleService} from './Services/ArticleService'
import {useRouter} from 'vue-router'

    let router=useRouter()
     let collapsed = ref<boolean>(false)
     let selectedKeys = ref<string[]>(['1'])

     let AxiosFunc=async ()=>{
         ArticleService.prototype.getArticle()
              .then((res)=>{
                  console.log(res); 
              }).catch(()=>{console.log("失败了")})
              .finally(()=>{
                console.log("over")
              })
     }
     let topage=(url:string)=>{
      router.push(url)
    }

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

#md-editor-v3{
  height: 1000px;
}
</style>
