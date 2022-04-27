static int pickDoor(int doorsCount, int[] exclusions)
{
    int door = new Random().Next(0, doorsCount);
    while (exclusions.Contains(door))
    {
        door = new Random().Next(0, doorsCount);
    }
    return door;
}

static bool swapDoor(bool win)
{
    if (win)
        return false;
    else
        return true;
}

Console.Write("How many doors? ");
int numDoors = Convert.ToInt16(Console.ReadLine());
Console.Write("How many Runs? ");
int runs = Convert.ToInt32(Console.ReadLine());
Console.Write("How often should you stay (1/{input} chance to stay)? ");
int swapWeight = Convert.ToInt16(Console.ReadLine()); // how often to swap the door (1 swaps every time, 2 swaps every other time, etc..). Ties to a Random function that picks a value less than or equal to 0 to less than swapWeight

int[] doors = new int[numDoors]; // array of doors and their states - -1 for loss, 1 for win, 2 for revealed
int[] exclusions = new int[numDoors - 1];
int contestantPick = -1;
bool win = false;
int wins = 0;
int swaps = 0;

Console.WriteLine("Welcome to Let's make a deal!\n");

using (var progress = new ProgressBar())
{
    for (int round = 0; round < runs; round++)
    {
        // Clear the board //
        Array.Fill(doors, -1);
        Array.Fill(exclusions, -1);
        contestantPick = -1;
        win = false;

        // Round 1 - pick a winner and a door //
        exclusions[0] = pickDoor(numDoors, exclusions); // pick a winner, the first number to be excluded
        doors[exclusions[0]] = 1;
        contestantPick = new Random().Next(0, numDoors); // contestantPick picks a door

        // Round 1.5 - reveal (exclude) all doors but one, then flip a coin to switch //
        for (int picker = 1; picker < exclusions.Length; picker++)
        {
            exclusions[picker] = pickDoor(numDoors, exclusions);
            doors[exclusions[picker]] = 2;
        }

        // This is just a game, we can tell the computer if it's won or not before Round 2
        if (doors[contestantPick] == 1)
        {
            win = true;
        }

        // Round 2 - pick whether to switch or not. Just swap your win half the time //
        int coin = new Random().Next(0, swapWeight);
        if (coin == 0)
        {
            win = swapDoor(win);
            swaps++;
        }

        if (win)
            wins++;

        progress.Report((double)round / runs);
    }
}

Console.WriteLine("Simulation complete!\n");
Console.WriteLine("{0} runs with {1} doors swapping {2:P2} percent of the time wins:", runs, numDoors, Convert.ToDouble(swaps) / runs);
Console.WriteLine(String.Format("{0:P2} of the time.", Convert.ToDouble(wins) / runs));
