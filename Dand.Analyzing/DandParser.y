%using Dand.RunTime;

%namespace Dand.Analyzing

%{
	SymbolTable symTable = SymbolTable.GetInstance;
	public MainProgram program = new MainProgram();
%}

%start program

%union {
    public long Integer;
    public string String;
    public double Double;
	public bool Bool;
	public Expression expr;
	public StatementList statementList;
	public IStatement  statement;
	public ArgumentList argumentList;
	public FunctionList functionList;
	public FunctionStatement functionStatment;
	public CallArgList callArgList;
}
// Defining Tokens
%token COMMENT
%token <String>	 IDENTIFIER
%token <Integer> INTEGER_LITERAL
%token <Double>	 DOUBLE_LITERAL
%token <Bool>	 BOOL_LITERAL
%token <String>	 STRING_LITERAL
%token EOL
%token DIM
%token BOOL
%token INT
%token STRING
%token DOUBLE
%token AS
%token FUNCTION
%token RETURN_
%token COMMA

%token BEGIN
%token END

// I/O Statement
%token PRINT
%token INPUT

// While Statement
%token WHILE
%token DO

// If condition. 
%token IF	
%token THEN
%token ELSE	

%token OP_RIGHT_PAR
%token OP_LEFT_PAR
%left OP_ASSIGN
%left OP_ADD OP_MINUS
%left OP_MUL OP_DIV 
%left OP_MODUL
%left OP_AND
%left OP_OR
%left OP_NOT
%left OP_EQU
%left OP_NOT_EQU
%left OP_LT
%left OP_GT
%left OP_GT_EQ
%left OP_LT_EQ


// YACC Rules    // $$.Function = $1.functionList
%%
program			:  functionList BEGIN EOL END {program.Statement = new StatementList(); program.Function = $1.functionList;}
				|  functionList BEGIN EOL statementList EOL END {program.Statement = $4.statementList; program.Function = $1.functionList;}
				;



statementList	:	/*Empty*/	{if($$.statementList == null)	{$$.statementList = new StatementList();}}

				|	statement	{	if($$.statementList == null)	{$$.statementList = new StatementList();}
									$$.statementList.InsertFront($1.statement);
									
								}
				|	statementList EOL statement	{ $1.statementList.Add($3.statement); $$.statementList = $1.statementList; }
				;
			

statement	:	varDecl		{ $$.statement = $1.statement; }
			|	assignOp	{ $$.statement = $1.statement; }
			|	printOp		{ $$.statement = $1.statement; }
			|	inputOp		{ $$.statement = $1.statement; }
			|	ifCond		{ $$.statement = $1.statement; }
			|	whileLoop	{ $$.statement = $1.statement; }
			|   functionCall { $$.statement = $1.statement; }
			|   functionReturn { $$.statement = $1.statement; } 

			;
// Variable Declaration
varDecl		:	DIM IDENTIFIER AS INT		{int yId = symTable.Add($2); symTable.SetType(yId, DandTypes.Integer); $$.statement = new VriableDeclStatement(yId);}
			|	DIM IDENTIFIER AS DOUBLE	{int yId = symTable.Add($2); symTable.SetType(yId, DandTypes.Double);  $$.statement = new VriableDeclStatement(yId);}
			|	DIM IDENTIFIER AS BOOL		{int yId = symTable.Add($2); symTable.SetType(yId, DandTypes.Boolean); $$.statement = new VriableDeclStatement(yId);}
			|	DIM IDENTIFIER AS STRING	{int yId = symTable.Add($2); symTable.SetType(yId, DandTypes.String);  $$.statement = new VriableDeclStatement(yId);}
			;

			
assignOp	:	IDENTIFIER OP_ASSIGN Expr		{$$.statement = new AssignmentStatement($1, $3.expr);}
			;

Expr		:	OP_LEFT_PAR Expr OP_RIGHT_PAR		{ $$.expr = $2.expr; }
			|	Literal						{ $$.expr = $1.expr; }
			|	IDENTIFIER					{ $$.expr = new Expression($1, true);}
			|	functionCall				{ $$.expr = new Expression($1.statement); }
			|	Expr OP_ADD Expr			{ $$.expr = new Expression(Operation.Add,$1.expr,$3.expr); }
			|	Expr OP_MINUS Expr			{ $$.expr = new Expression(Operation.Sub,$1.expr,$3.expr); }
			|	OP_MINUS Expr %prec OP_MUL	{ $$.expr = new Expression(Operation.UnaryMinus,null,$2.expr); }
			|	Expr OP_MUL Expr			{ $$.expr = new Expression(Operation.Mul,$1.expr,$3.expr); }
			|	Expr OP_DIV Expr			{ $$.expr = new Expression(Operation.Div,$1.expr,$3.expr); }
			|	Expr OP_MODUL Expr			{ $$.expr = new Expression(Operation.Modul,$1.expr,$3.expr); }
			|	Expr OP_AND Expr			{ $$.expr = new Expression(Operation.And,$1.expr,$3.expr); }		
			|	Expr OP_OR  Expr			{ $$.expr = new Expression(Operation.Or,$1.expr,$3.expr); }		
			|	Expr OP_NOT Expr			{ $$.expr = new Expression(Operation.Not,$1.expr,$3.expr); }		
			|	Expr OP_EQU Expr			{ $$.expr = new Expression(Operation.Equ,$1.expr,$3.expr); }
			|	Expr OP_NOT_EQU Expr		{ $$.expr = new Expression(Operation.NotEqu,$1.expr,$3.expr); }
			|	Expr OP_LT  Expr			{ $$.expr = new Expression(Operation.Lt,$1.expr,$3.expr); }		
			|	Expr OP_GT  Expr			{ $$.expr = new Expression(Operation.Gt,$1.expr,$3.expr); }		
			|	Expr OP_GT_EQ Expr			{ $$.expr = new Expression(Operation.GtEq,$1.expr,$3.expr); }	
			|	Expr OP_LT_EQ Expr			{ $$.expr = new Expression(Operation.LtEq,$1.expr,$3.expr); }	
			;

