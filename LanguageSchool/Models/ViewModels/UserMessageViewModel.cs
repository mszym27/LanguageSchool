using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class UserMessageViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public MessageType MessageType { get; set; }
        public string SentDate { get; set; }
        public string ReceivedDate { get; set; }
        public string Topic { get; set; }
        [DataType(DataType.MultilineText)]
        public string Contents { get; set; }
        public string ShortenedContents { get; set; }
        public bool HasBeenReceived { get; set; }
        public bool IsCyclical { get; set; }

        public UserMessageViewModel()
        {

        }

        public UserMessageViewModel(UserMessage userMessage)
        {
            Id = userMessage.Id;
            SentDate = userMessage.CreationDate.ToString("yyyy-MM-dd");
            ReceivedDate = userMessage.ReceivedDate.HasValue ? userMessage.ReceivedDate.Value.ToString("yyyy-MM-dd") : "-";
            Topic = userMessage.Message.Header;
            Contents = userMessage.Message.Contents;
            ShortenedContents = userMessage.Message.Contents.Substring(0, 70) + "...";
            HasBeenReceived = userMessage.HasBeenReceived;
            IsCyclical = userMessage.IsCyclical;
        }
    }
}