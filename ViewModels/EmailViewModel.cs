using EmailGenerator.Command;
using EmailGenerator.Controller;
using EmailGenerator.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EmailGenerator.ViewModels
{
	public class EmailViewModel
	{
		public EmailModel EmailModel { get; } = new EmailModel();
		public EmailViewModel()
		{
			EmailModel.SMTPServer = Properties.Settings.Default.SMTPServer;
			EmailModel.EmailFrom = Properties.Settings.Default.From;
			EmailModel.EmailTo = Properties.Settings.Default.To;
			EmailModel.Subject = Properties.Settings.Default.Subject;
			EmailModel.Message = Properties.Settings.Default.Message;
			EmailModel.ForceValidation();
		}
		private ICommand _addAttachment = null;
		public ICommand AddAttachtment
		{
			get
			{
				if (_addAttachment == null)
					_addAttachment = new DelegateCommand(AddAttachmentCommand, AddAttachmentEnabled);
				return _addAttachment;
			}
		}
		private bool AddAttachmentEnabled(object param)
		{
			return true;
		}
		private void AddAttachmentCommand(object param)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == true)
				EmailModel.AttachmentFiles.Add(openFileDialog.FileName);
		}
		private ICommand _removeAttachment = null;
		public ICommand RemoveAttachtment
		{
			get
			{
				if (_removeAttachment == null)
					_removeAttachment = new DelegateCommand(RemoveAttachmentCommand, RemoveAttachmentEnabled);
				return _removeAttachment;
			}
		}
		private bool RemoveAttachmentEnabled(object param)
		{
			return !string.IsNullOrEmpty(EmailModel.SelectedAttachment);
		}
		private void RemoveAttachmentCommand(object param)
		{
			if (!string.IsNullOrEmpty(EmailModel.SelectedAttachment))
			{
				EmailModel.AttachmentFiles.Remove(EmailModel.SelectedAttachment);
			}
		}

		private ICommand _sendMails = null;
		public ICommand SendMails
		{
			get
			{
				if (_sendMails == null)
					_sendMails = new DelegateCommand(SendMailsCommand, SendMailsEnabled);
				return _sendMails;
			}
		}
		private bool SendMailsEnabled(object param)
		{
			return !string.IsNullOrEmpty(EmailModel.SMTPServer) && !string.IsNullOrEmpty(EmailModel.EmailFrom) && !string.IsNullOrEmpty(EmailModel.EmailTo) && !string.IsNullOrEmpty(EmailModel.Subject) && EmailModel.NumberOfMails > 0 && !EmailModel.HasErrors;
		}

		private void SendMailsCommand(object param)
		{
			Properties.Settings.Default.SMTPServer = EmailModel.SMTPServer;
			Properties.Settings.Default.From = EmailModel.EmailFrom;
			Properties.Settings.Default.To = EmailModel.EmailTo;
			Properties.Settings.Default.Subject = EmailModel.Subject;
			Properties.Settings.Default.Message = EmailModel.Message;
			Properties.Settings.Default.Save();

			try
			{
				var sendMails = new SendMails(EmailModel);
				sendMails.Send();
				MessageBox.Show("Messages send");
			}
			catch(Exception ex)
            {
				MessageBox.Show(ex.Message);
            }
		}
	}
}
