# Music Player

## Úvod
Music Player je program umožňující spravování a přehrávání MP3 hudby v SQL Server databázi. Projekt je vytvořen v jazyce C# za využití grafické technologie WinForms.

## Technické požadavky
- Windows 10/11
- SQL Server instance

## Manuál

### Databáze
Nejprve je potřeba vytvořit SQL Server databázi pomocí skriptu uvedeného v souboru "db.sql". Tento skript naleznete i na konci tohoto dokumentu.
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

## SQL skript pro vytvoření databáze
```
CREATE TABLE image_data (
	img_id INT IDENTITY(1,1) PRIMARY KEY,
	img_data VARBINARY(MAX) NOT NULL
);

CREATE TABLE song_data (
	sd_id INT IDENTITY(1,1) PRIMARY KEY,
	sd_data VARBINARY(MAX) NOT NULL
);

CREATE TABLE artists (
	ar_id INT IDENTITY(1,1) PRIMARY KEY,
	ar_name NVARCHAR(500) NOT NULL,
	ar_img_id INT NULL FOREIGN KEY REFERENCES image_data(img_id) ON DELETE SET NULL
);

CREATE TABLE albums (
	alb_id INT IDENTITY(1,1) PRIMARY KEY,
	alb_img_id INT NULL FOREIGN KEY REFERENCES image_data(img_id) ON DELETE SET NULL,
	alb_ar_id INT NULL FOREIGN KEY REFERENCES artists(ar_id) ON DELETE SET NULL,
	alb_name NVARCHAR(500) NOT NULL,
);

CREATE TABLE genres (
	ge_id INT IDENTITY(1,1) PRIMARY KEY,
	ge_name NVARCHAR(500) NOT NULL,
);

CREATE TABLE albums_genres (
	ag_id INT IDENTITY(1,1) PRIMARY KEY,
	ag_alb_id INT NOT NULL FOREIGN KEY REFERENCES albums(alb_id) ON DELETE CASCADE,
	ag_ge_id INT NOT NULL FOREIGN KEY REFERENCES genres(ge_id) ON DELETE CASCADE
);

CREATE TABLE songs (
	so_id INT IDENTITY(1,1) PRIMARY KEY,
	so_sd_id INT NOT NULL FOREIGN KEY REFERENCES song_data(sd_id) ON DELETE CASCADE,
	so_name NVARCHAR(500) NOT NULL,
	so_length INT NOT NULL,
);

CREATE TABLE album_songs (
	as_id INT IDENTITY(1,1) PRIMARY KEY,
	as_alb_id INT NOT NULL FOREIGN KEY REFERENCES albums(alb_id) ON DELETE CASCADE,
	as_so_id INT NOT NULL FOREIGN KEY REFERENCES songs(so_id) ON DELETE CASCADE,
	as_order INT NOT NULL
);
```