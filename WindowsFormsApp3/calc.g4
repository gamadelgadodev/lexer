grammar calc;

options {							
    language=CSharp2;								//lenguaje objetivo de la gramatica
}

programa 			//el programa retornara un valor entero.
	: inicio | proposicion | fin//se imprime el valor que calculo el parser.
	;

inicio
	:
	etiqueta START NUM
	|
	START NUM
	;
fin
	: 
	etiqueta END etiqueta
	|
	END etiqueta
	|
	END
	;
proposicion
	:instruccion
	 |
	directiva
	 |
	etiqueta EQU
	;
instruccion
	:opinstruccion
	;
opinstruccion
	:etiqueta formato
	| 
	 formato
	;
formato
	:f1
	|
	 f2
	|
	 f3
	|
	 f4
	;
f1
	:CODOPF1 EOF
	;
f2
	:CODOPF2N NUM
	|
	 CODOPF2R REG
	|
	 CODOPF2RR REG COMA REG
	|
	 CODOPF2RN REG COMA NUM
	;
f3
	:simple3
	 |
	 indirecto3
	 |
	 inmediato3
	;
f4
	: MAS f3
	;
simple3
	:CODOPF3 id
	 |
	 CODOPF3 NUM
	 |
	 CODOPF3 NUM COMA REG
	 |
	 CODOPF3 ID COMA REG
	 |
	 CODOPF3 expresion
	;
indirecto3
	:CODOPF3 ARROBA NUM
	 |
	 CODOPF3 ARROBA id
	 |
	 CODOPF3 ARROBA expresion
	;
inmediato3
	:CODOPF3 GATO NUM
	 |
	 CODOPF3 GATO id
	 |
	 CODOPF3 GATO expresion
	;
directiva	:etiqueta  tipodirectiva opdirectiva
	|
	 tipodirectiva opdirectiva
	|
	 etiqueta  tipodirectiva expresion
	|
	 tipodirectiva expresion
	;
tipodirectiva
	: BYTE | WORD | RESB | RESW | BASE
	;
opdirectiva
	: NUM | constante | id
	;
etiqueta
	: LENG
	;
id
	: LENG
	;
constante
	: CONSTHEX 
	  |
	  CONSTCAD
	;
expresiones
	: ASTERISCO | expresion
	;

expresion
	: PARENTESISI | expresionprima | PARENTESISD
	; 
expresionprima
	:  NUM | LENG | GATO | NUM | MAS | MENOS 
	;
//  PALABRAS
CODOPF1
	: ('FIX' | 'FLOAT' | 'HIO' | 'NORM' | 'SIO' | 'TIO' | 'RSUB')
	;
CODOPF2RN
	: ('SHIFTL'| 'SHIFTR')
	;
CODOPF2N
	:('SVC')
	;
CODOPF2R
	: ('CLEAR' | 'TIXR')
	;
CODOPF2RR
	:('ADDR' |'COMPR' | 'DIVR' | 'MULR' | 'RMO'  | 'SUBR' )
	;
CODOPF3
	: ('ADD' | 'ADDF' | 'AND' | 'COMP' | 'COMPF' | 'DIV' | 'DIVF' | 'J' | 'JEQ' | 'JGT' | 'JLT' | 'JSUB' | 'LDA' | 'LDB' | 'LDCH' | 'LDF' | 'LDL' | 'LDS' | 'LDT' | 'LDX' | 'LPS' | 'MUL' | 'MULF' | 'OR' | 'RD' | 'SSK' | 'STA' | 'STB' | 'STCH' | 'STF' | 'STI' | 'STL' | 'STS' | 'STSW' | 'STT' | 'STX' | 'SUB' | 'SUBF' | 'TD' | 'TIX' | 'WD')
	;
START	
	: 'START'
	;
END	
	: 'END'
	;
BYTE
	: 'BYTE'
	;
WORD
	: 'WORD'
	;
EQU
	: 'EQU'
	;
RESB
	: 'RESB'
	;
BASE
	:	'BASE'
	;
RESW
	: 'RESW'
	;
REG
	: ('A' | 'X' | 'L' | 'B' | 'S' | 'T' | 'F' )
	;

//CARRACTERES
ARROBA
	: '@'
	;
GATO
	: '#'
	;
COMA
	:','
	;
MAS
	: '+'
	;
MENOS
	: '-'
	;
MUL
	: '*'
	;
PARENTESISD
	: ')'
	;
PARENTESISI
	: '('
	;
ASTERISCO
	: '*'
	;
CONSTHEX
	: 'X'+'\''+('0'..'Z')+ '\''
	;
CONSTCAD
	: 'C'+'\''+('0'..'Z')+ '\''
	;
LENG
	: ('A'..'Z')+|('A'..'Z')+('0'..'9')
	;
CODOP
	: 'ADD' | 'ADDF' | 'ADDR' | 'AND'
	;
NEWLINE			//token para identificar el final de la expresion.
	: '\n'
	;
NUM
	: ('0'..'F')+ | ('0'..'F')+'H' | ('0'..'F')+ 'h'
	;
WS
	: (' '| '\t' |'\r'|'\n')+ {Skip();}	//tokens que identifican las secuencas de escape.
	;