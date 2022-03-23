using CadastrosBasicos;
using System;
using System.Data;
using System.Data.SqlClient;


namespace BD
{
    public class ConexaoBD
    {
        public string DataSource { get; }
        public string Database { get; }
        public string UserName { get; }
        public string Password { get; }
        public string ConnString { get; }


        public ConexaoBD()
        {
            DataSource = @"LOCALHOST";
            Database = "BILTIFUL";
            UserName = "sa";
            Password = "250499";
            ConnString = @"Data Source=" + DataSource + ";Initial Catalog=" + Database + ";Persist Security Info=True;User ID=" + UserName + ";Password=" + Password;
        }

    }

}
