<template>
  <div>
    <a-button type="primary" @click="AddClick">
      新增
    </a-button>
  </div>
  <a-table :dataSource="Ref_ClassifyList" :columns="columns" :pagination="pagination" @change="Func_RefreshClassify">
    <template #bodyCell="{ column, record }">
      <template v-if="column.key === 'img'">
        <span>
          <img :url="UploadService.prototype.GetImg(record.imgId)"/>
        </span>
      </template>
      <template v-else-if="column.key === 'action'">
        <span>
          <a @click="ModifyClick(record)">编辑</a>
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
  <a-modal v-model:visible="ModifyMsgVisible" title="标签修改" @ok="ModifyFunc()">
    <a-input v-model:value="curRecord.classifyName"></a-input>
  </a-modal>
  <a-modal v-model:visible="DeletMsgVisible" title="标签删除" @ok="DeleteFunc(curRecord.id)">
    确定删除标签[{{ curRecord.classifyName }}]吗
  </a-modal>
  <a-modal v-model:visible="AddMsgVisible" title="新增标签" @ok="AddFunc()">
    <a-input v-model:value="curRecord.classifyName"></a-input>
  </a-modal>

</template>
<script setup lang="ts">
import { ArticleClassify } from "../../Entities/E_Article";
import { ref, computed } from 'vue'
import ClassifyService from "../../Services/ArticleService"
import UploadService from "../../Services/UploadService"
import { PageRequest } from "../../Entities/CommomEntity"
import { useRouter } from 'vue-router'
import { useArticleStore } from '../../Store/Store'
import { storeToRefs } from 'pinia';

const ArticleStore = useArticleStore();
const refStore = storeToRefs(ArticleStore);
const router = useRouter();
let Ref_ClassifyList = refStore.Classifies;
let Ref_Current = ref(1);
let curRecord = ref({} as ArticleClassify);
let DeletMsgVisible = ref(false);
let ModifyMsgVisible = ref(false);
let AddMsgVisible = ref(false);
const Func_RefreshClassify = () => {
  ArticleStore.GetClassifies();
};
Func_RefreshClassify();
const pagination = computed(() => ({
  total: Ref_ClassifyList.value.length,
  current: Ref_Current.value,
  pageSize: 10,
}));
const DeleteClick = (record: any) => {
  curRecord.value = record;
  DeletMsgVisible.value = true;
}
const ModifyClick = (record: any) => {
  curRecord.value = record;
  ModifyMsgVisible.value = true;
}
const AddClick = async () => {
  curRecord.value = {

  } as ArticleClassify;
  AddMsgVisible.value = true;
}
const DeleteFunc = async (id: string) => {
  var ret = await ClassifyService.prototype.DeleteArticleClassify(id);
  if (ret == true) {
    Func_RefreshClassify();
  }
  DeletMsgVisible.value = false;
}
const ModifyFunc = async () => {
  var ret = await ClassifyService.prototype.ModifyArticlCLassify(curRecord.value);
  if (ret == true) {
    Func_RefreshClassify();
  }
  ModifyMsgVisible.value = false;
}
const AddFunc = async () => {
  var ret = await ClassifyService.prototype.AddArticleClassify(curRecord.value);
  Func_RefreshClassify();
  AddMsgVisible.value = false;
}


const columns = [
  {
    title: '分类名',
    dataIndex: 'classifyName',
    key: 'classifyName',
  },
  {
    title: "图片",
    dataindex: 'ImgId',
    key: 'ImgId',
  },
  {
    title: 'Action',
    key: 'action',
  },
]
</script>
