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
</template>

<script setup lang='ts'>
import { ref, onBeforeMount } from 'vue'
import ArticleService from '../../../Services/ArticleService';
import { Article } from '../../../Entities/E_Article'
import { PageRequest } from '../../../Entities/CommomEntity'
import { useRouter } from 'vue-router'
import { ClockCircleOutlined } from '@ant-design/icons-vue'

const pageRequest: PageRequest = {
  page: 0,
  pageSize: 10
}

let ArticleList: Array<Article> = []
let Ref_ArticleList = ref(ArticleList)

onBeforeMount(() => {
  ArticleService.prototype.GetArticleByPage(pageRequest)
    .then(ret => {
      Ref_ArticleList.value = ret
    }
    );
}
);
let router = useRouter()

const ToDate = (DateTime: Date) => {
  let NewDate = new Date(DateTime);
  return NewDate.toLocaleDateString();
}



</script>

<style>
.ArticleStyle {

  font-size: large;
  margin: 10px;
  width: 700px;
  height: 150px;
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
</style>