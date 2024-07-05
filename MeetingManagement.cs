using System;
using System.Collections.Generic;

class Program
{
    static List<Spotkanie> spotkania = new List<Spotkanie>();
    static List<Uzytkownik> uzytkownicy = new List<Uzytkownik>();

    static void Main()
    {
        Console.WriteLine("Witaj w systemie rezerwacji spotkań!");

        // Dodajmy kilku przykładowych użytkowników na start
        uzytkownicy.Add(new Uzytkownik("Jan", "Kowalski", "jan.kowalski@email.com", Rola.Administrator));
        uzytkownicy.Add(new Uzytkownik("Anna", "Nowak", "anna.nowak@email.com", Rola.Uzytkownik));

        while (true)
        {
            Console.WriteLine("\nWybierz opcję:");
            Console.WriteLine("1. Dodaj spotkanie");
            Console.WriteLine("2. Przeglądaj spotkania");
            Console.WriteLine("3. Usuń spotkanie");
            Console.WriteLine("4. Dodaj użytkownika");
            Console.WriteLine("5. Przeglądaj użytkowników");
            Console.WriteLine("6. Przyznaj uprawnienia");
            Console.WriteLine("7. Wyjdź");

            Console.Write("Twój wybór: ");
            string opcja = Console.ReadLine();

            switch (opcja)
            {
                case "1":
                    DodajSpotkanie();
                    break;
                case "2":
                    PrzegladajSpotkania();
                    break;
                case "3":
                    UsunSpotkanie();
                    break;
                case "4":
                    DodajUzytkownika();
                    break;
                case "5":
                    PrzegladajUzytkownikow();
                    break;
                case "6":
                    PrzyznajUprawnienia();
                    break;
                case "7":
                    Console.WriteLine("Dziękujemy za korzystanie z systemu rezerwacji spotkań!");
                    return;
                default:
                    Console.WriteLine("Niepoprawna opcja. Spróbuj ponownie.");
                    break;
            }
        }
    }

    static void DodajSpotkanie()
    {
        Console.WriteLine("\nDodawanie nowego spotkania:");

        Console.Write("Podaj datę spotkania (RRRR-MM-DD): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime data))
        {
            Console.WriteLine("Niepoprawny format daty. Spróbuj ponownie.");
            return;
        }

        Console.Write("Podaj godzinę rozpoczęcia spotkania (HH:MM): ");
        if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan godzinaRozpoczecia))
        {
            Console.WriteLine("Niepoprawny format godziny. Spróbuj ponownie.");
            return;
        }

