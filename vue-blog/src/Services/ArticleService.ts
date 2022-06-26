import {get,post} from '../axiosInstance'


export class ArticleService{
    async getArticle(timeout?:number){
       return await get("Article/Index",undefined,1000)
    }
}
