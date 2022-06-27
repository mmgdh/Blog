import {get,post} from '../axiosInstance'


export class ArticleService{
    async getArticle(timeout:number=1000){
       return await get("Article/Index",undefined,timeout)
    }

    async AddArticleTag(parames:Parameters<any>){
        return await post("Article/AddTag",parames)
    }

}
