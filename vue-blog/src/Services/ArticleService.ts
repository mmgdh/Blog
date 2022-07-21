import {get,post} from '../axiosInstance'
import { Article,ArticleTag } from '../Entities/E_Article'


export default class ArticleService{
    async GetArticleById(_id:string){
       return await get("Article/GetArticleById",{id:_id})
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
