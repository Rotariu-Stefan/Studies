p(a). p(b). p(c).
sunt_p([]).
sunt_p([H|T]) :-
	p(H),
	sunt_p(T).


nodups(X,X).

nodups([H|T],L):-
	T=[H1|T1],
	H=H1,
	nodups([H1|T1],L).

nodups([H|T],L):-
	T=[H1|T1],
	not(H=H1),
	L=[H|L],
	nodups([H1|T1],L).

