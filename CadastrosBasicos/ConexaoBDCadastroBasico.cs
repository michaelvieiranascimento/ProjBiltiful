using CadastrosBasicos;
using System;
using System.Data;
using System.Data.SqlClient;


namespace BD
{
    public class ConexaoBDCadastroBasico
    {
        public string DataSource { get; }
        public string Database { get; }
        public string UserName { get; }
        public string Password { get; }
        public string ConnString { get; }


        public ConexaoBDCadastroBasico()
        {
            DataSource = @"LOCALHOST";
            Database = "BILTIFUL";
            UserName = "sa";
            Password = "250499";
            ConnString = @"Data Source=" + DataSource + ";Initial Catalog=" + Database + ";Persist Security Info=True;User ID=" + UserName + ";Password=" + Password;
        }

        // Area do Clinete(Gravar,Procurar e Editar)
        public void GravarCliente(Cliente cliente)
        {
            SqlConnection connection = new(ConnString);
            string cpf = cliente.CPF.Insert(3, ".").Insert(7, ".").Insert(11, ".");
            string nome = cliente.Nome.Trim();
            string dataNasc = cliente.DataNascimento.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char sexo = char.Parse(cliente.Sexo.ToString().ToUpper());
            string ultimaCompra = cliente.UltimaVenda.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string dataCadastro = cliente.DataCadastro.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char situacao = char.Parse(cliente.Situacao.ToString().ToUpper());

            try
            {
                using (connection)
                {
                    string sql = $"INSERT INTO CLIENTE VALUES('{cpf}', '{nome}', CONVERT(DATE, '{dataNasc}', 111), '{sexo}', CONVERT(DATE, '{ultimaCompra}', 111), CONVERT(DATE, '{dataCadastro}', 111), '{situacao}'); ";
                    connection.Open();
                    SqlCommand cmd = new(sql, connection);
                    Console.WriteLine(cmd.ToString());
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EX ->" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void ProcurarCliente(string cpf)
        {
            SqlConnection connection = new(ConnString);
            try
            {
                using (connection)
                {
                    string sql = $"SELECT CPF, NOME, CONVERT(DATE, DATA_NASCIMENTO , 111), CONVERT(DATE, ULTIMA_COMPRA , 111), CONVERT(DATE, DATA_CADASTRO , 111), SITUACAO FROM CLIENTE WHERE CPF = '{cpf}';"; 
                    connection.Open();
                    SqlCommand cmd = new(sql, connection);
                    Console.WriteLine(cmd.ToString());
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EX ->" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void EditarCliente(Cliente cliente)
        {
            SqlConnection connection = new(ConnString);
            string cpf = cliente.CPF.Insert(3, ".").Insert(7, ".").Insert(11, ".");
            string nome = cliente.Nome.Trim();
            string dataNasc = cliente.DataNascimento.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char sexo = char.Parse(cliente.Sexo.ToString().ToUpper());
            string ultimaCompra = cliente.UltimaVenda.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string dataCadastro = cliente.DataCadastro.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char situacao = char.Parse(cliente.Situacao.ToString().ToUpper());

            try
            {
                using (connection)
                {
                    string sql = $"UPDATE CLIENTE SET CPF = {cpf}', NOME = '{nome}', DATA_NASCIMENTO = CONVERT(DATE, '{dataNasc}', 111), SEXO = '{sexo}', ULTIMA_COMPRA = CONVERT(DATE, '{ultimaCompra}', 111), DATA_CADASTRO = CONVERT(DATE, '{dataCadastro}', 111), SITUACAO = '{situacao}' WHERE CPF = '{cpf}' ";
                    connection.Open();
                    SqlCommand cmd = new(sql, connection);
                    Console.WriteLine(cmd.ToString());
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EX ->" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //Area do Fornecedor(Gravar,Procurar e Editar)
        public void GravarFornecedor(Fornecedor fornecedor)
        {
            SqlConnection connection = new(ConnString);
            string CNPJ = fornecedor.CNPJ.Insert(2, ".").Insert(6, ".").Insert(9, "/").Insert(15, "-");
            string razaosocial = fornecedor.RazaoSocial.Trim();
            string dataAbertura = fornecedor.DataAbertura.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string ultimaCompra = fornecedor.UltimaCompra.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string dataCadastro = fornecedor.DataCadastro.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char situacao = char.Parse(fornecedor.Situacao.ToString().ToUpper());
            try
            {
                using (connection)
                {
                    string sql = $"INSERT INTO FORNECEDOR VALUES('{CNPJ}', '{razaosocial}', CONVERT(DATE, '{dataAbertura}', 111), CONVERT(DATE, '{ultimaCompra}', 111), CONVERT(DATE, '{dataCadastro}', 111),'{situacao}');";
                    connection.Open();
                    SqlCommand cmd = new(sql, connection);
                    Console.WriteLine(cmd.ToString());
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EX ->" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void ProcurarFornecedor(string cnpj)
        {
            SqlConnection connection = new(ConnString);
            try
            {
                using (connection)
                {
                    string sql = $"SELECT CNPJ, RAZAO_SOCIAL, CONVERT(DATE, DATA_ABERTURA , 111), CONVERT(DATE, ULTIMA_COMPRA , 111), CONVERT(DATE, DATA_CADASTRO , 111), SITUACAO FROM FORNECEDOR WHERE CNPJ = '{cnpj}';";
                    connection.Open();
                    SqlCommand cmd = new(sql, connection);
                    Console.WriteLine(cmd.ToString());
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EX ->" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void EditarFornecedor(Fornecedor fornecedor)
        {
            SqlConnection connection = new(ConnString);
            string CNPJ = fornecedor.CNPJ.Insert(2, ".").Insert(6, ".").Insert(9, "/").Insert(15, "-");
            string razaosocial = fornecedor.RazaoSocial.Trim();
            string dataAbertura = fornecedor.DataAbertura.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string ultimaCompra = fornecedor.UltimaCompra.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string dataCadastro = fornecedor.DataCadastro.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char situacao = char.Parse(fornecedor.Situacao.ToString().ToUpper());
            try
            {
                using (connection)
                {
                    string sql = $"UPDATE FORNECEDOR SET CNPJ = '{CNPJ}', RAZAO_SOCIAL = '{razaosocial}', DATA_ABERTURA = CONVERT(DATE, '{dataAbertura}', 111), ULTIMA_COMPRA = CONVERT(DATE, '{ultimaCompra}', 111), DATA_CADASTRO = CONVERT(DATE, '{dataCadastro}', 111), SITUACAO = '{situacao}' WHERE CNPJ = '{CNPJ}";
                    connection.Open();
                    SqlCommand cmd = new(sql, connection);
                    Console.WriteLine(cmd.ToString());
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EX ->" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //Area do Produto(Gravar,Procurar e Editar)
        public void GravarProduto(Produto produto)
        {
            SqlConnection connection = new(ConnString);
            string codigoBarras = produto.CodigoBarras;
            string nome = produto.Nome.Trim();
            decimal valorVenda = produto.ValorVenda;
            string ultimaVenda = produto.UltimaVenda.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string dataCadastro = produto.DataCadastro.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char situacao = char.Parse(produto.Situacao.ToString().ToUpper());

            try
            {
                using (connection)
                {
                    string sql = $"INSERT INTO PRODUTO VALUES('{codigoBarras}', '{nome}', '{valorVenda}', CONVERT(DATE, '{ultimaVenda}', 111), CONVERT(DATE, '{dataCadastro}', 111),'{situacao}');";
                    connection.Open();
                    SqlCommand cmd = new(sql, connection);
                    Console.WriteLine(cmd.ToString());
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EX ->" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void ProcurarProduto(string codigoBarras)
        {
            SqlConnection connection = new(ConnString);
            try
            {
                using (connection)
                {
                    string sql = $"SELECT CODIGO_BARRAS, NOME, VALOR_VENDA, CONVERT(DATE, ULTIMA_VENDA , 111), CONVERT(DATE, DATA_CADASTRO , 111), SITUACAO  FROM PRODUTO WHERE CODIGO_BARRAS = '{codigoBarras}';";
                    connection.Open();
                    SqlCommand cmd = new(sql, connection);
                    Console.WriteLine(cmd.ToString());
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EX ->" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void EditarProduto(Produto produto)
        {
            SqlConnection connection = new(ConnString);
            string codigoBarras = produto.CodigoBarras;
            string nome = produto.Nome.Trim();
            decimal valorVenda = produto.ValorVenda;
            string ultimaVenda = produto.UltimaVenda.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string dataCadastro = produto.DataCadastro.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char situacao = char.Parse(produto.Situacao.ToString().ToUpper());

            try
            {
                using (connection)
                {
                    string sql = $"UPDATE PRODUTO SET CODIGO_BARRAS = {codigoBarras}', NOME = '{nome}', VALOR_VENDA = '{valorVenda}', ULTIMA_VENDA = CONVERT(DATE, '{ultimaVenda}', 111), DATA_CADASTRO = CONVERT(DATE, '{dataCadastro}', 111), SITUACAO = '{situacao}' WHERE CODIGO_BARRAS = '{codigoBarras}'";
                    connection.Open();
                    SqlCommand cmd = new(sql, connection);
                    Console.WriteLine(cmd.ToString());
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EX ->" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //Area da MPrima(Gravar,Procurar e Editar)
        public void GravarMPrima(MPrima mprima)
        {
            SqlConnection connection = new(ConnString);
            string Id = mprima.Id;
            string nome = mprima.Nome.Trim();
            string ultimaCompra = mprima.UltimaCompra.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string dataCadastro = mprima.DataCadastro.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char situacao = char.Parse(mprima.Situacao.ToString().ToUpper());

            try
            {
                using (connection)
                {
                    string sql = $"INSERT INTO MPRIMA VALUES('{Id}', '{nome}', CONVERT(DATE, '{ultimaCompra}', 111), CONVERT(DATE, '{dataCadastro}', 111),'{situacao}');";
                    connection.Open();
                    SqlCommand cmd = new(sql, connection);
                    Console.WriteLine(cmd.ToString());
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EX ->" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void ProcurarMPrima(string Id)
        {
            SqlConnection connection = new(ConnString);
            try
            {
                using (connection)
                {
                    string sql = $"SELECT ID, NOME, CONVERT(DATE, ULTIMA_COMPRA , 111), CONVERT(DATE, DATA_CADASTRO , 111), SITUACAO FROM MPRIMA WHERE ID = '{Id}';";
                    connection.Open();
                    SqlCommand cmd = new(sql, connection);
                    Console.WriteLine(cmd.ToString());
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EX ->" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void EditarMPrima(MPrima mprima)
        {
            SqlConnection connection = new(ConnString);
            string Id = mprima.Id;
            string nome = mprima.Nome.Trim();
            string ultimaCompra = mprima.UltimaCompra.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string dataCadastro = mprima.DataCadastro.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char situacao = char.Parse(mprima.Situacao.ToString().ToUpper());

            try
            {
                using (connection)
                {
                    string sql = $"UPDATE MPRIMA SET ID = '{Id}', NOME = '{nome}', ULTIMA_COMPRA = CONVERT(DATE, '{ultimaCompra}', 111), DATA_CADASTRO = CONVERT(DATE, '{dataCadastro}', 111), SITUACAO = '{situacao}' WHERE ID = '{Id}'";
                    connection.Open();
                    SqlCommand cmd = new(sql, connection);
                    Console.WriteLine(cmd.ToString());
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EX ->" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


    }

}
