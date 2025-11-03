# Documento de Design – Pedidos de Venda (Nacional vs Internacional)

## Fase 1 – Conceituação (Sem Código)

O processamento de pedidos segue um ritual fixo composto por três etapas:
1. **Validar** – Garante que o pedido contenha dados mínimos e itens válidos.
2. **Calcular Total** – Soma o subtotal do pedido, aplicando políticas adicionais (frete, promoção, etc.).
3. **Emitir Recibo** – Gera o documento de confirmação com base no total calculado.

### Variações
- **Pedido Nacional:** aplica impostos internos e gera recibo fiscal no formato NF-e.
- **Pedido Internacional:** aplica taxas de importação, câmbio e custos aduaneiros, emitindo um *Commercial Invoice*.

### Justificativa
- **Herança:** usada para especializar o ritual fixo de processamento, mantendo o método `Processar()` na base e sobrescrevendo apenas os ganchos que variam.
- **Composição:** utilizada para aplicar políticas independentes (frete, promoção, seguro, etc.), evitando a criação de subclasses combinatórias e mantendo baixo acoplamento.

---

## Fase 2 – Design Orientado a Objetos (Sem Código)

### Contrato da Classe Base
```csharp
public class Pedido
{
    public string Processar()
    {
        Validar();
        var total = CalcularTotal();
        return EmitirRecibo(total);
    }

    protected virtual void Validar() { }
    protected virtual decimal CalcularSubtotal() => 100m;
    protected virtual string EmitirRecibo(decimal total) => $"Recibo: {total:C}";
}
```

### Regras do Princípio da Substituição de Liskov (LSP)

1. **Substituibilidade:** Qualquer cliente que usa `Pedido` deve funcionar igualmente com `PedidoNacional` ou `PedidoInternacional`, sem `is` ou *downcast*.
2. **Invariantes preservados:** As validações mínimas da base não podem ser enfraquecidas nas subclasses.
3. **Contratos de saída equivalentes:** O método `Processar()` deve sempre retornar um recibo coerente com o total calculado.

### Eixos Plugáveis (por Composição)

| Eixo       | Delegate (assinatura)      | Papel |
|-------------|----------------------------|--------|
| **Frete**   | `Func<decimal, decimal>`   | Aplica o custo de frete sobre o valor atual. |
| **Promoção**| `Func<decimal, decimal>`   | Aplica desconto/cupom sobre o valor atual. |

Esses delegates são injetados no construtor e permitem trocar políticas em tempo de execução, sem alterar subclasses.

---

## Resumo do Design
A base `Pedido` define o ritual fixo (**Template Method Pattern**). As classes `PedidoNacional` e `PedidoInternacional` sobrescrevem apenas os pontos de variação. Os delegates implementam composição, permitindo políticas independentes e trocáveis.
