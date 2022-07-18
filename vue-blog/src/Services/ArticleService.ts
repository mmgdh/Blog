import {get,post} from '../axiosInstance'
import { Article,ArticleTag } from '../Entities/E_Article'


export default class ArticleService{
    async getArticle(timeout:number=1000){
       return await get("Article/Index",undefined,timeout)
    }

    async AddArticle(Article: Article){
        return await post('Article/Add',Article)
    }

    async AddArticleTag(parames:Parameters<any>){
        return await post("Article/AddTag",parames)
    }

    async  GetAllArticleTags() {
        return await  get('Article/GetAllTags')
    }

    async GetArticleByPage(parames:any){
        return await get('Article/GetArticleByPage',parames);
    }

    public async  GetAllArticleClassify() {
        return await get('Article/GetAllArticleClassify')
    }

}
