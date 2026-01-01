-- Must be executed separately!

CREATE VIEW least_popular_song AS
SELECT TOP(1) so_name, so_listening_time FROM songs ORDER BY so_listening_time ASC;

CREATE VIEW most_popular_song AS
SELECT TOP(1) so_name, so_listening_time FROM songs ORDER BY so_listening_time DESC;

CREATE VIEW least_popular_artist AS
SELECT TOP(1) ar_name, ar_listening_time FROM artists ORDER BY ar_listening_time ASC;

CREATE VIEW most_popular_artist AS
SELECT TOP(1) ar_name, ar_listening_time FROM artists ORDER BY ar_listening_time DESC;

CREATE VIEW least_popular_album AS
SELECT TOP(1) alb_name, SUM(so_listening_time) AS 'album_listening_time' FROM albums INNER JOIN album_songs ON alb_id = as_alb_id INNER JOIN songs ON so_id = as_so_id GROUP BY alb_name ORDER BY SUM(so_listening_time) ASC;

CREATE VIEW most_popular_album AS
SELECT TOP(1) alb_name, ar_name, SUM(so_listening_time) AS 'album_listening_time' FROM albums INNER JOIN album_songs ON alb_id = as_alb_id INNER JOIN songs ON so_id = as_so_id GROUP BY alb_name ORDER BY SUM(so_listening_time) DESC;