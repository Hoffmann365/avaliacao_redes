# Projeto Jogo da Velha
Nesse Projeto foi desenvolvido uma aplicação cliente compatível com o [Servidor](https://github.com/Hoffmann365/avaliacao_redes/blob/main/Jogo%20da%20Velha/TicTacToeServer/TicTacToeServer/Program.cs) apresentado para o Jogo da Velha.

### No Projeto consta:
- Relatório contendo:
  - Fluxograma para a aplicação cliente;
  - Descrição do código-fonte correspondente a cada bloco do diagrama;
- Repositório do GitHub com o código da aplicação desenvolvida.

### Devs do Projeto
- Davi Hoffmann
- Karla Juliane
- Maria Vitória

## Cliente
A Aplicação cliente a seguir [TicTacToeClient](https://github.com/Hoffmann365/avaliacao_redes/blob/main/Jogo%20da%20Velha/TicTacToeClient/TicTacToeClient/Program.cs) acessa o servidor do Jogo da velha e serve tanto para o Jogador 1, quanto para o Jogador 2.

### Características

- O Cliente se conecta ao Servidor com o IP "127.0.0.1" na porta 13000.
- Após o início da partida o cliente recebe e exibe o tabuleiro.
- O jogador na vez irá digitar a jogada e o Cliente envia para o servidor.
- Após cada jogada o Cliente recebe e exibe o tabuleiro atualizado com as jogadas anteriores.
-  Quando a condição de Vitória/Empate for atingida o Cliente manda uma mensagem informando quem ganhou, encerra a conexão com o servidor e encerra a aplicação.

### Fluxograma
O Fluxograma a seguir mostra o funcionamento do Cliente.


```mermaid
flowchart TD
    A[Iniciar Cliente] --> B[Conectar ao Servidor]
    B --> C{Conexão Estabelecida?}
    C -- Não --> D[Exibir Mensagem de Falha na Conexão]
    C -- Sim --> E{Já vai Iniciar a Partida?}
    E -- Sim --> G[Receber Tabuleiro e Status de Jogo]
    E -- Não --> F[Aguardar Outro Jogador]
    F --> G
    G --> H{Sua Vez?}
    H -- Não --> I[Aguardar Outro Jogador]
    I --> H
    H -- Sim --> J[Exibir Tabuleiro]
    J --> K[Solicitar Jogada ao Jogador]
    K --> L[Enviar Jogada ao Servidor]
    L --> S[Receber Confirmação da Jogada]
    S --> M{Jogada Válida?}
    M -- Não --> N[Exibir Mensagem de Jogada Inválida]
    N --> K
    M -- Sim --> O[Exibir Confirmação de Jogada]
    O --> P[Receber Tabuleiro e Status do Jogo Atualizado]
    P --> Q{Jogo Encerrado?}
    Q -- Não --> I
    Q -- Sim --> R[Exibir Mensagem do Servidor]
    R --> U[Encerrar Jogo]
    U --> V[Fechar Conexão]
```

### Descrição do Código Fonte

Bibliotecas Utilizadas no Projeto:
```c#
using System;
using System.Net.Sockets;
using System.Text;
```

1. Conexão com o Servidor:
```c#
//Define o IP e a Porta
string server = "127.0.0.1";
int port = 13000;

//Conecta ao Servidor
client = new TcpClient(server, port); 
stream = client.GetStream();

//Mensagem de Confirmação de Conexão
Console.WriteLine("Conectado ao servidor do Jogo da Velha!");
```
Nesse primeiro bloco de código, o Cliente define o IP e a porta do Servidor e realiza a conexão. Após se conectar o Cliente exibe uma mensagem de confirmação de conexão.