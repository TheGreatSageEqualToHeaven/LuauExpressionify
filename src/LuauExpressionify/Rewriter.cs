using System.Security.Cryptography;
using Loretta.CodeAnalysis;
using Loretta.CodeAnalysis.Lua;
using Loretta.CodeAnalysis.Lua.Syntax;

namespace LuauExpressionify;

public class Rewriter : LuaSyntaxRewriter
{
    private readonly List<StatementSyntax> _validStatements;

    public Rewriter(List<StatementSyntax> validStatements)
    {
        _validStatements = validStatements;
    }

    private static readonly TypeSyntax HelloMessage = SyntaxFactory.ParseType("HiFromAcedia");

    private static readonly List<string> Memes = new()
    {
        "This file was rewritten by LuauExpressionify :)",
        "This is depressing",
        "You shall not pass!",
        "I use arch btw",
        "Painfuck",
        "Racel cant reverse",
        "I am not responsible for anything",

        "'if if if ' - SnowyShiro",
        "'painfuck, i love that' - SnowyShiro",

        "https://media.discordapp.net/attachments/947890161277083678/1042216481372508202/blinky.gif",
        "Deobfuscated: https://media.discordapp.net/attachments/532907112515502084/943000282181283890/scripting.gif",

        "https://i.imgur.com/x9O8Sri.png",

        "https://www.youtube.com/watch?v=tVjk_aFdfF8",
        "https://www.youtube.com/watch?v=nmxueMo9qMU",

        "(function() while true do end end)()"
    };

