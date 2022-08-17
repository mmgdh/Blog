using Commons;
using DomainCommon;

namespace ArticleService.Domain.Entities
{
    /// <summary>
    /// 文章分类
    /// </summary>
    public class ArticleClassify : BaseEntity
    {
        private string _ClassifyName = "";
        /// <summary>
        /// 标题
        /// </summary>
        public string ClassifyName
        {
            get
            {
                return _ClassifyName;
            }
            set
            {
                _ClassifyName = value;
                PinYin = PinYinHelper.GetFrist(value);
            }
        }
        public string PinYin { get; private set; } = "";
        public List<Article> Articles { get;  set; } = new List<Article>();

        public Guid? DefaultImgId { get; set; }


        public static ArticleClassify Create(string Name,Guid? DefaultImgId)
        {
            ArticleClassify articleClassify = new ArticleClassify();
            articleClassify.ClassifyName = Name;
            articleClassify.DefaultImgId = DefaultImgId;
            return articleClassify;
        }
    }
}
