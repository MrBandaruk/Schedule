using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Schedule.BLL.Model;

namespace Schedule.BLL.Attributes
{
    public class ImageRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var collection = (List<NewsImageModelItem>)value;

            if (collection.Count == 0 || collection.FirstOrDefault() != null)
            {
                return true;
            }

            return false;
        }
    }
}
