using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Random rnd = new Random();
        bool continua = true;
        int monete = 10;

        // Intro
        ScriviLento("\nBENVENUTO AL CASINO DEI DADI!", 50);
        Thread.Sleep(500);
        ScriviLento("\n=====================================\n", 20);
        Thread.Sleep(1000);

        while (continua && monete > 0)
        {
            Console.WriteLine("=====================================");
            Console.WriteLine($"Le tue monete: {monete}");
            Console.WriteLine("=====================================\n");

            // Scelta modalità
            Console.WriteLine("Scegli la modalità di gioco:");
            Console.WriteLine("1) Lancia 3 dadi: se fai più di 13 vinci");
            Console.WriteLine("2) Lancia 1 dado: se fai meno di 6 vinci");
            Console.Write("Scelta: ");
            int scelta = int.Parse(Console.ReadLine());
            Console.WriteLine();

            if (scelta != 1 && scelta != 2)
            {
                Console.WriteLine("Scelta non valida!\n");
                continue;
            }

            // Genera quota
            Console.Write("\nGenerazione quota");
            Caricamento(5);
            double quota = Math.Round(rnd.NextDouble() * 9 + 1, 1);
            Console.WriteLine("\n");
            Thread.Sleep(300);

            Console.WriteLine("╔═══════════════════════════════════╗");
            Thread.Sleep(200);

            Console.Write("║  QUOTA: ");
            Thread.Sleep(800);
            ScriviLento($"x{quota}", 100);
            Console.WriteLine("                   ║".Substring(quota.ToString().Length));

            Thread.Sleep(200);
            Console.WriteLine($"║  Monete disponibili: {monete,-10}║");
            Console.WriteLine("╚═══════════════════════════════════╝\n");
            Thread.Sleep(500);

            // Puntata
            int puntata = 0;
            bool puntataValida = false;

            while (!puntataValida)
            {
                Console.Write("Quante monete vuoi puntare? ");
                puntata = int.Parse(Console.ReadLine());

                if (puntata <= 0)
                    Console.WriteLine("Devi puntare almeno 1 moneta!");
                else if (puntata > monete)
                    Console.WriteLine($"Non hai abbastanza monete! (Hai {monete})");
                else
                    puntataValida = true;
            }

            int vincitaPotenziale = (int)(puntata * quota);
            Console.Write("\nVincita potenziale: ");
            Thread.Sleep(500);
            ScriviLento($"{vincitaPotenziale} monete!", 80);
            Console.WriteLine();
            Thread.Sleep(1000);

            // Countdown
            Console.WriteLine("\nPreparati al lancio...\n");
            Thread.Sleep(1000);

            for (int i = 3; i >= 1; i--)
            {
                Console.Write($"   {i}");
                Thread.Sleep(800);
                Console.Write(".");
                Thread.Sleep(300);
                Console.Write(".");
                Thread.Sleep(300);
                Console.Write(".");
                Thread.Sleep(300);
                Console.WriteLine();
            }

            Thread.Sleep(500);
            ScriviLento("\n   LANCIO!\n", 50);
            Thread.Sleep(800);

            bool haVinto = false;
            int somma = 0;

            if (scelta == 1)
            {
                // Modalità 1: 3 dadi
                Console.WriteLine("\nI dadi rotolano...\n");
                Thread.Sleep(500);
                Console.WriteLine("┌─────────────────────────┐");

                for (int i = 1; i <= 3; i++)
                {
                    int valore = rnd.Next(1, 7);
                    somma += valore;

                    Console.Write($"│  Dado {i}: [ ");
                    AnimazioneDado();
                    Thread.Sleep(300);
                    Console.WriteLine($"{valore} ]            │");
                    Thread.Sleep(600);
                }

                Console.WriteLine("├─────────────────────────┤");
                Thread.Sleep(500);

                Console.Write("│  SOMMA TOTALE: ");
                Thread.Sleep(1000);
                ScriviLento($"{somma}", 150);
                Console.WriteLine("         │".Substring(somma.ToString().Length));

                Thread.Sleep(300);
                Console.WriteLine($"│  Serviva: > 13          │");
                Console.WriteLine("└─────────────────────────┘");

                haVinto = somma > 13;
            }
            else if (scelta == 2)
            {
                // Modalità 2: 1 dado
                Console.WriteLine("\nIl dado rotola...\n");
                Thread.Sleep(500);

                int valore = rnd.Next(1, 7);
                somma = valore;

                Console.WriteLine("┌─────────────────────────┐");
                Console.Write("│  Dado: [ ");

                AnimazioneDado();
                AnimazioneDado();

                Thread.Sleep(500);
                Console.WriteLine($"{valore} ]             │");
                Thread.Sleep(500);
                Console.WriteLine("├─────────────────────────┤");
                Thread.Sleep(300);
                Console.WriteLine($"│  VALORE: {somma,-15}│");
                Console.WriteLine($"│  Serviva: < 6           │");
                Console.WriteLine("└─────────────────────────┘");

                haVinto = valore < 6;
            }

            // Risultato
            Thread.Sleep(1000);
            Console.WriteLine();
            ScriviLento("   Risultato", 100);
            Caricamento(6);
            Console.WriteLine("\n");
            Thread.Sleep(800);

            // Rullo
            Console.Write("   ");
            for (int i = 0; i < 8; i++)
            {
                Console.Write("• ");
                Thread.Sleep(150 + i * 50);
            }
            Console.WriteLine();
            Thread.Sleep(1000);

            if (haVinto)
            {
                int vincita = (int)(puntata * quota);
                monete += vincita;

                Console.WriteLine();
                EffettoVittoria();

                Console.WriteLine("\n╔═══════════════════════════════════╗");
                Console.Write("║     ");
                ScriviLento("HAI VINTO!", 80);
                Console.WriteLine("          ║");
                Thread.Sleep(500);
                Console.WriteLine($"║  Puntata: {puntata} x {quota} = +{vincita} monete".PadRight(36) + "║");
                Thread.Sleep(300);
                Console.Write("║  Nuovo saldo: ");
                ScriviLento($"{monete}", 100);
                Console.WriteLine(" monete".PadRight(18) + "║");
                Console.WriteLine("╚═══════════════════════════════════╝");
            }
            else
            {
                monete -= puntata;

                Console.WriteLine();
                EffettoSconfitta();

                Console.WriteLine("\n╔═══════════════════════════════════╗");
                Console.Write("║     ");
                ScriviLento("HAI PERSO!", 80);
                Console.WriteLine("          ║");
                Thread.Sleep(500);
                Console.WriteLine($"║  Perdi: -{puntata} monete".PadRight(36) + "║");
                Thread.Sleep(300);
                Console.WriteLine($"║  Nuovo saldo: {monete} monete".PadRight(36) + "║");
                Console.WriteLine("╚═══════════════════════════════════╝");
            }

            // Game Over
            if (monete <= 0)
            {
                Thread.Sleep(1000);
                Console.WriteLine();
                ScriviLento("\nHai finito le monete...", 80);
                Thread.Sleep(500);
                ScriviLento("\nGAME OVER\n", 100);
                break;
            }

            Thread.Sleep(1000);
            Console.Write("\nVuoi giocare ancora? (s/n): ");
            continua = Console.ReadLine().ToLower() == "s";
            Console.WriteLine();
        }

        // Riepilogo finale
        Thread.Sleep(500);
        Console.WriteLine("\n");
        ScriviLento("FINE DEL GIOCO\n", 60);
        Thread.Sleep(500);

        Console.Write("Monete finali: ");
        ScriviLento($"{monete}\n", 100);
        Thread.Sleep(500);

        if (monete > 10)
            ScriviLento("Complimenti! Sei in profitto!\n", 40);
        else if (monete > 0)
            ScriviLento("Sei in perdita, ma ti restano monete.\n", 40);
        else
            ScriviLento("Hai perso tutto!\n", 40);

        Thread.Sleep(500);
        ScriviLento("\nGrazie per aver giocato!\n", 50);
    }

    static void ScriviLento(string testo, int millisecondi)
    {
        foreach (char c in testo)
        {
            Console.Write(c);
            Thread.Sleep(millisecondi);
        }
    }

    static void Caricamento(int punti)
    {
        for (int i = 0; i < punti; i++)
        {
            Thread.Sleep(400);
            Console.Write(".");
        }
    }

    static void AnimazioneDado()
    {
        Random rnd = new Random();
        for (int i = 0; i < 6; i++)
        {
            Console.Write(rnd.Next(1, 7));
            Thread.Sleep(100);
            Console.Write("\b");
        }
    }

    static void EffettoVittoria()
    {
        string[] frames = { "*", "-", "+", "=" };
        for (int i = 0; i < 3; i++)
        {
            foreach (string frame in frames)
            {
                Console.Write($"\r   {frame} {frame} {frame} VITTORIA! {frame} {frame} {frame}   ");
                Thread.Sleep(150);
            }
        }
        Console.WriteLine();
    }

    static void EffettoSconfitta()
    {
        Console.Write("   ");
        for (int i = 0; i < 3; i++)
        {
            Console.Write("X ");
            Thread.Sleep(400);
        }
        Console.WriteLine();
    }
}
