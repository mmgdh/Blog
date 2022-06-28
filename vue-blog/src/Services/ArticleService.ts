import {get,post} from '../axiosInstance'
import { Article,ArticleTag } from '../Entities/E_Article'


export default class ArticleService{
    async getArticle(timeout:number=1000){
       return await get("Article/Index",undefined,timeout)
    }

    async AddArticleTag(parames:Parameters<any>){
        return await post("Article/AddTag",parames)
    }

     GetAllArticleTags() : any {
        var ret =  get('Article/GetAllTags',undefined)
        return ret
    }

}
