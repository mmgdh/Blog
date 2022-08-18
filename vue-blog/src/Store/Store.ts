import { defineStore } from 'pinia'
import { ArticleTag, ArticleClassify, Article } from '../Entities/E_Article'
import ArticleService from '../Services/ArticleService'

// useStore 可以是 useUser、useCart 之类的任何东西
// 第一个参数是应用程序中 store 的唯一 id
export const useArticleStore = defineStore('Article', {
  state: () => {
    return {

      Tags: {

      } as Array<ArticleTag>,
      Classifies: {

      } as Array<ArticleClassify>,
      AllArticleCount: Number,
      PageRequestParm: {
        page: 1,
        pageSize: 6,
        classifyId: ''
      }
    }
  },
  getters:{
     async CurPageArticles():Promise<Array<Article>> {
      var ret =await ArticleService.prototype.GetArticleByPage(this.PageRequestParm)
      return ret;
    },
  },
  actions: {
    async GetTags() {
      let ret = await ArticleService.prototype.GetAllArticleTags();
      this.Tags = ret;
    },
    async GetClassifies() {
      this.Classifies = await ArticleService.prototype.GetAllArticleClassify();
    },
    async GetArticleCount() {
      this.AllArticleCount = await ArticleService.prototype.GetArticleCount();
    },
    // async GetArticleByPage() {
    //   if (this.PageRequestParm.page == 0)
    //     this.CurPageArticles = await ArticleService.prototype.GetArticleByPage(this.PageRequestParm);
    // }
  }
})