using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Schedule.BLL.Model;

namespace Schedule.BLL.Attributes
{
    class ImageRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var collection = (List<NewsImageModelItem>)value;

            if (collection != null)
            {
                return true;
            }

            return true;
        }
    }
}
