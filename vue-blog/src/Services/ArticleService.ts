import {get,post,Delete} from '../axiosInstance'
import { Article,ArticleTag } from '../Entities/E_Article'


export default class ArticleService{
    async GetArticleById(_id:string){
       return await get("Article/GetArticleById",{id:_id})
    }

    async AddArticle(Article: Article){
        return await post('Article/Add',Article)
    }

    async ModifyArticle(Article: Article){
        return await post('Article/Modify',Article)
    }

    async DeleteArticle(ArticleId: string){
        return await Delete('Article/Delete',{id:ArticleId})
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
