set serveroutput on
DECLARE 
  vgame GAMES.GNAME%TYPE;
BEGIN
  SELECT GNAME INTO vgame FROM GAMES WHERE GID=11;
  DBMS_OUTPUT.PUT_LINE('joc: '||vgame||'Size sir:'||LENGTH(vgame));
  DBMS_OUTPUT.PUT_LINE('fin');
  vgame := 'Zumma';
  UPDATE GAMES 
    SET gname = vgame
    WHERE gid = 12;
END;
