namespace ArticleService.Domain
{
    public class ArticleDomainService
    {
        private readonly IArticleRepository repository;
        public ArticleDomainService(IArticleRepository _repository)
        {
            repository = _repository;
        }


        public async  Task<ArticleTag> CreateTag(string TagName)
        {
            if (await repository.TagNameIsExist(TagName))
            {
                throw new Exception("该标签已存在");
            }
            ArticleTag Tag = ArticleTag.Create(TagName);
            return Tag;
        }

     
    }
}
