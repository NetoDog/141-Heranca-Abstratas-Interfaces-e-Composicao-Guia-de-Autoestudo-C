using System;

namespace PedidosVenda
{
    public static class Promocoes
    {
        public static Func<decimal, decimal> Nenhuma() => total => total;
        public static Func<decimal, decimal> CupomPercentual(decimal percentual) => total => total * (1 - percentual);
    }
}
