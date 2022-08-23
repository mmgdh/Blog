import { get, post, Delete, put } from './_Service'
import { Article, ArticleClassify, ArticleTag } from '../Entities/E_Article'


const controler = "Article";

export default class ArticleService {
    public async GetArticleById(_id: string) {
        return await get(controler + "/GetArticleById", { id: _id })
    }

    async AddArticle(Article: Article) {
        return await post(controler + '/Add', Article)
    }

    async ModifyArticle(Article: Article) {
        return await put(controler + '/Modify', Article)
    }

    async DeleteArticle(ArticleId: string) {
        return await Delete(controler + '/Delete', { id: ArticleId })
    }

    async AddArticleTag(TagName: string) {
        return await post(controler + "/AddTag", {tagName:TagName})
    }
    async DeleteArticleTag(TagId: string) {
        return await Delete(controler + "/DeleteTag", {id:TagId})
    }
    async ModifyrticleTag(articleTag: ArticleTag) {
        return await put(controler + "/ModifyTag", articleTag)
    }

    async ModifyArticlCLassify(articleClassify: ArticleClassify) {
        return await put(controler + "/ModifyCLassify", articleClassify)
    }
    async AddArticleClassify(articleClassify: ArticleClassify) {
        return await post(controler + "/AddClassify", articleClassify)
    }
    async DeleteArticleClassify(ClassifyId: string) {
        return await Delete(controler + "/DeleteClassify", {id:ClassifyId})
    }
    async GetAllArticleTags() {
        return await get(controler + '/GetAllTags')
    }

    async GetArticleByPage(parames: any) {
        return await get(controler + '/GetArticleByPage', parames);
    }

    public async GetAllArticleClassify() {
        return await get(controler + '/GetAllArticleClassify')
    }

    public async GetArticleCount() {
        return await get(controler + '/GetArticleCount');
    }

}
