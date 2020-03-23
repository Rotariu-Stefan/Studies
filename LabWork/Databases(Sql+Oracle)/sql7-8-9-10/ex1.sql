--1.Afisati top 3 albume cu cele mai multe melodii si numarul melodiilor, care au genul acelasi cu cel al unui album introdus de la tastatura
set serveroutput on
set verify off
accept s_name char prompt 'Introduceti numele albumului: '

DECLARE

	e_fara_rezultate EXCEPTION;
	v_nume alb.album_name%TYPE;
	v_nrt alb.album_nrt%TYPE;
	v_gen alb.album_gen%TYPE;

CURSOR c_albume IS
SELECT album_name, album_nrt
FROM alb
WHERE album_gen=v_gen
ORDER BY album_nrt DESC;

BEGIN 
	BEGIN      
      		SELECT album_gen
      		INTO v_gen
      		FROM alb
      		WHERE album_name='&s_name';
     		
		EXCEPTION
      		WHEN NO_DATA_FOUND THEN
        		RAISE_APPLICATION_ERROR (-20201,'Albumul nu exista in tabel!');
	END;

	OPEN c_albume;
	LOOP
		FETCH c_albume INTO v_nume, v_nrt;
		EXIT WHEN c_albume%ROWCOUNT > 3 OR c_albume%NOTFOUND;
		dbms_output.put_line (v_nume||' cu '||v_nrt||' melodii ');
	END LOOP;

	IF c_albume%ROWCOUNT = 0 THEN
		RAISE e_fara_rezultate;
	END IF;
	CLOSE c_albume;
  
	EXCEPTION
		WHEN CURSOR_ALREADY_OPEN THEN
			dbms_output.put_line( 'Eroare! cursor deja deschis' );
		WHEN e_fara_rezultate THEN
			dbms_output.put_line( 'Nu sunt albume de genul albumului introdus' );
END;