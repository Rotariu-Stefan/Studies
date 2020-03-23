DROP TABLE art CASCADE CONSTRAINTS
/
CREATE TABLE art
(pk_artist_id       NUMBER(4)       CONSTRAINT pk_artist_id PRIMARY KEY,
 artist_name        VARCHAR2(30)    NOT NULL CONSTRAINT artist_name UNIQUE,
 artist_dob         DATE,                                               -- date of birth
 artist_genre       VARCHAR(15),
 artist_pal         NUMBER(2)       CHECK(artist_pal BETWEEN 1 AND 64), -- published albums
 artist_yoa         NUMBER(2)                                           -- years of activity
)
/
DROP TABLE alb CASCADE CONSTRAINTS
/
CREATE TABLE alb
(fk_alb_artist_id   NUMBER(4)       NOT NULL CONSTRAINT fk_alb_artist_id REFERENCES art(pk_artist_id),
 pk_album_id        NUMBER(4)       CONSTRAINT pk_album_id PRIMARY KEY,
 album_name         VARCHAR2(30)    NOT NULL,
 album_year         NUMBER(4), 
 album_gen          VARCHAR2(15),
 album_nrt          NUMBER(2)       CHECK(album_nrt BETWEEN 1 AND 40),  -- number of tracks
 album_sin          VARCHAR2(30)                                        -- single?
)
/
DROP TABLE mel CASCADE CONSTRAINTS
/
CREATE TABLE mel
(track_id           NUMBER(4)       CONSTRAINT track_id PRIMARY KEY,
 track_name         VARCHAR2(30)    NOT NULL,
 fk_mel_artist_id   NUMBER(4)       CONSTRAINT fk_mel_artist_id REFERENCES art(pk_artist_id),
 fk_album_id        NUMBER(4)       CONSTRAINT fk_album_id      REFERENCES alb(pk_album_id),
 track_genre        VARCHAR2(15),
 track_year         NUMBER(4),
 track_length       NUMBER(6)       NOT NULL CHECK(track_length > 1)			
);