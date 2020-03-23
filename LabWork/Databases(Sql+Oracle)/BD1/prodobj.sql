set serveroutput on
--DROP TABLE t_oprod;

CREATE OR REPLACE TYPE oprod AS OBJECT (
  
  opid NUMBER(3,0),
  opname CHAR(15 BYTE),
  oceo CHAR(15 BYTE),
  oworth NUMBER(10,0),
  
  MAP MEMBER FUNCTION getworth RETURN NUMBER,
  MEMBER PROCEDURE display );
/  
CREATE OR REPLACE TYPE BODY oprod AS
  MAP MEMBER FUNCTION getworth RETURN NUMBER IS
  BEGIN
    RETURN oworth;
  END;
  
  MEMBER PROCEDURE display IS
  BEGIN
    DBMS_OUTPUT.PUT_LINE('Producator:'||opid);
    DBMS_OUTPUT.PUT_LINE('Nume:'||opname);
    DBMS_OUTPUT.PUT_LINE('Lider:'||oceo);
    DBMS_OUTPUT.PUT_LINE('Valoare:'||oworth);
  END;
END;
/
CREATE TABLE t_oprod
(pr oprod);
DECLARE
  
  p1 oprod;
  CURSOR cprod IS
    select *
    from prod;
  v_cprod cprod%ROWTYPE;
  
BEGIN

  OPEN cprod;
  
  LOOP
    FETCH cprod INTO v_cprod;
    EXIT WHEN cprod%NOTFOUND;
    p1 := oprod(v_cprod.pid, v_cprod.pname, v_cprod.ceo, v_cprod.worth);
    
    INSERT INTO t_oprod VALUES(p1);
  END LOOP;
  
  CLOSE cprod;
  
END;





