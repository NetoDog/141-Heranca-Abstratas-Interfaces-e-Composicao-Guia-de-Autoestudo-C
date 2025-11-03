using System;

namespace PedidosVenda
{
    public sealed class PedidoNacional : Pedido
    {
        public PedidoNacional(Func<decimal, decimal>? frete = null, Func<decimal, decimal>? promocao = null)
            : base(frete, promocao) { }

        protected override void Validar() => Console.WriteLine("Validando pedido nacional...");

        protected override decimal CalcularSubtotal() => 120m;

        protected override string EmitirRecibo(decimal total) => $"NF-e: Total com impostos = {total:C}";
    }
}
