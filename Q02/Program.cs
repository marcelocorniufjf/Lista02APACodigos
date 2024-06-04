using System;
using System.Collections.Generic;

public class Grafo
{
    public Dictionary<string, List<string>> Vertices { get; set; }

    public Grafo()
    {
        Vertices = new Dictionary<string, List<string>>();
    }

    public void AdicionarVertice(string nome, List<string> vizinhos)
    {
        Vertices[nome] = vizinhos;
    }

    // Algoritmo DFS para encontrar todos os caminhos maximais e elementares
    private void DFS(string verticeAtual, List<string> caminho, HashSet<string> visitados, List<List<string>> caminhos)
    {
        visitados.Add(verticeAtual);
        caminho.Add(verticeAtual);

        bool caminhoMaximal = true;
        foreach (var vizinho in Vertices[verticeAtual])
        {
            if (!visitados.Contains(vizinho))
            {
                DFS(vizinho, caminho, visitados, caminhos);
                caminhoMaximal = false;
            }
        }

        if (caminhoMaximal)
        {
            caminhos.Add(new List<string>(caminho));
        }

        caminho.RemoveAt(caminho.Count - 1);
        visitados.Remove(verticeAtual);
    }

    public List<List<string>> CaminhosMaximaisElementares(string inicio)
    {
        var caminhos = new List<List<string>>();
        var caminho = new List<string>();
        var visitados = new HashSet<string>();
        DFS(inicio, caminho, visitados, caminhos);
        return caminhos;
    }
}

public class Program
{
    public static void Main()
    {
        var grafo = new Grafo();
        grafo.AdicionarVertice("A", new List<string> { "B", "C" });
        grafo.AdicionarVertice("B", new List<string> { "D" });
        grafo.AdicionarVertice("C", new List<string> { "D" });
        grafo.AdicionarVertice("D", new List<string>());

        var caminhos = grafo.CaminhosMaximaisElementares("A");
        foreach (var caminho in caminhos)
        {
            Console.WriteLine(string.Join(" -> ", caminho));
        }
        Console.ReadLine();
    }
}