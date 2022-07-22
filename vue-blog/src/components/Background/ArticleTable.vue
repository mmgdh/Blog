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
      <template v-else-if="column.key === 'createDateTime'">
        {{ record.createDateTime }}
      </template>
      <template v-else-if="column.key === 'classify'">
        <span>
          {{ record.classify.classifyName }}
        </span>
      </template>
      <template v-else-if="column.key === 'action'">
        <span>
          <a>编辑</a>
          <a-divider type="vertical" />
          <a @click="DeleteClick(record)">删除</a>
          <a-divider type="vertical" />
          <a class="ant-dropdown-link">
            More actions
          </a>
        </span>
      </template>
    </template>

  </a-table>
  <a-modal v-model:visible="DeletMsgVisible" title="Basic Modal" @ok="DeleteFunc(curRecord.id)">
    确定删除该文章[{{ curRecord.title }}]吗
  </a-modal>
  <button @click="a">123</button>
</template>
<script setup lang="ts">
import { Article } from "../../Entities/E_Article";
import { ref, onBeforeMount } from 'vue'
import ArticleService from "../../Services/ArticleService"
import { PageRequest } from "../../Entities/CommomEntity"
import MsgBox from "../common/MessageBox.vue"
import { any } from "vue-types";

let Articles: Article[] = [];
let pageRequestData: PageRequest = {
  page: 0,
  pageSize: 10
};
let Ref_ArticleList = ref(Articles);
const a = () => { console.log(curRecord) }
ArticleService.prototype.GetArticleByPage(pageRequestData)
  .then(ret => {
    Ref_ArticleList.value = ret;
    console.log(Ref_ArticleList.value);
  }
  );
let curRecord: any=ref(any);
let DeletMsgVisible = ref(false);
const DeleteClick = (record: any) => {
  curRecord.value = record;
  DeletMsgVisible.value = true;

}
const DeleteFunc = (id: string) => {
  DeletMsgVisible.value = false;
}
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
