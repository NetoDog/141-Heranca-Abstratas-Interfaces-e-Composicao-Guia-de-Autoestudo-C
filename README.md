# Pedidos de Venda (Nacional vs Internacional)

Projeto em C# que demonstra o uso de **herança controlada** (Template Method) e **composição via delegates**.

## Execução
```bash
dotnet test
```

## Estrutura
- `Pedido` define o ritual fixo: **Validar → CalcularTotal → EmitirRecibo**
- `PedidoNacional` e `PedidoInternacional` especializam o comportamento com `override`
- `Frete` e `Promoção` são *delegates* injetáveis, demonstrando **composição**
- Testes xUnit validam o **LSP** e a **troca de peças**
