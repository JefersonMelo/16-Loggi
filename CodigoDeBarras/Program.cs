using CodigoDeBarras.Util;
using CodigoDeBarras.RegraNegocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodigoDeBarras
{
	class Program
	{
		static FormatarCodBarras fcb = new FormatarCodBarras();
		static RestricoesDeEnvio envio = new RestricoesDeEnvio();
		static Menu menu = new Menu();

		[STAThread]
		static void Main(string[] args)
		{
			List<Pedido> listPedido = new List<Pedido>();
			Console.Write("Entre Com O Camino Do Arquivo De Texto A Ser Lido: ");
			string caminhoDados = Console.ReadLine();
			Console.Write("Entre Com O Nome Do Arquivo: ");
			string arquivoInicial = "pacotes";// Console.ReadLine(); //Remover comentário para entrada do usuário.

			int cont = 0;
			string caminhoSalvarRelatorio = "";
			string opcao;
			bool parar = false;

			do
			{
				if(string.IsNullOrEmpty(caminhoDados))
				{
					Console.WriteLine("Entre Com Um Local Válido\n" +
						"***** Programa Encerrado *****");
					break;
				}
				//Prepara formatando a visualização para o usuário.
				//Arquivo de origem não é editado.
				fcb.formatarArquivo(caminhoDados, arquivoInicial, listPedido);

				Console.WriteLine(menu.ImprimirMenu());
				Console.Write("Opção: ");

				//Leitura da opção.
				opcao = Console.ReadLine();
				Console.WriteLine();

				//0 é a opção do encerramento do programa.
				while(!string.IsNullOrEmpty(opcao))
				{
					switch(opcao)
					{
						case "1":
							//Visializar arquivo.
							//Formata o arquivo e mostra para o usuário.
							//Arquivo de origem não é editado.
							//Arquivo .txt não é gerado.
							visualizarArquivo(listPedido);
							opcao = "";
							break;

						case "2":
							//Gera uma visialização de código de barras válidos.
							//Mostra para o usuário, no console, apenas pacotes válidos.
							//Arquivo de origem não é editado.
							//Arquivo .txt não é gerado.
							listarCodigoBarrasValido(listPedido);
							opcao = "";
							break;

						case "3":
							//Gera uma visialização de código de barras inválidos.
							//Mostra para o usuário, no console, apenas pacotes invalidos.
							//Arquivo de origem não é editado.
							//Arquivo .txt não é gerado.
							listarCodigoBarrasInvalido(listPedido);
							opcao = "";
							break;

						case "4":
							//Gera uma visialização de código de barras ordenados por região e produto.
							//Mostra para o usuário, no console, apenas pacotes válidos.
							//Arquivo de origem não é editado.
							//Arquivo .txt não é gerado.
							listarOrdenadoPorDestino(listPedido);
							opcao = "";
							break;

						case "5":
							//Gera uma visialização dos vendedores.
							//Contagem das vendas de cada um.
							//Mostra para o usuário, no console, apenas pacotes válidos.
							//Arquivo de origem não é editado.
							//Arquivo .txt não é gerado.
							Console.WriteLine(vendasVendedores(listPedido));
							opcao = "";
							break;

						case "6":
							//Gera uma visialização de uma ogirem de envio.
							//Filtro por origem do pacote e produto.
							//Se Condição Satisfeita, retrona o pacote.
							//Se não, retorna pacote não localizado.
							//Arquivo de origem não é editado.
							//Arquivo .txt não é gerado.
							string origem, produto;
							Console.Write("Entre Com A Região De Origem: ");
							origem = Console.ReadLine();
							Console.Write("\nEntre Com O Produto: ");
							produto = Console.ReadLine();
							Console.WriteLine("\n");
							pesquisarPorOrigemProduto(origem, produto, listPedido);
							opcao = "";
							break;

						case "7":
							//Gera relatório.
							//Entrada do caminho para salvar o arquivo é obrigatória.
							//Um único caminho é necessário.
							//É obrigatório o título do relatório.
							//Arquivo de origem não é editado.
							//Arquivo .txt é gerado com o título fornecido.
							if(cont < 1)
							{
								Console.Write("Entre Com O Camino Onde Salvar O Relatório Gerado: ");
								caminhoSalvarRelatorio = Console.ReadLine();
							}
							cont++;

							Console.Write("Entre Com O Nome Do Relatório A Ser Gerado: ");
							string nomeRelatorio = Console.ReadLine();
							gerarRelatorio(caminhoSalvarRelatorio, nomeRelatorio, listPedido);
							opcao = "";
							break;

						case "8":
							//Limpeza do console
							Console.Clear();
							opcao = "";
							break;

						case "0":
							Console.WriteLine("***** Fim *****");
							opcao = "";
							parar = true;
							break;
					}

				}
				
			} while(parar != true);


		}//Fim: Main

		//Menu: 1 - Para Ler O Arquivo Completo
		private static void visualizarArquivo(List<Pedido> listPedido)
		{
			Console.WriteLine("***** Início Do Arquivo *****\n");

			foreach(var item in listPedido)
			{
				Console.WriteLine(item);
			}
			listPedido.Clear();
			Console.WriteLine("***** Leitura De Arquivo Conclída *****\n");
		}

		//Menu: 2 - Listar Código de Barras Válidos
		private static void listarCodigoBarrasValido(List<Pedido> listPedido)
		{
			Console.WriteLine("***** Início Código de Barras Válidos*****\n");

			envio.restricoes(listPedido);

			foreach(var item in listPedido)
			{
				Console.WriteLine(item);
			}
			listPedido.Clear();
			Console.WriteLine("***** Pesquisa Por Código de Barras Válido Concluída *****\n");
		}

		//Menu: 3 - Listar Código de Barras Inválidos
		private static void listarCodigoBarrasInvalido(List<Pedido> listPedido)
		{
			Console.WriteLine("***** Início Código de Barras Inválidos *****\n");

			List<Pedido> codInvalido = listPedido;
			codInvalido.RemoveAll(delegate (Pedido p)
			{
				return p.CodVendedor != "Inativo"
				&& p.Produto != "ND"
				&& p.Origem != "ND"
				&& p.Destino != "ND"
				&& !(p.Origem == "Centro-oeste" && p.Produto == "Jóias");
			});

			foreach(var item in codInvalido)
			{
				Console.WriteLine(item);
			}
			listPedido.Clear();
			Console.WriteLine("***** Pesquisa Por Código de Barras Inválido Concluída *****\n");
		}

		//Menu: 4 - Listar Agrupado Por Região
		private static void listarOrdenadoPorDestino(List<Pedido> listPedido)
		{
			Console.WriteLine("***** Início Agrupar Por Região *****\n");

			envio.restricoes(listPedido);

			IEnumerable<Pedido> ordemDestino = from pedido in listPedido
											   orderby pedido.Destino
											   select pedido;


			foreach(var item in ordemDestino)
			{
				Console.WriteLine(item);
			}
			listPedido.Clear();
			Console.WriteLine("***** Pesquisa Agrupar Por Região Concluída *****\n");

		}

		//Menu: 5 - Listar Vendas + Vendedores
		private static string vendasVendedores(List<Pedido> listPedido)
		{
			Console.WriteLine("***** Início Quantidade de Vendas Por Vendedor *****\n");

			envio.restricoes(listPedido);

			int v123 = 0, v124 = 0, v874 = 0, v845 = 0;

			foreach(var item in listPedido)
			{
				if(item.CodVendedor == "123") { v123++; }
				if(item.CodVendedor == "124") { v124++; }
				if(item.CodVendedor == "845") { v845++; }
				if(item.CodVendedor == "874")
				{
					v874++;
				}
			}
			listPedido.Clear();
			return $"Vendedor: 123, Vendas: {v123}\n" +
				   $"Vendedor: 124, Vendas: {v124}\n" +
				   $"Vendedor: 845, Vendas: {v845}\n" +
				   $"Vendedor: 874, Vendas: {v874}\n\n" +
				   $"***** Contar Vendas Por Vendedor Concluída *****\n";

		}

		//Menu: 6 - Pesquisar Por Origem e Produto
		private static void pesquisarPorOrigemProduto(string origem, string produto, List<Pedido> listPedido)
		{
			Console.WriteLine("***** Início Pesquisar Origem do Produto *****\n");

			foreach(var item in listPedido)
			{
				if(item.Origem == origem && item.Produto == produto)
				{
					Console.WriteLine("***** Produto Localizado! :D *****\n");
					Console.WriteLine($"\n{item}");
					break;
				}
			}
			listPedido.Clear();
			Console.WriteLine("***** Produto Não Localizado :( *****\n");
		}

		//Menu: 7 - Gerar Relatório
		private static void gerarRelatorio(string caminhoSalvarRelatorio, string nomeRelatorio, List<Pedido> listPedido)
		{
			Console.WriteLine("***** Início Gerar Relatório *****\n");

			envio.restricoes(listPedido);

			foreach(var item in listPedido)
			{
				if(item.Destino == nomeRelatorio)
				{
					listPedido.RemoveAll(delegate (Pedido p) { return p.Destino != nomeRelatorio; });
					break;
				}
				if(item.Produto == nomeRelatorio)
				{
					listPedido.RemoveAll(delegate (Pedido p) { return p.Produto != nomeRelatorio; });
					break;
				}
			}


			try
			{
				using(StreamWriter sw = new StreamWriter(Path.Combine($"{caminhoSalvarRelatorio}\\relatorio-{nomeRelatorio}.txt")))
				{
					sw.WriteLine($"***** Início Do Relatório {nomeRelatorio}*****\n");
					foreach(var item in listPedido)
					{
						sw.WriteLine(item);
					}
					sw.WriteLine($"***** Fim Do Relatório {nomeRelatorio}*****\n");
					listPedido.Clear();
					sw.Close();
				}
			}
			catch(Exception e)
			{
				Console.WriteLine("Exception: " + e.Message);
			}
			finally
			{
				Console.WriteLine("***** Geração Do Arquivo Concluída *****\n");
			}
		}

	}
}