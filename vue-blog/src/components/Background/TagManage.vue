<template>
  <a-table :dataSource="Ref_TagList" :columns="columns" :pagination="pagination" @change="Func_RefreshArtifcle">
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
          <a @click="router.push({ path: 'EditBlog', query: { 'TagId': record.id } })">编辑</a>
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
</template>
<script setup lang="ts">
import { ArticleTag } from "../../Entities/E_Article";
import { ref, computed } from 'vue'
import TagService from "../../Services/ArticleService"
import { PageRequest } from "../../Entities/CommomEntity"
import { useRouter } from 'vue-router'

const router = useRouter();
let Tags: ArticleTag[] = [];
let Ref_TagList = ref(Tags);
let Ref_pageTagCount=ref(1);
const Func_RefreshArtifcle = () => {
  TagService.prototype.GetAllArticleTags()
    .then(ret => {
      Ref_TagList.value=ret.Tags;
      Ref_pageTagCount.value=ret.pageTagCount;
    }
    );
};
Func_RefreshArtifcle({ pageSize: refPageData.value.pageSize, current: refPageData.value.page });

const pagination = computed(() => ({
  total: Ref_pageTagCount.value,
  current: refPageData.value.page,
  pageSize: refPageData.value.pageSize,
}));

let curRecord: any = ref();
let DeletMsgVisible = ref(false);
const DeleteClick = (record: any) => {
  curRecord.value = record;
  DeletMsgVisible.value = true;
}
const DeleteFunc = async (id: string) => {
  var ret = await TagService.prototype.DeleteTag(id);
  if (ret == true) {
    Func_RefreshArtifcle({ pageSize: refPageData.value.pageSize, current: refPageData.value.page });
  }
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
