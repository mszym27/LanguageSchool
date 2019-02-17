using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Models.ViewModels
{
    public class TableHeaderViewModel
    {
        public string ShownName;
        public string ColumnName;

        public TableHeaderViewModel(string ShownName, string ColumnName)
        {
            this.ShownName = ShownName;
            this.ColumnName = ColumnName;
        }
    }
}
