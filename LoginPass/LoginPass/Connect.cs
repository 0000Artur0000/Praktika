using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginPass
{
    class Connect
    {
        public static SqlConnectionStringBuilder jojo = new SqlConnectionStringBuilder()
        {
            //jojo.DataSource = "303-2\\SQLEXPRESS";
            DataSource = "DESKTOP-U5HC5KL",
            InitialCatalog = "bd",
            IntegratedSecurity = true
        };
    
    }
}
