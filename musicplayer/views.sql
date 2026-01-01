-- Must be executed separately!

CREATE VIEW least_popular_song AS
SELECT TOP(1) so_name, so_listening_time FROM songs ORDER BY so_listening_time ASC;

CREATE VIEW most_popular_song AS
SELECT TOP(1) so_name, so_listening_time FROM songs ORDER BY so_listening_time DESC;

CREATE VIEW least_popular_artist AS
SELECT TOP(1) ar_name, ar_listening_time FROM artists ORDER BY ar_listening_time ASC;

CREATE VIEW most_popular_artist AS
SELECT TOP(1) ar_name, ar_listening_time FROM artists ORDER BY ar_listening_time DESC;