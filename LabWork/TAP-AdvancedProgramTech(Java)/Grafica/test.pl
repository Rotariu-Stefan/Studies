:-dynamic loc/2.
loc(0,0).

size(7,7).

pt(a,1,1).  
pt(b,1,2).  
pt(c,1,3).  
pt(a,1,4).  
pt(a,1,5). 
pt(c,1,6).  
pt(c,1,7).
pt(b,2,1).
pt(c,2,2).
pt(c,2,3).
pt(b,2,4).
pt(c,2,5).
pt(a,2,6).
pt(b,2,7).
pt(c,3,1).
pt(a,3,2).
pt(b,3,3).
pt(c,3,4).
pt(c,3,5).
pt(b,3,6).
pt(b,3,7).
pt(a,4,1).
pt(b,4,2).
pt(c,4,3).
pt(b,4,4).
pt(a,4,5).
pt(c,4,6). 
pt(a,4,7).  
pt(a,5,1).  
pt(c,5,2).  
pt(b,5,3).  
pt(c,5,4).  
pt(a,5,5).  
pt(b,5,6).  
pt(c,5,7).
pt(b,6,1).
pt(c,6,2).
pt(a,6,3).
pt(a,6,4).
pt(a,6,5).
pt(c,6,6).
pt(a,6,7).
pt(a,7,1).
pt(a,7,2).
pt(b,7,3).
pt(b,7,4).
pt(c,7,5).
pt(a,7,6).
pt(b,7,7).


drum(A,B):-
	A=a,
	B=b.
drum(A,B):-
	A=b,
	B=c.
drum(A,B):-
	A=c,
	B=a.
mers(X,Y):-
	pt(V,X,Y),
	loc(X1,Y1),
	pt(V1,X1,Y1),
	drum(V1,V).

mazehelp:-
	write('Regula jocului este a->b->c->a->b->c\n'),
	write('Se navigheaza folosind predicatele "stanga.","dreapta","sus","jos"\n'),
	write('De fiecare data cand te mishti(conform regulei) se afiseaza un mesaj cu\n'),
	write('noile coordonate si valoarea de la aceste coordonate\n'),
	write('Altfel vei vedea un mesaj k nu potzi merge in directia aia\n'),
	write('Jocul se termina cand ajunga la locatzia (7,7)cand o sa primeshti si o felicitare\n'),
	write('Noroc! :P\n').

start(R):-
	retractall(loc(_,_)),
	assert(loc(1,1)),
	pt(X,1,1),
	R=joc_inceput(loc_curent(1,1),valoare(X)).


dr(R):-
	loc(X,Y),
	Y1 is Y+1,
	mers(X,Y1),
	retract(loc(X,Y)),
	assert(loc(X,Y1)),
	pt(V,X,Y1),
	(loc(7,7),
        R='Felicitari';
        R=locatie(noua_pozitzie(X,Y1),valoare(V))).
dr(R):-
	loc(X,Y),
	Y1 is Y+1,
	not(mers(X,Y1)),
	R='Nu se poate merge acolo!'.
dreapta(R):-
	dr(R).


stg(R):-
	loc(X,Y),
	Y1 is Y-1,
	mers(X,Y1),
	retract(loc(X,Y)),
	assert(loc(X,Y1)),
	pt(V,X,Y1),
	R=locatie(noua_pozitzie(X,Y1),valoare(V)).

stg(R):-
	loc(X,Y),
	Y1 is Y-1,
	not(mers(X,Y1)),
	R='Nu se poate merge acolo!'.
stanga(R):-
	stg(R).




js(R):-
	loc(X,Y),
	X1 is X+1,
	mers(X1,Y),
	retract(loc(X,Y)),
	assert(loc(X1,Y)),
	pt(V,X1,Y),
	R=locatie(noua_pozitzie(X1,Y),valoare(V)).
js(R):-
	loc(X,Y),
	X1 is X+1,
	not(mers(X1,Y)),
	R='Nu se poate merge acolo!'.
jos(R):-
	js(R).



ss(R):-
	loc(X,Y),
	X1 is X-1,
	mers(X1,Y),
	retract(loc(X,Y)),
	assert(loc(X1,Y)),
	pt(V,X1,Y),
	R=locatie(noua_pozitzie(X1,Y),valoare(V)).
ss(R):-
	loc(X,Y),
	X1 is X-1,
	not(mers(X1,Y)),
	R='Nu se poate merge acolo!'.
sus(R):-
	ss(R).

