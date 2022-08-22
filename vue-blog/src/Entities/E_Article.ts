
export interface Article {
    id: string,
    title: string,
    content: string,
    classify: ArticleClassify,
    createDateTime: Date,
    tags: Array<ArticleTag>,
    pinYin: string
}

export interface ArticleTag {
    tagName: string,
    id: string,
    pinYin: string,

}

export interface ArticleClassify {
    id: string,
    classifyName: string,
    pinYin: string,
    articleCount: number
}

export interface ArticlePageRequest {
    page: number,
    pageSize: number,
    ClassifyIds: Array<string>,
    TagIds: Array<string>,
    CreateTime: Date
}