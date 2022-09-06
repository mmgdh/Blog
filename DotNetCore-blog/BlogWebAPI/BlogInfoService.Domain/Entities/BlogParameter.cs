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
        public string ParamName { get; set; }

        public string ParamValue { get; set; }

        public string ParamType { get; set; }

        public static BlogParameter Create(string paramName,string paramValue,string paramType="Text")
        {
            BlogParameter blogParameter = new BlogParameter();
            blogParameter.ParamName = paramName;
            blogParameter.ParamValue = paramValue;
            return blogParameter;
            
        }
    }
}
