-- Se da ca intrare ID-ul unui joc shi un numar (X).
-- Sa se calculeze de cate ori ar trebui sa se repete vanzarile jocului respectiv
--    pentru a ajunge la suma de X.

set serveroutput on
set verify off
accept s_gid number format '999' prompt 'Scrie ID-ul unui joc: '
accept s_sum number format '9999' prompt 'Scrie suma necesara: '

DECLARE

  v_gid games.gid%TYPE;
  v_sum NUMBER;
  v_valvanz NUMBER;
  v_nmr NUMBER;
  v_tmp NUMBER;

BEGIN

  v_nmr:=0;
  v_sum:=&s_sum;
  
  SELECT gid
  INTO v_gid
  FROM games
  WHERE gid=&s_gid;
  
  SELECT NVL(SUM(s.cant*g.cost),0)
  INTO v_valvanz
  FROM games g, sales s
  WHERE s.gid=g.gid AND g.gid=v_gid;
  
  v_tmp:=v_valvanz;
  
  IF v_sum<=v_tmp THEN
    DBMS_OUTPUT.PUT_LINE('Vanzarile depasesc valoarea ceruta!');
  ELSE
    LOOP
      v_nmr:=v_nmr+1;
      v_tmp:=v_tmp+v_valvanz;
      EXIT WHEN v_sum<=v_tmp;
    END LOOP;
    DBMS_OUTPUT.PUT_LINE('Vanzarile trebuie sa creasca de '||v_nmr||' ori!');
  END IF;
      

EXCEPTION

  WHEN NO_DATA_FOUND THEN
    DBMS_OUTPUT.PUT_LINE( 'ERR:Nu exista acest joc!' );
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE( TO_CHAR( SQLCODE ) || ' ' || SQLERRM );

END;