using System;

class BankAccount
{
    public string KontoinnehavarensNamn { get; set; }
    public string Kontonummer { get; set; }
    public decimal Saldo { get; set; }

    // Konstruktor för att skapa ett nytt bankkonto
    public BankAccount(string kontoinnehavarensNamn, string kontonummer, decimal startSaldo)
    {
        KontoinnehavarensNamn = kontoinnehavarensNamn;
        Kontonummer = kontonummer;
        Saldo = startSaldo;
    }

    // Metod för insättning
    public void Insättning(decimal belopp)
    {
        if (belopp > 0)
        {
            Saldo += belopp;
            Console.WriteLine($"Insatt {belopp} till konto {Kontonummer}. Nytt saldo: {Saldo}");
        }
        else
        {
            Console.WriteLine("Felaktigt belopp. Försök igen.");
        }
    }

    // Metod för uttag
    public void Uttag(decimal belopp)
    {
        if (belopp > 0 && belopp <= Saldo)
        {
            Saldo -= belopp;
            Console.WriteLine($"Uttag på {belopp} från konto {Kontonummer}. Nytt saldo: {Saldo}");
        }
        else
        {
            Console.WriteLine("Uttag misslyckades. Kontrollera saldo eller belopp.");
        }
    }

    // Metod för att kontrollera saldo
    public void KontrolleraSaldo()
    {
        Console.WriteLine($"Saldo för konto {Kontonummer}: {Saldo}");
    }
}

class BankSystem
{
    private BankAccount personkonto;
    private BankAccount sparkonto;
    private BankAccount investeringskonto;

    // Konstruktor för att skapa de tre kontona
    public BankSystem()
    {
        personkonto = new BankAccount("Användare", "5168", 2500);
        sparkonto = new BankAccount("Användare", "5435", 15000);
        investeringskonto = new BankAccount("Användare", "5021", 10000);
    }

    // Metod för överföring mellan konton
    public void Överför(BankAccount frånKonto, BankAccount tillKonto, decimal belopp)
    {
        if (frånKonto.Saldo >= belopp)
        {
            frånKonto.Uttag(belopp);
            tillKonto.Insättning(belopp);
            Console.WriteLine($"Överfört {belopp} från {frånKonto.Kontonummer} till {tillKonto.Kontonummer}");
        }
        else
        {
            Console.WriteLine("Överföring misslyckades. Otillräckliga medel.");
        }
    }

    // Meny som visas för användaren
    public void VisaMeny()
    {
        while (true)
        {
            Console.WriteLine("\n--- BankSystem ---");
            Console.WriteLine("1. Insättning");
            Console.WriteLine("2. Uttag");
            Console.WriteLine("3. Överföring");
            Console.WriteLine("4. Kontrollera saldo");
            Console.WriteLine("5. Avsluta");
            Console.Write("Välj ett alternativ: ");

            string val = Console.ReadLine();

            switch (val)
            {
                case "1":
                    GörInsättning();
                    break;
                case "2":
                    GörUttag();
                    break;
                case "3":
                    GörÖverföring();
                    break;
                case "4":
                    KontrolleraSaldoFörKonto();
                    break;
                case "5":
                    Console.WriteLine("Tack för att du använder banksystemet!");
                    return;
                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    break;
            }
        }
    }

    // Metod för att hantera insättningar
    private void GörInsättning()
    {
        BankAccount konto = VäljKonto();

        if (konto == null)
        {
            Console.WriteLine("Felaktigt kontoval.");
            return;
        }

        Console.Write("Ange belopp att sätta in: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal belopp))
        {
            konto.Insättning(belopp);
        }
        else
        {
            Console.WriteLine("Ogiltig inmatning.");
        }
    }

    // Metod för att hantera uttag
    private void GörUttag()
    {
        BankAccount konto = VäljKonto();

        if (konto == null)
        {
            Console.WriteLine("Felaktigt kontoval.");
            return;
        }

        Console.Write("Ange belopp att ta ut: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal belopp))
        {
            konto.Uttag(belopp);
        }
        else
        {
            Console.WriteLine("Ogiltig inmatning.");
        }
    }

    // Metod för att hantera överföringar
    private void GörÖverföring()
    {
        Console.Write("Välj från vilket konto (1: Personkonto, 2: Sparkonto, 3: Investeringskonto): ");
        BankAccount frånKonto = VäljKonto();

        if (frånKonto == null)
        {
            Console.WriteLine("Felaktigt kontoval.");
            return;
        }

        Console.Write("Välj till vilket konto (1: Personkonto, 2: Sparkonto, 3: Investeringskonto): ");
        BankAccount tillKonto = VäljKonto();

        if (tillKonto == null)
        {
            Console.WriteLine("Felaktigt kontoval.");
            return;
        }

        Console.Write("Ange belopp att överföra: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal belopp))
        {
            Överför(frånKonto, tillKonto, belopp);
        }
        else
        {
            Console.WriteLine("Ogiltig inmatning.");
        }
    }

    // Metod för att hantera saldokontroll
    private void KontrolleraSaldoFörKonto()
    {
        BankAccount konto = VäljKonto();

        if (konto == null)
        {
            Console.WriteLine("Felaktigt kontoval.");
            return;
        }

        konto.KontrolleraSaldo();
    }

    // Metod för att välja konto
    private BankAccount VäljKonto()
    {
        string val = Console.ReadLine();

        switch (val)
        {
            case "1":
                return personkonto;
            case "2":
                return sparkonto;
            case "3":
                return investeringskonto;
            default:
                Console.WriteLine("Ogiltigt val.");
                return null;
        }
    }
}

