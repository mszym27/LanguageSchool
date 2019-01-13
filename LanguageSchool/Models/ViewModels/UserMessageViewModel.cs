using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchool.Models.ViewModels
{
    public class UserMessageViewModel
    {
        public int Id { get; set; }
        public string SentDate { get; set; }
        public string ReceivedDate { get; set; }
        public string Topic { get; set; }
        public string ShortenedContents { get; set; }
        [DataType(DataType.MultilineText)]
        public string Contents { get; set; }
        public bool HasBeenReceived { get; set; }
        public bool IsSystem { get; set; }

        // na potrzeby tworzenia

        public SelectList MessageTypes { get; set; }
        public int MessageTypeId { get; set; }
        public SelectList Users { get; set; }
        public int UserId { get; set; }
        public SelectList Groups { get; set; }
        public int GroupId { get; set; }
        public SelectList Courses { get; set; }
        public int CourseId { get; set; }
        public SelectList Roles { get; set; }
        public int RoleId { get; set; }

        public UserMessageViewModel()
        {

        }

        public UserMessageViewModel(UserMessage userMessage)
        {
            Id = userMessage.Id;
            SentDate = userMessage.Message.CreationDate.ToString("yyyy-MM-dd");
            ReceivedDate = userMessage.ReceivedDate.HasValue ? userMessage.ReceivedDate.Value.ToString("yyyy-MM-dd") : "-";
            Topic = userMessage.Message.Header;
            Contents = userMessage.Message.Contents;
            HasBeenReceived = userMessage.HasBeenReceived;
            IsSystem = userMessage.Message.IsSystem;

            ShortenedContents = (Contents.Length > 80) ? Contents.Substring(0, 80) + "..." : Contents;
        }
    }
}