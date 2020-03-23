g(X, Y) :- g(X, 0, Y).
g(0,A,A).

g(X,A,R):-
	1 is mod(X,2),
	Y is X-1,
	g(Y,A,R).

g(X,A,R) :-
	0 is mod(X,2),
        Y is X - 1,
        A1 is A + X,
        g(Y,A1,R).

rel(a1,a2).
rel(a2,a3).
rel(a3,a4).
rel(a4,a5).
rel(a5,a6).
rel(a6,a1).

min(X,Y,R):-
	X<Y,
	R is X.
min(X,Y,R):-
	X>=Y,
	R is Y.

relnr(X,X,0).
relnr(X,Y,1):-
	rel(X,Y).

relnr(X,Y,R):-
	rel(X,X1),
	relnr(X1,Y,R1),
	R is R1+1.

relnrB(X,Y,RB):-
	relnr(X,Y,R1),
	relnr(Y,X,R2),
	min(R1,R2,RB).


