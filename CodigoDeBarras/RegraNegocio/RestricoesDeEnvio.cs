using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodigoDeBarras.RegraNegocio
{
	class RestricoesDeEnvio
	{

		/*
		 ***** Restrições *****
		
		1) A Loggi não envia produtos que não sejam dos tipos acima
		informados.
		2) Não é possível despachar pacotes contendo jóias tendo como
		região de origem o Centro-oeste;
		3) O vendedor 584 está com seu CNPJ inativo e, portanto, não pode
		mais enviar pacotes pela Loggi, os códigos de barra que
		estiverem relacionados a este vendedor devem ser considerados
		inválidos.
		*/
		public void restricoes(List<Pedido> listPedido)
		{
			List<Pedido> removerVendedor = listPedido;
			List<Pedido> removerProduto = listPedido;
			List<Pedido> removerDestinoInvalido = listPedido;
			List<Pedido> removerOrigemInvalida = listPedido;
			List<Pedido> removerJoiaCentroOeste = listPedido;

			removerVendedor.RemoveAll(delegate (Pedido p) { return p.CodVendedor == "Inativo"; });
			removerProduto.RemoveAll(delegate (Pedido tp) { return tp.Produto == "ND"; });
			removerDestinoInvalido.RemoveAll(delegate (Pedido rdi) { return rdi.Destino == "ND"; });
			removerOrigemInvalida.RemoveAll(delegate (Pedido roi) { return roi.Origem == "ND"; });
			removerJoiaCentroOeste.RemoveAll(delegate (Pedido rjco) { return rjco.Origem == "Centro-oeste" && rjco.Produto == "Jóias"; });
		}
	}
}
