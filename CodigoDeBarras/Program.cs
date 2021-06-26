using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodigoDeBarras
{
	class Program
	{

		static TipoProduto tipoProduto = new TipoProduto();
		static Regiao regiao = new Regiao();
		static Vendedor vendedor = new Vendedor();

		[STAThread]
		static void Main(string[] args)
		{
			List<Pedido> listPedido = new List<Pedido>();
			Console.Write("Entre Com O Camino Do Arquivo De Texto A Ser Lido: ");
			string rota = Console.ReadLine();
			Console.Write("Entre Com O Nome Do Arquivo: ");
			string arquivoInicial = Console.ReadLine();
			var caminhoDados = rota.Replace("\\", "\\\\\"");
			caminhoDados = caminhoDados.Replace("\"", "");
			int cont = 0;
			string caminhoSalvarRelatorio = "";
			int opcao;

			do
			{
				formatarArquivo(caminhoDados, arquivoInicial, listPedido);

				Console.Write("\n***** Escolha Uma Opção *****\n\n" +
						   "1 - Para Ler O Arquivo Completo\n" +
						   "2 - Listar Código de Barras Válidos\n" +
						   "3 - Listar Código de Barras Inválidos\n" +
						   "4 - Listar Agrupado Por Região\n" +
						   "5 - Listar Vendas + Vendedores\n" +
						   "6 - Pesquisar Por Origem e Produto\n" +
						   "7 - Gerar Relatório\n" +
						   "8 - Limpar Console\n" +
						   "0 - Para Sair\n\n" +
						   "Opção: ");

				opcao = int.Parse(Console.ReadLine());
				Console.WriteLine();


				if(opcao == 1)
				{
					visualizarArquivo(listPedido);
				}
				if(opcao == 2)
				{
					codigoBarrasValido(listPedido);
				}
				if(opcao == 3)
				{
					codigoBarrasInvalido(listPedido);
				}
				if(opcao == 4)
				{
					listarAgrupadoPorRegiao(listPedido);
				}
				if(opcao == 5)
				{
					Console.WriteLine(vendasVendedores(listPedido));
				}
				if(opcao == 6)
				{
					string origem, produto;
					Console.Write("Entre Com A Região De Origem: ");
					origem = Console.ReadLine();
					Console.Write("\nEntre Com O Produto: ");
					produto = Console.ReadLine();
					Console.WriteLine("\n");
					pesquisarPorOrigemProduto(origem, produto, listPedido);
				}
				if(opcao == 7)
				{
					if(cont < 1)
					{
						Console.Write("Entre Com O Camino Onde Salvar O Relatório Gerado: ");
						caminhoSalvarRelatorio = Console.ReadLine();
					}
					cont++;

					Console.Write("Entre Com O Nome Do Relatório A Ser Gerado: ");
					string nomeRelatorio = Console.ReadLine();
					gerarRelatorio(caminhoSalvarRelatorio, nomeRelatorio, listPedido);
				}
				if(opcao == 8)
				{
					Console.Clear();
				}

			} while(opcao != 0);


		}//Fim: Main

		private static void formatarArquivo(string caminhoDados, string arquivoInicial, List<Pedido> listPedido)
		{
			string line;

			try
			{
				StreamReader sr = new StreamReader($"{caminhoDados}\\{arquivoInicial}.txt");
				line = sr.ReadLine();

				while(line != null)
				{
					string[] divPacotCodBarras = line.Split(':');
					long codBarras = long.Parse(divPacotCodBarras[1]);
					string codFormat = String.Format(@"{0:000\ 000\ 000\ 000\ 000}", codBarras);
					string[] trincaCodBarras = codFormat.Split();

					listPedido.Add(new Pedido
					{
						Pacote = divPacotCodBarras[0],
						CodBarras = codFormat,
						Origem = regiao.regiao(trincaCodBarras[0]),
						Destino = regiao.regiao(trincaCodBarras[1]),
						CodLoggi = trincaCodBarras[2],
						CodVendedor = vendedor.statusVendedor(trincaCodBarras[3]),
						Produto = tipoProduto.tipoProduto(trincaCodBarras[4])
					});

					line = sr.ReadLine();

				}
				sr.Close();

			}
			catch(Exception e)
			{
				Console.WriteLine("Exception: " + e.Message);
			}
		}

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

		private static void codigoBarrasValido(List<Pedido> listPedido)
		{
			Console.WriteLine("***** Início Do Arquivo *****\n");

			List<Pedido> codValido = listPedido;
			codValido.RemoveAll(delegate (Pedido p)
			{
				return p.CodVendedor == "Inativo"
				|| p.Produto == "ND"
				|| p.Origem == "ND"
				|| p.Destino == "ND";
			});

			foreach(var item in codValido)
			{
				Console.WriteLine(item);
			}
			listPedido.Clear();
			Console.WriteLine("***** Pesquisa Por Código de Barras Válido Concluída *****\n");
		}

		private static void codigoBarrasInvalido(List<Pedido> listPedido)
		{
			Console.WriteLine("***** Início Do Arquivo *****\n");

			List<Pedido> codInvalido = listPedido;
			codInvalido.RemoveAll(delegate (Pedido p)
			{
				return p.CodVendedor != "Inativo"
				&& p.Produto != "ND"
				&& p.Origem != "ND"
				&& p.Destino != "ND";
			});

			foreach(var item in codInvalido)
			{
				Console.WriteLine(item);
			}
			listPedido.Clear();
			Console.WriteLine("***** Pesquisa Por Código de Barras Inválido Concluída *****\n");
		}

		private static void listarAgrupadoPorRegiao(List<Pedido> listPedido)
		{
			Console.WriteLine("***** Início Do Arquivo *****\n");

			restricoes(listPedido);

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

		private static string vendasVendedores(List<Pedido> listPedido)
		{
			Console.WriteLine("***** Início Do Arquivo *****\n");

			int v123 = 0, v124 = 0, v874 = 0, v845 = 0;

			foreach(var item in listPedido)
			{
				if(item.CodVendedor == "123")
				{
					v123++;
				}
				if(item.CodVendedor == "124")
				{
					v124++;
				}
				if(item.CodVendedor == "845")
				{
					v845++;
				}
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

		private static void restricoes(List<Pedido> listPedido)
		{
			List<Pedido> removerVendedor = listPedido;
			List<Pedido> removerProduto = listPedido;
			List<Pedido> removerJoiaCentroOeste = listPedido;
			List<Pedido> removerRegiaoInvalida = listPedido;

			removerVendedor.RemoveAll(delegate (Pedido p) { return p.CodVendedor == "Inativo"; });
			removerProduto.RemoveAll(delegate (Pedido tp) { return tp.Produto == "ND"; });
			removerJoiaCentroOeste.RemoveAll(delegate (Pedido rjco) { return rjco.Origem == "Centro-oeste" && rjco.Produto == "Jóias"; });
			removerRegiaoInvalida.RemoveAll(delegate (Pedido rri) { return rri.Origem == "ND" || rri.Destino == "ND"; });
		}

		private static void gerarRelatorio(string caminhoSalvarRelatorio, string nomeRelatorio, List<Pedido> listPedido)
		{
			Console.WriteLine("***** Início Do Arquivo *****\n");

			restricoes(listPedido);

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

		private static void pesquisarPorOrigemProduto(string origem, string produto, List<Pedido> listPedido)
		{
			Console.WriteLine("***** Início Do Arquivo *****\n");

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
	}
}
