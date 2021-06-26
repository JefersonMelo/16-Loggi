using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodigoDeBarras.Util
{
	class FormatarCodBarras
	{
		static TipoProduto tipoProduto = new TipoProduto();
		static Regiao regiao = new Regiao();
		static Vendedor vendedor = new Vendedor();
		
		//Responssável por carregar o arquivo.
		//Formata visualização.
		//Arquivo de origem não é editado.
		public void formatarArquivo(string caminhoDados, string arquivoInicial, List<Pedido> listPedido)
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

	}
}
