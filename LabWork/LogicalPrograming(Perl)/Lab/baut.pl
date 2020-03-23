%% Demo coming from http://clwww.essex.ac.uk/course/LG519/2-facts/index_18.html

drinks(tom,Drink) :-
        acid(Drink),
        negru(Drink).

drinks(tom,Drink) :-
        galben(Drink),
	acid(Drink).

drinks(tom,Drink) :-
        fruct(Drink),
	not(galben(Drink)),
	not(verde(Drink)).

drinks(tom,Drink) :-
	incolor(Drink),
	not(Drink=apa).



vertical(line(point(X,Z),point(X,Y))):-
	not(Z=Y).
horizontal(line(point(X,Y),point(Z,Y))):-
	not(X=Z).


negru(cola).
negru(pepsi).
negru(cafea).

galben(portocale).
galben(bere).

verde(kiwi).

incolor(apa).
incolor(sprite).
incolor(tzuica).

acid(cola).
acid(pepsi).
acid(md).
acid(sprite).
acid(bere).

fruct(portocale).
fruct(kiwi).
fruct(mere).




