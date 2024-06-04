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

    // Algoritmo DFS para encontrar um caminho entre dois vértices
    private bool DFS(string atual, string alvo, List<string> caminho, HashSet<string> visitados)
    {
        visitados.Add(atual);
        caminho.Add(atual);

        if (atual == alvo)
        {
            return true;
        }

        foreach (var vizinho in Vertices[atual])
        {
            if (!visitados.Contains(vizinho))
            {
                if (DFS(vizinho, alvo, caminho, visitados))
                {
                    return true;
                }
            }
        }

        caminho.RemoveAt(caminho.Count - 1);
        return false;
    }

    public List<string> EncontrarCaminho(string inicio, string alvo)
    {
        var caminho = new List<string>();
        var visitados = new HashSet<string>();
        if (DFS(inicio, alvo, caminho, visitados))
        {
            return caminho;
        }
        else
        {
            return null;
        }
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

        var caminho = grafo.EncontrarCaminho("A", "D");
        if (caminho != null)
        {
            Console.WriteLine("Caminho encontrado:");
            Console.WriteLine(string.Join(" -> ", caminho));
        }
        else
        {
            Console.WriteLine("Nenhum caminho encontrado.");
        }
        Console.ReadLine();
    }
}