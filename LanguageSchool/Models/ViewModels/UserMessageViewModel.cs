using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageSchool.Models.ViewModels
{
    public class UserMessageViewModel
    {
        public int Id { get; }
        public string SentDate { get; }
        public string ReceivedDate { get; }
        public string Topic { get; }
        public string Contents { get; }
        public string ShortenedContents { get; }
        public bool HasBeenReceived { get; }
        public bool IsCyclical { get; }

        public UserMessageViewModel(UserMessage um)
        {
            Id = um.Id;
            SentDate = um.CreationDate.ToString("yyyy-MM-dd");
            ReceivedDate = um.ReceivedDate.HasValue ? um.ReceivedDate.Value.ToString("yyyy-MM-dd") : "-";
            Topic = um.Message.Header;
            Contents = um.Message.Contents;
            ShortenedContents = um.Message.Contents.Substring(0, 70) + "...";
            HasBeenReceived = um.HasBeenReceived;
            IsCyclical = um.IsCyclical;
        }
    }
}