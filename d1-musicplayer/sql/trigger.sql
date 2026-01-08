CREATE TRIGGER tr_updatelistneningtime ON songs AFTER UPDATE AS
BEGIN
	UPDATE artists SET ar_listening_time = ar_listening_time - deleted.so_listening_time + inserted.so_listening_time FROM artists INNER JOIN inserted ON artists.ar_id = inserted.so_ar_id INNER JOIN deleted ON artists.ar_id = deleted.so_ar_id;
END;