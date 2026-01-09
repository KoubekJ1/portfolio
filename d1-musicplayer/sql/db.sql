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

CREATE INDEX idx_autofill ON artists (ar_name, ar_listening_time DESC);

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