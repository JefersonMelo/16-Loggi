namespace CodigoDeBarras
{
    class TipoProduto
    {
        private readonly string joias = "000";
        private readonly string livros = "111";
        private readonly string eletronicos = "333";
        private readonly string bebidas = "555";
        private readonly string brinquedos = "888";

        public string tipoProduto(string produto)
        {
            if(produto == joias)
            {
                produto = "Jóias";
            }
            else if(produto == livros)
            {
                produto = "Livros";
            }
            else if(produto == eletronicos)
            {
                produto = "Eletrônicos";
            }
            else if(produto == bebidas)
            {
                produto = "Bebidas";
            }
            else if(produto == brinquedos)
            {
                produto = "Brinquedos";
            }
            else
            {
                produto = "ND";
            }

            return produto;
        }
    }
}
