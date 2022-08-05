import { defineStore } from 'pinia'
import { ArticleTag, ArticleClassify } from '../Entities/E_Article'
import ArticleService from '../Services/ArticleService'

// useStore 可以是 useUser、useCart 之类的任何东西
// 第一个参数是应用程序中 store 的唯一 id
export const useArticleStore = defineStore('Article', {
  state: () => {
    return {
      Tags: {

      } as Array<ArticleTag>,
      Classifies: {

      } as Array<ArticleClassify>
    }
  },
  actions: {
    async GetTags() {
      let ret = await ArticleService.prototype.GetAllArticleTags();
      this.Tags = ret;
    },
    async GetClassifies() {
      this.Classifies = await ArticleService.prototype.GetAllArticleClassify();
    }
  }
})