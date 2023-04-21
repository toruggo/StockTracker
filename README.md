# StockTracker
Aplicativo de console para monitorar o preço de uma ação em tempo real e avisar por email caso a cotação de um ativo da B3 caia mais do que certo nível, ou suba acima de outro.

## Como usar

### Configuração
As configurações do servidor SMTP para enviar emails estão no arquivo "EmailConfig.json", que deve ser editado com as informações corretas.

### Inicialização
Ele deve ser chamado via linha de comando com 3 parâmetros.
  1. O ativo a ser monitorado
  2. O preço de referência para venda
  3. O preço de referência para compra

Acesse o diretório "StockTracker/StockTracker/bin/Debug/net6.0/"

Utilize o comando "start StockTracker.exe" seguido pelos parâmetros.
Ex:
  > start StockTracker.exe PETR4 26.70 26.62
 
 
