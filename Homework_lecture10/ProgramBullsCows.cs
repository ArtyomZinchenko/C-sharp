﻿// Попробуйте написать программу "Быки и коровы".  Программа загадывает четырехзначное число с неповторяющимися цифрами. 
// Вы начинаете его угадывать. После каждой попытки программа сообщает подсказку - количество быков и коров. 
// Быки - это правильная цифра на правильном месте, коровы - правильные цифры на неправильном месте.
// Например, загадано число 1234, а вы ввели 1243, значит у нас 2 быка (1,2) и две коровы (3,4)
// Программа должна предусматривать показ статистики (кол-во попыток и время, затраченное на игру), а также выход по нулю, 
// если пользователю надоест угадывать

using System;           // for Console.WriteLine, Console.ReadLine, Random, etc.
using System.Linq;       // for Enumerable, Select, OrderBy, Contains, etc.
using System.Diagnostics; // for Stopwatch


static void DrawCow(string message)
{
    Console.WriteLine($"      (\"{message}\")");
    Console.WriteLine("       \\ -----------");
    Console.WriteLine("         (__)");
    Console.WriteLine("         (oo)");
    Console.WriteLine("  /------(..)");
    Console.WriteLine(" /-------|//");
    Console.WriteLine("/---------");
    Console.WriteLine("|  ||    ||");
    Console.WriteLine("^  ||----||");
    Console.WriteLine("   ^^    ^^");
}

static void ExplainRules()
{
    string message = "Guess the 4-digit number. Bulls are correct digits in correct places.\nCows are correct digits in wrong places. Enter 0 to exit.";
    DrawCow(message);
}

static int[] GenerateTarget()
{
    Random rand = new Random();
    return Enumerable.Range(0, 10).OrderBy(x => rand.Next()).Take(4).ToArray();
}

static (int Bulls, int Cows) CalculateBullsAndCows(int[] target, int[] guess)
{
    int bulls = 0, cows = 0; // Malvina and Buratino style :)
    for (int i = 0; i < 4; i++)
    {
        if (guess[i] == target[i])
        {
            bulls++;
        }
        else if (target.Contains(guess[i]))
        {
            cows++;
        }
    }
    return (Bulls: bulls, Cows: cows);
}

Stopwatch stopwatch = new Stopwatch();
int attempts = 0;

Console.WriteLine("----------------------- Welcome to the 'Bulls and Cows' game! -----------------------");
Console.WriteLine(" ");
Console.WriteLine(" ");
ExplainRules();

int[] target = GenerateTarget();


Console.WriteLine("To exit, enter 0.");
stopwatch.Start();

while (true)
{
    Console.Write("Enter your guess: ");
    string input = Console.ReadLine();

    if (input == "0")
    {
        stopwatch.Stop();
        Console.WriteLine($"Game over. You made {attempts} attempts.");
        Console.WriteLine($"Time spent on the game: {Math.Ceiling(stopwatch.Elapsed.TotalSeconds)} seconds.");
        break;
    }

    int[] guess = input.Select(ch => ch - '0').ToArray();
    if (guess.Length != 4 || guess.Distinct().Count() != 4)
    {
        Console.WriteLine("Invalid input. Try again.");
        continue;
    }

    attempts++;

    var result = CalculateBullsAndCows(target, guess);
    Console.WriteLine($"Bulls: {result.Bulls}, Cows: {result.Cows}");
    Console.WriteLine($"Time spent on the game: {Math.Ceiling(stopwatch.Elapsed.TotalSeconds)} seconds. You made {attempts} attempts.");
    DrawCow($"Bulls: {result.Bulls}, Cows: {result.Cows}");

    if (result.Bulls == 4)
    {
        stopwatch.Stop();
        Console.WriteLine($"Congratulations! You've guessed the number {string.Join("", target)} in {attempts} attempts.");
        Console.WriteLine($"Time spent on the game: {Math.Ceiling(stopwatch.Elapsed.TotalSeconds)} seconds.");
        break;
    }
}