        Console.Write("Podaj godzinę zakończenia spotkania (HH:MM): ");
        if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan godzinaZakonczenia))
        {
            Console.WriteLine("Niepoprawny format godziny. Spróbuj ponownie.");
            return;
        }

        Console.Write("Podaj temat spotkania: ");
        string temat = Console.ReadLine();

        Console.Write("Podaj listę uczestników (oddzielone przecinkami): ");
        string[] uczestnicy = Console.ReadLine().Split(',');

        Spotkanie noweSpotkanie = new Spotkanie(data, godzinaRozpoczecia, godzinaZakonczenia, temat, uczestnicy);
        spotkania.Add(noweSpotkanie);

        Console.WriteLine("Spotkanie zostało dodane.");
    }

    static void PrzegladajSpotkania()
    {
        Console.WriteLine("\nLista spotkań:");

        if (spotkania.Count == 0)
        {
            Console.WriteLine("Brak zaplanowanych spotkań.");
        }
        else
        {
            foreach (var spotkanie in spotkania)
            {
                Console.WriteLine(spotkanie);
            }
        }
    }

    static void UsunSpotkanie()
    {
        Console.WriteLine("\nUsuwanie spotkania:");

        Console.Write("Podaj numer spotkania do usunięcia: ");
        if (int.TryParse(Console.ReadLine(), out int numer) && numer >= 1 && numer <= spotkania.Count)
        {
            spotkania.RemoveAt(numer - 1);
            Console.WriteLine("Spotkanie zostało usunięte.");
        }
        else
        {
            Console.WriteLine("Niepoprawny numer spotkania. Spróbuj ponownie.");
        }
    }

    static void DodajUzytkownika()
    {
        Console.WriteLine("\nDodawanie nowego użytkownika:");

        Console.Write("Podaj imię: ");
        string imie = Console.ReadLine();

        Console.Write("Podaj nazwisko: ");
        string nazwisko = Console.ReadLine();

        Console.Write("Podaj adres email: ");
        string email = Console.ReadLine();

        Console.WriteLine("Wybierz rolę użytkownika:");
        Console.WriteLine("1. Administrator");
        Console.WriteLine("2. Użytkownik");

        Rola rola = Rola.Uzytkownik;
        switch (Console.ReadLine())
        {
            case "1":
                rola = Rola.Administrator;
                break;
            case "2":
                rola = Rola.Uzytkownik;
                break;
            default:
                Console.WriteLine("Niepoprawna opcja. Ustawiono rolę domyślną (Użytkownik).");
                break;
        }

        Uzytkownik nowyUzytkownik = new Uzytkownik(imie, nazwisko, email, rola);
        uzytkownicy.Add(nowyUzytkownik);

        Console.WriteLine("Użytkownik został dodany.");
    }

    static void PrzegladajUzytkownikow()
    {
        Console.WriteLine("\nLista użytkowników:");

        if (uzytkownicy.Count == 0)
        {
            Console.WriteLine("Brak zarejestrowanych użytkowników.");
        }
        else
        {
            foreach (var uzytkownik in uzytkownicy)
            {
                Console.WriteLine(uzytkownik);
            }
        }
    }

    static void PrzyznajUprawnienia()
    {
        Console.WriteLine("\nPrzyznawanie uprawnień:");

        if (uzytkownicy.Count == 0)
        {
            Console.WriteLine("Brak zarejestrowanych użytkowników.");
            return;
        }

        Console.Write("Podaj numer użytkownika, któremu chcesz przyznać uprawnienia: ");
        if (int.TryParse(Console.ReadLine(), out int numer) && numer >= 1 && numer <= uzytkownicy.Count)
        {
            Console.WriteLine("Wybierz nową rolę użytkownika:");
            Console.WriteLine("1. Administrator");
            Console.WriteLine("2. Użytkownik");

            Rola nowaRola = uzytkownicy[numer - 1].Rola;
            switch (Console.ReadLine())
            {
                case "1":
                    nowaRola = Rola.Administrator;
                    break;
                case "2":
                    nowaRola = Rola.Uzytkownik;
                    break;
                default:
                    Console.WriteLine("Niepoprawna opcja. Zostawiono istniejącą rolę użytkownika.");
                    break;
            }

            uzytkownicy[numer - 1].Rola = nowaRola;
            Console.WriteLine("Uprawnienia użytkownika zostały zaktualizowane.");
        }
        else
        {
            Console.WriteLine("Niepoprawny numer użytkownika. Spróbuj ponownie.");
        }
    }
}

enum Rola
{
    Administrator,
    Uzytkownik
}

class Uzytkownik
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public string Email { get; set; }
    public Rola Rola { get; set; }

    public Uzytkownik(string imie, string nazwisko, string email, Rola rola)
    {
        Imie = imie;
        Nazwisko = nazwisko;
        Email = email;
        Rola = rola;
    }

    public override string ToString()
    {
        return $"Imię: {Imie}, Nazwisko: {Nazwisko}, Email: {Email}, Rola: {Rola}";
    }
}

class Spotkanie
{
    public DateTime Data { get; }
    public TimeSpan GodzinaRozpoczecia { get; }
    public TimeSpan GodzinaZakonczenia { get; }
    public string Temat { get; }
    public string[] Uczestnicy { get; }

    public Spotkanie(DateTime data, TimeSpan godzinaRozpoczecia, TimeSpan godzinaZakonczenia, string temat, string[] uczestnicy)
    {
        Data = data;
        GodzinaRozpoczecia = godzinaRozpoczecia;
        GodzinaZakonczenia = godzinaZakonczenia;
        Temat = temat;
        Uczestnicy = uczestnicy;
    }

    public override string ToString()
    {
        return $"Data: {Data.ToShortDateString()}, Godzina rozpoczęcia: {GodzinaRozpoczecia}, Godzina zakończenia: {GodzinaZakonczenia}, Temat: {Temat}, Uczestnicy: {string.Join(", ", Uczestnicy)}";
    }
}
