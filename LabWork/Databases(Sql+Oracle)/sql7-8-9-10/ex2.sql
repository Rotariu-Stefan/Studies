--2.Sa se afiseze acei artisti care au mai multi ani de activitate decat media, dar mai putini decat un numar introdus de la tastatura
set serveroutput on
set verify off
accept s_yoa number format '99' prompt 'Introduceti numarul de ani: '

DECLARE

	e_yoa_mediu EXCEPTION;
	e_fara_rezultate EXCEPTION;
	v_yoa_mediu art.artist_yoa%TYPE;
	v_yoa art.artist_yoa%TYPE;
	v_nume art.artist_name%TYPE;
	
CURSOR c_artisti IS
SELECT artist_name,artist_yoa
FROM art
WHERE artist_yoa<&s_yoa and artist_yoa>v_yoa_mediu;
   
BEGIN
	SELECT AVG(artist_yoa)
	INTO v_yoa_mediu
	FROM art;

	IF v_yoa_mediu>&s_yoa THEN
		RAISE e_yoa_mediu;
	END IF;

	OPEN c_artisti;
	LOOP
		FETCH c_artisti INTO v_nume,v_yoa;
		EXIT WHEN c_artisti%NOTFOUND;
		dbms_output.put_line(v_nume||' are '||v_yoa||' ani de activitate.');
	END LOOP;
   
	IF c_artisti%ROWCOUNT = 0 THEN
		RAISE e_fara_rezultate;
	END IF;
	CLOSE c_artisti;

	EXCEPTION 
		WHEN CURSOR_ALREADY_OPEN THEN 
			dbms_output.put_line( 'Eroare! cursor deja deschis' );
		WHEN e_yoa_mediu THEN 
			dbms_output.put_line('Numarul introdus este mai mic decat media!');
		WHEN e_fara_rezultate THEN 
			dbms_output.put_line('Nu sunt rezultate');
END;



 