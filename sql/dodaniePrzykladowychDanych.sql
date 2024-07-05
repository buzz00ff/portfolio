-- dodanie przykładowych danych (wygenerowanych przez AI)
INSERT INTO `czytelnik` (`login`, `haslo`, `imie`, `nazwisko`, `adres`, `miasto`, `wojewodztwo`, `telefon`, `kod_pocztowy`, `email`) VALUES
('jdoe', 'password123', 'John', 'Doe', '123 Main St', 'Warszawa', 'Mazowieckie', '123456789', '00-001', 'jdoe@example.com'),
('asmith', 'mypassword', 'Alice', 'Smith', '456 Oak St', 'Kraków', 'Małopolskie', '987654321', '30-002', 'asmith@example.com'),
('bwhite', 'securepass', 'Bob', 'White', '789 Pine St', 'Gdańsk', 'Pomorskie', '564738291', '80-003', 'bwhite@example.com'),
('ejones', 'strongpwd', 'Emily', 'Jones', '321 Maple St', 'Wrocław', 'Dolnośląskie', '192837465', '50-004', 'ejones@example.com'),
('dmiller', 'password', 'David', 'Miller', '654 Elm St', 'Poznań', 'Wielkopolskie', '273645192', '60-005', 'dmiller@example.com'),
('hgreen', 'letmein', 'Hannah', 'Green', '987 Birch St', 'Łódź', 'Łódzkie', '345678910', '90-006', 'hgreen@example.com'),
('rlee', 'password321', 'Robert', 'Lee', '654 Cedar St', 'Szczecin', 'Zachodniopomorskie', '876543210', '70-007', 'rlee@example.com'),
('kking', 'mypassword1', 'Karen', 'King', '321 Walnut St', 'Lublin', 'Lubelskie', '102938475', '20-008', 'kking@example.com'),
('cgarcia', 'mypassword2', 'Carlos', 'Garcia', '123 Spruce St', 'Bydgoszcz', 'Kujawsko-Pomorskie', '019283746', '85-009', 'cgarcia@example.com'),
('mjohnson', 'mypassword3', 'Maria', 'Johnson', '789 Fir St', 'Białystok', 'Podlaskie', '564738291', '15-010', 'mjohnson@example.com');

INSERT INTO `kategoria` (`nazwa`) VALUES
('Fiction'),
('Non-Fiction'),
('Science'),
('Technology'),
('History'),
('Biography'),
('Fantasy'),
('Mystery'),
('Horror'),
('Romance');

INSERT INTO `ksiazka` (`id_kategoria`, `isbn`, `tytul`, `autor`, `stron`, `wydawnictwo`, `rok_wydania`, `opis`) VALUES
(1, '9783161484100', 'The Great Gatsby', 'F. Scott Fitzgerald', 218, 'Scribner', 1925, 'A novel set in the Jazz Age that tells the story of Jay Gatsby and his unrequited love for Daisy Buchanan.'),
(2, '9781566199094', 'Sapiens', 'Yuval Noah Harari', 443, 'Harper', 2011, 'A brief history of humankind from the Stone Age to the 21st century.'),
(3, '9780262033848', 'A Brief History of Time', 'Stephen Hawking', 212, 'Bantam Books', 1988, 'An overview of cosmology and black holes for the general reader.'),
(4, '9781250079704', 'The Pragmatic Programmer', 'Andrew Hunt and David Thomas', 352, 'Addison-Wesley', 1999, 'A book about software engineering and best practices for programmers.'),
(5, '9780743273565', '1776', 'David McCullough', 386, 'Simon & Schuster', 2005, 'A historical book about the year 1776 during the American Revolution.'),
(6, '9780374529015', 'Steve Jobs', 'Walter Isaacson', 656, 'Simon & Schuster', 2011, 'A biography of Steve Jobs, the co-founder of Apple Inc.'),
(7, '9780743273566', 'Harry Potter and the Sorcerers Stone', 'J.K. Rowling', 309, 'Bloomsbury', 1997, 'The first book in the Harry Potter series about a young wizard\'s adventures.'),
(8, '9780385504201', 'The Da Vinci Code', 'Dan Brown', 454, 'Doubleday', 2003, 'A mystery thriller that follows symbologist Robert Langdon.'),
(9, '9780394703905', 'The Shining', 'Stephen King', 447, 'Doubleday', 1977, 'A horror novel about a haunted hotel and its caretaker.'),
(10, '9781250079711', 'Pride and Prejudice', 'Jane Austen', 279, 'T. Egerton', 1813, 'A romantic novel that deals with the issues of marriage, money, and love in early 19th-century England.');

INSERT INTO `zamowienie` (`id_czytelnik`, `id_ksiazka`, `data_zamowienia`, `data_odbioru`, `data_zwrotu`) VALUES
(1, 1, '2024-01-01 10:00:00', '2024-01-02 14:00:00', '2024-01-12 09:00:00'),
(2, 2, '2024-01-05 11:00:00', '2024-01-06 15:00:00', '2024-01-15 10:00:00'),
(3, 3, '2024-01-10 09:00:00', '2024-01-11 13:00:00', '2024-01-20 11:00:00'),
(4, 4, '2024-01-15 12:00:00', '2024-01-16 16:00:00', '2024-01-25 12:00:00'),
(5, 5, '2024-01-20 08:00:00', '2024-01-21 14:00:00', '2024-01-30 13:00:00'),
(6, 6, '2024-01-25 13:00:00', '2024-01-26 17:00:00', '2024-02-04 14:00:00'),
(7, 7, '2024-01-30 10:00:00', '2024-01-31 15:00:00', '2024-02-09 11:00:00'),
(8, 8, '2024-02-01 11:00:00', '2024-02-02 14:00:00', '2024-02-11 09:00:00'),
(9, 9, '2024-02-05 09:00:00', '2024-02-06 13:00:00', '2024-02-15 10:00:00'),
(10, 10, '2024-02-10 12:00:00', '2024-02-11 16:00:00', '2024-02-20 12:00:00');

INSERT INTO `admin` (`login`, `haslo`) VALUES
('admin1', 'adminpass1'),
('admin2', 'adminpass2'),
('admin3', 'adminpass3'),
('admin4', 'adminpass4'),
('admin5', 'adminpass5'),
('admin6', 'adminpass6'),
('admin7', 'adminpass7'),
('admin8', 'adminpass8'),
('admin9', 'adminpass9'),
('admin10', 'adminpass10');

INSERT INTO `bibliotekarz` (`login`, `haslo`) VALUES
('lib1', 'libpass1'),
('lib2', 'libpass2'),
('lib3', 'libpass3'),
('lib4', 'libpass4'),
('lib5', 'libpass5'),
('lib6', 'libpass6'),
('lib7', 'libpass7'),
('lib8', 'libpass8'),
('lib9', 'libpass9'),
('lib10', 'libpass10');



