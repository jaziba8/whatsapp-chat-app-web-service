using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
       

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "getcountry/{id}/{name}", ResponseFormat = WebMessageFormat.Json)]
        Service1.student[] getcountry(string id,string name);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "SendMail/{To}", ResponseFormat = WebMessageFormat.Json)]

         string SendMail(String To);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "checkuserfrombiitdb/{phone}/{email}", ResponseFormat = WebMessageFormat.Json)]
        string checkuserfrombiitdb(string phone, string email);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "verifyusercode/{code}/{phone}", ResponseFormat = WebMessageFormat.Json)]
        string verifyusercode(string code,string phone);




    }

}
