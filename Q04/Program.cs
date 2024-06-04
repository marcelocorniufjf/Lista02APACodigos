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

    // Algoritmo DFS para verificar a conectividade do grafo
    private void DFS(string vertice, HashSet<string> visitados)
    {
        visitados.Add(vertice);
        foreach (var vizinho in Vertices[vertice])
        {
            if (!visitados.Contains(vizinho))
            {
                DFS(vizinho, visitados);
            }
        }
    }

    public bool EhConexo(string inicio)
    {
        var visitados = new HashSet<string>();
        DFS(inicio, visitados);
        return visitados.Count == Vertices.Count;
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

        Console.WriteLine(grafo.EhConexo("A") ? "O grafo é conexo." : "O grafo não é conexo.");
        Console.ReadLine();
    }
}