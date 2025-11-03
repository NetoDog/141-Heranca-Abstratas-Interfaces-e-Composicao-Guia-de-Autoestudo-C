using System;

namespace PedidosVenda
{
    public sealed class PedidoInternacional : Pedido
    {
        public PedidoInternacional(Func<decimal, decimal>? frete = null, Func<decimal, decimal>? promocao = null)
            : base(frete, promocao) { }

        protected override void Validar() => Console.WriteLine("Validando pedido internacional...");

        protected override decimal CalcularSubtotal() => 200m;

        protected override string EmitirRecibo(decimal total) => $"Commercial Invoice: Total USD {total:C}";
    }
}
