namespace MVC.Controllers
{
    using System.IO;
    using System.Net;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using Models;

    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Index(string email, string password)
        {
            var request = WebRequest.Create("http://localhost:9393/Api/Authorize/Authenticate?email=" + email + "&password=" + password);
            request.Credentials = CredentialCache.DefaultCredentials;

            using (var response = request.GetResponse())
            {
                var dataStream = response.GetResponseStream();
                if (dataStream != null)
                {
                    var reader = new StreamReader(dataStream);
                    var responseFromServer = reader.ReadToEnd();

                    var jsonSerializer = new JavaScriptSerializer();
                    var logon = jsonSerializer.Deserialize<LogonResult>(responseFromServer);

                    switch (logon.Role)
                    {
                        case "admin":
                            this.Response.Redirect("/Admin?id=" + logon.Id);
                            break;
                        case "staff":
                            this.Response.Redirect("/Staff?id=" + logon.Id);
                            break;
                        case "student":
                            this.Response.Redirect("/Student?id=" + logon.Id);
                            break;
                    }
                }
            }

            return this.View();
        }

        public ActionResult Index2()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Index2(string email, string password)
        {
            var postData = "UserName=" + email + "&Password=" + password;
            var request = WebRequest.Create("http://localhost:9393/Api/Authorize/Authenticate");
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            var byteArray = System.Text.Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var response = request.GetResponse();

            var dataStreamReponse = response.GetResponseStream();
            if (dataStreamReponse != null)
            {
                var reader = new StreamReader(dataStreamReponse);
                reader.ReadToEnd();
                reader.Close();
                dataStreamReponse.Close();
            }

            response.Close();

            return this.View();
        }

        public ActionResult Index3()
        {
            return this.View();
        }

        public ActionResult LogOff()
        {
            this.Session["role"] = null;
            this.Session["id"] = null;
            this.Session["email"] = null;
            return this.RedirectToAction("Index", "Home");
        }
    }
}
