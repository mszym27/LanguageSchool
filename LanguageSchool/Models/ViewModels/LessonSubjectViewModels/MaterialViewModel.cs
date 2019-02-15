using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Models.ViewModels
{
    public class MaterialViewModel
    {
        public int LessonSubjectId { get; set; }
        [Required(ErrorMessage = "Wymagane jest uzupełnienie nazwy")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        [Required(ErrorMessage = "Wymagane jest wskazanie pliku")]
        public HttpPostedFileBase File { get; set; }
    }
}
