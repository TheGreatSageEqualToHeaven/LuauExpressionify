using Loretta.CodeAnalysis;
using Loretta.CodeAnalysis.Lua;
using LuauExpressionify;

if (args.Length == 0)
    throw new Exception("Incorrect amount of arguments passed!");

if (!File.Exists(args[0]))
    throw new Exception("Provided file does not exist!");

var script = File.ReadAllText(args[0]);

var parsed = LuaSyntaxTree.ParseText(script);

var root = parsed.GetRoot();

var walker = new Walker();
walker.Visit(root);
var rewriter = new Rewriter(walker.ValidStatements);

File.WriteAllText("output.lua", rewriter.Visit(root).NormalizeWhitespace().ToFullString());
