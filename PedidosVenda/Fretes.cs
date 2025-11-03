using System;

namespace PedidosVenda
{
    public static class Fretes
    {
        public static Func<decimal, decimal> Fixo(decimal valor) => total => total + valor;
        public static Func<decimal, decimal> Percentual(decimal percentual) => total => total * (1 + percentual);
    }
}
