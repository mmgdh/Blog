using ArticleService.Domain.Entities;
using ArticleService.Domain.IRepository;

namespace ArticleService.Domain
{
    public class ArticleDomainService
    {
        private readonly IArticleTagRepository TagRepository;
        private readonly IArticleClassifyRepository ClassifyRepository;
        public ArticleDomainService(IArticleTagRepository tagRepository, IArticleClassifyRepository classifyRepository)
        {
            TagRepository = tagRepository;
            ClassifyRepository = classifyRepository;
        }


        public async Task<ArticleTag> CreateTag(string TagName)
        {
            if (await TagRepository.TagNameIsExistAsync(TagName))
            {
                throw new Exception("该标签已存在");
            }
            ArticleTag Tag = ArticleTag.Create(TagName);
            return Tag;
        }

        public async Task<ArticleClassify> CreateClassify(string ClassifyName)
        {
            if (await ClassifyRepository.ArticleClassifyNameIsExistAsync(ClassifyName))
            {
                throw new Exception("该分类已存在");
            }
            ArticleClassify Classify = ArticleClassify.Create(ClassifyName, null);
            return Classify;
        }


    }
}
