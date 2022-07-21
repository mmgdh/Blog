<template>
  <a-table :dataSource="Ref_ArticleList" :columns="columns">
    <template #bodyCell="{ column, record }">
      <template v-if="column.key === 'tags'">
        <span>
          <a-tag v-for="tag in record.tags" :key="tag.id" :color="tag.tagName.length > 5 ? 'geekblue' : 'green'">
            {{ tag.tagName.toUpperCase() }}
          </a-tag>
        </span>
      </template>
      <template v-else-if="column.key === 'classify'">
        <span>
          {{ record.classify.classifyName }}
        </span>
      </template>
      <template v-else-if="column.key === 'action'">
        <span>
          <a>Invite 一 {{ record.name }}</a>
          <a-divider type="vertical" />
          <a>Delete</a>
          <a-divider type="vertical" />
          <a class="ant-dropdown-link">
            More actions
            <down-outlined />
          </a>
        </span>
      </template>
    </template>

  </a-table>
</template>
<script setup lang="ts">
import { Article } from "../../Entities/E_Article";
import { ref, onBeforeMount } from 'vue'
import ArticleService from "../../Services/ArticleService"
import { PageRequest } from "../../Entities/CommomEntity"
import { any } from "vue-types";

let Articles: Article[] = [];
let pageRequestData: PageRequest = {
  page: 0,
  pageSize: 10
};
let Ref_ArticleList = ref(Articles);

ArticleService.prototype.GetArticleByPage(pageRequestData)
  .then(ret => {
    Ref_ArticleList.value = ret;
    console.log(Ref_ArticleList.value);
  }
  );


const columns = [
  {
    title: '标题',
    dataIndex: 'title',
    key: 'title',
  },
  {
    title: '分类',
    dataIndex: 'classify',
    key: 'classify',
  },
  {
    title: '标签',
    dataIndex: 'tags',
    key: 'tags',
  },
  {
    title: '创建日期',
    dataIndex: 'createDateTime',
    key: 'createDateTime',
  },
  {
    title: 'Action',
    key: 'action',
  },
]
</script>
