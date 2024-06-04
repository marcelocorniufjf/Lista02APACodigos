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

    // Algoritmo de ordenação topológica com detecção de ciclos
    private bool DFS(string vertice, HashSet<string> visitados, HashSet<string> recursao, Stack<string> ordem)
    {
        visitados.Add(vertice);
        recursao.Add(vertice);

        foreach (var vizinho in Vertices[vertice])
        {
            if (!visitados.Contains(vizinho))
            {
                if (DFS(vizinho, visitados, recursao, ordem))
                {
                    return true; // Ciclo detectado
                }
            }
            else if (recursao.Contains(vizinho))
            {
                return true; // Ciclo detectado
            }
        }

        recursao.Remove(vertice);
        ordem.Push(vertice);
        return false;
    }

    public (List<string>, bool) OrdenacaoTopologica()
    {
        var visitados = new HashSet<string>();
        var recursao = new HashSet<string>();
        var ordem = new Stack<string>();

        foreach (var vertice in Vertices.Keys)
        {
            if (!visitados.Contains(vertice))
            {
                if (DFS(vertice, visitados, recursao, ordem))
                {
                    return (null, true); // Ciclo detectado
                }
            }
        }

        var resultado = new List<string>(ordem);
        return (resultado, false);
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

        var (ordem, ciclo) = grafo.OrdenacaoTopologica();
        if (ciclo)
        {
            Console.WriteLine("Ciclo detectado no grafo.");
        }
        else
        {
            Console.WriteLine("Ordenação topológica:");
            Console.WriteLine(string.Join(" -> ", ordem));
        }
        Console.ReadLine();
    }
}