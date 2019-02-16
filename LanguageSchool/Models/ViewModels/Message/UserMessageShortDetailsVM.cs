using System;
using System.Web.Mvc;
using LanguageSchool.DAL;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace LanguageSchool.Models.ViewModels.MessageViewModels
{
    public class UserMessageShortDetailsVM
    {
        public int UserMessageId { get; }
        public bool HasBeenReceived { get; }
        public bool IsSystem { get; }
        public string SentDate { get; }
        public string ReceivedDate { get; }
        public string Topic { get; }
        public string ShortenedContents { get; }

        public UserMessageShortDetailsVM(UserMessage userMessage)
        {
            UserMessageId = userMessage.Id;
            SentDate = userMessage.Message.CreationDate.ToString("yyyy-MM-dd");
            ReceivedDate = userMessage.ReceivedDate.HasValue ? userMessage.ReceivedDate.Value.ToString("yyyy-MM-dd") : "-";
            Topic = userMessage.Message.Header;
            HasBeenReceived = userMessage.HasBeenReceived;
            IsSystem = userMessage.Message.IsSystem;

            var contents = userMessage.Message.Contents;
            ShortenedContents = (contents.Length > 80) ? contents.Substring(0, 80) + "..." : contents;
        }
    }
}