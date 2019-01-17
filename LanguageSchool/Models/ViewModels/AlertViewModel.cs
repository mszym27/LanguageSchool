using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Models.ViewModels
{
    public class AlertViewModel
    {
        public string AlertType;
        public string Title;
        public string Message;

        public AlertViewModel(string AlertType, string Title, string Message)
        {
            this.AlertType = AlertType;
            this.Title = Title;
            this.Message = Message;
        }
    }
}
