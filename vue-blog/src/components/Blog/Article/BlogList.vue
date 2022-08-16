<template>
  <div class="hvr-grow ArticleStyle DivCSS" v-for="_Article in Ref_ArticleList" :key="_Article.id"
    @click="router.push({ path: 'ShowArticle', query: { 'ArticleId': _Article.id } })">
    <div class="TitleStyle">{{ _Article.title }}</div>

    <div class="BottomStyle">
      <ClockCircleOutlined />
      <span>{{ ToDate(_Article.createDateTime) }}</span>
      <span>分类：{{ _Article.classify.classifyName }}</span>
      <span class="TagStyle" v-for="_Tag in _Article.tags">
        {{ _Tag.tagName }}
      </span>
    </div>

  </div>

  <div class="PageStyle">
    <a-pagination :show-quick-jumper="false" v-model="refPage.page" :total="ArticleCount"
      :page-size='pageRequest.pageSize' show-less-items @change="onChange" />
  </div>
</template>

<script setup lang='ts'>
import { ref, onBeforeMount } from 'vue'
import ArticleService from '../../../Services/ArticleService';
import { Article } from '../../../Entities/E_Article'
import { PageRequest } from '../../../Entities/CommomEntity'
import { useRouter } from 'vue-router'
import { ClockCircleOutlined } from '@ant-design/icons-vue'

let router = useRouter()
const pageRequest: PageRequest = {
  page: 1,
  pageSize: 6
}
let refPage = ref(pageRequest);
let ArticleList: Array<Article> = []
let Ref_ArticleList = ref(ArticleList);
let ArticleCount = ref(1)
let ShowQuikJumper = ref(false)

onBeforeMount(() => {
  FuncGetArticleByPage();
  ArticleAllCount();
}
);
//获取总页码
const ArticleAllCount = () => {
  ArticleService.prototype.GetArticleCount().then(res => {
    ArticleCount.value = res
    if (res > 30) {
      ShowQuikJumper.value = true;
    }
  })
}
//页码改变
const onChange = (pageNumber: number) => {
  refPage.value.page = pageNumber;
  FuncGetArticleByPage();
}
//根据页码获取文章列表
const FuncGetArticleByPage = () => {
  ArticleService.prototype.GetArticleByPage(refPage.value)
    .then(ret => {
      Ref_ArticleList.value = ret
    }
    );
}
//日期转换
const ToDate = (DateTime: Date) => {
  let NewDate = new Date(DateTime);
  return NewDate.toLocaleDateString();
}

</script>

<style>
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