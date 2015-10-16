using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace HNGHRMS.Web.Core.Mapping
{
    public class DateTimeTypeConverter : ITypeConverter<DateTime,string>
    {
        public String Convert(ResolutionContext context)
        {
            DateTime source = (DateTime)context.SourceValue;
            return source.ToString("dd/MM/yyyy");
        }
    }
}
