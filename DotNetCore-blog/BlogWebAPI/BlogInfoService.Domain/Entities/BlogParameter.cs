using Commons;
using DomainCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogInfoService.Domain.Entities
{
    public class BlogParameter: AggregateRootEntity
    {
        private string _name;
        public string ParamName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                PinYin = PinYinHelper.GetFrist(value);
            }
        }

        public string ParamValue { get; set; }

        public string ParamType { get; set; }

        public string PinYin { get; private set; } = "";

        public static BlogParameter Create(string paramName,string paramValue,string? paramType)
        {
            if (string.IsNullOrEmpty(paramType)) paramType = "Text";
            BlogParameter blogParameter = new BlogParameter();
            blogParameter.ParamName = paramName;
            blogParameter.ParamValue = paramValue;
            blogParameter.ParamType = paramType;
            return blogParameter;
            
        }
    }
}
