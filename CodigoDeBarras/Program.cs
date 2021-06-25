using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodigoDeBarras
{
    class Program
    {

        public static TipoProduto tipoProduto = new TipoProduto();
        static Regiao regiao = new Regiao();
        static Vendedor vendedor = new Vendedor();

        [STAThread]
        static void Main(string[] args)
        {
            List<Pedido> listPedido = new List<Pedido>();
            string line;

            //Console.Write("Rota: ");
            string rota = Console.ReadLine();
            var replacement = rota.Replace("\\", "\\\\\"");
            replacement = replacement.Replace("\"", "");

            //Console.Write("Caminho: ");
            string caminho = Console.ReadLine();
            string relatorio = "relatorio";


            try
            {
                StreamReader sr = new StreamReader($"{replacement}");
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

                    //Remover Comentário para ver no console
                    //Console.WriteLine(line);

                    line = sr.ReadLine();

                }
                sr.Close();

            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Fim da Leitura do Arquivo\n\n");
            }

            gerarRelatorio(caminho, relatorio, listPedido);
        }

        private static void gerarRelatorio(string caminho, string relatorio, List<Pedido> listPedido)
        {
            List<Pedido> removerVendedor = listPedido;
            List<Pedido> removerProduto = listPedido;
            List<Pedido> removerJoiaCentroOeste = listPedido;

            removerVendedor.RemoveAll(delegate(Pedido p) { return p.CodVendedor == "Inativo"; });
            removerProduto.RemoveAll(delegate(Pedido tp) { return tp.Produto == "ND"; });
            removerJoiaCentroOeste.RemoveAll(delegate(Pedido rjco) { return rjco.Origem == "Centro-oeste" && rjco.Produto == "Jóias"; });

            try
            {
                using(StreamWriter sw = new StreamWriter(Path.Combine($"{caminho}\\{relatorio}.txt")))
                {
                    foreach(var item in listPedido)
                    {
                        sw.WriteLine(item);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
