using Berzerker_AlonBrayer;
Random rand = new();
Player player1 = new("AB", RandomRaceChooser());
Player player2 = new("DBD", RandomRaceChooser());
Loot l = new();

for (int i = 0; i < rand.Next(3, 6); i++)
{
    player1.InstantiateNewUnit(player1.Race, player1.army);
}

for (int i = 0; i < rand.Next(2, 6); i++)
{
    player1.InstantiateNewInventory(player1.inventory);
}

for (int i = 0; i < rand.Next(3, 6); i++)
{
    player1.InstantiateNewUnit(player2.Race, player2.army);
}

for (int i = 0; i < rand.Next(2, 6); i++)
{
    player2.InstantiateNewInventory(player2.inventory);
}

Console.WriteLine(player1.Name + "'s army");
foreach (var item in player1.army)
{
    Console.WriteLine(item);
}
Console.WriteLine();

Console.WriteLine(player1.Name + "'s inventory");
foreach (var item in player1.inventory)
{
    Console.WriteLine(item);
}
Console.WriteLine();

Console.WriteLine(player2.Name + "'s army");
foreach (var item in player2.army)
{
    Console.WriteLine(item);
}
Console.WriteLine();

Console.WriteLine(player2.Name + "'s inventory");
foreach (var item in player2.inventory)
{
    Console.WriteLine(item);
}
Console.WriteLine();

Battle b = new(player1.army, player2.army);
b.BattleLoop(player1.army, player2.army);

if (b.player1Win)
{
    l.LootTransferer(player2, player1);
    Console.WriteLine(player1.Name + " has won and looted all of " + player2.Name + "'s inventory");
    Console.WriteLine(player1.Name + "'s inventory");
    foreach (var item in player1.inventory)
    {
        Console.WriteLine(item);
    }
}

else if(b.player2Win)
{
    l.LootTransferer(player1, player2);
    Console.WriteLine(player2.Name + " has won and looted all of " + player1.Name + "'s inventory");
    Console.WriteLine(player2.Name + "'s inventory");
    foreach (var item in player2.inventory)
    {
        Console.WriteLine(item);
    }
}


int RandomRaceChooser()
{
    return rand.Next(1, 4);
}


