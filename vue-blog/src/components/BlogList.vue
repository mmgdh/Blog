    <template>
      <div v-for="item in ArticleList">
        {{ item.Title }}
      </div>
      <a-descriptions title="User Info" bordered>
        <a-descriptions-item label="UserName">Zhou Maomao</a-descriptions-item>
        <a-descriptions-item label="Telephone">1810000000</a-descriptions-item>
        <a-descriptions-item label="Live">Hangzhou, Zhejiang</a-descriptions-item>
        <a-descriptions-item label="Remark">empty</a-descriptions-item>
        <a-descriptions-item label="Address">
          No. 18, Wantang Road, Xihu District, Hangzhou, Zhejiang, China
        </a-descriptions-item>
      </a-descriptions>
    </template>

<script setup lang='ts'>
import { ref, onBeforeMount } from 'vue'
import { number } from 'vue-types';
import ArticleService from '../Services/ArticleService';
import { Article } from '../Entities/E_Article'
interface requestData {
  page: number,
  pageSize: number
}
const pageRequest: requestData = {
  page: 0,
  pageSize: 10
}

let ArticleList: any = ref([])

onBeforeMount(() =>
  ArticleService.prototype.GetArticleByPage(pageRequest)
    .then(ret =>
      ArticleList.value = ret
    )
);





</script>

<style>
</style>