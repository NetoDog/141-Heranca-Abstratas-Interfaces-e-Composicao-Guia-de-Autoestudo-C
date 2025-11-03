# Documento de Testes – Pedidos de Venda (Atividade 1)

## Objetivo
Descrever os testes automáticos e manuais que validam os requisitos da Atividade 1: garantir o ritual fixo (Processar), respeitar LSP (substituição) e demonstrar a flexibilidade da composição (troca de delegates de Frete e Promoção).

---

## Como executar os testes automáticos
1. Extraia o zip e abra o terminal na pasta onde está o arquivo `PedidosVenda.sln`.
2. Execute:
```bash
dotnet test
```
O comando compilará os projetos e executará os testes xUnit presentes em `PedidosVenda.Tests`.

---

## Testes Implementados (Automáticos - xUnit)

### 1) LSP: Deve_Respeitar_LSP_Com_Subtipos
**Objetivo:** Verificar que a função cliente que aceita `Pedido` e chama `Processar()` funciona corretamente com `PedidoNacional` e `PedidoInternacional` sem `is`/downcast.  
**Implementação:** Cria instâncias de `PedidoNacional` e `PedidoInternacional`, chama `Processar()` em cada uma e verifica partes esperadas do recibo (ex.: "NF-e" para nacional e "Invoice" para internacional).  
**Critério de sucesso:** Asserções que validam a presença dos termos esperados nos recibos.  

### 2) Composição: Deve_Trocar_Politicas_De_Composicao
**Objetivo:** Demonstrar que alterar os delegates de composição (frete e promoção) altera o resultado do cálculo sem criar subclasses novas.  
**Implementação:** Constrói dois objetos `Pedido` com diferentes combinações de `Fretes` e `Promocoes` (ex.: percentual + cupom vs. frete fixo + nenhuma) e compara os recibos/resultados.  
**Critério de sucesso:** Os recibos (strings) são diferentes, provando que a composição mudou o comportamento.  

---

## Testes Manuais Recomendados
1. **Validar saída numérica do cálculo:** Instanciar `PedidoNacional` com `Fretes.Fixo(50)` e `Promocoes.Nenhuma()` e confirmar o total esperado (subtotal 120 + 50 = 170). Executar pequena ferramenta console para Print do recibo.  
2. **Comparar formatos de recibo:** Confirmar manualmente que `PedidoNacional` emite string iniciando com `NF-e:` e `PedidoInternacional` com `Commercial Invoice:`.  
3. **Testar invariantes de validação:** Criar cenário em que a validação da base falharia se enfraquecida (por exemplo, se houvesse regra mínima de itens) — atualmente `Validar()` da base é vazio; caso você altere para exigir campos mínimos, crie testes que confirmem que subclasses não relaxam essa exigência.

---

## Mapping de Testes para Rubrica
- **LSP:** Teste `Deve_Respeitar_LSP_Com_Subtipos` → cobre 10 pts da rubrica de testes.  
- **Composição:** Teste `Deve_Trocar_Politicas_De_Composicao` → cobre os 10 pts restantes da rubrica de testes.  
- **Qualidade/Execução:** `dotnet test` verde e estrutura do projeto correta → 10 pts de qualidade de entrega (README, estrutura).

---

## Observações Técnicas
- Os testes assumem comportamento atual dos recibos (strings) tal como implementado nos métodos `EmitirRecibo` das classes folha. Se você alterar os textos dos recibos, atualize os asserts correspondentes nos testes.  
- Para validações numéricas mais robustas, recomenda-se adicionar testes que parseiem o valor numérico do recibo ou expor o total calculado via método interno/propósito de teste (evitar expor produção somente por testes — use `InternalsVisibleTo` ou APIs de teste específicas se necessário).

---

## Resultado Esperado
Ao executar `dotnet test` os testes devem passar (Status: Passed). Se algum teste falhar, o output do `dotnet test` mostrará a asserção que não foi satisfeita com stack trace para diagnóstico.

---

Se quiser, eu incluo também um pequeno utilitário console (Program.cs) que imprime exemplos de execução (ex.: instâncias com pares de políticas) para facilitar testes manuais e verificação de valores. Diga se quer que eu adicione agora.
