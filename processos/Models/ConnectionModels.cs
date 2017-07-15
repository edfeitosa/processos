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
            return "Database=processos; Data Source=localhost; User Id=root; Password=; pooling=false".ToString();
        }
    }
}