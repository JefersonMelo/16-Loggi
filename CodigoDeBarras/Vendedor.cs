namespace CodigoDeBarras
{
    class Vendedor
    {
        private readonly string[] inativos = { "584" };

        public string statusVendedor(string codVendedor)
        {
            for(int i = 0; i < inativos.Length; i++)
            {
                if(inativos[i].Equals(codVendedor))
                {
                    codVendedor = "Inativo";
                }
            }

            return codVendedor;
        }
    }
}
