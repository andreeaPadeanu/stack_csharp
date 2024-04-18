using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("in.txt");
        List<Stack<char>> stacks = new List<Stack<char>>();
        List<string> movements = new List<string>();

        ParseInput(lines, stacks, movements);

        foreach (string instruction in movements)
        {
            string[] parts = instruction.Split(' ');
            int quantity = int.Parse(parts[1]);
            int from = int.Parse(parts[3]) - 1;
            int to = int.Parse(parts[5]) - 1;

            MoveCrates(stacks, quantity, from, to);
        }

        string result = "";
        foreach (Stack<char> stack in stacks)
        {
            if (stack.Count > 0)
            {
                result += stack.Peek();
            }
        }

        Console.WriteLine(result);
    }

    static void ParseInput(string[] lines, List<Stack<char>> stacks, List<string> instructions)
    {
        int startIndex = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
            {
                startIndex = i + 1;
                break;
            }
        }

        int maxIndex = 0;
        for (int i = startIndex - 2; i >= 0; i--)
        {
            string line = lines[i];
            for (int j = 1; j < line.Length; j += 4)
            {
                if (line[j] != ' ')
                {
                    int stackIndex = (j - 1) / 4;
                    if (stackIndex >= stacks.Count)
                    {
                        stacks.Add(new Stack<char>());
                    }
                    stacks[stackIndex].Push(line[j]);
                    if (stackIndex > maxIndex)
                    {
                        maxIndex = stackIndex;
                    }
                }
            }
        }

        for (int i = 0; i < stacks.Count; i++)
        {
            stacks[i] = new Stack<char>(stacks[i]);
        }

        for (int i = startIndex; i < lines.Length; i++)
        {
            instructions.Add(lines[i]);
        }
    }

    static void MoveCrates(List<Stack<char>> stacks, int quantity, int from, int to)
    {
        List<char> tempList = new List<char>();

        for (int i = 0; i < quantity; i++)
        {
            if (stacks[from].Count > 0)
            {
                tempList.Add(stacks[from].Pop());
            }
        }

        tempList.Reverse();

        foreach (char crate in tempList)
        {
            stacks[to].Push(crate);
        }
    }
}