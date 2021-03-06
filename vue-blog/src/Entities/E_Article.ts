
export interface Article {
    id:string,
    title: string,
    content: string,
    classify:ArticleClassify,
    createDateTime:Date,
    tags: Array<ArticleTag>,
    pinYin:string
}

export interface ArticleTag {
    tagName: string,
    id: string
}

export interface ArticleClassify {
    id: string,
    classifyName: string,
    pinYin: string
}