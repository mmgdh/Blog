using ArticleService.Domain.Entities;

namespace ArticleService.WebAPI.Controllers.ViewModels.ResponseModel
{
    public class ArticleTagResponse
    {
        public Guid Id { get; set; }
        public string TagName { get; set; }
        public string PinYin { get; set; }
        public int ArticleCount { get; set; }

        public static ArticleTagResponse CreateResponse(ArticleTag articleTag,int ArticleCount =0)
        {
            ArticleTagResponse tagResponse =new ArticleTagResponse();
            tagResponse.Id=articleTag.Id;
            tagResponse.TagName=articleTag.TagName;
            tagResponse.PinYin=articleTag.PinYin;
            tagResponse.ArticleCount = ArticleCount;
            return tagResponse;
        }

        public static ArticleTagResponse[] CreatelstResponse(Dictionary<ArticleTag,int> keyValues)
        {
            List<ArticleTagResponse> lstResponse = new List<ArticleTagResponse> ();
            foreach(var kvp in keyValues)
                lstResponse.Add(CreateResponse(kvp.Key,kvp.Value));
            return lstResponse.ToArray();
        }
        public static ArticleTagResponse[] CreatelstResponse(ArticleTag[] articleTags)
        {
            List<ArticleTagResponse> lstResponse = new List<ArticleTagResponse>();
            foreach (var kvp in articleTags)
                lstResponse.Add(CreateResponse(kvp));
            return lstResponse.ToArray();
        }
    }
}
