namespace CodigoDeBarras
{
	class TipoProduto
	{
		const string joias = "000";
		const string livros = "111";
		const string eletronicos = "333";
		const string bebidas = "555";
		const string brinquedos = "888";

		public string tipoProduto(string produto)
		{	
			switch(produto)
			{
				case joias:
					produto = "Jóias";
					break;
				case livros:
					produto = "Livros";
					break;
				 case eletronicos:
					produto = "Eletrônicos";
					break;
				 case bebidas:
					produto = "Bebidas";
					break;
				 case brinquedos:
					produto = "Brinquedos";
					break;
				default:
					produto = "ND";
					break;
			}
			return produto;
		}
	}
}
