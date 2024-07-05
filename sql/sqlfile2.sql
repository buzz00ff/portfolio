-- lista czytelnikow biblioteki
SELECT id_czytelnik, imie, nazwisko, miasto, telefon FROM biblioteka.czytelnik;

-- liczba zamówień dla każdej książki
SELECT
    kat.nazwa AS kategoria, COUNT(z.id_zamowienie) AS liczba_zamowien
FROM
    zamowienie z
    JOIN ksiazka k ON z.id_ksiazka = k.id_ksiazka
    JOIN kategoria kat ON k.id_kategoria = kat.id_kategoria
GROUP BY
    kat.nazwa
ORDER BY
    liczba_zamowien DESC;

-- średnia liczba stron książek zamawianych przez danego czytelnika

SELECT
    c.imie, c.nazwisko, AVG(k.stron) AS srednia_liczba_stron
FROM
    zamowienie z
    JOIN czytelnik c ON z.id_czytelnik = c.id_czytelnik
    JOIN ksiazka k ON z.id_ksiazka = k.id_ksiazka
WHERE
    c.login = 'exampleLogin'  -- login danego użytkownika
GROUP BY
    c.imie, c.nazwisko;

-- najbardziej aktywne miesiące pod względem zamówień

SELECT
    DATE_FORMAT(data_zamowienia, '%Y-%m') AS miesiac, COUNT(id_zamowienie) AS liczba_zamowien
FROM
    zamowienie
GROUP BY
    miesiac
ORDER BY
    liczba_zamowien DESC;


-- średni czas wypożyczenia książek

SELECT
    k.tytul, AVG(DATEDIFF(z.data_zwrotu, z.data_odbioru)) AS sredni_czas_wypozyczenia
FROM
    zamowienie z
    JOIN ksiazka k ON z.id_ksiazka = k.id_ksiazka
WHERE
    z.data_zwrotu IS NOT NULL
GROUP BY
    k.tytul
ORDER BY
    sredni_czas_wypozyczenia DESC;
