using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewRestoranoSistema
{
    public interface ISendEmail
    {
        
        //string SenderName { get { return "RestaurantCity"; } set {  } }
        //public string SenderEmail { get { return "sdfg.mail.com"; } set { } }
     

        string SendEmail();
        
    }
}
