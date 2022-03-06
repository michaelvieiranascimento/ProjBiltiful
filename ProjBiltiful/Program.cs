using System;
using System.IO;
using VendasProdutos;
using CadastrosBasicos;
using ProducaoCosmeticos;
using System.Globalization;

namespace ProjBiltiful
{
    internal class Program
    {
        public static void GerarPastas()
        {
            string caminhoInicial = Directory.GetCurrentDirectory();
            Console.WriteLine(caminhoInicial);
            string caminhoFinal = Path.Combine(caminhoInicial, "ProjBiltiful");
            Directory.CreateDirectory(caminhoFinal);

            string pastaMateriaPrima = Path.Combine(caminhoFinal, "MateriaPrima");
            Directory.CreateDirectory(pastaMateriaPrima);

            string pastaProduto = Path.Combine(caminhoFinal, "Produto");
            Directory.CreateDirectory(pastaProduto);

        }

        static void Main(string[] args)
        {

            var cultureInformation = new CultureInfo("pt-BR");
            cultureInformation.NumberFormat.CurrencySymbol = "R$";
            CultureInfo.DefaultThreadCurrentCulture = cultureInformation;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInformation;

            int value;

            do
            {
                Console.Clear();
                Console.Write(@"=============== BITIFUL ===============
1. Cadastros
2. Vendas
3. Compra de Materia-Prima
4. Producao
0 - Sair
Insira uma opcao valida: ");

                switch (value = int.Parse(Console.ReadLine()))
                {
                    case 0:
                        Environment.Exit(0);
                        break;

                    case 1:
                        break;

                    case 2:
                        MenuVendas.SubMenu();
                        break;

                    case 3:
                        break;

                    case 4:
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida");
                        Console.ReadKey();
                        break;
                }
            } while (value != 0);
        }
    }
}