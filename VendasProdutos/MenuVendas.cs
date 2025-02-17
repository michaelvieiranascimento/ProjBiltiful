﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastrosBasicos;
using CadastrosBasicos.ManipulaArquivos;

namespace VendasProdutos
{
    public class MenuVendas
    {
        public static void SubMenu()
        {
            new Arquivos();

            string opcao;

            do
            {
                Console.Clear();

                Console.WriteLine("=============== VENDAS ===============");
                Console.WriteLine("1. Nova Venda");
                Console.WriteLine("2. Consultar Venda");
                Console.WriteLine("3. Imprimir Registros de Venda");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("0. Voltar");
                Console.Write("\nEscolha: ");

                switch (opcao = Console.ReadLine())
                {
                    case "1":
                        NovaVenda();
                        break;

                    case "2":
                        LocalizarVenda();
                        break;
                    case "3":
                        new Venda().ImpressaoPorRegistro();
                        break;
                    case "0":
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida");
                        Console.WriteLine("\nPressione ENTER para voltar ao menu");
                        break;
                }
            } while (opcao != "0");
        }


        public static void NovaVenda()
        {
            Console.Clear();

            Cliente cliente;

            Console.WriteLine("informe o CPF do cliente:");
            string cpf = Console.ReadLine();

            if (new Read().ProcurarCPFBloqueado(cpf) == true)
            {
                Console.Clear();
                Console.WriteLine("\n Falha ao iniciar a venda. Procure pelo gerente do local.");
                Console.WriteLine("\n Pressione ENTER para voltar ao menu");
                Console.ReadKey();
                return;
            }
            else
            {
                cliente = new Read().ProcuraCliente(cpf);

                if (cliente == null)
                {
                    Console.Clear();
                    Console.WriteLine("\nCliente não encontrado");
                    Console.WriteLine("\n Pressione ENTER para voltar ao menu");
                    Console.ReadKey();
                    return;
                }

            }

            Console.Clear();

            Venda venda = new Venda();

            venda.Cliente = cliente.CPF;
            venda.DataVenda = DateTime.Now.Date;

            Console.Write($"Venda Nº {venda.Id.ToString().PadLeft(5, '0')}\tData: {venda.DataVenda.ToString("dd/MM/yyyy")}");
            Console.WriteLine();

            List<ItemVenda> itensVenda = new List<ItemVenda>();

            int itens = 1;
            string escolha;

            do
            {
                Produto produto;
                int qtd = 0;
                decimal totalItens = 0;

                do
                {
                    produto = new Produto();

                    Console.WriteLine("\nDigite o Código do Produto:");
                    string codProduto = Console.ReadLine();

                    produto = produto.RetornaProduto(codProduto);

                    if (produto == null)
                    {
                        Console.WriteLine("\nProduto não encontrado ou código inválido.");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }

                    Console.WriteLine("\nInforme a quantidade:");
                    qtd = int.Parse(Console.ReadLine());


                    if (qtd <= 0 || qtd > 999)
                    {
                        Console.WriteLine("Informe uma quantidade entre 1 e 999");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }

                    totalItens = qtd * produto.ValorVenda;

                    if (totalItens > (decimal)9999.99)
                    {
                        Console.WriteLine("Valor total dos item passou o limite permitido de $ 9.999,99");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                } while ((qtd <= 0 || qtd > 999) || totalItens > (decimal)9999.99 || produto == null);

                Console.Clear();


                itensVenda.Add(new ItemVenda(venda.Id, produto.CodigoBarras, qtd, produto.ValorVenda));

                Console.WriteLine("Id\tProduto\t\tQtd\tV.Unitário\tT.Item");
                Console.WriteLine("------------------------------------------------------");

                decimal valorTotal = 0;

                itensVenda.ForEach(item =>
                {
                    Console.WriteLine(item.ToString());
                    valorTotal += item.TotalItem;
                    venda.ValorTotal = valorTotal;
                });

                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine($"\t\t\t\t\t\t{venda.ValorTotal.ToString("#.00")}");


                do
                {
                    Console.WriteLine("\nAdicionar novo produto?");
                    Console.WriteLine("[ S ] Sim\t[ N ] Não");
                    escolha = Console.ReadLine().ToUpper();

                    Console.Clear();
                } while (escolha != "S" && escolha != "N");


                if (escolha == "S")
                    itens++;
                else
                    break;

                if (itens == 4)
                {
                    Console.Clear();
                    Console.WriteLine("Seu carrinho está cheio!");
                    Console.ReadKey();
                    break;
                }

            } while (itens != 4);


            do
            {
                Console.Clear();
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("                           CLIENTE                        ");
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Nome:\t\t{cliente.Nome.TrimStart(' ')}");
                Console.WriteLine($"CPF:\t\t{cliente.CPF.Insert(3, ".").Insert(7, ".").Insert(11, "-")}");
                Console.WriteLine($"Data Nasc.:\t{cliente.DataNascimento.ToString("dd/MM/yyyy")}");
                Console.WriteLine($"Ultima Compra:\t{cliente.UltimaVenda.ToString("dd/MM/yyyy")}");
                Console.WriteLine("\n\n----------------------------------------------------------");
                Console.WriteLine($"Venda Nº {venda.Id.ToString().PadLeft(5, '0')}\t\t\tData: {venda.DataVenda.ToString("dd/MM/yyyy")}");
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("\n\nId\tProduto\t\tQtd\tV.Unitário\tT.Item");
                Console.WriteLine("----------------------------------------------------------");
                itensVenda.ForEach(item => Console.WriteLine(item.ToString()));
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"\t\t\t\t\t\t{venda.ValorTotal.ToString("#.00")}");

                Console.WriteLine("\n\n");

                Console.WriteLine("[ F ] Finalizar venda\t[ C ] Cancelar venda");
                escolha = Console.ReadLine().ToUpper();

            } while (escolha != "F" && escolha != "C");

            if (escolha == "F")
            {
                ItemVenda itemVenda = new ItemVenda();

                itensVenda.ForEach(item =>
                {
                    new Produto().Atualizar(item.Produto, venda.DataVenda.ToString("dd/MM/yyyy"));
                });

                itemVenda.Cadastrar(itensVenda);

                venda.Cadastrar();
                
                cliente.UltimaVenda = venda.DataVenda;

                new Write().EditarCliente(cliente);

                Console.WriteLine("\n\nVenda cadastrada com sucesso!\nPressione ENTER para voltar ao Menu Vendas...");

                Console.ReadKey();
            }
        }

        public static void LocalizarVenda()
        {
            Console.Clear();

            Venda venda = new Venda();
            ItemVenda itemVenda = new ItemVenda();

            Console.WriteLine("Informe a venda que deseja buscar: ");
            int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine();

            venda = venda.Localizar(id);

            if (venda != null)
            {
                List<ItemVenda> itens = itemVenda.Localizar(venda.Id);

                Console.Write($"\nVenda Nº {venda.Id.ToString().PadLeft(5, '0')}\tData: {venda.DataVenda.ToString("dd/MM/yyyy")}");
                Console.WriteLine("\n\n");

                Console.WriteLine("Id\tProduto\t\tQtd\tV.Unitário\tT.Item");
                Console.WriteLine("------------------------------------------------------");
                itens.ForEach(item => Console.WriteLine(item.ToString()));
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine($"\t\t\t\t\t\t{venda.ValorTotal.ToString("#.00")}");

                Console.WriteLine("\nPressione ENTER para voltar ao menu...\n");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("venda não registrada!\nPressione ENTER para voltar ao menu...");
                Console.ReadLine();
            }
        }

    }
}