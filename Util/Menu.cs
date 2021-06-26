namespace CodigoDeBarras.Util
{
	class Menu
	{
		public string ImprimirMenu( )
		{
			return "\n\n***** Escolha Uma Opção *****\n\n" +
					"1 - Para Ler O Arquivo Completo\n" +
					"2 - Listar Código de Barras Válidos\n" +
					"3 - Listar Código de Barras Inválidos\n" +
					"4 - Listar Agrupado Por Região\n" +
					"5 - Listar Vendas + Vendedores\n" +
					"6 - Pesquisar Por Origem e Produto\n" +
					"7 - Gerar Relatório\n" +
					"8 - Limpar Console\n" +
					"0 - Para Sair\n";
		}
	}
}
