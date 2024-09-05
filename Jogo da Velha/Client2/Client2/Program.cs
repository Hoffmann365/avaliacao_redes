using System;
using System.Net.Sockets;
using System.Text;

class TicTacToeClient
{
    static void Main(string[] args)
    {
        TcpClient client = null;
        NetworkStream stream = null;
        string server = "127.0.0.1";
        int port = 13000;
        string startMsg = string.Empty;
        string playerSymbol = "O";
        string response = string.Empty;
        bool gameStarted = false;
        bool gameEnded = false;
        int found = 0;
        int bytesRead = 0;
        bool ignore = false;
        string board = string.Empty;
        
        try
        {
            client = new TcpClient(server, port); // Conectando ao servidor local
            stream = client.GetStream();

            Console.WriteLine("Conectado ao servidor do Jogo da Velha!");

            byte[] buffer = new byte[512];

            while (!gameStarted)
            {
                // Ler resposta inicial do servidor (esperando outro jogador ou início do jogo)
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                startMsg = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

                if (startMsg == "1" && !ignore)
                {
                    Console.WriteLine("Aguardando outro jogador...");
                    playerSymbol = "X";
                    ignore = true;
                }
                else if (startMsg == "0")
                {
                    Console.WriteLine("A partida vai começar!");
                    gameStarted = true;
                }
            }
            
            
            
            Console.WriteLine(playerSymbol);

            // Loop do jogo
            while (!gameEnded)
            {
                // Recebe o tabuleiro atual do servidor
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                board = response;
                
                // Verificar se houve vitória ou empate
                if (response.EndsWith("1"))
                {
                    if (playerSymbol == "O")
                    {
                        Console.WriteLine("Você venceu!");
                        
                    }
                    else
                    {
                        Console.WriteLine("Oponente venceu!");
                    }
                    gameEnded = true;
                }
                else if (response.EndsWith("2"))
                {
                    if (playerSymbol == "X")
                    {
                        Console.WriteLine("Você venceu!");
                        
                    }
                    else
                    {
                        Console.WriteLine("Oponente venceu!");
                    }
                    gameEnded = true;
                }
                else if (response.EndsWith("3"))
                {
                    Console.WriteLine("Empate!");
                    gameEnded = true;
                }
                
                // Receber a solicitação de jogada (X ou O)
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                response = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                
                Console.WriteLine(response);

                if (playerSymbol == "X")
                {
                    if (response == "X")
                    {
                        // Exibe o tabuleiro
                        Console.WriteLine(board);
                        ExibirTabuleiro(board);
                        Console.WriteLine("Sua vez! Insira a posição (1-9):");
                    }
                    else
                    {
                        Console.WriteLine("Vez do Oponente");
                        continue;
                    }
                }
                if (playerSymbol == "O")
                {
                    if (response == "O")
                    {
                        // Exibe o tabuleiro
                        Console.WriteLine(board);
                        ExibirTabuleiro(board);
                        Console.WriteLine("Sua vez! Insira a posição (1-9):");
                    }
                    else
                    {
                        Console.WriteLine("Vez do Oponente");
                        continue;
                    }
                }

                while (true)
                {
                    string jogada = Console.ReadLine();
                    byte[] msg = Encoding.UTF8.GetBytes(jogada);
                    stream.Write(msg, 0, msg.Length);

                    // Receber a confirmação da jogada
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    response = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

                    if (response == "-1")
                    {
                        Console.WriteLine("Jogada inválida, tente novamente.");
                    }
                    else
                    {
                        Console.WriteLine("Jogada registrada com sucesso!");
                        break;
                    }
                }
                
                
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
        finally
        {
            stream?.Close();
            client?.Close();
        }

        Console.WriteLine("Conexão encerrada.");
    }

    static void ExibirTabuleiro(string board)
    {
        Console.WriteLine("Tabuleiro Atual:");
        Console.WriteLine($"{board[0]} | {board[1]} | {board[2]}");
        Console.WriteLine("--+---+--");
        Console.WriteLine($"{board[3]} | {board[4]} | {board[5]}");
        Console.WriteLine("--+---+--");
        Console.WriteLine($"{board[6]} | {board[7]} | {board[8]}");
    }
}
