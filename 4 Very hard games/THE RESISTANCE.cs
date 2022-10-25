using System;
using System.Collections.Generic;

class Solution
{
    static void Main(string[] args)
    {
        Dictionary<char, string> Translate = new Dictionary<char, string>(){
            {'A', ".-"}, {'B', "-..."}, {'C',  "-.-."}, {'D',  "-.."},
            {'E', "."}, {'F', "..-."}, {'G', "--."}, {'H',  "...."},
            {'I', ".."}, {'J', ".---"}, {'K',  "-.-"}, {'L',  ".-.."},
            {'M', "--"}, {'N', "-."}, {'O',  "---"}, {'P',  ".--."},
            {'Q', "--.-"}, {'R', ".-."}, {'S', "..."}, {'T', "-"},
            {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X',  "-..-"},
            {'Y', "-.--"}, {'Z',  "--.."}
        };

        string L = Console.ReadLine();
        int N = int.Parse(Console.ReadLine());
        
        Node root = new Node();
        for (int i = 0; i < N; i++)
        {
            string word = Console.ReadLine();

            string wordInMorse = "";
            foreach (char wordChar in word) wordInMorse += Translate[wordChar];

            Node currentNode = root;
            foreach (char morseChar in wordInMorse)
            {
                if (morseChar == '.')
                {
                    if (currentNode.dot == null) currentNode.dot = new Node();
                    currentNode = currentNode.dot;
                }
                else
                {
                    if (currentNode.dash == null) currentNode.dash = new Node();
                    currentNode = currentNode.dash;
                }
            }
            currentNode.numberOfWords++;
        }

        ulong[] combinations = new ulong[L.Length + 1];
        combinations[0] = 1;

        for (int i = 0; i < L.Length; i++)
        {
            if (combinations[i] > 0)
            {
                Node currentNode = root;
                for (int j = 0; j < L.Substring(i).Length; j++)
                {
                    if (L.Substring(i)[j] == '.') 
                    {
                        if (currentNode.dot == null) break;
                        currentNode = currentNode.dot;
                    }
                    else
                    {
                        if (currentNode.dash == null) break;
                        currentNode = currentNode.dash;
                    }
                    
                    if (currentNode.numberOfWords > 0)
                    {
                        combinations[i + j + 1] += combinations[i] * (ulong)currentNode.numberOfWords;
                    }
                }
            }
        }

        Console.WriteLine(combinations[L.Length]);
    }
}

public class Node
{
    #nullable enable
    public Node? dot, dash;
    #nullable disable
    public int numberOfWords;
}

// https://www.codingame.com/ide/puzzle/the-resistance