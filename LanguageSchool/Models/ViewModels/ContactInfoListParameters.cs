using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Models.ViewModels
{
    public class ContactInfoListParameters : ListParameters
    {
        public Nullable<int> pageSize;

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> creationDateFrom;
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> creationDateTo;
		public string fullName;
		public string city;
		public string street;
		public string phoneNumber;
		public string emailAdress;
		public Nullable<bool> showUserData;
		public Nullable<bool> showContactRequests;
    }
}