    private static ExpressionSyntax GetMemeString() =>
        SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,
            SyntaxFactory.Literal(Memes[RandomNumberGenerator.GetInt32(Memes.Count)]));

    public override SyntaxNode? VisitExpressionStatement(ExpressionStatementSyntax node)
    {
        if (node.Expression is FunctionCallExpressionSyntax functionCall)
        {
            var innerFunction = SyntaxFactory.AnonymousFunctionExpression(
                SyntaxFactory.ParameterList(),
                SyntaxFactory.StatementList(SyntaxFactory.ReturnStatement(
                    SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(functionCall.Expression))
                )
            );

            var outerFunction = SyntaxFactory.AnonymousFunctionExpression(
                SyntaxFactory.ParameterList(),
                SyntaxFactory.StatementList(SyntaxFactory.ReturnStatement(
                    SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(SyntaxFactory.IfExpression(
                        GetMemeString(),
                        SyntaxFactory.FunctionCallExpression(
                            SyntaxFactory.ParenthesizedExpression(innerFunction),
                            SyntaxFactory.ExpressionListFunctionArgument(
                                SyntaxFactory.SingletonSeparatedList(GetMemeString()))
                        ),
                        GetMemeString()
                    )))));

            return SyntaxFactory.ExpressionStatement(
                SyntaxFactory.FunctionCallExpression(
                    SyntaxFactory.FunctionCallExpression(SyntaxFactory.ParenthesizedExpression(outerFunction),
                        SyntaxFactory.ExpressionListFunctionArgument()),
                    (FunctionArgumentSyntax)Visit(functionCall.Argument)),
                SyntaxFactory.Token(SyntaxKind.SemicolonToken)
            );
        }

        return base.VisitExpressionStatement(node);
    }

    public override SyntaxNode? VisitFunctionCallExpression(FunctionCallExpressionSyntax node)
    {
        if (node.Parent is ExpressionStatementSyntax)
            return base.VisitFunctionCallExpression(node);

        return SyntaxFactory.IfExpression(
            GetMemeString(),
            SyntaxFactory.FunctionCallExpression(SyntaxFactory.FunctionCallExpression(
                SyntaxFactory.ParenthesizedExpression(
                    SyntaxFactory.AnonymousFunctionExpression(
                        SyntaxFactory.ParameterList(),
                        SyntaxFactory.StatementList(SyntaxFactory.ReturnStatement(
                            SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(node.Expression))
                        )
                    )
                ),
                SyntaxFactory.ExpressionListFunctionArgument(SyntaxFactory.SingletonSeparatedList(GetMemeString()))
            ), (FunctionArgumentSyntax)Visit(node.Argument)),
            GetMemeString()
        );
    }

    public override SyntaxNode? VisitIfStatement(IfStatementSyntax node)
    {
        switch (RandomNumberGenerator.GetInt32(2))
        {
            case 0:
            case 1 when node.ElseIfClauses.Count == 0:
            {
                var condition = (ExpressionSyntax)Visit(node.Condition);

                for (var i = 0; i < RandomNumberGenerator.GetInt32(5); i++)
                {
                    condition = SyntaxFactory.IfExpression(
                        condition,
                        GetMemeString(),
                        SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression)
                    );
                }

                var elseIfClauses = new List<ElseIfClauseSyntax>();

                foreach (var elseIf in node.ElseIfClauses)
                {
                    var elseIfCondition = (ExpressionSyntax)Visit(elseIf.Condition);

                    for (var i = 0; i < RandomNumberGenerator.GetInt32(5); i++)
                    {
                        elseIfCondition = SyntaxFactory.IfExpression(
                            elseIfCondition,
                            GetMemeString(),
                            SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression)
                        );
                    }

                    elseIfClauses.Add(SyntaxFactory.ElseIfClause(elseIfCondition,
                        (StatementListSyntax)Visit(elseIf.Body)));
                }

                return SyntaxFactory.IfStatement(condition, (StatementListSyntax)Visit(node.Body),
                    SyntaxFactory.List(elseIfClauses), (ElseClauseSyntax?)Visit(node.ElseClause));
            }
            case 1:
            {
                foreach (var descendantNode in node.DescendantNodes())
                {
                    if (descendantNode is ContinueStatementSyntax or ReturnStatementSyntax or BreakStatementSyntax)
                        break;
                }

                var elseIfClauses = new List<ElseIfExpressionClauseSyntax>();

                foreach (var elseIf in node.ElseIfClauses)
                {
                    elseIfClauses.Add(SyntaxFactory.ElseIfExpressionClause(
                        (ExpressionSyntax)Visit(Visit(elseIf.Condition)),
                        SyntaxFactory.FunctionCallExpression(
                            SyntaxFactory.ParenthesizedExpression(
                                SyntaxFactory.AnonymousFunctionExpression(SyntaxFactory.ParameterList(),
                                    (StatementListSyntax)Visit(elseIf.Body))),
                            SyntaxFactory.ExpressionListFunctionArgument())));
                }

                var elseExpression = node.ElseClause is { } elseClause
                    ? SyntaxFactory.FunctionCallExpression(
                        SyntaxFactory.ParenthesizedExpression(SyntaxFactory.AnonymousFunctionExpression(
                            SyntaxFactory.ParameterList(), (StatementListSyntax)Visit(elseClause.ElseBody))),
                        SyntaxFactory.ExpressionListFunctionArgument())
                    : GetMemeString();

                var trueBody = SyntaxFactory.FunctionCallExpression(
                    SyntaxFactory.ParenthesizedExpression(
                        SyntaxFactory.AnonymousFunctionExpression(SyntaxFactory.ParameterList(),
                            (StatementListSyntax)Visit(node.Body))), SyntaxFactory.ExpressionListFunctionArgument());

                return SyntaxFactory.LocalVariableDeclarationStatement(
                    SyntaxFactory.SingletonSeparatedList(SyntaxFactory.LocalDeclarationName("ToHellWithYourOvertime")),
                    SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(SyntaxFactory.IfExpression(
                        (ExpressionSyntax)Visit(node.Condition), trueBody, SyntaxFactory.List(elseIfClauses),
                        elseExpression)));
            }
        }

        return base.VisitIfStatement(node);
    }

    public override SyntaxNode? VisitDoStatement(DoStatementSyntax node) =>
        SyntaxFactory.ExpressionStatement(SyntaxFactory.FunctionCallExpression(
            SyntaxFactory.ParenthesizedExpression(
                SyntaxFactory.AnonymousFunctionExpression(SyntaxFactory.ParameterList(), node.Body)),
            SyntaxFactory.ExpressionListFunctionArgument()), SyntaxFactory.Token(SyntaxKind.SemicolonToken));

    public override SyntaxNode? VisitLiteralExpression(LiteralExpressionSyntax node)
    {
        if (node.IsKind(SyntaxKind.NumericalLiteralExpression))
        {
            var memeString = (LiteralExpressionSyntax)GetMemeString();
            var memeStringLength = ((string)memeString.Token.Value!).Length;

            var number = SyntaxFactory.LiteralExpression(SyntaxKind.NumericalLiteralExpression,
                SyntaxFactory.Literal((double)node.Token.Value! - memeStringLength));
            return SyntaxFactory.ParenthesizedExpression(SyntaxFactory.BinaryExpression(SyntaxKind.SubtractExpression,
                number, SyntaxFactory.Token(SyntaxKind.PlusToken),
                SyntaxFactory.UnaryExpression(SyntaxKind.LengthExpression, SyntaxFactory.Token(SyntaxKind.HashToken),
                    memeString)));
        }

        return base.VisitLiteralExpression(node);
    }
    
    public override SyntaxNode? VisitNumericForStatement(NumericForStatementSyntax node)
    {
        var initialValue = (ExpressionSyntax)SyntaxFactory.IfExpression(
            SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression),
            node.InitialValue, SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression));

        var finalValue = (ExpressionSyntax)SyntaxFactory.IfExpression(
            SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression),
            node.FinalValue, SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression));

        // temporary fix for a Loretta bug
        if (node.StepValue is not null)
        {
            return SyntaxFactory.NumericForStatement(node.ForKeyword, node.Identifier, node.EqualsToken, initialValue,
                node.FinalValueCommaToken, finalValue, node.StepValueCommaToken,
                (ExpressionSyntax)SyntaxFactory.IfExpression(
                    SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression), node.StepValue,
                    SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression)), node.DoKeyword,
                (StatementListSyntax)Visit(node.Body), node.EndKeyword, node.SemicolonToken);
        }

        return SyntaxFactory.NumericForStatement(node.ForKeyword, node.Identifier, node.EqualsToken, initialValue,
            node.FinalValueCommaToken, finalValue, node.StepValueCommaToken, null, node.DoKeyword,
            (StatementListSyntax)Visit(node.Body), node.EndKeyword, node.SemicolonToken);
    }

    public override SyntaxNode? VisitWhileStatement(WhileStatementSyntax node)
    {
        var condition = (ExpressionSyntax)Visit(node.Condition);

        for (var i = 0; i < RandomNumberGenerator.GetInt32(5); i++)
        {
            condition = SyntaxFactory.IfExpression(
                condition,
                GetMemeString(),
                SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression)
            );
        }

        return SyntaxFactory.WhileStatement(condition, node.Body);
    }

    public override SyntaxNode? VisitBinaryExpression(BinaryExpressionSyntax node)
    {
        var leftExpression = SyntaxFactory.ParenthesizedExpression(SyntaxFactory.IfExpression(
            SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression), (ExpressionSyntax)Visit(node.Left),
            SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression)));
        var rightExpression = SyntaxFactory.ParenthesizedExpression(SyntaxFactory.IfExpression(
            SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression), (ExpressionSyntax)Visit(node.Right),
            SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression)));

        return SyntaxFactory.BinaryExpression(node.Kind(), leftExpression, node.OperatorToken, rightExpression);
    }

    public override SyntaxNode? VisitParenthesizedExpression(ParenthesizedExpressionSyntax node) =>
        SyntaxFactory.ParenthesizedExpression(SyntaxFactory.TypeCastExpression((ExpressionSyntax)Visit(node.Expression),
            HelloMessage));

    public override SyntaxNode? VisitElementAccessExpression(ElementAccessExpressionSyntax node) =>
        SyntaxFactory.ElementAccessExpression(
            SyntaxFactory.ParenthesizedExpression(SyntaxFactory.IfExpression(
                SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression), node.Expression,
                SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression))), node.KeyExpression);

    public override SyntaxNode? VisitLocalFunctionDeclarationStatement(LocalFunctionDeclarationStatementSyntax node) =>
        SyntaxFactory.LocalVariableDeclarationStatement(
            SyntaxFactory.SingletonSeparatedList(SyntaxFactory.LocalDeclarationName(node.Name)),
            SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(SyntaxFactory.IfExpression(
                SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression),
                SyntaxFactory.AnonymousFunctionExpression(node.Parameters, (StatementListSyntax)Visit(node.Body)),
                SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression))));

    public override SyntaxNode? VisitCompoundAssignmentStatement(CompoundAssignmentStatementSyntax node)
    {
        var operatorKind = SyntaxFacts.GetCompoundAssignmentOperator(node.AssignmentOperatorToken.Kind()).Value;
        var expressionKind = SyntaxFacts.GetBinaryExpression(operatorKind).Value;

        var binOp = SyntaxFactory.BinaryExpression(expressionKind, node.Variable, SyntaxFactory.Token(operatorKind),
            SyntaxFactory.IfExpression(SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression),
                node.Expression, SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression)));

        return SyntaxFactory.AssignmentStatement(
            SyntaxFactory.SingletonSeparatedList(node.Variable),
            SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(binOp)
        );
    }

    public override SyntaxNode? VisitTableConstructorExpression(TableConstructorExpressionSyntax node)
    {
        var fields = new List<TableFieldSyntax>();

        var arraySize = 0;
        foreach (var field in node.Fields)
        {
            switch (field)
            {
                case ExpressionKeyedTableFieldSyntax expressionKeyedTableFieldSyntax:
                {
                    fields.Add(SyntaxFactory.ExpressionKeyedTableField(
                        SyntaxFactory.IfExpression(SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression),
                            expressionKeyedTableFieldSyntax.Key,
                            SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression)),
                        expressionKeyedTableFieldSyntax.Value));
                    break;
                }
                case IdentifierKeyedTableFieldSyntax identifierKeyedTableFieldSyntax:
                {
                    fields.Add(SyntaxFactory.ExpressionKeyedTableField(
                        SyntaxFactory.IfExpression(SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression),
                            SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,
                                SyntaxFactory.Literal(identifierKeyedTableFieldSyntax.Identifier.Text)),
                            SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression)),
                        identifierKeyedTableFieldSyntax.Value));
                    break;
                }
                case UnkeyedTableFieldSyntax unkeyedTableFieldSyntax:
                {
                    arraySize++;
                    fields.Add(SyntaxFactory.ExpressionKeyedTableField(
                        SyntaxFactory.IfExpression(SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression),
                            SyntaxFactory.LiteralExpression(SyntaxKind.NumericalLiteralExpression,
                                SyntaxFactory.Literal(arraySize)),
                            SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression)),
                        unkeyedTableFieldSyntax.Value));
                    break;
                }
            }
        }

        return SyntaxFactory.TableConstructorExpression(SyntaxFactory.SeparatedList(fields));
    }

    public override SyntaxNode? VisitStatementList(StatementListSyntax node)
    {
        var statements = VisitList(node.Statements);

        if (_validStatements.Count == 0 || statements.Count < 3)
            return base.VisitStatementList(node);

        for (var i = 0; i < RandomNumberGenerator.GetInt32(statements.Count); i++)
        {
            var typeDeclaration = SyntaxFactory.TypeDeclarationStatement(SyntaxFactory.Identifier("Oh"),
                SyntaxFactory.TypeParameterList(
                    SyntaxFactory.SingletonSeparatedList<SyntaxNode>(SyntaxFactory.TypeParameter("Jesus"))),
                SyntaxFactory.TypeofType(SyntaxFactory.AnonymousFunctionExpression(SyntaxFactory.ParameterList(),
                    SyntaxFactory.StatementList(
                        (StatementSyntax)Visit(
                            _validStatements[RandomNumberGenerator.GetInt32(_validStatements.Count)])))));
            var location = RandomNumberGenerator.GetInt32(statements.Count);
            statements = statements.Insert(location, typeDeclaration);
        }

        return SyntaxFactory.StatementList(statements);
    }
    
    // Fix for generic for statements being visited
    public override SyntaxNode? VisitGenericForStatement(GenericForStatementSyntax node) => SyntaxFactory.GenericForStatement(node.Identifiers, node.Expressions, (StatementListSyntax)Visit(node.Body));
}