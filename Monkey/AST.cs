﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ReSharper disable InconsistentNaming

namespace Monkey
{
  public interface INode
  {
    string TokenLiteral();
    string ToString();
  }

  public interface IStatement : INode
  {
    void StatementNode();
  }

  public interface IExpression : INode
  {
    void ExpressionNode();
  }

  public class Code : INode
  {
    public List<IStatement> Statements { get; set; } = new List<IStatement>();

    public string TokenLiteral()
    {
      if (Statements.Count > 0)
        return Statements[0].TokenLiteral();
      return "";
    }

    public override string ToString()
    {
      var str = "";
      foreach (var statement in Statements)
        str += statement.ToString() + "\n";
      return str;
    }
  }

  public class ExpressionStatement : IStatement
  {
    public Token token { get; set; }
    public IExpression Expression { get; set; }

    public string TokenLiteral()
    {
      return token.Literal;
    }

    public override string ToString()
    {
      if (Expression != null)
        return Expression.ToString();
      return "";
    }

    public void StatementNode()
    {
      throw new NotImplementedException();
    }
  }

  public class StringLiteral : IExpression
  {
    public Token token { get; set; }
    public string Value { get; set; }

    public string TokenLiteral()
    {
      return token.Literal;
    }

    public override string ToString()
    {
      return token.Literal;
    }

    public void ExpressionNode()
    {
      throw new NotImplementedException();
    }
  }

  public class IntegerLiteral : IExpression
  {
    public Token token { get; set; }
    public long Value { get; set; }

    public string TokenLiteral()
    {
      return token.Literal;
    }

    public override string ToString()
    {
      return token.Literal;
    }

    public void ExpressionNode()
    {
      throw new NotImplementedException();
    }
  }

  public class PrefixExpression : IExpression
  {
    public Token token { get; set; }
    public string op { get; set; }
    public IExpression right { get; set; }

    public string TokenLiteral()
    {
      return token.Literal;
    }

    public override string ToString()
    {
      return "(" + op + right.ToString() + ")";
    }

    public void ExpressionNode()
    {
      throw new NotImplementedException();
    }
  }

  public class InfixExpression : IExpression
  {
    public Token token { get; set; }
    public IExpression left { get; set; }
    public string op { get; set; }
    public IExpression right { get; set; }

    public string TokenLiteral()
    {
      return token.Literal;
    }

    public override string ToString()
    {
      return "(" + left?.ToString() + " " + op + " " + right?.ToString() + ")";
    }

    public void ExpressionNode()
    {
      throw new NotImplementedException();
    }
  }

  public class FunctionLiteral : IExpression
  {
    public Token token { get; set; }
    public List<Identifier> Parameters { get; set; } = new List<Identifier>();
    public BlockStatement Body { get; set; }

    public string TokenLiteral()
    {
      return token.Literal;
    }

    public void ExpressionNode()
    {
      throw new NotImplementedException();
    }

    public override string ToString()
    {
      var str = "";
      str += token.Literal;

      var parameters = from p in Parameters
        select p.Value;

      str += "(";
      str += string.Join(",", parameters);
      str += ")";

      str += Body.ToString();

      return str;
    }
  }

  public class CallExpression : IExpression
  {
    public Token token { get; set; }
    public IExpression Function { get; set; }
    public List<IExpression> Arguments { get; set; }

    public string TokenLiteral()
    {
      return token.Literal;
    }

    public void ExpressionNode()
    {
      throw new NotImplementedException();
    }

    public override string ToString()
    {
      var str = "";

      str += Function.ToString();
      str += "(";

      var args = from arg in Arguments select arg.ToString();

      str += string.Join(",", args);

      str += ")";

      return str;
    }
  }

  public class BlockStatement : IStatement
  {
    public Token token { get; set; }
    public List<IStatement> Statements { get; set; } = new List<IStatement>();

    public string TokenLiteral()
    {
      return token.Literal;
    }

    public void StatementNode()
    {
      throw new NotImplementedException();
    }

    public override string ToString()
    {
      var str = "";
      foreach (var statement in Statements)
        str += statement.ToString() + "\n";
      return str;
    }
  }

  public class ReturnStatement : IStatement
  {
    public Token token { get; set; }
    public IExpression ReturnValue { get; set; }

    public string TokenLiteral()
    {
      return token.Literal;
    }

    public void StatementNode()
    {
      throw new NotImplementedException();
    }

    public override string ToString()
    {
      return $"{token.Literal} {ReturnValue}";
    }
  }

  public class LetStatement : IStatement
  {
    public Token token { get; set; }
    public Identifier Name { get; set; }
    public IExpression Value { get; set; }

    public string TokenLiteral()
    {
      return token.Literal;
    }

    public void StatementNode()
    {
      throw new NotImplementedException();
    }

    public override string ToString()
    {
      var sb = new StringBuilder();

      sb.Append(TokenLiteral() + " " + Name + " = " + Value?.ToString());

      return sb.ToString();
    }
  }

  public class IfExpression : IExpression
  {
    public Token token { get; set; }
    public IExpression Condition { get; set; }
    public BlockStatement Consequence { get; set; }
    public INode Alternative { get; set; }

    public string TokenLiteral()
    {
      return token.Literal;
    }

    public void ExpressionNode()
    {
      throw new NotImplementedException();
    }

    public override string ToString()
    {
      var str = "";
      str += "if";
      str += Condition.ToString();
      str += " " + Consequence;

      if (Alternative != null)
        str += "else " + Alternative;

      return str;
    }
  }

  public class Identifier : IExpression
  {
    public Token token { get; set; }
    public string Value { get; set; }

    public string TokenLiteral()
    {
      return token.Literal;
    }

    public void ExpressionNode()
    {
      throw new NotImplementedException();
    }

    public override string ToString()
    {
      return Value;
    }
  }
}