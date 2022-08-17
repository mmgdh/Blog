using ArticleService.Domain.Entities;

namespace ArticleService.WebAPI.Controllers.ViewModels.ResponseModel
{
    public class ArticleClassifyResponse
    {
        public string id { get; set; }
        public string classifyName { get; set; }
        public string pinYin { get; set; }
        public int ArticleCount { get; set; }

        public ArticleClassifyResponse(ArticleClassify articleClassify)
        {
            this.id = articleClassify.Id.ToString();
            this.classifyName = articleClassify.ClassifyName;
            this.pinYin = articleClassify.PinYin;
            this.ArticleCount = articleClassify.Articles.Count;
        }
    }

}
