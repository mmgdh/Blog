<template>
  <div class="hvr-grow  DivCSS" v-for="_Article in Ref_ArticleList" :key="_Article.id" @click="router.push({
    path: 'ShowArticle'
    query: { 'ArticleId': _Article.id }
  })">
    <ArticleCardVue :ArticleData="_Article">

    </ArticleCardVue>

  </div>

  <div class="PageStyle">
    <a-pagination :show-quick-jumper="false" v-model="refPage.page" :total="1" :page-size='pageRequest.pageSize'
      show-less-items @change="onChange" />
  </div>
</template>

<script setup lang='ts'>
import { ref, onBeforeMount, watch } from 'vue'
import ArticleService from '../../../Services/ArticleService';
import { Article } from '../../../Entities/E_Article'
import { useRouter } from 'vue-router'
import { ClockCircleOutlined } from '@ant-design/icons-vue'
import { useArticleStore } from '../../../Store/Store'
import { storeToRefs } from 'pinia';
import ArticleCardVue from './ArticleCard.vue';

let ArticleStore = useArticleStore();
let router = useRouter()
let refStore = storeToRefs(ArticleStore);
let refPage = refStore.PageRequestParm;
let Ref_ArticleList = refStore.CurPageArticles;
let ArticleCount = refStore.CurArticleCount ?? 0;
let ShowQuikJumper = ref(false)

watch(refPage.value, () => {
  ArticleStore.GetArticleByPage();
})

watch(ArticleCount.value, (newvalue, oldvalue) => {
  if (newvalue > 30) {
    ShowQuikJumper.value = true;
  }
})

ArticleStore.$patch((state) => {
  state.PageRequestParm.page = 1;
  state.PageRequestParm.pageSize = 6
});


let pageRequest = refStore.PageRequestParm;
//页码改变
const onChange = (pageNumber: number) => {
  refPage.value.page = pageNumber;
}
//日期转换
const ToDate = (DateTime: Date) => {
  let NewDate = new Date(DateTime);
  return NewDate.toLocaleDateString();
}

</script>

<style scoped>
.ArticleStyle {

  font-size: large;
  margin: 10px;
  width: 90%;
  height: 200px;
  position: relative;
}

.ArticleStyle span {
  padding-left: 5px;
}

.TitleStyle {
  font-family: "Microsoft YaHei", 微软雅黑;
  font-size: x-large;
  text-align: center;
  width: 100%;
}

.BottomStyle {
  bottom: 0px;
  position: absolute;
  width: 100%;
  height: 30px;
  font-size: small;

}

.TagStyle {
  background-color: darkorange;
  color: white;
  float: right;
  border-radius: 5px;
  text-align: center;
  margin-right: 10px;
}

.PageStyle {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 90%;
  margin: 10px;
}
</style>