%using Dand.RunTime;

%namespace Dand.Analyzing

%option stack, minimize, parser, verbose, persistbuffer, unicode, compressNext, embedbuffers

%{
public void yyerror(string format, params object[] args) 
{
	System.Console.Error.WriteLine("Error: line {0} - " + format, yyline);
}
%}


Digit		[0-9]

Identifier		\$([a-zA-Z]([a-zA-Z0-9_])*)
IntegerLiteral	{Digit}+
DoubleLiteral	{Digit}+(\.{Digit}+)?
BooleanLiteral	(true|false)
StringLiteral	\".*\"

WhiteSpace		[ \t]
Eol				(\r\n?|\n)

Anything		.

%%
{WhiteSpace}+	{ ; }

{Eol}+		{ return (int) Tokens.EOL; }

{Identifier}				{yylval.String = yytext.Substring(1);
						   return (int) Tokens.IDENTIFIER; }

{IntegerLiteral}					{ Int64.TryParse (yytext, NumberStyles.Integer, CultureInfo.CurrentCulture, out yylval.Integer);
						   return (int) Tokens.INTEGER_LITERAL; }

{DoubleLiteral}					{ double.TryParse (yytext, NumberStyles.Float, CultureInfo.CurrentCulture, out yylval.Double); 
						   return (int) Tokens.DOUBLE_LITERAL; }

{StringLiteral}					{ if (yytext.Length > 2) { yylval.String = yytext.Substring(1, yytext.Length - 2); }
									else { yylval.String = ""; }
								return (int) Tokens.STRING_LITERAL; }

{BooleanLiteral}					{ bool.TryParse(yytext, out yylval.Bool);
						   return (int) Tokens.BOOL_LITERAL; }

","			{ return (int) Tokens.COMMA; }
"="			{ return (int) Tokens.OP_ASSIGN; }
"+"		    { return (int) Tokens.OP_ADD; }
"-"			{ return (int) Tokens.OP_MINUS; }
"*"			{ return (int) Tokens.OP_MUL; }
"/"			{ return (int) Tokens.OP_DIV; }
"%"			{ return (int) Tokens.OP_MODUL; }
"("			{ return (int) Tokens.OP_LEFT_PAR; }
")"			{ return (int) Tokens.OP_RIGHT_PAR; }
"and"		{ return (int) Tokens.OP_AND; }
"or"		{ return (int) Tokens.OP_OR; }
"not"		{ return (int) Tokens.OP_NOT; }
"=="		{ return (int) Tokens.OP_EQU; }
"!="		{ return (int) Tokens.OP_NOT_EQU; }
"<"			{ return (int) Tokens.OP_LT; }
">"			{ return (int) Tokens.OP_GT; }
">="		{ return (int) Tokens.OP_GT_EQ; }
"<="		{ return (int) Tokens.OP_LT_EQ; }

"var"		{ return (int) Tokens.DIM; }	
"bool"		{ return (int) Tokens.BOOL; }
"int"		{ return (int) Tokens.INT; }
"as"		{ return (int) Tokens.AS; }
"begin"		{ return (int) Tokens.BEGIN; }
"end"		{ return (int) Tokens.END; }
"input"		{ return (int) Tokens.INPUT; }
"print"		{ return (int) Tokens.PRINT; }
"if"		{ return (int) Tokens.IF; }
"then"		{ return (int) Tokens.THEN; }
"else"		{ return (int) Tokens.ELSE; }
"while"	    { return (int) Tokens.WHILE; }
"do"		{ return (int) Tokens.DO; }
"function"  { return (int) Tokens.FUNCTION;} 
"return"    { return (int) Tokens.RETURN_;}
{Anything}	{ throw new Exception(string.Format("Syntax Error: line {0} - {1}", yyline, yytext));}