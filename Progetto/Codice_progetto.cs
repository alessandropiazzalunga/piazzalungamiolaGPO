```csharp
using System;
using System.Threading;

/// <summary>
/// CLASSE PRINCIPALE: Program. Contiene l'orchestratore logico del gioco d'azzardo console.
/// </summary>
/// <remarks>
/// **FUNZIONI PRINCIPALI DEL PROGRAMMA:**
/// 1. **Gestione dello Stato:** Mantenere e aggiornare lo stato finanziario del giocatore (<see cref="monete"/>) attraverso i round.
/// 2. **Interazione Utente:** Acquisire le scelte di modalità e puntata tramite input console e fornire feedback visivo.
/// 3. **Simulazione Stocastica:** Eseguire lanci di dadi utilizzando <see cref="System.Random"/> secondo due regole predefinite (Modalità 1 e 2).
/// 4. **Sincronizzazione Temporale:** Utilizzare <see cref="System.Threading.Thread.Sleep"/> per introdurre pause intenzionali, rallentando l'esecuzione per migliorare la percezione dell'esperienza utente.
/// 5. **Reporting:** Fornire un riepilogo dettagliato al termine della sessione di gioco.
/// 
/// La documentazione Doxygen è redatta per spiegare la funzione operativa di ogni componente in termini di gestione dello stato e flusso di controllo.
/// </remarks>
class Program
{
    /// <summary>
    /// Metodo di ingresso del programma. Avvia la sequenza di esecuzione.
    /// </summary>
    /// <remarks>
    /// Questo metodo è responsabile dell'inizializzazione delle risorse globali e dell'entrata nel ciclo principale di gioco. 
    /// La sua corretta esecuzione assicura che tutte le variabili di stato iniziali siano impostate prima del primo input utente.
    /// </remarks>
    static void Main()
    {
        // ==========================================================================================================
        // SETUP INIZIALE: DICHIARAZIONE DELLE RISORSE E VARIABILI DI STATO
        // ==========================================================================================================

        /// <summary>
        /// Dichiarazione dell'oggetto <see cref="System.Random"/>. Questo oggetto deve essere creato una volta sola 
        /// perché la sua sequenza di numeri casuali è deterministica una volta che il seme viene impostato (implicitamente dall'ora di sistema).
        /// </summary>
        Random rnd = new Random();

        /// <summary>
        /// Flag di controllo del ciclo 'while'. Il suo valore booleano determina se il gioco deve continuare 
        /// a chiedere nuovi round all'utente dopo la conclusione del round corrente.
        /// </summary>
        bool continua = true;

        /// <summary>
        /// Variabile che rappresenta il capitale del giocatore. Inizializzata al valore base di 10.
        /// </summary>
        int monete = 10;

        // --- Sequenza di benvenuto ---
        ScriviLento("\nBENVENUTO AL CASINO DEI DADI!", 50);
        Thread.Sleep(500); // Pausa per la lettura.
        ScriviLento("\n=====================================\n", 20);
        Thread.Sleep(1000); // Pausa prolungata.

        /// <summary>
        /// **LOOP DI GIOCO (WHILE)**: Il costrutto iterativo primario che governa il gioco.
        /// Continua finché l'utente non sceglie di fermarsi (impostando <see cref="continua"/> a false) o finché il capitale <see cref="monete"/> non si esaurisce.
        /// </summary>
        while (continua && monete > 0)
        {
            // ==========================================================================================================
            // FASE 1: INFORMAZIONE STATO E RICHIESTA SCELTA
            // ==========================================================================================================
            Console.WriteLine("=====================================");
            Console.WriteLine($"Le tue monete: {monete}");
            Console.WriteLine("=====================================\n");

            Console.WriteLine("Scegli la modalità di gioco:");
            Console.WriteLine("1) Lancia 3 dadi: se fai più di 13 vinci");
            Console.WriteLine("2) Lancia 1 dado: se fai meno di 6 vinci");
            Console.Write("Scelta: ");

            // Tentativo di parsing sicuro dell'input utente per determinare la logica del round.
            if (!int.TryParse(Console.ReadLine(), out int scelta) || (scelta != 1 && scelta != 2))
            {
                Console.WriteLine("Input non valido per la selezione della modalità. Ritorno al punto di scelta del round.\n");
                continue; // Riavvia l'iterazione del ciclo 'while'.
            }
            Console.WriteLine();

            // ==========================================================================================================
            // FASE 2: GENERAZIONE E PRESENTAZIONE DEL MOLTIPLICATORE DI VINCITA (QUOTA)
            // ==========================================================================================================
            Console.Write("\nInizializzazione fattore moltiplicativo...");
            Caricamento(5);

            /// <summary>
            /// Calcolo del valore <see cref="quota"/>. Questo moltiplicatore è l'elemento chiave di rischio/ricompensa del round.
            /// </summary>
            double quota = Math.Round(rnd.NextDouble() * 9 + 1, 1);
            Console.WriteLine("\n");
            Thread.Sleep(300);

            // Output formattato dei parametri per trasparenza.
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

            // ==========================================================================================================
            // FASE 3: INPUT E VALIDAZIONE DELLA PUNTATA
            // ==========================================================================================================

            /// <summary>
            /// Variabile per memorizzare la quantità scommessa dall'utente per questo round.
            /// </summary>
            int puntata = 0;

            /// <summary>
            /// Flag di controllo per assicurare che la puntata rispetti i vincoli finanziari e minimi.
            /// </summary>
            bool puntataValida = false;

            while (!puntataValida)
            {
                Console.Write("Quante monete vuoi puntare? ");

                // Tentativo di conversione dell'input utente in un valore intero.
                if (!int.TryParse(Console.ReadLine(), out puntata))
                {
                    Console.WriteLine("Input non conforme al tipo <int>. La richiesta di puntata deve essere un numero intero.");
                    continue;
                }

                if (puntata <= 0)
                    Console.WriteLine("Vincolo di puntata minima non soddisfatto (richiesto > 0).");
                else if (puntata > monete)
                    Console.WriteLine($"Superamento limite di credito. Saldo attuale: {monete}.");
                else
                    puntataValida = true; // Uscita dal loop di validazione.
            }

            /// <summary>
            /// Pre-calcolo dell'eventuale guadagno, troncato per coerenza con la gestione in interi.
            /// </summary>
            int vincitaPotenziale = (int)(puntata * quota);
            Console.Write("\nCalcolo del ritorno potenziale...");
            Thread.Sleep(500);
            ScriviLento($"{vincitaPotenziale} monete!", 80);
            Console.WriteLine();
            Thread.Sleep(1000);

            // Countdown di attesa per la suspense.
            Console.WriteLine("\nPreparazione al lancio...");
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

            /// <summary>
            /// Stato che registra l'esito del lancio.
            /// </summary>
            bool haVinto = false;

            /// <summary>
            /// Variabile di accumulo per i risultati dei dadi.
            /// </summary>
            int somma = 0;

            // ==========================================================================================================
            // BLOCCO 4: ESECUZIONE DEL MODELLO STOCASTICO SCELTO
            // ==========================================================================================================
            if (scelta == 1)
            {
                // Logica per 3 dadi
                Console.WriteLine("\nSimulazione di 3 lanci D6...");
                Thread.Sleep(500);
                Console.WriteLine("┌─────────────────────────┐");

                for (int i = 1; i <= 3; i++)
                {
                    int valore = rnd.Next(1, 7); // Generazione del risultato.
                    somma += valore; // Aggiornamento del totale.

                    Console.Write($"│  Dado {i}: [ ");
                    AnimazioneDado(); // Visualizzazione dell'animazione del dado.
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
                Console.WriteLine($"│  Regola applicata: Somma > 13 │");
                Console.WriteLine("└─────────────────────────┘");

                haVinto = somma > 13;
            }
            else if (scelta == 2)
            {
                // Logica per 1 dado
                Console.WriteLine("\nSimulazione di 1 lancio D6...");
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
                Console.WriteLine($"│  Regola applicata: Valore < 6 │");
                Console.WriteLine("└─────────────────────────┘");

                haVinto = valore < 6;
            }

            // ==========================================================================================================
            // BLOCCO 5: CONFRONTO ESITO E MODIFICA DELLO STATO FINANZIARIO
            // ==========================================================================================================
            Thread.Sleep(1000);
            Console.WriteLine();
            ScriviLento("   Verifica condizioni di vincita...", 100);
            Caricamento(6);
            Console.WriteLine("\n");
            Thread.Sleep(800);

            // Breve sequenza di punti per marcare la rivelazione.
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
                monete += vincita; // Aggiornamento positivo del capitale.

                Console.WriteLine();
                EffettoVittoria();

                Console.WriteLine("\n╔═══════════════════════════════════╗");
                Console.Write("║     ");
                ScriviLento("ESITO POSITIVO", 80);
                Console.WriteLine("          ║");
                Thread.Sleep(500);
                Console.WriteLine($"║  Incremento del Capitale: +{vincita} unità".PadRight(36) + "║");
                Thread.Sleep(300);
                Console.Write("║  Nuovo Saldo: ");
                ScriviLento($"{monete}", 100);
                Console.WriteLine(" unità".PadRight(18) + "║");
                Console.WriteLine("╚═══════════════════════════════════╝");
            }
            else
            {
                monete -= puntata; // Aggiornamento negativo del capitale.

                Console.WriteLine();
                EffettoSconfitta();

                Console.WriteLine("\n╔═══════════════════════════════════╗");
                Console.Write("║     ");
                ScriviLento("ESITO NEGATIVO", 80);
                Console.WriteLine("          ║");
                Thread.Sleep(500);
                Console.WriteLine($"║  Decremento del Capitale: -{puntata} unità".PadRight(36) + "║");
                Thread.Sleep(300);
                Console.WriteLine($"║  Nuovo Saldo: {monete} unità".PadRight(36) + "║");
                Console.WriteLine("╚═══════════════════════════════════╝");
            }

            // Controllo di uscita per esaurimento fondi.
            if (monete <= 0)
            {
                Thread.Sleep(1000);
                Console.WriteLine();
                ScriviLento("\nVerifica condizione di uscita: Il capitale è insufficiente per procedere.", 80);
                Thread.Sleep(500);
                ScriviLento("\nTERMINAZIONE DEL GIOCO: FONDI ESAURITI\n", 100);
                break;
            }

            // Richiesta di continuazione esplicita per il round successivo.
            Thread.Sleep(1000);
            Console.Write("\nRichiesta di continuazione (impostazione flag 'continua')...");
            continua = Console.ReadLine().ToLower() == "s"; // Aggiorna il flag di controllo del ciclo.
            Console.WriteLine();
        }

        // ==========================================================================================================
        // CHIUSURA: REPORT FINALE DELLA SESSIONE
        // ==========================================================================================================
        Thread.Sleep(500);
        Console.WriteLine("\n");
        ScriviLento("TERMINAZIONE DEL LOOP DI GIOCO", 60);
        Thread.Sleep(500);

        Console.Write("Capitale finale accumulato: ");
        ScriviLento($"{monete}\n", 100);
        Thread.Sleep(500);

        if (monete > 10)
            ScriviLento("Rendiconto: Profitto netto rispetto all'investimento iniziale.\n", 40);
        else if (monete > 0)
            ScriviLento("Rendiconto: Perdita netta, ma capitale residuo positivo.\n", 40);
        else
            ScriviLento("Rendiconto: Capitale esaurito.\n", 40);

        Thread.Sleep(500);
        ScriviLento("\nChiusura del thread principale.\n", 50);
    }

    /// <summary>
    /// Funzione per la visualizzazione testuale ritardata (effetto macchina da scrivere).
    /// </summary>
    /// <param name="testo">La stringa sorgente da emettere.</param>
    /// <param name="millisecondi">Il parametro di temporizzazione applicato tra l'emissione di caratteri adiacenti.</param>
    static void ScriviLento(string testo, int millisecondi)
    {
        foreach (char c in testo)
        {
            Console.Write(c);
            Thread.Sleep(millisecondi);
        }
    }

    /// <summary>
    /// Routine di visualizzazione per indicare un'attesa attiva (progresso).
    /// </summary>
    /// <param name="punti">Il numero di iterazioni di visualizzazione del punto di attesa.</param>
    static void Caricamento(int punti)
    {
        for (int i = 0; i < punti; i++)
        {
            Thread.Sleep(400);
            Console.Write(".");
        }
    }

    /// <summary>
    /// Routine visiva che simula la dinamica di rotolamento di un dado prima del suo arresto finale.
    /// </summary>
    static void AnimazioneDado()
    {
        Random rnd = new Random();
        for (int i = 0; i < 6; i++)
        {
            Console.Write(rnd.Next(1, 7));
            Thread.Sleep(100);
            Console.Write("\b"); // Cancellazione del carattere appena scritto.
        }
    }

    /// <summary>
    /// Implementa l'animazione visiva di celebrazione per un esito positivo (vittoria).
    /// </summary>
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

    /// <summary>
    /// Implementa l'animazione visiva di segnalazione per un esito negativo (sconfitta).
    /// </summary>
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
```
