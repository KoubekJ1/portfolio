# Music Player

## Úvod
Music Player je program umožňující spravování a přehrávání MP3 hudby v SQL Server databázi. Projekt je vytvořen v jazyce C# za využití grafické technologie WinForms.

## Technické požadavky
- Windows 10/11
- SQL Server instance

## Manuál

### Databáze
Nejprve je potřeba vytvořit SQL Server databázi pomocí skriptu uvedeného v souboru "db.sql", který se nachází ve složce "sql". Tento skript naleznete i na konci tohoto dokumentu.
SQL databáze může být vzdálená či lokální.

### Konfigurace
Veškerá konfigurace připojení k databázi se provádí v souboru "App.config". V souboru lze upravit následující nastavení:

#### DataSource
"DataSource" určuje název či adresu SQL serveru, na kterém se nachází Vaše databáze.

#### InitialCatalog
"InitialCatalog" určuje název konkrétní databáze, se kterou bude program pracovat.

#### IntegratedSecurity
"IntegratedSecurity" určuje, zda-li se pro přihlášení na SQL server použijí přihlašovací údaje uživatele přihlášeného do operačního systému Windows. Pokud je tato hodnota nastavena na "true", nemusí se nastavit hodnoty "UserID" a "Password".

#### UserID
"UserID" určuje jméno uživatele, které se použije při přihlášení na SQL server. Pokud je nastavena hodnota "IntegratedSecurity" na true, tato hodnota se nemusí specifikovat

#### Password
"UserID" určuje heslo uživatele, které se použije při přihlášení na SQL server. Pokud je nastavena hodnota "IntegratedSecurity" na true, tato hodnota se nemusí specifikovat

### Spuštění
Po vytvoření databáze a následné konfiguraci programu lze již samotný program spustit. Pokud vše proběhne v pořádku, zobrazí se okno "Music Player". Pokud dojde k chybě
při připojení k databázi, zobrazí se chybová hláška a program se vypne.

### Práce s programem
Po úspěšném spuštění programu lze začít program používat.

#### Navigace v uživatelském rozhraní
V hlavním okně programu naleznete veškéra data, která byla do databáze přidána. Pomocí tlačítek vlevo si můžete zobrazit data v požadovaném formátu. Můžete si zobrazit dostupnou hudbu setříděnou podle umělců a alb, pouze umělců či nesetříděnou. Na horní straně obrazovky naleznete možnosti pro přidání nových dat.

#### Přidání umělce
Pro přidání umělce otevřete kolonku "Add" na horní straně obrazovky a vyberte "Artist". Nyní se Vám zobrazí okno pro přidání umělce. Zadejte název umělce do textového pole,
případně přidejte umělci obrázek pomocí tlačítka "Change", a následně stiskněte tlačítko "Add".

#### Přidání skladby
Pro přidání umělce otevřete kolonku "Add" na horní straně obrazovky a vyberte "Song". Nyní se Vám zobrazí okno pro přidání skladby. Zadejte návez skladby do textového pole,
následně stiskněte tlačítko "Select file" a vyberte požadovaný soubor ve formátu MP3. Pro následné přidání skladby stiskněte tlačítko "Add".

#### Přidání alba
Pro přidání alba otevřete kolonku "Add" na horní straně obrazovky a vyberte "Album". Nyní se Vám zobrazí okno pro přidání alba. Zadejte návez alba do textového pole, přiřaďte albu obrázek pomocí tlačítka "Change", vyberte, kterému umělci album náleží stisknutím tlačítka "Pick Artist", následně vyberte požadovaného umělce, a následně
přiřaďte albu skladby za pomocí tlačítka "Add". Při výběru skladeb můžete vybrat existující skladby, nebo můžete přidat novou skladbu stisknutím tlačítka "New".
Pořadí skladeb v albu můžete měnit výběrem skladby a stiskem tlačítka s šipkou v požadovaném směru.

#### Úprava dat
Chcete-li upravit existující data v databázi, nalezněte v uživatelském rozhraní požadovaného umělce/album/skladbu a stiskněte tlačítko "Edit". Nyní můžete libovolně upravit
informace o záznamu a následně je uložit stisknutím tlačítka "Save".

#### Poslech
Po přidání dat do databáze naleznete v uživatelském rozhraní přidanou hudbu, kterou si můžete zobrazit dle sekce "Navigace v uživatelském rozhraní". Pro poslech písně stiskněte u požadované písně tlačítko "Play". Pokud máte hudbu zobrazenou v rámci alba, po dohrání skladby se automaticky pustí následující píseň v posloupnosti.

