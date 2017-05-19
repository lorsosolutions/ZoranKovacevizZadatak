using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoranKovacevizZadatak
{
    class Konekcija
    {
        String connString;
        SqlConnection SQLCON;

        public Konekcija()
        {

            connString = @"Data Source=SAVA-PC\SQLEXPRESS ;Initial Catalog=ZoranKovacevic; Integrated Security=True";
            SQLCON = new SqlConnection(connString);
        }

        public void OtvoriKonekciju() {
            SQLCON.Open();
        }

        public void ZatvoriKonekciju() {
            SQLCON.Close();
        }

        public SqlConnection GetConnection() {
            return SQLCON;
        }

    }
}
