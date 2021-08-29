using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailGenerator.Models
{
	public class EmailModel : ModelBase
	{
		private string _smtpServer = string.Empty;

		public string SMTPServer
		{
			get { return _smtpServer; }
			set
			{
				if (_smtpServer != value)
				{
					_smtpServer = value;
					OnPropertyChanged(nameof(SMTPServer));
				}
				if (string.IsNullOrEmpty(_smtpServer))
					AddError(nameof(SMTPServer), "SMTP server empty");
				else
					RemoveError(nameof(SMTPServer));
			}
		}

		private string _emailFrom = string.Empty;

		public string EmailFrom
		{
			get { return _emailFrom; }
			set
			{
				if (_emailFrom != value)
				{
					_emailFrom = value;
					OnPropertyChanged(nameof(EmailFrom));
				}
				if (string.IsNullOrEmpty(_emailFrom))
					AddError(nameof(EmailFrom), "Email from empty");
				else
					RemoveError(nameof(EmailFrom));
			}
		}

		private string _emailTo = string.Empty;

		public string EmailTo
		{
			get { return _emailTo; }
			set
			{
				if (_emailTo != value)
				{
					_emailTo = value;
					OnPropertyChanged(nameof(EmailTo));
				}
				if (string.IsNullOrEmpty(_emailTo))
					AddError(nameof(EmailTo), "Email to empty");
				else
					RemoveError(nameof(EmailTo));
			}
		}

		private string _subject = string.Empty;

		public string Subject
		{
			get { return _subject; }
			set
			{
				if (_subject != value)
				{
					_subject = value;
					OnPropertyChanged(nameof(Subject));
				}
				if (string.IsNullOrEmpty(_subject))
					AddError(nameof(Subject), "Subject empty");
				else
					RemoveError(nameof(Subject));
			}
		}

		private ObservableCollection<string> _attachmentFiles = new ObservableCollection<string>();

		public ObservableCollection<string> AttachmentFiles
		{
			get { return _attachmentFiles; }
			set
			{
				if (_attachmentFiles != value)
				{
					_attachmentFiles = value;
					OnPropertyChanged(nameof(AttachmentFiles));
				}
			}
		}
		private string _selectedAttachment = string.Empty;

		public string SelectedAttachment
		{
			get { return _selectedAttachment; }
			set
			{
				if (_selectedAttachment != value)
				{
					_selectedAttachment = value;
					OnPropertyChanged(nameof(SelectedAttachment));
				}
			}
		}


		private int _numberOfMails = 10;

		public int NumberOfMails
		{
			get { return _numberOfMails; }
			set
			{
				if (_numberOfMails != value)
				{
					_numberOfMails = value;
					OnPropertyChanged(nameof(NumberOfMails));
				}
				if (_numberOfMails == 0)
					AddError(nameof(NumberOfMails), "Number must be 1 or more");
				else
					RemoveError(nameof(NumberOfMails));
			}
		}
        private string _message = string.Empty;

        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }

        public void ForceValidation()
		{
			SMTPServer = SMTPServer;
			EmailFrom = EmailFrom;
			EmailTo = EmailTo;
			Subject = Subject;
			Message = Message;
			NumberOfMails = NumberOfMails;
		}
		
	}
}
