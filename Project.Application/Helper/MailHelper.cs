using MailKit.Net.Smtp;
using MimeKit;
using Project.Application.Models.DTOs.AppUserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Helper
{
    public static class MailHelper
    {
        public static void Send(string email, int code)
        {
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("Kağan Eriş", "projemaka@gmail.com"); // Mailin kimden gideceği!
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", email);

            mimeMessage.From.Add(mailboxAddressFrom);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "Kayıt işlemini gerçekleştirmek için onay kodunuz: " + code;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = "Onay Kodu";

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("projemaka@gmail.com", "wvgdopwbgegdlgcl");
            client.Send(mimeMessage);
            client.Disconnect(true);
        }

        public static void SendContact(ContactUsDTO contactUsDTO)
        {
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("HamburgerProjesi Admin", "projemaka@gmail.com"); // Mailin kimden gideceği!
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", "burgermaka4@gmail.com");

            mimeMessage.From.Add(mailboxAddressFrom);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"<ul>
	<li>
	<h3><span style=""font-family:Georgia,serif""><strong>Adı: &nbsp;</strong>{contactUsDTO.Name}</span></h3>
	</li>
	<li>
	<h3><span style=""font-family:Georgia,serif""><strong>Email: </strong>{contactUsDTO.Email}</span></h3>
	</li>
	<li>
	<h3><span style=""font-family:Georgia,serif""><strong>Konu: </strong>{contactUsDTO.Subject}</span></h3>
	</li>
	<li>
	<h3><span style=""font-family:Georgia,serif""><strong>Mesaj: </strong>{contactUsDTO.Message}</span></h3>
	</li>
</ul>";
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = contactUsDTO.Subject;

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("projemaka@gmail.com", "wvgdopwbgegdlgcl");
            client.Send(mimeMessage);
            client.Disconnect(true);
        }
    }
}
