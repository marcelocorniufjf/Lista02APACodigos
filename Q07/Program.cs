using System;
using System.Collections.Generic;

public class RestaUm
{
    // Função para determinar o vencedor dado o estado atual do jogo
    public

 string Jogo(int moedas, bool vezDoA)
    {
        if (moedas == 1)
        {
            return vezDoA ? "B" : "A";
        }

        for (int move = 1; move <= 3; move++)
        {
            if (moedas - move > 0)
            {
                string vencedor = Jogo(moedas - move, !vezDoA);
                if ((vezDoA && vencedor == "A") || (!vezDoA && vencedor == "B"))
                {
                    return vezDoA ? "A" : "B";
                }
            }
        }

        return vezDoA ? "B" : "A";
    }

    // Função para determinar a jogada ótima para o jogador A
    public int JogadaOtimais(int moedas)
    {
        for (int move = 1; move <= 3; move++)
        {
            if (moedas - move > 0 && Jogo(moedas - move, false) == "A")
            {
                return move;
            }
        }

        return -1; // Sem jogada ótima
    }
}

public class Program
{
    public static void Main()
    {
        var jogo = new RestaUm();
        int moedas = 10;
        Console.WriteLine($"Vencedor: {jogo.Jogo(moedas, true)}");
        Console.WriteLine($"Jogada ótima para A: {jogo.JogadaOtimais(moedas)}");
        Console.ReadLine();
    }
}