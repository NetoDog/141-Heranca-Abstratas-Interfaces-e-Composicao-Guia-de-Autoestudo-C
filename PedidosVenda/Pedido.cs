using System;

namespace PedidosVenda
{
    public class Pedido
    {
        private readonly Func<decimal, decimal> _frete;
        private readonly Func<decimal, decimal> _promocao;

        public Pedido(Func<decimal, decimal>? frete = null, Func<decimal, decimal>? promocao = null)
        {
            _frete = frete ?? (v => v);
            _promocao = promocao ?? (v => v);
        }

        public string Processar()
        {
            Validar();
            var total = CalcularTotal();
            return EmitirRecibo(total);
        }

        protected virtual void Validar() { }

        protected virtual decimal CalcularSubtotal() => 100m;

        protected decimal CalcularTotal()
        {
            var subtotal = CalcularSubtotal();
            subtotal = _frete(subtotal);
            subtotal = _promocao(subtotal);
            return subtotal;
        }

        protected virtual string EmitirRecibo(decimal total) => $"Recibo: {total:C}";
    }
}
