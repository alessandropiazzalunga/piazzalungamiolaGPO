
# 🎲 Sistema Casinò sul Lancio dei Dadi

Un’estensione completa che trasforma il gioco del dado in un’esperienza **da casinò**, con gestione monete, scommesse e quote dinamiche.  
Il documento descrive **funzionalità**, **specifiche tecniche** e **codice C#** di esempio pronto all’uso.

---

## 📖 Panoramica

### Obiettivi
- Aumentare l’engagement con **meccaniche di scommessa** e **quote variabili**.
- Garantire **robustezza** grazie alla **validazione degli input**.
- Fornire **feedback chiari** tramite una UI testuale con box ASCII e colori.
- Chiudere la sessione con **statistiche finali** (profitto/perdita).

### Modalità di gioco (immutate)
- **Modalità 1 — “Supera 13”**: lancia 3 dadi, **vinci se somma > 13**.
- **Modalità 2 — “Sotto il 3”**: lancia 1 dado, **vinci se valore < 3**.

### Nuove feature introdotte
- **Gestione monete**: saldo iniziale **10 monete**, puntata per ogni partita.
- **Quote dinamiche**: generazione casuale da **1.0x** a **10.0x** (arrotondate a 1 decimale).
-- **Validazione puntate**: evita puntate non numeriche, negative, zero o oltre il saldo.
- **Visualizzazione saldo**: aggiornato dopo ogni esito.
- **Game Over**: chiusura automatica quando il saldo arriva a **0**.
- **Statistiche finali**: messaggi di profitto/perdita con riepilogo.

---

## 📦 Requisiti

- **Linguaggio**: C#  
- **Runtime**: **.NET Framework 4.7+** _oppure_ **.NET Core 3.1+**
- **Dipendenze**: solo librerie standard .NET (nessuna dipendenza esterna)

---

## 🔧 Specifiche Tecniche

### Quote dinamiche
La quota viene generata a ogni partita:
```csharp
double quota = Math.Round(rnd.NextDouble() * 9 + 1, 1);


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
