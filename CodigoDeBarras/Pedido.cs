namespace CodigoDeBarras
{
    class Pedido
    {
        public string Pacote { get; set; }
        public string CodBarras { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public string CodLoggi { get; set; }
        public string CodVendedor { get; set; }
        public string Produto { get; set; }

        public override string ToString( )
        {
            return $"{Pacote}\n" +
                   $"Código: {CodBarras}\n" +
                   $"Região de origem: {Origem}\n" +
                   $"Região de destino: {Destino}\n" +
                   $"Código Loggi: {CodLoggi}\n" +
                   $"Código do vendedor do produto: {CodVendedor}\n" +
                   $"Tipo do produto: {Produto}\n\n";
        }
    }
}
