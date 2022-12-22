using System;
using System.Collections.Generic;


static class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("INTEL 8086");
        Console.Write("Wbierz na czym chcesz operowac: ");

        Console.WriteLine("\n=============================\n");
        Console.WriteLine("1) Rejestry");
        Console.WriteLine("2) Pamiec RAM");
        Console.WriteLine("x) exit");
        Console.WriteLine("\n=============================");

        switch (Convert.ToInt32(Console.ReadLine())) {
            
            case 1:
                RegistersSim.Run();
                break;

            case 2:
                MemorySim.Run();
                break;

            default:
                return;
        }

        Console.WriteLine("Koniec programu.");
    }
}