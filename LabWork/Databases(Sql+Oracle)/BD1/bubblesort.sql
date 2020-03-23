set serveroutput on


DECLARE

  TYPE vector IS VARRAY(11) OF games.cost%TYPE;
  ve vector;

  CURSOR costs IS
    select cost
    from games;
  vcosts games.cost%TYPE;
  
  i INTEGER:=1;
  sorted INTEGER:=0;

BEGIN

  ve:=vector(0,0,0,0,0,0,0,0,0,0,0);
  OPEN costs;
  

  LOOP
  
    FETCH costs INTO vcosts;
    ve(i):=vcosts;
    DBMS_OUTPUT.PUT_LINE(ve(i)||'  ');
    i:=i+1;
    EXIT WHEN i>11;
    
  END LOOP;
  
  WHILE sorted=0
  LOOP
    sorted:=1;
    FOR i IN 1..10
    LOOP
      IF ve(i)>ve(i+1) THEN
        sorted:=0;
        vcosts:=ve(i);
        ve(i):=ve(i+1);
        ve(i+1):=vcosts;
      END IF;
    END LOOP;
  
  
  END LOOP;

  DBMS_OUTPUT.PUT_LINE(' ------------ ');
  FOR i IN 1..11 LOOP
    DBMS_OUTPUT.PUT_LINE(ve(i)||'  ');
  END LOOP;


END;