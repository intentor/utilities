/*********************************************
Intentor.Utilities
**********************************************
Copyright � 2009-2012 Andr� "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Intentor.Utilities {
    /// <summary>
    /// Cont�m m�todos para acesso a e-mails.
    /// </summary>
    public static class SmtpHelper {
        /// <summary>
        /// Realiza envio de e-mail.
        /// </summary>
        /// <param name="from">Remetente do e-mail.</param>
        /// <param name="to">Destinat�rio do e-mail.</param>
        /// <param name="subject">Assunto do e-mail.</param>
        /// <param name="message">Mensagem do e-mail.</param>
        /// <remarks>O host utilizado ser� aquele presente no web.config.</remarks>
        public static void SendMail(string from
            , string to
            , string subject
            , string message) {
            SendMail(from, new List<string>(1) { to }, null, null, subject, message, true, null, null, null);
        }

        /// <summary>
        /// Realiza envio de e-mail.
        /// </summary>
        /// <param name="from">Remetente do e-mail.</param>
        /// <param name="to">Destinat�rio do e-mail.</param>
        /// <param name="subject">Assunto do e-mail.</param>
        /// <param name="message">Mensagem do e-mail.</param>
        /// <param name="host">Endre�o do servidor de e-mails.</param>
        public static void SendMail(string from
            , string to
            , string subject
            , string message
            , string host) {
            SendMail(from, new List<string>(1) { to }, null, null, subject, message, true, null, host, null);
        }

        /// <summary>
        /// Realiza envio de e-mail.
        /// </summary>
        /// <param name="from">Remetente do e-mail.</param>
        /// <param name="to">Lista de destinat�rios do e-mail.</param>
        /// <param name="cc">Lista de destinat�rios que receber�o c�pia do e-mail.</param>
        /// <param name="cco">Lista de destinat�rios que receber�o c�pia oculta do e-mail.</param>
        /// <param name="subject">Assunto do e-mail.</param>
        /// <param name="message">Mensagem do e-mail.</param>
        /// <param name="isBodyHtml">Indica se o corpo do e-mail � em formato HTML.</param>
        /// <param name="attachments">Anexos ao e-mail.</param>
        /// <param name="host">Endre�o do servidor de e-mails.</param>
        /// <param name="credentials">Credenciais de acesso ao servidor de e-mail.</param>
        /// <remarks>Caso nenhum host seja definido, ser�o utilizadas as configura��es presentes no web.config.</remarks>
        public static void SendMail(string from
            , List<string> to
            , List<string> cc
            , List<string> cco
            , string subject
            , string message
            , bool isBodyHtml
            , List<Attachment> attachments
            , string host
            , ICredentialsByHost credentials) {
            MailMessage mail = new MailMessage();

            //Endere�os.
            mail.From = new MailAddress(from);
            foreach (string address in to) mail.To.Add(address);
            if (cc != null) foreach (string address in cc) mail.CC.Add(address);
            if (cco != null) foreach (string address in cco) mail.Bcc.Add(address);

            //Conte�do do e-mail.
            mail.Subject = subject;
            mail.IsBodyHtml = isBodyHtml;
            mail.Body = message;

            //Anexos.
            if (attachments != null) foreach (Attachment a in attachments) mail.Attachments.Add(a);

            //Realiza envio do e-mail.
            SmtpClient smtp = new SmtpClient();
            if (host != null) smtp.Host = host;
            if (credentials != null) smtp.Credentials = credentials;
            smtp.Send(mail);
        }
    }
}
