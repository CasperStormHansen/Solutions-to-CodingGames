using System;

class Solution
{
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        Room[] roomArray = new Room[N];
        for (int i = 0; i < N; i++) roomArray[i] = new Room(Console.ReadLine().Split(' '));
        Console.WriteLine(roomArray[0].MoneyMaxFromHere(roomArray));
    }
}

public class Room
{
    int moneyHere, doorOne, doorTwo, moneyMaxFromHere = -1;

    public Room(string[] input)
    {
        moneyHere = int.Parse(input[1]);
        doorOne = (input[2] == "E") ? -1 : int.Parse(input[2]);
        doorTwo = (input[3] == "E") ? -1 : int.Parse(input[3]);
    }

    public int MoneyMaxFromHere(Room[] roomArray)
    {
        if (moneyMaxFromHere == -1)
        {
            moneyMaxFromHere = moneyHere + Math.Max(
                ((doorOne == -1)? 0 : roomArray[doorOne].MoneyMaxFromHere(roomArray)),
                ((doorTwo == -1)? 0 : roomArray[doorTwo].MoneyMaxFromHere(roomArray))
            );
        }

        return moneyMaxFromHere;
    }
}

// https://www.codingame.com/ide/puzzle/blunder-episode-2