#### Ovládání přehrávání
Ovládání momentálního přehrávání se ovládá pomocí horní sekce uživatelského rozhraní. Při poslechu skladby lze skladbu pozastavit stiskem "Stop" a následně znovu pustit stiskem "Play". Pro přechod do jiné části skladby přesuňte posuvník na požadovanou pozici. Pro úpravu hlasitosti přesuňte posuvník "Volume" na požadovanou polohu. Pro přeskočení písně či přehrání předchozí písně stiskněte tlačítko s šipkami v požadovaném směru.

## Implementace

### Struktura tříd
#### Models
Artist, Album, Song, ImageData, SongData (obsahují vlastnosti odpovídající schématu DB).

#### Data Access (DAO)
ArtistDAO, AlbumDAO, SongDAO, IconImageDAO
#### UI (WinForms)
MusicPlayerWindow, Add/Edit dialogy, komponenty pro ovládání přehrávání.
### Použité návrhové vzory
  - Data Access Object Pattern pro oddělení perzistence.
  - Singleton pro správu globální instance přehrávače a připojení k databázi.
  - Observer / Event-driven pro notifikace stavu přehrávání (play, pause, progress).
### Použité NuGet balíčky
  - NAudio — přehrávání a zpracování audio formátů.
  - Microsoft.Data.SqlClient (nebo System.Data.SqlClient) — připojení k MS SQL Serveru.

## Databáze

- Přehled tabulek
  - image_data, song_data, artists, albums, songs, album_songs, genres, albums_genres (viz SQL skript níže).
- Indexy a jejich dopad na výkon
  - idx_ar_autofill ON artists(ar_name, ar_listening_time DESC)
    - Zrychluje vyhledávání a autocompletion podle jména umělce a zároveň upřednostňuje často poslouchané položky.
  - idx_so_autofill ON songs(so_name, so_listening_time DESC)
    - Podobně zrychluje vyhledávání skladeb a dotazy, které vybírají top dle poslechů.
- Trigger(y)
  - tr_updatelistneningtime ON songs AFTER UPDATE
    - Účel: Po aktualizaci záznamu skladby upraví souhrnné poslechové časy asociovaného umělce (ar_listening_time).
- Views (pohledy)
  - least_popular_song, most_popular_song, least_popular_artist, most_popular_artist, least_popular_album, most_popular_album
    - Poskytují předpřipravené dotazy pro rychlé získání "top" či "bottom" položek bez opakovaného psaní logiky.
    - V kombinaci s indexy a předpočítanými sloupci (např. ar_listening_time, so_listening_time) lze získat výsledky velmi rychle.

## Databázové schéma
Níže je uvedeno databázové schéma.

