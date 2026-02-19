using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GPSFA_WinForms
{
    public class ProductRepository
    {
        public static DataTable BuscarTodosProdutos()
        {
            DataTable dt = new DataTable();
            using (MySqlCommand comm = new MySqlCommand())
            {
                comm.CommandText = "SELECT descricao, quantidade, peso, unidade, codBar, dataDeEntrada, dataDeValidade FROM tbprodutos ORDER BY dataDeEntrada DESC;";
                comm.Connection = DataBaseConnection.OpenConnection();
                
                MySqlDataAdapter da = new MySqlDataAdapter(comm);
                da.Fill(dt);

                DataBaseConnection.CloseConnection();
            }

            return dt;
        }
    }
}
