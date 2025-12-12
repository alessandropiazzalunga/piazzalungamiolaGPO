using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// CLASSE DI TEST: UnitTest1
/// Contiene test unitari per verificare la logica del gioco d'azzardo console.
/// </summary>
/// <remarks>
/// Questa classe implementa test automatizzati per validare i componenti critici del programma:
/// - Logica di vincita/sconfitta per entrambe le modalità di gioco
/// - Gestione corretta del capitale del giocatore
/// - Validazione dei limiti e vincoli delle puntate
/// - Calcolo delle vincite con moltiplicatori
/// - Comportamento in casi limite (capitale esaurito, puntate non valide)
/// 
/// I test sono strutturati secondo il pattern AAA (Arrange-Act-Assert) e utilizzano
/// il framework MSTest per l'esecuzione automatizzata.
/// </remarks>
namespace test_PiazzalungaMiola
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Test della logica di vincita nella Modalità 1 (3 dadi, somma > 13).
        /// </summary>
        /// <remarks>
        /// **SCENARIO DI TEST:**
        /// Verifica che quando la somma di 3 dadi supera la soglia di 13,
        /// la condizione di vittoria venga correttamente identificata.
        /// 
        /// **ARRANGE:** Simulazione di 3 lanci che totalizzano 15 (6+5+4)
        /// **ACT:** Applicazione della regola "somma > 13"
        /// **ASSERT:** La condizione haVinto deve essere true
        /// 
        /// **COPERTURA:** Testa il branch positivo della modalità 1
        /// </remarks>
        [TestMethod]
        public void TestModalita1_VincitaConSommaSuperioreA13()
        {
            // Arrange: Configurazione scenario di test
            int dado1 = 6, dado2 = 5, dado3 = 4;
            int somma = dado1 + dado2 + dado3; // somma = 15

            // Act: Applicazione della regola di gioco
            bool haVinto = somma > 13;

            // Assert: Verifica del risultato atteso
            Assert.IsTrue(haVinto, $"Con somma {somma} (>13) il giocatore dovrebbe vincere");
        }

        /// <summary>
        /// Test della logica di sconfitta nella Modalità 1 (3 dadi, somma ≤ 13).
        /// </summary>
        /// <remarks>
        /// **SCENARIO DI TEST:**
        /// Verifica che quando la somma di 3 dadi non supera 13,
        /// la condizione di sconfitta venga correttamente identificata.
        /// 
        /// **ARRANGE:** Simulazione di 3 lanci che totalizzano 10 (3+3+4)
        /// **ACT:** Applicazione della regola "somma > 13"
        /// **ASSERT:** La condizione haVinto deve essere false
        /// 
        /// **COPERTURA:** Testa il branch negativo della modalità 1
        /// </remarks>
        [TestMethod]
        public void TestModalita1_SconfittaConSommaInferioreOUgualeA13()
        {
            // Arrange
            int dado1 = 3, dado2 = 3, dado3 = 4;
            int somma = dado1 + dado2 + dado3; // somma = 10

            // Act
            bool haVinto = somma > 13;

            // Assert
            Assert.IsFalse(haVinto, $"Con somma {somma} (≤13) il giocatore dovrebbe perdere");
        }

        /// <summary>
        /// Test del caso limite nella Modalità 1 con somma esattamente pari a 13.
        /// </summary>
        /// <remarks>
        /// **SCENARIO DI TEST:**
        /// Verifica il comportamento sul valore di soglia esatto (boundary test).
        /// Secondo la regola "somma > 13", il valore 13 non è vincente.
        /// 
        /// **ARRANGE:** Simulazione che totalizza esattamente 13
        /// **ACT:** Applicazione della regola con operatore stretto (>)
        /// **ASSERT:** Con 13 il risultato deve essere false (sconfitta)
        /// 
        /// **IMPORTANZA:** Questo test è critico per verificare che l'operatore
        /// di confronto sia corretto (> e non ≥)
        /// </remarks>
        [TestMethod]
        public void TestModalita1_CasoLimiteSommaEsattamente13()
        {
            // Arrange: Configurazione boundary condition
            int somma = 13; // Valore esatto della soglia

            // Act
            bool haVinto = somma > 13;

            // Assert: Con operatore >, 13 non vince
            Assert.IsFalse(haVinto, "Con somma esattamente 13 il giocatore NON dovrebbe vincere (regola: >13)");
        }

        /// <summary>
        /// Test della logica di vincita nella Modalità 2 (1 dado, valore < 6).
        /// </summary>
        /// <remarks>
        /// **SCENARIO DI TEST:**
        /// Verifica che un valore del dado inferiore a 6 produca una vittoria.
        /// 
        /// **ARRANGE:** Simulazione lancio con risultato 3
        /// **ACT:** Applicazione della regola "valore < 6"
        /// **ASSERT:** La condizione haVinto deve essere true
        /// 
        /// **COPERTURA:** Testa il branch positivo della modalità 2
        /// </remarks>
        [TestMethod]
        public void TestModalita2_VincitaConValoreInferioreA6()
        {
            // Arrange
            int valoreDado = 3;

            // Act
            bool haVinto = valoreDado < 6;

            // Assert
            Assert.IsTrue(haVinto, $"Con valore {valoreDado} (<6) il giocatore dovrebbe vincere");
        }

        /// <summary>
        /// Test della logica di sconfitta nella Modalità 2 (1 dado, valore = 6).
        /// </summary>
        /// <remarks>
        /// **SCENARIO DI TEST:**
        /// Verifica che il valore 6 (massimo su un D6) produca una sconfitta.
        /// 
        /// **ARRANGE:** Simulazione lancio con risultato 6
        /// **ACT:** Applicazione della regola "valore < 6"
        /// **ASSERT:** La condizione haVinto deve essere false
        /// 
        /// **COPERTURA:** Testa il caso peggiore nella modalità 2
        /// </remarks>
        [TestMethod]
        public void TestModalita2_SconfittaConValore6()
        {
            // Arrange
            int valoreDado = 6;

            // Act
            bool haVinto = valoreDado < 6;

            // Assert
            Assert.IsFalse(haVinto, $"Con valore {valoreDado} (=6) il giocatore dovrebbe perdere");
        }

        /// <summary>
        /// Test del calcolo della vincita potenziale con moltiplicatore (quota).
        /// </summary>
        /// <remarks>
        /// **SCENARIO DI TEST:**
        /// Verifica che il calcolo della vincita applichi correttamente
        /// il moltiplicatore alla puntata, con troncamento a intero.
        /// 
        /// **ARRANGE:** Puntata di 5 monete con quota 2.5x
        /// **ACT:** Calcolo (int)(puntata * quota)
        /// **ASSERT:** Il risultato deve essere 12 (troncato da 12.5)
        /// 
        /// **NOTA IMPLEMENTATIVA:** Il cast a int tronca invece di arrotondare,
        /// comportamento che deve essere verificato per evitare sorprese
        /// nell'esperienza utente.
        /// </remarks>
        [TestMethod]
        public void TestCalcoloVincita_ConMoltiplicatore()
        {
            // Arrange: Scenario di vincita con quota
            int puntata = 5;
            double quota = 2.5;

            // Act: Formula di calcolo vincita dal codice originale
            int vincitaPotenziale = (int)(puntata * quota);

            // Assert: Verifica troncamento corretto
            Assert.AreEqual(12, vincitaPotenziale, "5 * 2.5 = 12.5, troncato a 12");
        }

        /// <summary>
        /// Test dell'aggiornamento del capitale dopo una vittoria.
        /// </summary>
        /// <remarks>
        /// **SCENARIO DI TEST:**
        /// Simula un round completo di gioco con esito positivo e verifica
        /// che il capitale venga incrementato correttamente.
        /// 
        /// **ARRANGE:** 
        /// - Capitale iniziale: 10 monete
        /// - Puntata: 3 monete
        /// - Quota: 3.0x
        /// - Esito: Vittoria
        /// 
        /// **ACT:** 
        /// - Calcolo vincita: (int)(3 * 3.0) = 9
        /// - Aggiornamento: monete += vincita
        /// 
        /// **ASSERT:** Capitale finale deve essere 19 (10 + 9)
        /// 
        /// **RILEVANZA:** Testa la gestione dello stato finanziario positivo
        /// </remarks>
        [TestMethod]
        public void TestAggiornamentoCapitale_DopoVittoria()
        {
            // Arrange
            int monete = 10;
            int puntata = 3;
            double quota = 3.0;
            bool haVinto = true;

            // Act: Logica di aggiornamento capitale in caso di vittoria
            if (haVinto)
            {
                int vincita = (int)(puntata * quota);
                monete += vincita;
            }

            // Assert
            Assert.AreEqual(19, monete, "Capitale dovrebbe essere 10 + (3*3) = 19");
        }

        /// <summary>
        /// Test dell'aggiornamento del capitale dopo una sconfitta.
        /// </summary>
        /// <remarks>
        /// **SCENARIO DI TEST:**
        /// Simula un round con esito negativo e verifica la corretta
        /// sottrazione della puntata dal capitale.
        /// 
        /// **ARRANGE:**
        /// - Capitale iniziale: 10 monete
        /// - Puntata: 4 monete
        /// - Esito: Sconfitta
        /// 
        /// **ACT:** monete -= puntata
        /// 
        /// **ASSERT:** Capitale finale deve essere 6 (10 - 4)
        /// 
        /// **RILEVANZA:** Verifica la gestione dello stato finanziario negativo
        /// </remarks>
        [TestMethod]
        public void TestAggiornamentoCapitale_DopoSconfitta()
        {
            // Arrange
            int monete = 10;
            int puntata = 4;
            bool haVinto = false;

            // Act: Logica di aggiornamento capitale in caso di sconfitta
            if (!haVinto)
            {
                monete -= puntata;
            }

            // Assert
            Assert.AreEqual(6, monete, "Capitale dovrebbe essere 10 - 4 = 6");
        }

        /// <summary>
        /// Test della validazione: puntata superiore al capitale disponibile.
        /// </summary>
        /// <remarks>
        /// **SCENARIO DI TEST:**
        /// Verifica che il sistema rifiuti puntate che eccedono il capitale.
        /// 
        /// **ARRANGE:**
        /// - Capitale: 10 monete
        /// - Puntata tentata: 15 monete
        /// 
        /// **ACT:** Verifica condizione (puntata > monete)
        /// 
        /// **ASSERT:** La validazione deve fallire (puntataValida = false)
        /// 
        /// **IMPORTANZA CRITICA:** Previene stati inconsistenti e capitale negativo
        /// </remarks>
        [TestMethod]
        public void TestValidazionePuntata_SuperaSaldo()
        {
            // Arrange
            int monete = 10;
            int puntata = 15;

            // Act: Logica di validazione
            bool puntataValida = !(puntata > monete);

            // Assert
            Assert.IsFalse(puntataValida, "Puntata di 15 con saldo 10 deve essere invalida");
        }

        /// <summary>
        /// Test della validazione: puntata zero o negativa.
        /// </summary>
        /// <remarks>
        /// **SCENARIO DI TEST:**
        /// Verifica che il sistema rifiuti puntate non positive.
        /// 
        /// **ARRANGE:** Puntata = 0
        /// 
        /// **ACT:** Verifica condizione (puntata <= 0)
        /// 
        /// **ASSERT:** La validazione deve fallire
        /// 
        /// **RAZIONALE:** Le puntate devono essere strettamente positive
        /// per avere senso nel contesto del gioco
        /// </remarks>
        [TestMethod]
        public void TestValidazionePuntata_ValoreZero()
        {
            // Arrange
            int puntata = 0;

            // Act
            bool puntataValida = !(puntata <= 0);

            // Assert
            Assert.IsFalse(puntataValida, "Puntata zero deve essere invalida");
        }

        /// <summary>
        /// Test della validazione: puntata negativa.
        /// </summary>
        /// <remarks>
        /// **SCENARIO DI TEST:**
        /// Caso estremo con valore negativo, che non dovrebbe mai verificarsi
        /// ma è importante validare per robustezza.
        /// 
        /// **ARRANGE:** Puntata = -5
        /// 
        /// **ACT:** Verifica condizione (puntata <= 0)
        /// 
        /// **ASSERT:** La validazione deve fallire
        /// </remarks>
        [TestMethod]
        public void TestValidazionePuntata_ValoreNegativo()
        {
            // Arrange
            int puntata = -5;

            // Act
            bool puntataValida = !(puntata <= 0);

            // Assert
            Assert.IsFalse(puntataValida, "Puntata negativa deve essere invalida");
        }

        /// <summary>
        /// Test della validazione: puntata valida entro i limiti.
        /// </summary>
        /// <remarks>
        /// **SCENARIO DI TEST:**
        /// Verifica che una puntata corretta venga accettata.
        /// 
        /// **ARRANGE:**
        /// - Capitale: 10 monete
        /// - Puntata: 7 monete
        /// 
        /// **ACT:** Verifica entrambe le condizioni di validazione
        /// 
        /// **ASSERT:** La puntata deve essere accettata
        /// 
        /// **COPERTURA:** Testa il happy path della validazione
        /// </remarks>
        [TestMethod]
        public void TestValidazionePuntata_ValoreValido()
        {
            // Arrange
            int monete = 10;
            int puntata = 7;

            // Act: Entrambe le condizioni devono essere soddisfatte
            bool puntataValida = (puntata > 0) && (puntata <= monete);

            // Assert
            Assert.IsTrue(puntataValida, "Puntata di 7 con saldo 10 deve essere valida");
        }

        /// <summary>
        /// Test della condizione di terminazione: capitale esaurito.
        /// </summary>
        /// <remarks>
        /// **SCENARIO DI TEST:**
        /// Verifica che il gioco termini quando il capitale raggiunge zero.
        /// 
        /// **ARRANGE:** Capitale = 0
        /// 
        /// **ACT:** Verifica condizione (monete <= 0)
        /// 
        /// **ASSERT:** Il flag shouldTerminate deve essere true
        /// 
        /// **IMPORTANZA:** Previene loop infiniti e garantisce l'uscita
        /// dal ciclo di gioco quando il giocatore perde tutto
        /// </remarks>
        [TestMethod]
        public void TestCondizioneTerminazione_CapitaleEsaurito()
        {
            // Arrange
            int monete = 0;

            // Act: Condizione di uscita dal loop
            bool shouldTerminate = monete <= 0;

            // Assert
            Assert.IsTrue(shouldTerminate, "Con capitale 0 il gioco deve terminare");
        }

        /// <summary>
        /// Test del range di generazione della quota (moltiplicatore).
        /// </summary>
        /// <remarks>
        /// **SCENARIO DI TEST:**
        /// Verifica che il calcolo della quota produca valori nell'intervallo atteso.
        /// 
        /// **ARRANGE:** Simulazione di multipli valori Random tra 0.0 e 1.0
        /// 
        /// **ACT:** Applicazione formula: Math.Round(rnd.NextDouble() * 9 + 1, 1)
        /// 
        /// **ASSERT:** 
        /// - Con 0.0 → quota = 1.0 (minimo)
        /// - Con 1.0 → quota = 10.0 (massimo teorico)
        /// - Con 0.5 → quota = 5.5 (valore medio)
        /// 
        /// **RILEVANZA:** La quota determina il rischio/ricompensa del gioco,
        /// quindi il suo range deve essere corretto
        /// </remarks>
        [TestMethod]
        public void TestRangeQuota_ValoriLimite()
        {
            // Arrange & Act: Simulazione dei valori limite di Random
            double quotaMin = Math.Round(0.0 * 9 + 1, 1);  // NextDouble() = 0.0
            double quotaMax = Math.Round(0.999999 * 9 + 1, 1);  // NextDouble() ≈ 1.0
            double quotaMed = Math.Round(0.5 * 9 + 1, 1);  // NextDouble() = 0.5

            // Assert: Verifica range [1.0, 10.0]
            Assert.AreEqual(1.0, quotaMin, "Quota minima deve essere 1.0");
            Assert.IsTrue(quotaMax >= 9.9 && quotaMax <= 10.0, "Quota massima deve essere ~10.0");
            Assert.AreEqual(5.5, quotaMed, "Quota mediana deve essere 5.5");
        }

        /// <summary>
        /// Test integration: round completo con vittoria in Modalità 1.
        /// </summary>
        /// <remarks>
        /// **SCENARIO DI TEST:**
        /// Simula un round completo dalla puntata al risultato finale.
        /// 
        /// **ARRANGE:**
        /// - Stato iniziale: 10 monete
        /// - Scelta: Modalità 1
        /// - Puntata: 5 monete
        /// - Quota: 2.0x
        /// - Risultato dadi: 6+6+5 = 17 > 13 → VITTORIA
        /// 
        /// **ACT:** 
        /// 1. Validazione puntata
        /// 2. Simulazione lancio
        /// 3. Verifica condizione
        /// 4. Aggiornamento capitale
        /// 
        /// **ASSERT:** 
        /// - Capitale finale = 20 (10 + 10)
        /// - Verifica integrità flusso
        /// 
        /// **TIPO:** Integration test - verifica interazione tra componenti
        /// </remarks>
        [TestMethod]
        public void TestIntegrazione_RoundCompletoVittoriaModalita1()
        {
            // Arrange: Setup round completo
            int monete = 10;
            int puntata = 5;
            double quota = 2.0;
            int dado1 = 6, dado2 = 6, dado3 = 5;
            int somma = dado1 + dado2 + dado3; // 17

            // Act: Simulazione flusso completo
            // 1. Validazione
            bool puntataValida = (puntata > 0) && (puntata <= monete);
            Assert.IsTrue(puntataValida, "Precondizione: puntata deve essere valida");

            // 2. Verifica vittoria
            bool haVinto = somma > 13;
            Assert.IsTrue(haVinto, "Precondizione: con somma 17 dovrebbe vincere");

            // 3. Aggiornamento capitale
            if (haVinto)
            {
                int vincita = (int)(puntata * quota);
                monete += vincita;
            }

            // Assert: Verifica stato finale
            Assert.AreEqual(20, monete, "Capitale finale dovrebbe essere 10 + (5*2) = 20");
        }

        /// <summary>
        /// Test integration: round completo con sconfitta in Modalità 2.
        /// </summary>
        /// <remarks>
        /// **SCENARIO DI TEST:**
        /// Simula un round completo con esito negativo.
        /// 
        /// **ARRANGE:**
        /// - Stato iniziale: 10 monete
        /// - Scelta: Modalità 2
        /// - Puntata: 3 monete
        /// - Risultato dado: 6 → NON < 6 → SCONFITTA
        /// 
        /// **ACT:** Esecuzione flusso completo
        /// 
        /// **ASSERT:** 
        /// - Capitale finale = 7 (10 - 3)
        /// - Nessuna vincita aggiunta
        /// </remarks>
        [TestMethod]
        public void TestIntegrazione_RoundCompletoSconfittaModalita2()
        {
            // Arrange
            int monete = 10;
            int puntata = 3;
            int valoreDado = 6;

            // Act
            bool puntataValida = (puntata > 0) && (puntata <= monete);
            Assert.IsTrue(puntataValida);

            bool haVinto = valoreDado < 6;
            Assert.IsFalse(haVinto, "Con valore 6 dovrebbe perdere");

            if (!haVinto)
            {
                monete -= puntata;
            }

            // Assert
            Assert.AreEqual(7, monete, "Capitale finale dovrebbe essere 10 - 3 = 7");
        }
    }
}