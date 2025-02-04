%%http://www.utgjiu.ro/math/mbuneci/book/exp/cap04.pdf

suma(nr(X,Y),rez(Z)):-
	Z is X+Y.

diferenta(nr(X,Y),rez(Z)):-
	Z is X-Y.

produs(nr(X,Y),rez(Z)):-
	Z is X*Y.

impartire(nr(X,Y),rez(Z)):-
	Z is X/Y.

paritate(nr(X),par):-
	0 is mod(X,2).
paritate(nr(X),impar):-
	1 is mod(X,2).

operatii(nr(X,Y), produs(R)):-
	produs(nr(X,Y),rez(R)).
operatii(nr(X,Y),suma(R)):-
	suma(nr(X,Y),rez(R)).
operatii(nr(X,Y),diferenta(R)):-
	diferenta(nr(X,Y),rez(R)).
operatii(nr(X,Y),impartire(R)):-
	impartire(nr(X,Y),rez(R)).
operatii(nr(X,_),paritate(X,R)):-
	paritate(nr(X),R).
operatii(nr(_,Y),paritate(Y,R)):-
	paritate(nr(Y),R).