## SQL skript pro vytvoření databáze
```
USE [musicplayer]

DROP TABLE IF EXISTS album_songs;
DROP TABLE IF EXISTS songs;
DROP TABLE IF EXISTS albums_genres;
DROP TABLE IF EXISTS genres;
DROP TABLE IF EXISTS albums;
DROP TABLE IF EXISTS artists;
DROP TABLE IF EXISTS song_data;
DROP TABLE IF EXISTS image_data;

CREATE TABLE image_data (
    img_id INT IDENTITY(1,1),
    img_data VARBINARY(MAX) NOT NULL,
    
    CONSTRAINT pk_image_data PRIMARY KEY (img_id)
);

CREATE TABLE song_data (
    sd_id INT IDENTITY(1,1),
    sd_data VARBINARY(MAX) NOT NULL,
    
    CONSTRAINT pk_song_data PRIMARY KEY (sd_id)
);

CREATE TABLE artists (
    ar_id INT IDENTITY(1,1),
    ar_name NVARCHAR(100) NOT NULL,
    ar_img_id INT NULL,
    ar_listening_time INT NOT NULL CONSTRAINT df_ar_listening_time DEFAULT (0),

    CONSTRAINT pk_ar PRIMARY KEY (ar_id),
    CONSTRAINT uq_ar_name UNIQUE (ar_name),
    CONSTRAINT ck_ar_name CHECK (LEN(ar_name) >= 3),
    CONSTRAINT fk_ar_img_id FOREIGN KEY (ar_img_id) REFERENCES image_data(img_id) ON DELETE SET NULL,
    CONSTRAINT ck_ar_listening_time CHECK (ar_listening_time >= 0)
);

CREATE TABLE albums (
    alb_id INT IDENTITY(1,1),
    alb_img_id INT NULL,
    alb_ar_id INT NULL,
    alb_name NVARCHAR(100) NOT NULL,
    alb_type NCHAR(2) NOT NULL,
    alb_release_date DATE NOT NULL,
    alb_featured BIT NOT NULL CONSTRAINT df_alb_featured DEFAULT (0),

    CONSTRAINT pk_alb PRIMARY KEY (alb_id),
    CONSTRAINT fk_alb_img_id FOREIGN KEY (alb_img_id) REFERENCES image_data(img_id) ON DELETE SET NULL,
    CONSTRAINT fk_alb_ar_id FOREIGN KEY (alb_ar_id) REFERENCES artists(ar_id) ON DELETE SET NULL,
    CONSTRAINT ck_alb_name CHECK (LEN(alb_name) >= 3),
    CONSTRAINT ck_alb_type CHECK (alb_type IN ('lp', 'ep', 'sp'))
);

CREATE TABLE songs (
    so_id INT IDENTITY(1,1),
    so_sd_id INT NOT NULL,
    so_ar_id INT NULL,
    so_name NVARCHAR(100) NOT NULL,
    so_length INT NOT NULL CONSTRAINT df_so_length DEFAULT (0),
    so_listening_time INT NOT NULL CONSTRAINT df_so_listening_time DEFAULT (0),
    so_rating FLOAT NOT NULL CONSTRAINT df_so_rating DEFAULT (0),

    CONSTRAINT pk_so PRIMARY KEY (so_id),
    CONSTRAINT fk_so_sd_id FOREIGN KEY (so_sd_id) REFERENCES song_data(sd_id) ON DELETE CASCADE,
    CONSTRAINT fk_so_ar_id FOREIGN KEY (so_ar_id) REFERENCES artists(ar_id) ON DELETE SET NULL,
    CONSTRAINT ck_so_name CHECK (LEN(so_name) >= 3),
    CONSTRAINT ck_so_length CHECK (so_length >= 0),
    CONSTRAINT ck_so_listening_time CHECK (so_listening_time >= 0),
    CONSTRAINT ck_so_rating CHECK (so_rating >= 0 AND so_rating <= 5)
);

CREATE TABLE album_songs (
    as_id INT IDENTITY(1,1),
    as_alb_id INT NOT NULL,
    as_so_id INT NOT NULL,
    as_order INT NOT NULL CONSTRAINT df_as_order DEFAULT (0),

    CONSTRAINT pk_as PRIMARY KEY (as_id),
    CONSTRAINT fk_as_alb_id FOREIGN KEY (as_alb_id) REFERENCES albums(alb_id) ON DELETE CASCADE,
    CONSTRAINT fk_as_so_id FOREIGN KEY (as_so_id) REFERENCES songs(so_id) ON DELETE CASCADE,
    CONSTRAINT ck_as_order CHECK (as_order >= 0)
);

CREATE INDEX idx_ar_autofill ON artists (ar_name, ar_listening_time DESC);
CREATE INDEX idx_so_autofill ON songs (so_name, so_listening_time DESC);

GO

CREATE OR ALTER TRIGGER tr_updatelistneningtime ON songs AFTER UPDATE AS
BEGIN
	UPDATE artists SET ar_listening_time = ar_listening_time - deleted.so_listening_time + inserted.so_listening_time FROM artists INNER JOIN inserted ON artists.ar_id = inserted.so_ar_id INNER JOIN deleted ON artists.ar_id = deleted.so_ar_id;
END;
GO

CREATE OR ALTER VIEW least_popular_song AS
SELECT TOP(1) so_name, so_listening_time FROM songs ORDER BY so_listening_time ASC;
GO

CREATE OR ALTER VIEW most_popular_song AS
SELECT TOP(1) so_name, so_listening_time FROM songs ORDER BY so_listening_time DESC;
GO

CREATE OR ALTER VIEW least_popular_artist AS
SELECT TOP(1) ar_name, ar_listening_time FROM artists ORDER BY ar_listening_time ASC;
GO

CREATE OR ALTER VIEW most_popular_artist AS
SELECT TOP(1) ar_name, ar_listening_time FROM artists ORDER BY ar_listening_time DESC;
GO

CREATE OR ALTER VIEW least_popular_album AS
SELECT TOP(1) alb_name, SUM(so_listening_time) AS 'alb_listening_time' FROM albums INNER JOIN album_songs ON alb_id = as_alb_id INNER JOIN songs ON so_id = as_so_id GROUP BY alb_name ORDER BY SUM(so_listening_time) ASC;
GO

CREATE OR ALTER VIEW most_popular_album AS
SELECT TOP(1) alb_name, SUM(so_listening_time) AS 'alb_listening_time' FROM albums INNER JOIN album_songs ON alb_id = as_alb_id INNER JOIN songs ON so_id = as_so_id GROUP BY alb_name ORDER BY SUM(so_listening_time) DESC;
GO
```