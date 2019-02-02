using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.Models.ViewModels
{
    public class DeleteDialogViewModel
    {
        public int Id { get; }
        public string Dialog { get; }
        public string ControllerName { get; }
        public string ActionName { get; }
        public string ModalId { get; }

        public DeleteDialogViewModel(int Id, string ControllerName, string Dialog)
        {
            this.Id = Id;
            this.Dialog = Dialog;
            this.ControllerName = ControllerName;

            this.ModalId = "deleteDialog";
            this.ActionName = "Delete";
        }

        public DeleteDialogViewModel(int Id, string ControllerName, string Dialog, string ModalId)
        {
            this.Id = Id;
            this.Dialog = Dialog;
            this.ControllerName = ControllerName;
            this.ModalId = ModalId;

            this.ActionName = "Delete";
        }

        public DeleteDialogViewModel(int Id, string ControllerName, string ActionName, string Dialog, string ModalId)
        {
            this.Id = Id;
            this.Dialog = Dialog;
            this.ControllerName = ControllerName;
            this.ActionName = ActionName;
            this.ModalId = ModalId;
        }
    }
}
