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
            var message = userMessage.Message;

            if (message.MessageTypeId == (int)Consts.MessageTypes.StudentWelcome)
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
            IsSystem = message.IsSystem;
            Contents = message.Contents;
        }
    }
}