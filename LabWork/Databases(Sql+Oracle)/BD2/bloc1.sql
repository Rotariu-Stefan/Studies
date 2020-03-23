-- Se da ca intrare ID-ul unui producator.
-- Verifica daca valoarea sumei totala a vanzarilor jocurilor unui producator
--    reprezinta macar 10% din valoare acestuia.
-- Daca nu, calculeaza cu cat ar trebui sa creasca vanzarile pentru a fi adevarat.

set serveroutput on
set verify off
accept s_pid number format '999' prompt 'Scrie ID-ul unui producator: '

DECLARE

  v_pid prod.pid%TYPE;
  v_valvanz prod.worth%TYPE;
  v_worth prod.worth%TYPE;

BEGIN

  SELECT pid, worth
  INTO v_pid, v_worth
  FROM prod
  WHERE pid=&s_pid;
  
  SELECT NVL(SUM(s.cant*g.cost),0)
  INTO v_valvanz
  FROM games g, sales s
  WHERE s.gid=g.gid AND g.pid=v_pid AND g.tcost=1;
  
  v_worth:=v_worth/10;
  
  IF v_valvanz=0 THEN
    DBMS_OUTPUT.PUT_LINE('Nu exista vanzari!');
  ELSE 
    IF v_valvanz>=v_worth THEN
         DBMS_OUTPUT.PUT_LINE('DA!');
    ELSE
         DBMS_OUTPUT.PUT_LINE('NU! Vanzarile trebuie sa creasca cu: '|| (v_worth-v_valvanz));
    END IF;
  END IF;
          
EXCEPTION

  WHEN NO_DATA_FOUND THEN
    DBMS_OUTPUT.PUT_LINE( 'ERR:Nu exista acest producator!' );
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE( TO_CHAR( SQLCODE ) || ' ' || SQLERRM );

END;