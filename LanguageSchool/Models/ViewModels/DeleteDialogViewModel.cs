using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Models.ViewModels
{
    public class DeleteDialogViewModel
    {
        public int Id { get; set; }
        public string Dialog { get; set; }
        public string ControllerName { get; set; }

        public DeleteDialogViewModel(int Id, string ControllerName, string Dialog)
        {
            this.Id = Id;
            this.Dialog = Dialog;
            this.ControllerName = ControllerName;
        }
    }
}
