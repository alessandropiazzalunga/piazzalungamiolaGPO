# Sistema casinò sul lancio dei dadi 

## 📖 Descrizione: feature suspence, seconda feature

Questa feature aggiunge effetti di suspense e drammaticità al gioco, senza modificare la logica economica o le modalità di gioco. L’obiettivo è rendere l’esperienza più coinvolgente tramite ritardi prestabiliti.

Il sistema introduce:
- **Lentezza di scrittura**: testo mostrato lentamente carattere per carattere.
- **Caricamento**: effetto di attesa con puntini.
- **Animazione del dado**: simulazione del dado che rotola.

## 🔧 Come si integra nel progetto

Queste funzioni possono essere chiamate:

- Prima di mostrare il risultato del dado (`AnimazioneDado`).  
- Dopo la valutazione della giocata per enfatizzare il risultato (`EffettoVittoria` / `EffettoSconfitta`).  
- Per creare suspense durante messaggi importanti (`ScriviLento`, `Caricamento`).  

Non modifica le regole: il gioco mantiene le due modalità originali.

## 📦 Requisiti

### Requisiti tecnici
- **Linguaggio**: C# (.NET Framework 4.7+ o .NET Core 3.1+)

### Dipendenze
- Solo librerie standard .NET (`System`, `System.Threading`)

## ✔️ Vantaggi della feature

- Migliora l’esperienza utente con suspense e drammaticità.
- Aumenta l’immersione del giocatore senza alterare la logica di gioco.
- Supporta un feedback visivo chiaro su vittoria o sconfitta.

---

# Nuove sezioni del codice – Suspense

## 1) ScriviLento

```csharp
static void ScriviLento(string testo, int millisecondi) 
{ 
    foreach (char c in testo) 
    { 
        Console.Write(c); 
        Thread.Sleep(millisecondi); 
    } 
}
```
## 2) Caricamento

```csharp
static void Caricamento(int punti) 
{ 
    for (int i = 0; i < punti; i++) 
    { 
        Thread.Sleep(400); 
        Console.Write("."); 
    } 
}
```
## 3) AnimazioneDado

```csharp
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
```

## 4) EffettoVittoria

```csharp
static void EffettoVittoria() 
{ 
    for (int i = 0; i < 3; i++)
    {
        Console.Write("Vittoria!");
        Thread.Sleep(200);
        Console.Write("\r");
        Console.Write("         ");
        Thread.Sleep(200);
        Console.Write("\r");
    }
    Console.WriteLine("Hai vinto!");
}
```

## 5) EffettoSconfitta

```csharp
static void EffettoSconfitta() 
{ 
    for (int i = 0; i < 3; i++)
    {
        Console.Write("Sconfitta!");
        Thread.Sleep(200);
        Console.Write("\r");
        Console.Write("           ");
        Thread.Sleep(200);
        Console.Write("\r");
    }
    Console.WriteLine("Hai perso!");
}
```
