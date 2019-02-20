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
            var message = userMessage.Message;

            if(message.MessageTypeId == (int)Consts.MessageTypes.StudentWelcome)
            {
                var student = userMessage.User;

                SentDate = student.CreationDate.ToString("yyyy-MM-dd");
            }
            else
            {
                SentDate = message.CreationDate.ToString("yyyy-MM-dd");
            }

            UserMessageId = userMessage.Id;
            ReceivedDate = userMessage.ReceivedDate.HasValue ? userMessage.ReceivedDate.Value.ToString("yyyy-MM-dd") : "-";
            Topic = message.Header;
            HasBeenReceived = userMessage.HasBeenReceived;
            IsSystem = message.IsSystem;

            var contents = userMessage.Message.Contents.Replace("<br>", " ");
            ShortenedContents = (contents.Length > 80) ? contents.Substring(0, 80) + "..." : contents;
        }
    }
}