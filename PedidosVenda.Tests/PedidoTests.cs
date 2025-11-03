using Xunit;

namespace PedidosVenda.Tests
{
    public class PedidoTests
    {
        private string ProcessarPedido(PedidosVenda.Pedido pedido) => pedido.Processar();

        [Fact]
        public void Deve_Respeitar_LSP_Com_Subtipos()
        {
            var nacional = new PedidosVenda.PedidoNacional();
            var internacional = new PedidosVenda.PedidoInternacional();

            var reciboNacional = ProcessarPedido(nacional);
            var reciboInternacional = ProcessarPedido(internacional);

            Assert.Contains("NF-e", reciboNacional);
            Assert.Contains("Invoice", reciboInternacional);
        }

        [Fact]
        public void Deve_Trocar_Politicas_De_Composicao()
        {
            var pedido = new PedidosVenda.Pedido(
                Fretes.Percentual(0.1m),
                Promocoes.CupomPercentual(0.2m)
            );

            var recibo1 = pedido.Processar();

            var pedidoAlterado = new PedidosVenda.Pedido(
                Fretes.Fixo(50m),
                Promocoes.Nenhuma()
            );

            var recibo2 = pedidoAlterado.Processar();

            Assert.NotEqual(recibo1, recibo2);
        }
    }
}
