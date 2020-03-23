-- Se da ca intrare un ID de producator si un numar (X).
-- Cresteti/scadeti costul tuturor jocurilor vandute de producatorul respectiv cu X.
-- Apoi calculati valoare totala rezultata din vanzarea a cate 10 jocuri(din fiecare) printr-o functie.
--      Daca aceasta valoare este peste 600 cresteti worth-ul cu 10%, daca nu scadeti. 
-- Sa se afiseze schimbarile in tabele.

set serveroutput on
set verify off
accept s_pid number format '999' prompt 'Scrie ID-ul unui producator: '
accept s_nmr number format '999' prompt 'Scrie numarul: '

DECLARE

  v_pid prod.pid%TYPE;
  v_nmr NUMBER(3);
  v_incdec NUMBER(3);
  v_worth prod.worth%TYPE;
  
  FUNCTION calc_sales(p_nmr NUMBER, p_pid prod.pid%TYPE) RETURN NUMBER IS
      v_suma NUMBER(6):=0;
      v_cost games.cost%TYPE;
      
      CURSOR cc IS
        SELECT g.cost
        FROM games g, prod p
        WHERE p.pid=g.pid AND p.pid=p_pid;
     
    BEGIN
    
        OPEN cc;
        FETCH cc INTO v_cost;
  
        IF cc%NOTFOUND THEN
          DBMS_OUTPUT.PUT_LINE('Nu exista jocuri vandute de acest producator !');
          RETURN 0;
        ELSE
          LOOP
            v_suma:=v_suma+(p_nmr*v_cost);
            FETCH cc INTO v_cost;
            EXIT WHEN cc%NOTFOUND;
          END LOOP;
        END IF;
  
        CLOSE cc;
        RETURN v_suma;      
    END;

BEGIN

  v_pid:=&s_pid;
  v_nmr:=&s_nmr;
  
  DBMS_OUTPUT.PUT_LINE('Valoare vanzari inainte de modificari:'||calc_sales(10,v_pid));
  UPDATE games
    SET cost=cost+v_nmr
    WHERE pid=v_pid;
  DBMS_OUTPUT.PUT_LINE('Valoare vanzari dupa:'||calc_sales(10,v_pid));
  
  IF calc_sales(10,v_pid)>=600 THEN
    v_incdec:=110;
  ELSE
    v_incdec:=90;
  END IF;
  
  SELECT worth
  INTO v_worth
  FROM prod
  WHERE pid=v_pid;
  DBMS_OUTPUT.PUT_LINE('Valoare worth inainte de a fi schimbata:'||v_worth);
  
  UPDATE prod
    SET worth=worth*v_incdec/100
    WHERE pid=v_pid;
  
  SELECT worth
  INTO v_worth
  FROM prod
  WHERE pid=v_pid;
  DBMS_OUTPUT.PUT_LINE('Valoare worth dupa schimbare:'||v_worth);

EXCEPTION

  WHEN NO_DATA_FOUND THEN
    dbms_output.put_line( 'Nu exista acest producator!' );
  WHEN OTHERS THEN
    dbms_output.put_line( TO_CHAR( SQLCODE ) || ' ' || SQLERRM );

END;