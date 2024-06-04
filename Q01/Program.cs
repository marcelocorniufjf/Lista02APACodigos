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

    // Algoritmo de Dijkstra para calcular as distâncias mínimas
    public Dictionary<string, int> Dijkstra(string inicio)
    {
        var distancias = new Dictionary<string, int>();
        var pq = new PriorityQueue<(int distancia, string vertice), int>();

        foreach (var vertice in Vertices)
        {
            distancias[vertice.Key] = int.MaxValue;
        }
        distancias[inicio] = 0;
        pq.Enqueue((0, inicio), 0);

        while (pq.Count > 0)
        {
            var (distanciaAtual, verticeAtual) = pq.Dequeue();
            foreach (var vizinho in Vertices[verticeAtual])
            {
                var novaDistancia = distanciaAtual + vizinho.Value;
                if (novaDistancia < distancias[vizinho.Key])
                {
                    distancias[vizinho.Key] = novaDistancia;
                    pq.Enqueue((novaDistancia, vizinho.Key), novaDistancia);
                }
            }
        }

        return distancias;
    }

    // Calcula a distância média a partir de um vértice inicial
    public double DistanciaMedia(string inicio)
    {
        var distancias = Dijkstra(inicio);
        int totalDistancia = 0;
        foreach (var distancia in distancias.Values)
        {
            if (distancia != int.MaxValue)
            {
                totalDistancia += distancia;
            }
        }
        return totalDistancia / (double)(Vertices.Count - 1);
    }
}

public class Program
{
    public static void Main()
    {
        var grafo = new Grafo();
        grafo.AdicionarVertice("A", new Dictionary<string, int> { { "B", 1 }, { "C", 4 } });
        grafo.AdicionarVertice("B", new Dictionary<string, int> { { "C", 2 }, { "D", 5 } });
        grafo.AdicionarVertice("C", new Dictionary<string, int> { { "D", 1 } });
        grafo.AdicionarVertice("D", new Dictionary<string, int>());

        Console.WriteLine($"Distância média a partir do vértice A: {grafo.DistanciaMedia("A")}");
        Console.ReadLine();
    }
}