Literal		:	STRING_LITERAL	{$$.expr = new Expression($1);}
			|	BOOL_LITERAL	{$$.expr = new Expression($1);}
			|	INTEGER_LITERAL	{$$.expr = new Expression($1);}
			|	DOUBLE_LITERAL	{$$.expr = new Expression($1);}		
			;

printOp		:	PRINT Expr	{$$.statement = new PrintStatement($2.expr);}
			;

inputOp		:	INPUT IDENTIFIER {$$.statement = new InputStatement($2);}
			;
			
ifCond		:	IF OP_LEFT_PAR Expr OP_RIGHT_PAR THEN EOL ifBody else END
				{$$.statement = new IfCondStatement($3.expr,$7.statementList,$8.statementList);}
			;

ifBody		:	/*Empty*/				{$$.statementList = new StatementList();}
			|	statementList EOL		{$$.statementList = $1.statementList;}
			;

else		:	/* Empty */			{$$.statementList = new StatementList();}
			|	ELSE EOL elseBody	{$$.statementList = $3.statementList;}
			;

elseBody	:	/*Empty*/			{$$.statementList = new StatementList();}
			|	statementList EOL	{$$.statementList = $1.statementList;}
			;

whileLoop	:	WHILE OP_LEFT_PAR Expr OP_RIGHT_PAR DO EOL whileBody END
				{$$.statement = new WhileLoopStatement($3.expr,$7.statementList);}
			;

whileBody	:	/*Empty*/			{$$.statementList = new StatementList();}
			|	statementList EOL	{$$.statementList = $1.statementList;}
			;
			
functionList : /*Empty*/	{if($$.functionList == null)	{$$.functionList = new FunctionList();}}
			 | function EOL functionList { $3.functionList.add($1.functionStatment); $$.functionList = $3.functionList;}
			 ;

function    : 	FUNCTION IDENTIFIER OP_LEFT_PAR  OP_RIGHT_PAR EOL BEGIN functionBody EOL END
				{int id = symTable.Add($2); symTable.SetType(id, DandTypes.Function); $$.functionStatment = new FunctionStatement(id, new ArgumentList(), $7.statementList);}
			
			|   FUNCTION IDENTIFIER OP_LEFT_PAR argumentList  OP_RIGHT_PAR	EOL BEGIN functionBody EOL END
				{int id = symTable.Add($2); symTable.SetType(id, DandTypes.Function); $$.functionStatment = new FunctionStatement(id, $4.argumentList, $8.statementList);}
			;

functionBody	:	/*Empty*/	{if($$.statementList == null)	{$$.statementList = new StatementList();}}

				|	functionStatement	{	if($$.statementList == null)	{$$.statementList = new StatementList();}
									$$.statementList.InsertFront($1.statement, true);
									
								}
				|	functionBody EOL functionStatement	{ $1.statementList.Add($3.statement, true); $$.statementList = $1.statementList; }
				;

functionStatement	:	localVarDecl		{ $$.statement = $1.statement; }
			|	assignOp	{ $$.statement = $1.statement; }
			|	printOp		{ $$.statement = $1.statement; }
			|	inputOp		{ $$.statement = $1.statement; }
			|	ifCond		{ $$.statement = $1.statement; }
			|	whileLoop	{ $$.statement = $1.statement; }
			|   functionReturn      { $$.statement = $1.statement; } 
			|   functionCall { $$.statement = $1.statement; }
			;

functionReturn: RETURN_ Expr { $$.statement = new FunctionReturn($2.expr); }
		      ;

localVarDecl:	DIM IDENTIFIER AS INT   { $$.statement = new LocalVariable($2, DandTypes.Integer);}
			|	DIM IDENTIFIER AS BOOL	{ $$.statement = new LocalVariable($2, DandTypes.Boolean);}
			;

argumentList :  argument { if($$.argumentList == null) {$$.argumentList = new ArgumentList();}  $$.argumentList.insert($1.statement);}
			|	argumentList COMMA argument {$1.argumentList.insert($3.statement); $$.argumentList = $1.argumentList;}
			;

argument    :  	IDENTIFIER AS INT      { $$.statement = new LocalVariable($1, DandTypes.Integer);}
			|   IDENTIFIER AS BOOL     { $$.statement = new LocalVariable($1, DandTypes.Boolean);}			
			;
functionCall : IDENTIFIER OP_LEFT_PAR OP_RIGHT_PAR
				{ $$.statement = new FunctionCall($1, new CallArgList()); }
				|IDENTIFIER OP_LEFT_PAR callArgList OP_RIGHT_PAR
				 { $$.statement = new FunctionCall($1, $3.callArgList); }
			 ;

callArgList :callArg
			{ if($$.callArgList == null) { $$.callArgList = new CallArgList();}
			$$.callArgList.add($1.statement); }
			| callArgList COMMA callArg
			{$1.callArgList.add($3.statement); $$.callArgList = $1.callArgList;}
			;

callArg     : Expr { $$.statement = new CallArg($1.expr); }
			;
%%

// No argument CTOR. By deafult Parser's ctor requires scanner as param.
public Parser(Scanner scn) : base(scn) { }