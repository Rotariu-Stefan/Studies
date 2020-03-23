-- Se da ca intrare ID-ul unui joc.
-- Sa se afiseze toate jocurile care au aceleasi caracteristici generale cu jocul dat(tipul, tipul platii, multiplayer).
-- Sa se afiseze(separat) jocurile produse de acelasi producator(cu cel al jocului dat).

set serveroutput on
set verify off
accept s_gid number format '999' prompt 'Scrie ID-ul unui joc: '

DECLARE
  
  invalid_entry EXCEPTION;
  PRAGMA EXCEPTION_INIT(invalid_entry, -06550);
  
  game_repeat EXCEPTION;
  
  v_gid games.gid%TYPE;
  v_pname prod.pname%TYPE;
  
  TYPE rec_game IS RECORD(
    v_id games.gname%TYPE,
    v_name games.gname%TYPE);
  v_gname rec_game;

  TYPE rec_carac IS RECORD (
    v_multi games.multi%TYPE,
    v_tcost games.tcost%TYPE,
    v_type  games.type%TYPE);
  v_rec_carac rec_carac;
    
  CURSOR cc(p_rec rec_carac) IS
    SELECT gid, gname
    FROM games
    WHERE type=p_rec.v_type AND tcost=p_rec.v_tcost AND multi=p_rec.v_multi AND NOT gid=v_gid;
  
  CURSOR cp IS
    SELECT g.gid, g.gname 
    FROM prod p, games g
    WHERE p.pid=g.pid AND p.pname=v_pname AND NOT g.gid=v_gid;
  
BEGIN

  v_gid:=&s_gid;
  
  IF v_gid<1 OR v_gid>11 THEN 
    RAISE_APPLICATION_ERROR (-20001,'ERR:Invalid ID de joc!');
  END IF;
  
  SELECT multi, tcost, type
  INTO v_rec_carac
  FROM games
  WHERE gid=v_gid;
  
  OPEN cc(v_rec_carac);
  FETCH cc INTO v_gname;
  
  IF v_gid=v_gname.v_id THEN
    RAISE game_repeat;
  END IF;
  IF cc%NOTFOUND THEN
    DBMS_OUTPUT.PUT_LINE('Nu exista mai exista jocuri cu aceleasi atribute!');
  ELSE
    IF v_gid=v_gname.v_id THEN
      RAISE game_repeat;
    END IF;
    DBMS_OUTPUT.PUT_LINE('Jocurile ce au aceleasi atribute sunt:');
    LOOP
      DBMS_OUTPUT.PUT_LINE(v_gname.v_id||'  '||v_gname.v_name);
      FETCH cc INTO v_gname;
      EXIT WHEN cc%NOTFOUND;
    END LOOP;
  END IF;
  CLOSE cc;
  
  SELECT p.pname
  INTO v_pname
  FROM prod p, games g
  WHERE p.pid=g.pid AND g.gid=v_gid;
  
  OPEN cp;
  FETCH cp INTO v_gname;
  
  IF v_gid=v_gname.v_id THEN
    RAISE game_repeat;
  END IF;  
  IF cp%NOTFOUND THEN
    DBMS_OUTPUT.PUT_LINE('Nu exista jocuri cu acelasi producator('||v_pname||') !');
  ELSE
    IF v_gid=v_gname.v_id THEN
      RAISE game_repeat;
    END IF;
    DBMS_OUTPUT.PUT_LINE('Jocurile ce au acelasi producator( '||v_pname||') sunt:');
    LOOP
      DBMS_OUTPUT.PUT_LINE(v_gname.v_id||'  '||v_gname.v_name);
      FETCH cp INTO v_gname;
      EXIT WHEN cp%NOTFOUND;
    END LOOP;
  END IF;
  CLOSE cp;

EXCEPTION

  WHEN NO_DATA_FOUND THEN
    DBMS_OUTPUT.PUT_LINE( 'ERR:Nu exista acest joc!' );
  WHEN CURSOR_ALREADY_OPEN THEN
    DBMS_OUTPUT.PUT_LINE( 'ERR:Cursorul este deja deschis!');
  WHEN INVALID_CURSOR THEN
    DBMS_OUTPUT.PUT_LINE( 'ERR:Cursor invalid!');
  
  WHEN game_repeat THEN
    DBMS_OUTPUT.PUT_LINE(' ERR:Jocurile afisare trebuie sa fie diferite de cel intrat!');
  WHEN invalid_entry THEN
    DBMS_OUTPUT.PUT_LINE(' ERR:Nu a fost gasit un identificator!');
    
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE( TO_CHAR( SQLCODE ) || ' ' || SQLERRM );

END;