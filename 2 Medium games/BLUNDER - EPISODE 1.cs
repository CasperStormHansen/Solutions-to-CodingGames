using System;
using System.Linq;
using System.Collections.Generic;

public struct State
{
    // fields
    public int x;
    public int y;
    public string direction;
    public List<int[]> removedObstacles;
    public bool reversedPriorities;
    public bool breakerMode;

    // helper functions
    static int deltaX(string direction)
    {
        if (direction == "WEST") return -1;
        if (direction == "EAST") return 1;
        return 0;
    }

    static int deltaY(string direction)
    {
        if (direction == "NORTH") return -1;
        if (direction == "SOUTH") return 1;
        return 0;
    }

    // methods
    public State nextState(string[] map, List<int[]> teleporters)
    {
        State nextState = new State();
        char mapChar = map[y][x];
        // rule 7
        nextState.removedObstacles = this.removedObstacles.ToList();
        if (this.breakerMode && mapChar == 'X') nextState.removedObstacles.Add(new int[2] { y, x });
        nextState.breakerMode = (mapChar == 'B')? !this.breakerMode : this.breakerMode; 
        // rule 5
        string preferredDirection = this.direction;
        if (mapChar == 'S') preferredDirection = "SOUTH";
        if (mapChar == 'E') preferredDirection = "EAST";
        if (mapChar == 'N') preferredDirection = "NORTH";
        if (mapChar == 'W') preferredDirection = "WEST";
        // rule 6
        nextState.reversedPriorities = (mapChar == 'I')? !this.reversedPriorities : this.reversedPriorities;
        string[] prioritizedDirections = (nextState.reversedPriorities)? new string[] { preferredDirection, "WEST", "NORTH", "EAST", "SOUTH" } : new string[] { preferredDirection, "SOUTH", "EAST", "NORTH", "WEST" };
        // rules 3, 4
        foreach (string direction in prioritizedDirections)
        {
            int hypotheticalY = y + deltaY(direction);
            int hypotheticalX = x + deltaX(direction);
            if (map[hypotheticalY][hypotheticalX] != '#' && 
                (map[hypotheticalY][hypotheticalX] != 'X' ||
                    nextState.breakerMode ||
                    nextState.removedObstacles.Any(loc => loc[0] == hypotheticalY && loc[1] == hypotheticalX))) // rule 7
            {
                nextState.direction = direction;
                break;
            }
        }
        nextState.x = this.x + deltaX(nextState.direction);
        nextState.y = this.y + deltaY(nextState.direction);
        // rule 8
        if (map[nextState.y][nextState.x] == 'T')
        {
            if (nextState.y == teleporters[0][0] && nextState.x == teleporters[0][1])
            {
                nextState.y = teleporters[1][0];
                nextState.x = teleporters[1][1];
            }
            else
            {
                nextState.y = teleporters[0][0];
                nextState.x = teleporters[0][1];
            }
        }
        return nextState;
    }

    // equality
    public bool Equals(State other)
    {
        return this.x == other.x && this.y == other.y && this.direction == other.direction && this.removedObstacles.Count == other.removedObstacles.Count && this.reversedPriorities == other.reversedPriorities && this.breakerMode == other.breakerMode;
    }
}

class Solution
{
    static void Main(string[] args)
    {
        // initialization
        string[] inputs = Console.ReadLine().Split(' ');
        int L = int.Parse(inputs[0]);
        int C = int.Parse(inputs[1]);
        string[] map = new string[L];
        List<int[]> teleporters = new();
        State initialState = new State()
        {
            direction = "SOUTH", // rule 1
            removedObstacles = new List<int[]>(),
            reversedPriorities = false,
            breakerMode = false
        };
        for (int yy = 0; yy < L; yy++)
        {
            map[yy] = Console.ReadLine();
            Console.Error.WriteLine(map[yy]);
            // rule 1
            int startingX = map[yy].IndexOf('@');
            if (startingX >= 0)
            {
                initialState.x = startingX;
                initialState.y = yy;
            }
            // rule 8
            for (int xx = 0; xx < C; xx++)
            {
                if (map[yy][xx] == 'T') teleporters.Add(new int[] { yy, xx });
            }
        }
        State currentState = initialState;
        List<State> journey = new();
        journey.Add(initialState);

        // main loop
        while (true)
        {
            if (map[currentState.y][currentState.x] == '$')
            {
                for (int i = 1; i < journey.Count; i++)
                {
                    Console.WriteLine(journey[i].direction);
                }
                break;
            }
            currentState = currentState.nextState(map, teleporters);
            if (journey.Any(state => state.Equals(currentState)))
            {
                Console.WriteLine("LOOP");
                break;
            }
            journey.Add(currentState);
        }
    }
}