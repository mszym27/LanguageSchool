using System;
using System.Web.Mvc;
using LanguageSchool.DAL;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace LanguageSchool.Models.ViewModels.MessageViewModels
{
    public class UserMessageDetailsVM
    {
        public int UserMessageId { get; }
        public bool IsSystem { get; }
        public string SentDate { get; }
        public string ReceivedDate { get; }
        public string Topic { get; }
        [DataType(DataType.MultilineText)]
        public string Contents { get; }

        public UserMessageDetailsVM(UserMessage userMessage)
        {
            UserMessageId = userMessage.Id;
            SentDate = userMessage.Message.CreationDate.ToString("yyyy-MM-dd");
            ReceivedDate = userMessage.ReceivedDate.HasValue ? userMessage.ReceivedDate.Value.ToString("yyyy-MM-dd") : "-";
            Topic = userMessage.Message.Header;
            IsSystem = userMessage.Message.IsSystem;
            Contents = userMessage.Message.Contents;
        }
    }
}