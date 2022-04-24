using System;

static sbyte pickDoor(int doorsCount, int[] exclusions)
{
    sbyte door = (sbyte)new Random().Next(0, doorsCount);
    while (exclusions.Contains(door))
    {
        door = (sbyte)new Random().Next(0, doorsCount);
    }
    return door;
}

const sbyte numDoors = 3;
const int runs = 1000;
sbyte[] doors = new sbyte[numDoors];
sbyte[] exclusions = new sbyte[numDoors - 2];
sbyte contestant;

Console.WriteLine("Welcome to Let's make a deal!");
