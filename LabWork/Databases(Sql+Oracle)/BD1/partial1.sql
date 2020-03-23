set serveroutput on;
CREATE OR REPLACE PROCEDURE test2 AS
  
      CURSOR taxcurs (c_cat contribuabili.categoria%TYPE, c_cod contribuabili.cod%TYPE) IS
      select T.cod
      from taxe T
      where T.categoria=c_cat
      and not T.suma<=sums(c_cod, T.cod)
      and eincasari(c_cod, T.cod)=1; 
      
    
    CURSOR neachit IS
      select *
      from contribuabili C;
      
    vneachit neachit%ROWTYPE;
    vtax taxcurs%ROWTYPE;



BEGIN
DBMS_OUTPUT.PUT_LINE('Contribuabilii ce au achitat toate taxele din categoria lor sunt:');
DBMS_OUTPUT.PUT_LINE('COD            NUME');
DBMS_OUTPUT.PUT_LINE('---------------------');
                                   
OPEN neachit;
LOOP
  
  FETCH neachit INTO vneachit;
  EXIT WHEN neachit%NOTFOUND;
  
  OPEN taxcurs(vneachit.categoria, vneachit.cod);
  FETCH taxcurs INTO vtax;
  IF taxcurs%NOTFOUND
    THEN DBMS_OUTPUT.PUT_LINE(vneachit.cod||'           '||vneachit.nume);
  END IF;
  CLOSE taxcurs;

END LOOP;
  
END test2;
/
CALL test2();