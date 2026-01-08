USE [musicplayer]

DELETE FROM albums;

INSERT INTO albums (alb_img_id, alb_ar_id, alb_name) VALUES (2, 1, 'Album1');
INSERT INTO albums (alb_img_id, alb_ar_id, alb_name) VALUES (1, 1, 'Album2');
INSERT INTO albums (alb_img_id, alb_ar_id, alb_name) VALUES (2, 1, 'Album3');

INSERT INTO albums (alb_img_id, alb_ar_id, alb_name) VALUES (1, 2, 'Album4');
INSERT INTO albums (alb_img_id, alb_ar_id, alb_name) VALUES (2, 2, 'Album5');
INSERT INTO albums (alb_img_id, alb_ar_id, alb_name) VALUES (1, 2, 'Album6');