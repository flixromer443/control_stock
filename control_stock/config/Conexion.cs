using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_stock.config
{
    internal class Conexion
    {
        private static string datasource = "Data Source=control_stockV01.db";
        public SQLiteConnection conectar()
        {
            return new SQLiteConnection(datasource);
        }
    }
    
}
