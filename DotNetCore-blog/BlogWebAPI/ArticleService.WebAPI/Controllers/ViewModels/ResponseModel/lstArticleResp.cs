using ArticleService.Domain.Entities;

namespace ArticleService.WebAPI.Controllers.ViewModels.ResponseModel
{
    public class lstArticleResp
    {
        public int PageArticleCount { get; set; }
        public ArticleResp[] articles { get; set; }

        public static ArticleResp[] CreateArticles(Article[] article)
        {
            var ret = article.Select(article =>
            {
                ArticleResp resp = new ArticleResp();

                article.Classify?.Articles?.Clear();
                article.Tags.ForEach(x => x.Articles.Clear());
                resp.id = article.Id;
                resp.Title = article.Title;
                resp.Classify = article.Classify;
                resp.Description = article.Description;
                resp.Tags = article.Tags;
                resp.PinYin = article.PinYin;
                resp.ImageId = article.ImageId;
                resp.CreateDateTime = article.CreationTime;
                resp.UpdateDateTime = article.LastModificationTime;
                return resp;
            });
            return ret.ToArray();


        }
    }
}
