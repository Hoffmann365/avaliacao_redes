```mermaid
flowchart TD
    A["Iniciar Cliente"] --> B["Conectar ao Servidor"]
    B --> C{"Conexão Estabelecida?"}
    C -- Não --> D["Exibir Mensagem de Falha na conexão"]
    C -- Sim --> E["Receber Menu do Servidor"]
    E --> F["Escolher Opção"]
    F --> G["Enviar Opção ao Servidor"]
    G --> H{"Opção Enviada"}
    H -- 0: Sair --> I["Receber Mensagem de Encerramento"]
    H -- 1: Incrementar --> J["Receber Confirmação de Incremento"]
    H -- 2: Decrementar --> K["Receber Confirmação de Decremento"]
    H -- 3: Exibir Número --> L["Receber Valor do Número"]
    H -- Inválido --> M["Receber Mensagem de Opção Inválida"]
    I --> N["Fechar Conexão"]
    J --> E
    K --> E
    L --> E
    M --> E
    N --> O["Fim"]
```
```c#
try
        {
            //configura o servidor e a porta
            string server = "127.0.0.1";
            int port = 13000;

            //conecta ao servidor
            TcpClient client = new TcpClient(server, port);

            // Obtém o stream para ler e escrever dados
            NetworkStream stream = client.GetStream();

            // Buffer para armazenar os dados recebidos do servidor
            byte[] buffer = new byte[256];
            int bytesRead;

            // Recebe e exibe o menu inicial do servidor
            bytesRead = stream.Read(buffer, 0, buffer.Length);
            string serverMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine(serverMessage);

```
