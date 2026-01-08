--USE [musicplayer]

DROP TABLE IF EXISTS album_songs;
DROP TABLE IF EXISTS songs;
DROP TABLE IF EXISTS albums_genres;
DROP TABLE IF EXISTS genres;
DROP TABLE IF EXISTS albums;
DROP TABLE IF EXISTS artists;
DROP TABLE IF EXISTS song_data;
DROP TABLE IF EXISTS image_data;

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
	ar_name NVARCHAR(100) NOT NULL UNIQUE CHECK(LEN(ar_name) >= 3),
	ar_img_id INT NULL FOREIGN KEY REFERENCES image_data(img_id) ON DELETE SET NULL,
	ar_listening_time INT NOT NULL DEFAULT(0) CHECK(ar_listening_time >= 0) -- Denormalized attributed calculated based on a trigger
);

CREATE TABLE albums (
	alb_id INT IDENTITY(1,1) PRIMARY KEY,
	alb_img_id INT NULL FOREIGN KEY REFERENCES image_data(img_id) ON DELETE SET NULL,
	alb_ar_id INT NULL FOREIGN KEY REFERENCES artists(ar_id) ON DELETE SET NULL,
	alb_name NVARCHAR(100) NOT NULL CHECK(LEN(alb_name) >= 3),
	alb_type NCHAR(2) NOT NULL CHECK(alb_type IN ('lp', 'ep', 'sp')),
	alb_release_date DATE NOT NULL,
	alb_featured BIT NOT NULL DEFAULT(0)
);

CREATE TABLE songs (
	so_id INT IDENTITY(1,1) PRIMARY KEY,
	so_sd_id INT NOT NULL FOREIGN KEY REFERENCES song_data(sd_id) ON DELETE CASCADE,
	so_ar_id INT NULL FOREIGN KEY REFERENCES artists(ar_id) ON DELETE SET NULL,
	so_name NVARCHAR(100) NOT NULL CHECK(LEN(so_name) >= 3),
	so_length INT NOT NULL DEFAULT(0) CHECK(so_length >= 0),
	so_listening_time INT NOT NULL DEFAULT(0) CHECK(so_listening_time >= 0),
	so_rating FLOAT NOT NULL DEFAULT(0) CHECK(so_rating >= 0 AND so_rating <= 5)
);

CREATE TABLE album_songs (
	as_id INT IDENTITY(1,1) PRIMARY KEY,
	as_alb_id INT NOT NULL FOREIGN KEY REFERENCES albums(alb_id) ON DELETE CASCADE,
	as_so_id INT NOT NULL FOREIGN KEY REFERENCES songs(so_id) ON DELETE CASCADE,
	as_order INT NOT NULL DEFAULT(0) CHECK(as_order >= 0)
);

CREATE INDEX idx_autofill ON artists (ar_name, ar_listening_time DESC);