using System;
using System.Collections.Generic;

public class Grafo
{
    public Dictionary<string, Dictionary<string, int>> Vertices { get; set; }

    public Grafo()
    {
        Vertices = new Dictionary<string, Dictionary<string, int>>();
    }

    public void AdicionarVertice(string nome, Dictionary<string, int> vizinhos)
    {
        Vertices[nome] = vizinhos;
    }

    // Algoritmo DFS para calcular a folga livre e a folga garantida
    private int DFS(string vertice, Dictionary<string, int> finishTime, int tempoAtual)
    {
        if (finishTime.ContainsKey(vertice))
        {
            return finishTime[vertice];
        }

        int maxTempo = tempoAtual;
        foreach (var vizinho in Vertices[vertice])
        {
            maxTempo = Math.Max(maxTempo, DFS(vizinho.Key, finishTime, tempoAtual + vizinho.Value));
        }

        finishTime[vertice] = maxTempo;
        return maxTempo;
    }

    public Dictionary<string, int> CalcularFolgas(string inicio)
    {
        var finishTime = new Dictionary<string, int>();
        foreach (var vertice in Vertices.Keys)
        {
            if (!finishTime.ContainsKey(vertice))
            {
                DFS(vertice, finishTime, 0);
            }
        }
        return finishTime;
    }
}

public class Program
{
    public static void Main()
    {
        var grafo = new Grafo();
        grafo.AdicionarVertice("A", new Dictionary<string, int> { { "B", 5 }, { "C", 10 } });
        grafo.AdicionarVertice("B", new Dictionary<string, int> { { "D", 3 } });
        grafo.AdicionarVertice("C", new Dictionary<string, int> { { "D", 2 } });
        grafo.AdicionarVertice("D", new Dictionary<string, int>());

        var folgas = grafo.CalcularFolgas("A");
        foreach (var folga in folgas)
        {
            Console.WriteLine($"Vértice: {folga.Key}, Folga: {folga.Value}");
        }
        Console.ReadLine();
    }
}