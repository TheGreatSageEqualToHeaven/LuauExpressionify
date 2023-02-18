using Loretta.CodeAnalysis.Lua;
using Loretta.CodeAnalysis.Lua.Syntax;

namespace LuauExpressionify;

public class Walker : LuaSyntaxWalker
{
    public readonly List<StatementSyntax> ValidStatements = new();
    
    public override void VisitStatementList(StatementListSyntax node)
    {
        if (node.Parent is not CompilationUnitSyntax)
            ValidStatements.AddRange(node.Statements);
        
        base.VisitStatementList(node);
    }
}