**Sistema casinò sul lancio dei dadi**

## 📖 Descrizione: prima feature del progetto
Questa feature trasforma il semplice gioco del dado in un'esperienza di casinò completa e coinvolgente. Il sistema introduce:

- **Gestione monete**: Il giocatore inizia con 10 monete e può scommettere su ogni partita
- **Sistema di quote dinamiche**: ogni partita genera una quota casuale (da 1.0x a 10.0x) che moltiplica la vincita potenziale
- **Validazione input**: controlli sulle puntate per evitare errori
- **Game Over**: il gioco termina quando le monete finiscono, con statistiche finali

## 🔧 Come si integra nel progetto

La feature mantiene le due modalità di gioco originali:
- **Modalità 1**: "Supera 13" (3 dadi, vinci se somma > 13)
- **Modalità 2**: "Sotto il 3" (1 dado, vinci se valore < 3)

Aggiunte rispetto alla prima versione:
- Sistema di scommesse con monete virtuali
- Quote dinamiche che moltiplicano le vincite
- Sistema di validazione puntate
- Visualizzazione del saldo dopo ogni giocata

## 📦 Requisiti

### Requisiti tecnici
- **Linguaggio**: C# (.NET Framework 4.7+ o .NET Core 3.1+)

### Dipendenze
Solo librerie standard .NET (nessuna dipendenza esterna)

##  Vantaggi della feature
- ✅ Aumenta il coinvolgimento del giocatore
- ✅ Introduce elementi di risk/reward con quote variabili
- ✅ Sistema di validazione per evitare errori

---


## Nuove sezioni del codice

Sono qui elencate le nuove funzioni di codice implementate nel nostro progetto

## 1) mostraRisultatoVittoria

``` csharp
static void mostraRisultatoVittoria(int puntata, double quota, ref int monete)
{
    int vincita = (int)(puntata * quota);
    monete += vincita;

    Console.WriteLine("╔═══════════════════════════════════╗");
    Console.WriteLine("║        hai vinto!                 ║");
    Console.WriteLine($"║  puntata: {puntata} x {quota} = +{vincita} monete".PadRight(36) + "║");
    Console.WriteLine($"║  nuovo saldo: {monete} monete".PadRight(36) + "║");
    Console.WriteLine("╚═══════════════════════════════════╝");
}
```

## 2) mostraRisultatoSconfitta

``` csharp
static void mostraRisultatoSconfitta(int puntata, ref int monete)
{
    monete -= puntata;

    Console.WriteLine("╔═══════════════════════════════════╗");
    Console.WriteLine("║        hai perso!                 ║");
    Console.WriteLine($"║  perdi: -{puntata} monete".PadRight(36) + "║");
    Console.WriteLine($"║  nuovo saldo: {monete} monete".PadRight(36) + "║");
    Console.WriteLine("╚═══════════════════════════════════╝");
}
```

## 3) mostraGameOver

``` csharp
static void mostraGameOver()
{
    Console.WriteLine("\nhai finito le monete! game over!");
}
```

## 4) mostraStatisticheFinali

``` csharp
static void mostraStatisticheFinali(int monete)
{
    Console.WriteLine("\nrisultato finale");
    Console.WriteLine($"monete finali: {monete}");

    if (monete > 10)
        Console.WriteLine("sei in profitto!");
    else if (monete > 0)
        Console.WriteLine("sei in perdita, ma ti restano monete.");
    else
        Console.WriteLine("hai perso tutto!");

    Console.WriteLine("\ngrazie per aver giocato!");
}
```

## 5) lanciaTreDadi

``` csharp
static int lanciaTreDadi(Random rnd)
{
    int somma = 0;

    Console.WriteLine("risultato dei 3 dadi:");
    Console.WriteLine("┌─────────────────────────┐");

    for (int i = 1; i <= 3; i++)
    {
        int v = rnd.Next(1, 7);
        somma += v;
        Console.WriteLine($"│  dado {i}: [ {v} ]            │");
    }

    Console.WriteLine("├─────────────────────────┤");
    Console.WriteLine($"│  somma totale: {somma,-9}│");
    Console.WriteLine("└─────────────────────────┘");

    return somma;
}
```

## 6) lanciaUnDado

``` csharp
static int lanciaUnDado(Random rnd)
{
    int valore = rnd.Next(1, 7);

    Console.WriteLine("risultato del dado:");
    Console.WriteLine("┌─────────────────────────┐");
    Console.WriteLine($"│  dado: [ {valore} ]             │");
    Console.WriteLine("└─────────────────────────┘");

    return valore;
}
```

## 📝 Specifiche sul codice

- **Math.Round()**: arrotonda la quota a 1 decimale
- **PadRight()**: allinea il testo nei box ASCII
- **String interpolation**: `$"{variabile}"` per testo dinamico
