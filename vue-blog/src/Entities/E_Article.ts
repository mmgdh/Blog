
export interface Article{
    Title: string,
    Tags: Array<ArticleTag>,
}

export interface ArticleTag {
    TagName: string,
    Id: string
}