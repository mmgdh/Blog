
export interface Article {
    Title: string,
    Content: string,
    Tags: Array<ArticleTag>
}

export interface ArticleTag {
    TagName: string,
    id: string
}

export interface ArticleClassify {
    id: string,
    classifyName: string,
    pinyin: string
}