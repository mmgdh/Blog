
export interface Article {
    id:string,
    title: string,
    content: string,
    tags: Array<ArticleTag>
}

export interface ArticleTag {
    tagName: string,
    id: string
}

export interface ArticleClassify {
    id: string,
    classifyName: string,
    pinyin: string
}