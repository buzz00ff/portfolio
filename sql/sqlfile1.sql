USE biblioteka;
CREATE TABLE IF NOT EXISTS `biblioteka`.`czytelnik` (

`id_czytelnik` INT NOT NULL AUTO_INCREMENT COMMENT 'Klucz główny przydzielony automatycznie',

`login` VARCHAR(50) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Nazwa czytelnika potrzeba przy logowaniu.',
`haslo` VARCHAR(45) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Hasło niezaszyfrowane',
`imie` VARCHAR(100) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Imię ',
`nazwisko` VARCHAR(100) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Nazwisko',
`adres` VARCHAR(200) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Adres zamieszkania np.: ul. Przykład 3/12',
`miasto` VARCHAR(45) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Miasto',
`wojewodztwo` VARCHAR(100) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Nazwa województwa',
`telefon` VARCHAR(50) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NULL COMMENT 'Telefony',
`kod_pocztowy` VARCHAR(45) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Kod pocztowy',
`email` VARCHAR(100) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Adres e-mail',
PRIMARY KEY (`id_czytelnik`));

CREATE TABLE IF NOT EXISTS `biblioteka`.`kategoria` (
`id_kategoria` INT NOT NULL AUTO_INCREMENT COMMENT 'Klucz główny przydzielony automatycznie',
`nazwa` VARCHAR(200) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Nazwa kategorii',
PRIMARY KEY (`id_kategoria`));


CREATE TABLE IF NOT EXISTS `biblioteka`.`ksiazka` (
`id_ksiazka` INT NOT NULL AUTO_INCREMENT COMMENT 'Klucz główny przydzielony automatycznie',
`id_kategoria` INT NOT NULL COMMENT 'Klucz obcy z tabeli kategoria',
`isbn` VARCHAR(13) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Niepowtarzalny 13-cyfrowy identyfikator książki',
`tytul` VARCHAR(200) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Tytuł książki',
`autor` VARCHAR(70) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Imię i Nazwisko autora książki',
`stron` INT(4) NOT NULL COMMENT 'Liczba stron książki',
`wydawnictwo` VARCHAR(50) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Nazwa wydawnictwa, w którym wydano książkę',
`rok_wydania` INT(4) NOT NULL COMMENT 'Rok wydania książki',
`opis` TEXT CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NULL COMMENT 'Opis książki',
PRIMARY KEY (`id_ksiazka`),
INDEX `fk_ksiazka_kategoria1_idx` (`id_kategoria` ASC),
CONSTRAINT `fk_ksiazka_kategoria1`
FOREIGN KEY (`id_kategoria`)
REFERENCES `biblioteka`.`kategoria` (`id_kategoria`)
ON DELETE NO ACTION
ON UPDATE NO ACTION);

CREATE TABLE IF NOT EXISTS `biblioteka`.`zamowienie` (
`id_zamowienie` INT NOT NULL AUTO_INCREMENT COMMENT 'Klucz główny przydzielony automatycznie',
`id_czytelnik` INT NOT NULL COMMENT 'Klucz obcy z tabeli czytelnik',
`id_ksiazka` INT NOT NULL COMMENT 'Klucz obcy z tabeli ksiazka',
`data_zamowienia` DATETIME NOT NULL COMMENT 'Data złożenia zamówienia w bibliotece',
`data_odbioru` DATETIME NULL COMMENT 'Data odbioru książki z biblioteki ',
`data_zwrotu` DATETIME NULL COMMENT 'Data zwrotu książki do biblioteki',
PRIMARY KEY (`id_zamowienie`),
INDEX `fk_zamowienie_czytelnik1_idx` (`id_czytelnik` ASC),
INDEX `fk_zamowienie_ksiazka1_idx` (`id_ksiazka` ASC),
CONSTRAINT `fk_zamowienie_czytelnik1`
FOREIGN KEY (`id_czytelnik`)
REFERENCES `biblioteka`.`czytelnik` (`id_czytelnik`)
ON DELETE NO ACTION
ON UPDATE NO ACTION,
CONSTRAINT `fk_zamowienie_ksiazka1`
FOREIGN KEY (`id_ksiazka`)
REFERENCES `biblioteka`.`ksiazka` (`id_ksiazka`)
ON DELETE NO ACTION
ON UPDATE NO ACTION);

CREATE TABLE IF NOT EXISTS `biblioteka`.`admin` (
`id_admin` INT NOT NULL AUTO_INCREMENT COMMENT 'Klucz główny przydzielony automatycznie',
`login` VARCHAR(50) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Nazwa administratora potrzebna przy logowaniu',
`haslo` VARCHAR(50) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Hasło niezaszyfrowane',
PRIMARY KEY (`id_admin`));

CREATE TABLE IF NOT EXISTS `biblioteka`.`bibliotekarz` (
`id_bibliotekarz` INT NOT NULL AUTO_INCREMENT COMMENT 'Klucz główny przydzielony automatycznie',
`login` VARCHAR(50) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Nazwa bibliotekarza potrzebna przy logowaniu',
`haslo` VARCHAR(50) CHARACTER SET 'utf8' COLLATE 'utf8_polish_ci' NOT NULL COMMENT 'Hasło niezaszyfrowane',
PRIMARY KEY (`id_bibliotekarz`));

