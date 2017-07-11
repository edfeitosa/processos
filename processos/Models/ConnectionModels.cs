using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace processos.Models
{
    public class ConnectionModels
    {
        public string Connection()
        {
            return "Persist Security Info=False;server=Localhost;database=webservice;uid=root;pwd=".ToString();
        }
    }
}