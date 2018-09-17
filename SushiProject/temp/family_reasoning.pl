parentOf(A, B) :- hasChild(A, B).
motherOf(A, B) :- hasChild(A, B), female(A).
fatherOf(A, B) :- parentOf(A, B), male(A).
grandparentOf(A, B) hasChild(A, C), hasChild(C, B).
grandmotherOf(A, B) hasChild(A, C), hasChild(C, B), male(A).
grandfatherOf(A, B) hasChild(A, C), hasChild(C, B), female(A).
greatgrandparentOf(A, B) hasChild(A, C), hasChild(C, B).
greatgrandmotherOf(A, B) hasChild(A, C), hasChild(C, B), male(A).
greatgrandfatherOf(A, B) hasChild(A, C), hasChild(C, B), female(A).
childOf(A, B) :- hasChild(B, A).
daughterOf(A, B) :- hasChild(B, A), female(A).
sonOf(A, B) :- hasChild(B, A), male(A).
grandchildOf(A, B) :- hasChild(B, C), hasChild(C, A).
granddaughterOf(A, B) :- hasChild(B, C), hasChild(C, A), female(A).
grandsonOf(A, B) :- hasChild(B, C), hasChild(C, A), male(A).
greatgrandchildOf(A, B) :- hasChild(B, C), hasChild(C, D), hasChild(D, A).
greatgranddaughterOf(A, B) :- hasChild(B, C), hasChild(C, D), hasChild(D, A), female(A).
greatgrandsonOf(A, B) :- hasChild(B, C), hasChild(C, D), hasChild(D, A), male(A).
ancestorOf(A, B) :- hasChild(A, B).
ancestorOf(A, B) :- hasChild(A, C), ancestorOf(C, B).
ancestorOf(A, B, N).
descendantOf(A, B, N).
related(A, B) :- hasChild(B, A).
related(A, B) :- hasChild(A, B).
related(A, B) :- related(A, C), related(B, C).
parent(A) :- hasChild(A, _).
childless(A) :- \x hasChild(A, _).
hasChildren(A, L).
countChildren(A, N).
sibling(X, Y) :- hasChild(C, A), hasChild(C, B), hasChild(D, A), hasChild(D, B), dif(C, D).
sisterOf(A, B) :- hasChild(C, A), hasChild(C, B), hasChild(D, A), hasChild(D, B), dif(C, D), female(A).
brotherOf(A, B) :- hasChild(C, A), hasChild(C, B), hasChild(D, A), hasChild(D, B), dif(C, D), male(A).
stepSibling(A, B) :- hasChild(C, A), hasChild(C, B), hasChild(D, A), hasChild(E, B), dif(D, E), dif(C, E), dif(C, D).
stepSisterOf(A, B) :- hasChild(C, A), hasChild(C, B), hasChild(D, A), hasChild(E, B), dif(D, E), dif(C, E), dif(C, D), female(A).
stepBrotherOf(A, B) :- hasChild(C, A), hasChild(C, B), hasChild(D, A), hasChild(E, B), dif(D, E), dif(C, E), dif(C, D), male(A).
cousin(A, B) :- hasChild(C, A), hasChild(D, B), hasChild(E, C), hasChild(E, D), dif(C, D).
getSpecies(A, B) :- species(A, B).
pet(A) :- owner(B, A), species(A, cat).
pet(A) :- owner(B, A), species(A, dog).
feral(A) :- species(A, cat), \+ owns(X, A).
feral(A) :- species(A, dog), \+ owns(X, A).