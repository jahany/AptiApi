using AptinetDataAccessLibrary.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Net.Mail;
using Azure;
using System;
using System.Collections.Generic;
using Azure.Communication.Email;
using AptinetDataAccessLibrary.Dtos.Requests;



namespace AptiApi.Controllers
{
    [Route("api/APP")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;

        readonly AP_DBContext _db;
        public AppController(AP_DBContext db, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _db = db;
            Environment = environment;
        }

        [HttpGet("GetAppVersion")]
        public IActionResult GetData()
        {
            var res = from v in _db.Versions
                      orderby v.id descending
                      select v.appVersion;
            return Ok(res.First());
        }
        [HttpGet("GetDbVersion")]
        public IActionResult GetDbVersion()
        {
            var res = from v in _db.Versions
                      orderby v.id descending
                      select v.dbVersion;
            return Ok(res.First());
        }
        [HttpGet("GetImagesVersion")]
        public IActionResult GetImagesVersion()
        {
            var res = from v in _db.Versions
                      orderby v.id descending
                      select v.imagesVersion;
            return Ok(res.First());
        }

        [HttpGet("download")]
        public ActionResult Download()
        {
            string contentPath = this.Environment.ContentRootPath;

            string filePath = contentPath + "/aptinet.zip";

            //string filePath = "/home/downloads/aptinet.zip";
            string fileName = "aptinet.zip";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName);

        }

        [HttpGet("downloadProdpics")]
        public ActionResult DownloadProdPics()
        {
            string contentPath = this.Environment.ContentRootPath;

            string filePath = contentPath + "/prodpics.zip";

            //string filePath = "/home/downloads/prodpics.zip";
            string fileName = "prodpics.zip";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName);

        }
        [HttpGet("downloadUserpics")]
        public ActionResult downloaduserpics()
        {
            string filePath = "/home/downloads/userpics.zip";
            string fileName = "userpics.zip";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName);

        }
        [HttpPost("upload-file")]
        public async Task<IActionResult> UploadFile([FromQuery] IFormFile file)
        {
            try
            {
                //20210101
                Environment.WebRootPath = Directory.GetCurrentDirectory();
                string uploads = Path.Combine(Environment.WebRootPath, "uploads");
                //string uploads = "/home/uploads";
                string d = DateTime.Now.Day.ToString();
                if (d.Length == 1)
                {
                    d = "0" + d;
                }
                string m = DateTime.Now.Month.ToString();
                if (m.Length == 1)
                {
                    m = "0" + m;
                }
                string dir = DateTime.Now.Year.ToString() + m + d;
                string resDirectory = Path.Combine(uploads, dir);
                if (Directory.Exists(resDirectory) != true)
                {
                    Directory.CreateDirectory(resDirectory);

                }

                if (file.Length > 0)
                {
                    string filePath = Path.Combine(resDirectory, file.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
                return Ok("1");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost("sendMail")]
        public string sendMail(SendEmail se)
        {
            string connectionString = "endpoint=https://aptinet-com-ser.unitedstates.communication.azure.com/;accesskey=N+W/f/vXl3HdG2yTZKPwS8heHXoEozGxa97+jRZ6n5x/9b33iifOTHrhuimTv67EoByr2YcLhblAwEBDSzLUIw==";
            var emailClient = new EmailClient(connectionString);

            string p = "";
            foreach (var item in se.products)
            {
                p += "<p> " + item.barcode + " --- " + item.name + "---" + item.count + " --- " + item.productPrice + " --- " + item.productFinalPrice + "---" + item.productTotalFinalPrice + "</p>";
            }
            p += "<p>" + se.paymentTime + "</p>";
            p += "<p>" + se.totalPrice + "</p>";
            p += "<p>" + se.totalFinalPrice + "</p>";
            p += "<p>" + se.priceToPay + "</p>";

            string content = "<html>" + p + "</html>";
            EmailSendOperation emailSendOperation = emailClient.Send(
              WaitUntil.Completed,
              senderAddress: "SmartCart@ae6a3057-efab-402f-ab5f-d2c8d59c5fe4.azurecomm.net",
              recipientAddress: se.emailAddress,
              subject: "Invoice",
              htmlContent: content,
              plainTextContent: "your Reciept");







            //MailAddress to = new MailAddress(mail);
            //MailAddress from = new MailAddress(from1);

            //MailMessage email = new MailMessage(from, to);
            //email.Subject = "Testing out email sending";
            //email.Body = "Hello all the way from the land of C#";

            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "localhost";
            //smtp.Port = 587;
            //smtp.Credentials = new NetworkCredential("mailuser", "Asmsaf1657");
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtp.EnableSsl = false;

            //try
            //{
            //    /* Send method called below is what will send off our email 
            //     * unless an exception is thrown.
            //     */
            //    smtp.Send(email);
            //}
            //catch (SmtpException ex)
            //{
            //    return (ex.ToString());
            //}
            return "-1";
        }
    }
}

