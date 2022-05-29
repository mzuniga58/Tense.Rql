using System;
using System.Collections.Generic;
using System.Web;

namespace Tense.Rql
{
	/// <summary>
	/// RQL Parser
	/// </summary>
	internal class RqlParser : IDisposable
	{
		internal QueueStack<Token> _tokenStack = new();
		internal Stack<RqlNode> _nodeStack = new();
		internal Stack<ParseState> _stateStack = new();
		internal Lexer lexer = new();

		/// <summary>
		/// Instantiates an RQL Parser
		/// </summary>
		public RqlParser()
		{
		}

		/// <summary>
		/// Converts the string representation of an RQL Query to its <see cref="RqlNode"/>
		/// equivalent by using culture-specific format information.
		/// </summary>
		/// <param name="s">A string that contains an RQL Query to convert.</param>
		/// <returns>An object that is equivalent to the RQL Query contained in <paramref name="s"/>.</returns>
		/// <exception cref="RqlFormatException">s does not contain a valid string representation of a valid RQL query.</exception>
		public static RqlNode Parse(string? s)
		{
			if (string.IsNullOrWhiteSpace(s))
				return new RqlNode(RqlOperation.NOOP);

			var rqlQuery = HttpUtility.UrlDecode(s);

			if (rqlQuery.StartsWith("?"))
				rqlQuery = rqlQuery[1..];

			if (rqlQuery.StartsWith("RQL="))
				rqlQuery = rqlQuery[4..];

			using var parser = new RqlParser();
			return parser.InternalParse(rqlQuery);
		}

		/// <summary>
		/// Converts the string representation of an RQL Query to its <see cref="RqlNode"/>
		/// equivalent by using culture-specific format information.
		/// </summary>
		/// <param name="s">A string that contains an RQL Query to convert.</param>
		/// <returns>An object that is equivalent to the RQL Query contained in <paramref name="s"/>.</returns>
		/// <exception cref="RqlFormatException">s does not contain a valid string representation of a valid RQL query.</exception>
		internal RqlNode InternalParse(string? s)
		{
			if (string.IsNullOrWhiteSpace(s))
				return new RqlNode(RqlOperation.NOOP);

				lexer = new Lexer();
				lexer.Scan(s);

			while (!lexer.EndOfStream)
			{
				var token = lexer.Pull();

				switch (CurrentState)
				{
					case ParseState.INITIAL_STATE:
						ProcessNominalState(token);
						break;

					case ParseState.LIMIT1:
						ProcessLimit1(token);
						break;

					case ParseState.LIMIT2:
						ProcessLimit2(token);
						break;

					case ParseState.LIMIT3:
						ProcessLimit3(token);
						break;

					case ParseState.LIMIT4:
						ProcessLimit4(token);
						break;

					case ParseState.LIMIT5:
						ProcessLimit5(token);
						break;

					case ParseState.SELECT1:
						ProcessSelect1(token);
						break;

					case ParseState.SELECT2:
						ProcessSelect2(token);
						break;

					case ParseState.ANDOP1:
						ProcessAndop1(token);
						break;

					case ParseState.ANDOP2:
						ProcessAndop2(token);
						break;

					case ParseState.ANDOP3:
						ProcessAndop3(token);
						break;

					case ParseState.OROP1:
						ProcessOrop1(token);
						break;

					case ParseState.OROP2:
						ProcessOrop2(token);
						break;

					case ParseState.SORT1:
						ProcessSort1(token);
						break;

					case ParseState.SORT2:
						ProcessSort2(token);
						break;

					case ParseState.SORT3:
						ProcessSort3(token);
						break;

					case ParseState.NOARGS1:
						ProcessNoArgs1(token);
						break;

					case ParseState.NOARGS2:
						ProcessNoArgs2(token);
						break;

					case ParseState.OPARG1:
						ProcessOparg1(token);
						break;

					case ParseState.OPARG2:
						ProcessOparg2(token);
						break;

					case ParseState.PROPVAL1:
						ProcessPropVal1(token);
						break;

					case ParseState.PROPVAL2:
						ProcessPropVal2(token);
						break;

					case ParseState.PROPVAL3:
						ProcessPropVal3(token);
						break;

					case ParseState.CONDITIONALOP1:
						ProcessConditionalOperation1(token);
						break;

					case ParseState.CONDITIONALOP2:
						ProcessConditionalOperation2(token);
						break;

					case ParseState.CONDITIONALOP3:
						ProcessConditionalOperation3(token);
						break;

					case ParseState.CONDITIONALOP4:
						ProcessConditionalOperation4(token);
						break;

					case ParseState.CONDITIONALOP5:
						ProcessConditionalOperation5(token);
						break;

					case ParseState.GROUP1:
						ProcessGroup1(token);
						break;

					case ParseState.GROUP2:
						ProcessGroup2(token);
						break;

					case ParseState.GROUPORPROPERTY:
						ProcessGroupOrProperty(token);
						break;

					case ParseState.PROPEXP1:
						ProcessPropertyExpression1(token);
						break;

					case ParseState.PROPEXP2:
						ProcessPropertyExpression2(token);
						break;

					case ParseState.PROPEXP3:
						ProcessPropertyExpression3(token);
						break;

					case ParseState.PROPEXP4:
						ProcessPropertyExpression4(token);
						break;

					case ParseState.PROPEXP5:
						ProcessPropertyExpression5(token);
						break;

					case ParseState.PROPERTY1:
						ProcessProperty1(token);
						break;

					case ParseState.PROPERTY2:
						ProcessProperty2(token);
						break;

					case ParseState.PROPERTY3:
						ProcessProperty3(token);
						break;

					case ParseState.PROPERTY4:
						ProcessProperty4(token);
						break;

					case ParseState.PROPERTY5:
						ProcessProperty5(token);
						break;

					case ParseState.INITPROPERTY1:
						ProcessInitProperty1(token);
						break;

					case ParseState.INITPROPERTY2:
						ProcessInitProperty2(token);
						break;

					case ParseState.INITPROPERTY3:
						ProcessInitProperty3(token);
						break;

					case ParseState.INITPROPERTY4:
						ProcessInitProperty4(token);
						break;

					case ParseState.INITPROPERTY5:
						ProcessInitProperty5(token);
						break;

					case ParseState.INITPROPERTYOPERATOR:
						ProcessInitPropertyOperator(token);
						break;

					case ParseState.INITCONDITIONAL:
						ProcessInitConditional(token);
						break;

					case ParseState.AGGREGATE1:
						ProcessAggregate1(token);
						break;

					case ParseState.AGGREGATE2:
						ProcessAggregate2(token);
						break;

					case ParseState.AGGREGATE3:
						ProcessAggregate3(token);
						break;

					case ParseState.AGGREGATE4:
						ProcessAggregate4(token);
						break;

					case ParseState.AGGREGATE5:
						ProcessAggregate5(token);
						break;

					case ParseState.ONEORNONEARGS1:
						ProcessOneOrNoneArgs1(token);
						break;

					case ParseState.ONEORNONEARGS2:
						ProcessOneOrNoneArgs2(token);
						break;

					case ParseState.ONEORNONEARGS3:
						ProcessOneOrNoneArgs2(token);
						break;
				}
			}

			if ( CurrentState != ParseState.INITIAL_STATE)
            {
				if (CurrentState == ParseState.PROPERTY4 || CurrentState == ParseState.LIMIT5)
					throw new RqlFormatException("RQL Query syntax error, expecting closing ).");

				else if (CurrentState == ParseState.PROPERTY1)
					throw new RqlFormatException("RQL Query syntax error, expecting PROPERTY.");

				else if (CurrentState == ParseState.SORT1 || CurrentState == ParseState.SELECT1 || CurrentState == ParseState.LIMIT1 || CurrentState == ParseState.NOARGS1)
					throw new RqlFormatException("RQL Query syntax error, expecting (.");

				else if (CurrentState == ParseState.SORT2)
					throw new RqlFormatException("RQL Query syntax error, expecting PROPERTY.");

				else if (CurrentState == ParseState.LIMIT2 || CurrentState == ParseState.LIMIT4)
					throw new RqlFormatException("RQL Query syntax error, expecting VALUE.");

				else if (CurrentState == ParseState.LIMIT3)
					throw new RqlFormatException("RQL Query syntax error, expecting VALUE or closing ).");

				throw new RqlFormatException("RQL Query syntax error.");
			}

			while (CurrentNode == null && _nodeStack.Count > 1)
			{
				_nodeStack.Pop();
				_stateStack.Pop();
			}

			if (CurrentNode != null)
			{
				if (CurrentNode.CountOf(RqlOperation.AGGREGATE) > 1)
					throw new RqlFormatException("An RQL Query cannot contain more than one AGGREGATE clause.");

				if (CurrentNode.CountOf(RqlOperation.DISTINCT) > 1)
					throw new RqlFormatException("An RQL Query cannot contain more than one DISTINCT clause.");

				if (CurrentNode.CountOf(RqlOperation.ONE) > 1)
					throw new RqlFormatException("An RQL Query cannot contain more than one ONE clause.");

				if (CurrentNode.CountOf(RqlOperation.FIRST) > 1)
					throw new RqlFormatException("An RQL Query cannot contain more than one FIRST clause.");

				if (CurrentNode.CountOf(RqlOperation.LIMIT) > 1)
					throw new RqlFormatException("An RQL Query cannot contain more than one LIMIT clause.");

				if (CurrentNode.CountOf(RqlOperation.VALUES) > 1)
					throw new RqlFormatException("An RQL Query cannot contain more than one VALUES clause.");

				if (CurrentNode.Contains(RqlOperation.SELECT) && CurrentNode.Contains(RqlOperation.VALUES))
					throw new RqlFormatException("An RQL Query that contains a VALUES clause cannot contain a SELECT clause.");

				if (CurrentNode.Contains(RqlOperation.AGGREGATE))
				{
					var mainNode = RqlNode.Copy(CurrentNode);
					mainNode.Remove(RqlOperation.AGGREGATE);

					if (mainNode.Operation != RqlOperation.NOOP)
					{
						if (mainNode.Contains(RqlOperation.COUNT))
							throw new RqlFormatException("An RQL Query that contains an AGGREGATE operation cannot have a COUNT clause outside of the AGGREGATE clause.");

						if (mainNode.Contains(RqlOperation.MIN))
							throw new RqlFormatException("An RQL Query that contains an AGGREGATE operation cannot have a MIN clause outside of the AGGREGATE clause.");

						if (mainNode.Contains(RqlOperation.MAX))
							throw new RqlFormatException("An RQL Query that contains an AGGREGATE operation cannot have a MAX clause outside of the AGGREGATE clause.");

						if (mainNode.Contains(RqlOperation.MEAN))
							throw new RqlFormatException("An RQL Query that contains an AGGREGATE operation cannot have a MEAN clause outside of the AGGREGATE clause.");

						if (mainNode.Contains(RqlOperation.SUM))
							throw new RqlFormatException("An RQL Query that contains an AGGREGATE operation cannot have a SUM clause outside of the AGGREGATE clause.");
					}
				}
				else
				{
					var _count = CurrentNode.CountOf(RqlOperation.COUNT);

					if (_count > 1)
						throw new RqlFormatException("An RQL Query cannot contain more than one COUNT clause.");

					if (CurrentNode.Contains(RqlOperation.LIMIT))
					{
						if (_count > 0)
							throw new RqlFormatException("An RQL Query that contains a COUNT clause outside of an AGGREGATE cannot contain a LIMIT clause.");

						if (CurrentNode.Contains(RqlOperation.MIN))
							throw new RqlFormatException("An RQL Query that contains a MIN clause outside of an AGGREGATE cannot contain a LIMIT clause.");

						if (CurrentNode.Contains(RqlOperation.MAX))
							throw new RqlFormatException("An RQL Query that contains a MAX clause outside of an AGGREGATE cannot contain a LIMIT clause.");

						if (CurrentNode.Contains(RqlOperation.MEAN))
							throw new RqlFormatException("An RQL Query that contains a MEAN clause outside of an AGGREGATE cannot contain a LIMIT clause.");

						if (CurrentNode.Contains(RqlOperation.SUM))
							throw new RqlFormatException("An RQL Query that contains a SUM clause outside of an AGGREGATE cannot contain a LIMIT clause.");
					}

					if (_count > 0)
					{
						var countClause = CurrentNode.Find(RqlOperation.COUNT);

						if (countClause != null && countClause.Count > 0)
							throw new RqlFormatException("A COUNT clause outside of an AGGREGATE function cannot reference a property.");
					}
				}

				var maxNodes = CurrentNode.FindAll(RqlOperation.MAX);
				var minNodes = CurrentNode.FindAll(RqlOperation.MIN);
				var meanNodes = CurrentNode.FindAll(RqlOperation.MEAN);
				var sumNodes = CurrentNode.FindAll(RqlOperation.SUM);
				var countNodes = CurrentNode.FindAll(RqlOperation.COUNT);

				var aggregateNodes = new List<RqlNode>();
				aggregateNodes.AddRange(maxNodes);
				aggregateNodes.AddRange(minNodes);
				aggregateNodes.AddRange(meanNodes);
				aggregateNodes.AddRange(sumNodes);

				foreach (var countNode in countNodes)
					if (countNode.Count > 0)
						aggregateNodes.Add(countNode);

				for (int i = 0; i < aggregateNodes.Count; i++)
				{
					var propertyNode = aggregateNodes[i].NonNullValue<RqlNode>(0);

					for (int j = 0; j < aggregateNodes.Count; j++)
					{
						if (i != j)
						{
							var comparrisonNode = aggregateNodes[j].NonNullValue<RqlNode>(0);

							if (propertyNode.Equals(comparrisonNode))
								throw new RqlFormatException("An RQL Query cannot contain more than one aggregate clause referencing a single property.");
						}
					}
				}

				CurrentNode.Consolidate(RqlOperation.SELECT);
				CurrentNode.Consolidate(RqlOperation.SORT);
			}

			return CurrentNode ?? new RqlNode(RqlOperation.NOOP);
		}

		internal ParseState CurrentState
		{
			get
			{
				if (_stateStack.Count == 0)
					return ParseState.INITIAL_STATE;

				return _stateStack.Peek();
			}
		}

		internal void SetState(ParseState newState)
		{
			_stateStack.Pop();
			_stateStack.Push(newState);
		}

		internal RqlNode PopNode()
		{
			var node = _nodeStack.Pop();
			_stateStack.Pop();

			return node;
		}

		internal RqlNode? CurrentNode
		{
			get
			{
				if (_nodeStack.Count == 0)
					return null;

				return _nodeStack.Peek();
			}
		}

		internal void SetNode(RqlNode newNode, ParseState newState)
		{
			_nodeStack.Push(newNode);
			_stateStack.Push(newState);
		}

		internal void Popup()
		{
			if (_stateStack.Count > 1)
			{
				var node = PopNode();

				if (CurrentNode != null)
				{
					CurrentNode.Add(node);
				}
			}
			else
				SetState(ParseState.INITIAL_STATE);
		}

		internal void ProcessNominalState(Token token)
		{
			if (token.Symbol == Symbol.PROPERTY)
			{
				SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.INITPROPERTY1);
				lexer.Requeue(token);
			}
			else if (token.Symbol == Symbol.LPAREN)
			{
				if (lexer.Peek().Symbol == Symbol.PROPERTY)
				{
					if (lexer.LookForward(1).Symbol == Symbol.COMMA || lexer.LookForward(1).Symbol == Symbol.RPAREN)
					{
						SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.INITPROPERTY1);
						lexer.Requeue(token);
					}
					else
					{
						SetNode(new RqlNode(RqlOperation.NOOP), ParseState.GROUP1);
					}
				}
				else
				{
					SetNode(new RqlNode(RqlOperation.NOOP), ParseState.GROUP1);
				}
			}
			else if (token.Symbol == Symbol.AND)
			{
				if (CurrentNode != null)
				{
					if (CurrentNode.Operation != RqlOperation.AND)
					{
						var node = new RqlNode(RqlOperation.AND);
						node.Add(PopNode());
						SetNode(node, ParseState.INITIAL_STATE);
					}
				}
				else
					throw new RqlFormatException($"Invalid RQL syntax at AND.");
			}
			else if (token.Symbol == Symbol.OR)
			{
				if (CurrentNode != null)
				{
					if (CurrentNode.Operation != RqlOperation.OR)
					{
						var node = new RqlNode(RqlOperation.OR);
						node.Add(PopNode());
						SetNode(node, ParseState.INITIAL_STATE);
					}
				}
				else
					throw new RqlFormatException($"Invalid RQL syntax at OR.");
			}
			else if (token.Symbol == Symbol.ANDOP)
			{
				SetNode(new RqlNode(RqlOperation.AND), ParseState.ANDOP1);
			}
			else if (token.Symbol == Symbol.OROP)
			{
				SetNode(new RqlNode(RqlOperation.OR), ParseState.OROP1);
			}
			else if (token.Symbol == Symbol.RPAREN)
			{
				Popup();
			}
			else if (token.Symbol == Symbol.LIMIT)
			{
				SetNode(new RqlNode(RqlOperation.LIMIT), ParseState.LIMIT1);
			}
			else if (token.Symbol == Symbol.SELECT)
			{
				SetNode(new RqlNode(RqlOperation.SELECT), ParseState.SELECT1);
			}
			else if (token.Symbol == Symbol.SORT)
			{
				SetNode(new RqlNode(RqlOperation.SORT), ParseState.SORT1);
			}
			else if (token.Symbol == Symbol.VALUES)
			{
				SetNode(new RqlNode(RqlOperation.VALUES), ParseState.OPARG1);
			}
			else if (token.Symbol == Symbol.DISTINCT)
			{
				SetNode(new RqlNode(RqlOperation.DISTINCT), ParseState.NOARGS1);
			}
			else if (token.Symbol == Symbol.First)
			{
				SetNode(new RqlNode(RqlOperation.FIRST), ParseState.NOARGS1);
			}
			else if (token.Symbol == Symbol.ONE)
			{
				SetNode(new RqlNode(RqlOperation.ONE), ParseState.NOARGS1);
			}
			else if (token.Symbol == Symbol.COUNT)
			{
				SetNode(new RqlNode(RqlOperation.COUNT), ParseState.ONEORNONEARGS1);
			}
			else if (token.Symbol == Symbol.SUM)
			{
				SetNode(new RqlNode(RqlOperation.SUM), ParseState.OPARG1);
			}
			else if (token.Symbol == Symbol.MIN)
			{
				SetNode(new RqlNode(RqlOperation.MIN), ParseState.OPARG1);
			}
			else if (token.Symbol == Symbol.MAX)
			{
				SetNode(new RqlNode(RqlOperation.MAX), ParseState.OPARG1);
			}
			else if (token.Symbol == Symbol.MEAN)
			{
				SetNode(new RqlNode(RqlOperation.MEAN), ParseState.OPARG1);
			}
			else if (token.Symbol == Symbol.IN)
			{
				SetNode(new RqlNode(RqlOperation.IN), ParseState.PROPVAL1);
			}
			else if (token.Symbol == Symbol.OUT)
			{
				SetNode(new RqlNode(RqlOperation.OUT), ParseState.PROPVAL1);
			}
			else if (token.Symbol == Symbol.EQOP)
			{
				var node = new RqlNode(RqlOperation.EQ);
				SetNode(node, ParseState.CONDITIONALOP1);
			}
			else if (token.Symbol == Symbol.NEOP)
			{
				var node = new RqlNode(RqlOperation.NE);
				SetNode(node, ParseState.CONDITIONALOP1);
			}
			else if (token.Symbol == Symbol.GTOP)
			{
				var node = new RqlNode(RqlOperation.GT);
				SetNode(node, ParseState.CONDITIONALOP1);
			}
			else if (token.Symbol == Symbol.GEOP)
			{
				var node = new RqlNode(RqlOperation.GE);
				SetNode(node, ParseState.CONDITIONALOP1);
			}
			else if (token.Symbol == Symbol.LEOP)
			{
				var node = new RqlNode(RqlOperation.LE);
				SetNode(node, ParseState.CONDITIONALOP1);
			}
			else if (token.Symbol == Symbol.LTOP)
			{
				var node = new RqlNode(RqlOperation.LT);
				SetNode(node, ParseState.CONDITIONALOP1);
			}
			else if (token.Symbol == Symbol.CONTAINS)
			{
				SetNode(new RqlNode(RqlOperation.CONTAINS), ParseState.PROPEXP1);
			}
			else if (token.Symbol == Symbol.EXCLUDES)
			{
				SetNode(new RqlNode(RqlOperation.EXCLUDES), ParseState.PROPEXP1);
			}
			else if (token.Symbol == Symbol.LIKE)
			{
				SetNode(new RqlNode(RqlOperation.LIKE), ParseState.PROPEXP1);
			}
			else if (token.Symbol == Symbol.AGGREGATE)
			{
				SetNode(new RqlNode(RqlOperation.AGGREGATE), ParseState.AGGREGATE1);
			}
			else
			{
				throw new RqlFormatException($"Invalid RQL syntax at {token}");
			}
		}

		internal void ProcessPropertyExpression1(Token token)
		{
			if (token.Symbol == Symbol.LPAREN)
			{
				SetState(ParseState.PROPEXP2);
				SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.PROPERTY1);
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessPropertyExpression2(Token token)
		{
			if (token.Symbol == Symbol.COMMA)
			{
				SetState(ParseState.PROPEXP3);
			}
			else if (token.Symbol == Symbol.RPAREN)
			{
				Popup();
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessPropertyExpression3(Token token)
		{
			if (IsValue(token))
			{
				SetState(ParseState.PROPEXP2);

				if (CurrentNode != null)
					CurrentNode.Add(token.NonNullValue<object>());
				else
					throw new RqlFormatException($"Invalid RQL syntax at {token}.");
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessPropertyExpression4(Token token)
		{
			if (token.Symbol == Symbol.STRING)
			{
				_tokenStack.Push(token);
				SetState(ParseState.PROPEXP5);
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessPropertyExpression5(Token token)
		{
			if (token.Symbol == Symbol.RPAREN)
			{
				if (_tokenStack.Pull() is Token expression &&
					 _tokenStack.Pull() is Token property &&
					 _tokenStack.Pull() is Token operation)
				{
					RqlNode? node = null;

					if (operation.Symbol == Symbol.CONTAINS)
					{
						node = new RqlNode(RqlOperation.CONTAINS);
					}
					else if (operation.Symbol == Symbol.EXCLUDES)
					{
						node = new RqlNode(RqlOperation.EXCLUDES);
					}
					else if (operation.Symbol == Symbol.LIKE)
					{
						node = new RqlNode(RqlOperation.LIKE);
					}

					if (node != null)
					{
						node.Add(property.NonNullValue<string>());
						node.Add(expression.NonNullValue<string>());

						Popup();
						SetNode(node, ParseState.INITIAL_STATE);
					}
					else
						throw new RqlFormatException($"Invalid RQL syntax at {token}.");
				}
				else
					throw new RqlFormatException($"Invalid RQL syntax at {token}.");
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessConditionalOperation1(Token token)
		{
			if (token.Symbol == Symbol.LPAREN)
			{
				SetState(ParseState.CONDITIONALOP2);
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessConditionalOperation2(Token token)
		{
			if (token.Symbol == Symbol.PROPERTY)
			{
				SetState(ParseState.CONDITIONALOP3);
				lexer.Requeue(token);
				SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.PROPERTY1);
			}
			else if (token.Symbol == Symbol.LPAREN)
			{
				SetState(ParseState.CONDITIONALOP3);
				lexer.Requeue(token);
				SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.PROPERTY1);
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessConditionalOperation3(Token token)
		{
			if (token.Symbol == Symbol.COMMA)
			{
				SetState(ParseState.CONDITIONALOP4);
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessConditionalOperation4(Token token)
		{
			if (IsValue(token))
			{
				if (CurrentNode != null)
				{
					CurrentNode.Add(token.NonNullValue<object>());
					SetState(ParseState.CONDITIONALOP5);
				}
				else
					throw new RqlFormatException($"Invalid RQL syntax at {token}.");
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		private bool IsValue(Token token)
		{
			if (token.Symbol == Symbol.TINY ||
				token.Symbol == Symbol.UTINY ||
				token.Symbol == Symbol.SHORT ||
				token.Symbol == Symbol.USHORT ||
				token.Symbol == Symbol.INTEGER ||
				token.Symbol == Symbol.UINTEGER ||
				token.Symbol == Symbol.LONG ||
				token.Symbol == Symbol.ULONG ||
				token.Symbol == Symbol.CHAR ||
				token.Symbol == Symbol.BOOLEAN ||
				token.Symbol == Symbol.DECIMAL ||
				token.Symbol == Symbol.DOUBLE ||
				token.Symbol == Symbol.SINGLE ||
				token.Symbol == Symbol.UNIQUEIDENTIFIER ||
				token.Symbol == Symbol.DATETIME ||
				token.Symbol == Symbol.TIMESPAN ||
				token.Symbol == Symbol.STRING ||
				token.Symbol == Symbol.PROPERTY ||
				token.Symbol == Symbol.BINARY ||
				token.Symbol == Symbol.URI ||
				token.Symbol == Symbol.NULL)
			{
				return true;
			}

			return false;
		}

		internal void ProcessConditionalOperation5(Token token)
		{
			if (token.Symbol == Symbol.RPAREN)
			{
				SetState(ParseState.INITIAL_STATE);
				Popup();
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}


		internal void ProcessPropVal1(Token token)
		{
			if (token.Symbol == Symbol.LPAREN)
			{
				SetState(ParseState.PROPVAL2);

				SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.PROPERTY1);
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessPropVal2(Token token)
		{
			if (token.Symbol == Symbol.COMMA)
			{
				SetState(ParseState.PROPVAL3);
			}
			else if (token.Symbol == Symbol.RPAREN)
			{
				Popup();
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessPropVal3(Token token)
		{
			if (IsValue(token))
			{
				if (CurrentNode != null)
				{
					CurrentNode.Add(token.NonNullValue<object>());
					SetState(ParseState.PROPVAL2);
				}
				else
					throw new RqlFormatException($"Invalid RQL syntax at {token}.");
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessAnd1(Token token)
		{
			if (token.Symbol == Symbol.PROPERTY)
			{
				SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.PROPERTY1);
				lexer.Requeue(token);
			}
			else if (token.Symbol == Symbol.LPAREN)
			{
				SetNode(new RqlNode(RqlOperation.NOOP), ParseState.GROUPORPROPERTY);
				lexer.Requeue(token);
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessNoArgs1(Token token)
		{
			if (token.Symbol == Symbol.LPAREN)
			{
				SetState(ParseState.NOARGS2);
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessNoArgs2(Token token)
		{
			if (token.Symbol == Symbol.RPAREN)
			{
				Popup();
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessOneOrNoneArgs1(Token token)
		{
			if (token.Symbol == Symbol.LPAREN)
			{
				SetState(ParseState.ONEORNONEARGS2);
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessOneOrNoneArgs2(Token token)
		{
			if (token.Symbol == Symbol.RPAREN)
			{
				Popup();
			}
			else if (token.Symbol == Symbol.PROPERTY)
			{
				SetState(ParseState.ONEORNONEARGS3);
				SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.PROPERTY1);
				lexer.Requeue(token);
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessOneOrNoneArgs3(Token token)
		{
			if (token.Symbol == Symbol.RPAREN)
			{
				Popup();
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessOparg1(Token token)
		{
			if (token.Symbol == Symbol.LPAREN)
			{
				SetState(ParseState.OPARG2);
				SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.PROPERTY1);
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessOparg2(Token token)
		{
			if (token.Symbol == Symbol.RPAREN)
			{
				Popup();
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessSort1(Token token)
		{
			if (token.Symbol == Symbol.LPAREN)
			{
				SetState(ParseState.SORT2);
			}
			else
				throw new RqlFormatException($"RQL Query syntax error at {token.Value<string>()}, expecting (.");
		}

		internal void ProcessSort2(Token token)
		{
			if (CurrentNode != null)
			{
				if (token.Symbol == Symbol.PLUS)
				{
					SetNode(new RqlNode(RqlOperation.SORTPROPERTY), ParseState.SORT3);
					CurrentNode.Add(RqlSortOrder.Ascending);
					SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.PROPERTY1);
				}
				else if (token.Symbol == Symbol.MINUS)
				{
					SetNode(new RqlNode(RqlOperation.SORTPROPERTY), ParseState.SORT3);
					CurrentNode.Add(RqlSortOrder.Descending);
					SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.PROPERTY1);
				}
				else if (token.Symbol == Symbol.PROPERTY)
				{
					SetNode(new RqlNode(RqlOperation.SORTPROPERTY), ParseState.SORT3);
					CurrentNode.Add(RqlSortOrder.Ascending);
					SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.PROPERTY1);
					lexer.Requeue(token);
				}
				else
                {
					throw new RqlFormatException($"RQL Query syntax error at {token.Value<string>()}, expecting PROPERTY.");
				}
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessSort3(Token token)
		{
			Popup();

			if (token.Symbol == Symbol.COMMA)
			{
				SetState(ParseState.SORT2);
			}
			else if (token.Symbol == Symbol.RPAREN)
			{
				Popup();
			}
			else
				throw new RqlFormatException($"RQL Query syntax error at {token.Value<string>()}, expected PROPERTY or closing ).");
		}

		internal void ProcessSelect1(Token token)
		{
			if (token.Symbol == Symbol.LPAREN)
			{
				SetState(ParseState.SELECT2);
				SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.PROPERTY1);
			}
			else
				throw new RqlFormatException($"RQL Query syntax error at {token.Value<string>()}, expecting (.");
		}

		internal void ProcessSelect2(Token token)
		{
			if (token.Symbol == Symbol.COMMA)
			{
				SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.PROPERTY1);
			}
			else if (token.Symbol == Symbol.RPAREN)
			{
				Popup();
			}
			else
				throw new RqlFormatException($"RQL Query syntax error at {token.Value<string>()}, expecting PROPERTY or closing ).");
		}

		internal void ProcessLimit1(Token token)
		{
			if (token.Symbol == Symbol.LPAREN)
			{
				SetState(ParseState.LIMIT2);
			}
			else
				throw new RqlFormatException($"RQL Query syntax error at {token.Value<string>()}, expecting (.");
		}

		internal void ProcessLimit2(Token token)
		{
			if (CurrentNode != null)
			{
				if (token.Symbol == Symbol.TINY)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<sbyte>()));
					SetState(ParseState.LIMIT3);
				}
				else if (token.Symbol == Symbol.SHORT)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<short>()));
					SetState(ParseState.LIMIT3);
				}
				else if (token.Symbol == Symbol.INTEGER)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<int>()));
					SetState(ParseState.LIMIT3);
				}
				else if (token.Symbol == Symbol.LONG)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<long>()));
					SetState(ParseState.LIMIT3);
				}
				else if (token.Symbol == Symbol.UTINY)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<byte>()));
					SetState(ParseState.LIMIT3);
				}
				else if (token.Symbol == Symbol.USHORT)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<ushort>()));
					SetState(ParseState.LIMIT3);
				}
				else if (token.Symbol == Symbol.UINTEGER)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<uint>()));
					SetState(ParseState.LIMIT3);
				}
				else if (token.Symbol == Symbol.ULONG)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<ulong>()));
					SetState(ParseState.LIMIT3);
				}
				else if (token.Symbol == Symbol.SINGLE)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<float>()));
					SetState(ParseState.LIMIT3);
				}
				else if (token.Symbol == Symbol.DOUBLE)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<double>()));
					SetState(ParseState.LIMIT3);
				}
				else if (token.Symbol == Symbol.DECIMAL)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<decimal>()));
					SetState(ParseState.LIMIT3);
				}
				else
					throw new RqlFormatException($"RQL Query syntax error at {token.Value<string>()}, expecting VALUE.");
			}
		}

		internal void ProcessLimit3(Token token)
		{
			if (token.Symbol == Symbol.COMMA)
			{
				SetState(ParseState.LIMIT4);
			}
			else if (token.Symbol == Symbol.RPAREN)
			{
				Popup();
			}
			else
				throw new RqlFormatException($"RQL Query syntax error at {token.Value<string>()}, expecting VALUE or closing ).");
		}

		internal void ProcessLimit4(Token token)
		{
			if (CurrentNode != null)
			{
				if (token.Symbol == Symbol.TINY)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<sbyte>()));
					SetState(ParseState.LIMIT5);
				}
				else if (token.Symbol == Symbol.SHORT)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<short>()));
					SetState(ParseState.LIMIT5);
				}
				else if (token.Symbol == Symbol.INTEGER)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<int>()));
					SetState(ParseState.LIMIT5);
				}
				else if (token.Symbol == Symbol.LONG)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<long>()));
					SetState(ParseState.LIMIT5);
				}
				else if (token.Symbol == Symbol.UTINY)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<byte>()));
					SetState(ParseState.LIMIT5);
				}
				else if (token.Symbol == Symbol.USHORT)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<ushort>()));
					SetState(ParseState.LIMIT5);
				}
				else if (token.Symbol == Symbol.UINTEGER)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<uint>()));
					SetState(ParseState.LIMIT5);
				}
				else if (token.Symbol == Symbol.ULONG)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<ulong>()));
					SetState(ParseState.LIMIT5);
				}
				else if (token.Symbol == Symbol.SINGLE)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<float>()));
					SetState(ParseState.LIMIT5);
				}
				else if (token.Symbol == Symbol.DOUBLE)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<double>()));
					SetState(ParseState.LIMIT5);
				}
				else if (token.Symbol == Symbol.DECIMAL)
				{
					CurrentNode.Add(Convert.ToUInt64(token.NonNullValue<decimal>()));
					SetState(ParseState.LIMIT5);
				}
				else
					throw new RqlFormatException($"RQL Query syntax error at {token.Value<string>()}, expecting VALUE.");
			}
		}

		internal void ProcessLimit5(Token token)
		{
			if (token.Symbol == Symbol.RPAREN)
			{
				Popup();
			}
			else
				throw new RqlFormatException($"RQL Query syntax error at {token.Value<string>()}, expecting closing ).");
		}

		internal void ProcessAggregate1(Token token)
		{
			if (token.Symbol == Symbol.LPAREN)
			{
				SetState(ParseState.AGGREGATE2);
				SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.PROPERTY1);
			}
			else
				throw new RqlFormatException($"RQL Query syntax error at {token.Value<string>()}, expecting (.");
		}

		internal void ProcessAggregate2(Token token)
		{
			if (token.Symbol == Symbol.COMMA)
			{
				SetState(ParseState.AGGREGATE3);
			}
			else
				throw new RqlFormatException($"Unrecognized token {token}. Aborting scan.");
		}

		internal void ProcessAggregate3(Token token)
		{
			if (token.Symbol == Symbol.PROPERTY)
			{
				SetState(ParseState.AGGREGATE2);
				SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.PROPERTY1);
				lexer.Requeue(token);
			}
			else if (token.Symbol == Symbol.LPAREN)
			{
				SetState(ParseState.AGGREGATE2);
				SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.PROPERTY1);
				lexer.Requeue(token);
			}
			else
				ProcessAggregate4(token);
		}

		internal void ProcessAggregate4(Token token)
		{
			if (token.Symbol == Symbol.SUM)
			{
				SetState(ParseState.AGGREGATE5);
				ProcessNominalState(token);
			}
			else if (token.Symbol == Symbol.MAX)
			{
				SetState(ParseState.AGGREGATE5);
				ProcessNominalState(token);
			}
			else if (token.Symbol == Symbol.MIN)
			{
				SetState(ParseState.AGGREGATE5);
				ProcessNominalState(token);
			}
			else if (token.Symbol == Symbol.MEAN)
			{
				SetState(ParseState.AGGREGATE5);
				ProcessNominalState(token);
			}
			else if (token.Symbol == Symbol.COUNT)
			{
				SetState(ParseState.AGGREGATE5);
				ProcessNominalState(token);
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessAggregate5(Token token)
		{
			if (token.Symbol == Symbol.COMMA)
			{
				SetState(ParseState.AGGREGATE4);
			}
			else if (token.Symbol == Symbol.RPAREN)
			{
				Popup();
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessAndop1(Token token)
		{
			if (token.Symbol == Symbol.LPAREN)
			{
				SetState(ParseState.ANDOP2);
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessAndop2(Token token)
		{
			SetState(ParseState.ANDOP3);
			ProcessNominalState(token);
		}

		internal void ProcessAndop3(Token token)
		{
			if (token.Symbol == Symbol.COMMA)
			{
				SetState(ParseState.ANDOP2);
			}
			else if (token.Symbol == Symbol.RPAREN)
			{
				Popup();
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessOrop1(Token token)
		{
			if (token.Symbol == Symbol.LPAREN)
			{
				SetState(ParseState.OROP2);
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessOrop2(Token token)
		{
			SetState(ParseState.OROP3);
			ProcessNominalState(token);
		}

		internal void ProcessOrop3(Token token)
		{
			if (token.Symbol == Symbol.COMMA)
			{
				SetState(ParseState.OROP2);
			}
			else if (token.Symbol == Symbol.RPAREN)
			{
				Popup();
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessGroupOrProperty(Token token)
		{
			if (token.Symbol == Symbol.PROPERTY)
			{
				if (lexer.Peek().Symbol == Symbol.FORWARDSLASH)
				{
					lexer.Requeue(token);
					SetState(ParseState.PROPERTY1);
				}
				else if (lexer.Peek().Symbol == Symbol.COMMA)
				{
					lexer.Requeue(token);
					SetState(ParseState.PROPERTY1);
				}
				else if (lexer.Peek().Symbol == Symbol.RPAREN)
				{
					lexer.Requeue(token);
					SetState(ParseState.PROPERTY1);
				}
				else
				{
					SetNode(new RqlNode(RqlOperation.PROPERTY), ParseState.PROPERTY1);
					lexer.Requeue(token);
				}
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessGroup1(Token token)
		{
			SetState(ParseState.GROUP2);
			ProcessNominalState(token);
		}

		internal void ProcessGroup2(Token token)
		{
			if (CurrentNode != null)
			{
				if (token.Symbol == Symbol.OR)
				{
					if (CurrentNode.Operation == RqlOperation.NOOP)
					{
						CurrentNode.Operation = RqlOperation.OR;
						SetState(ParseState.GROUP1);
					}
					else if (CurrentNode.Operation == RqlOperation.OR)
					{
						SetState(ParseState.GROUP1);
					}
					else if (CurrentNode.Operation == RqlOperation.AND)
					{
						var newNode = new RqlNode(RqlOperation.OR);
						newNode.Add(PopNode());
						SetNode(newNode, ParseState.GROUP1);
					}
					else
						throw new RqlFormatException($"Invalid RQL syntax at {token}.");
				}
				else if (token.Symbol == Symbol.AND)
				{
					if (CurrentNode.Operation == RqlOperation.NOOP)
					{
						CurrentNode.Operation = RqlOperation.AND;
						SetState(ParseState.GROUP1);
					}
					else if (CurrentNode.Operation == RqlOperation.AND)
					{
						SetState(ParseState.GROUP1);
					}
					else if (CurrentNode.Operation == RqlOperation.OR)
					{
						var newNode = new RqlNode(RqlOperation.AND);
						newNode.Add(PopNode());
						SetNode(newNode, ParseState.GROUP1);
					}
					else
						throw new RqlFormatException($"Invalid RQL syntax at {token}.");
				}
				else if (token.Symbol == Symbol.RPAREN)
				{
					Popup();
				}
				else
					throw new RqlFormatException($"Invalid RQL syntax at {token}.");
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		#region Property Parsing
		/// <summary>
		/// A property must start with a PROPERTY name, or a (
		/// </summary>
		/// <param name="token"></param>
		internal void ProcessProperty1(Token token)
		{
			if (token.Symbol == Symbol.LPAREN)
			{
				SetState(ParseState.PROPERTY2);
			}
			else if (token.Symbol == Symbol.PROPERTY)
			{
				if (CurrentNode != null)
				{
					CurrentNode.Add(token.NonNullValue<string>());
					SetState(ParseState.PROPERTY4);
				}
				else
					throw new RqlFormatException($"RQL Query syntax error at {token.Value<string>()}.");
			}
			else
				throw new RqlFormatException($"RQL Query syntax error at {token.Value<string>()}.");
		}

		/// <summary>
		/// The property is of the form (name,name,...)
		/// At this point, the Next thing better be a PROPERTY name
		/// </summary>
		/// <param name="token"></param>
		internal void ProcessProperty2(Token token)
		{
			if (token.Symbol == Symbol.PROPERTY)
			{
				if (CurrentNode != null)
				{
					CurrentNode.Add(token.NonNullValue<string>());
					SetState(ParseState.PROPERTY3);
				}
				else
					throw new RqlFormatException($"Invalid RQL syntax at {token}.");
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		/// <summary>
		/// The property is of the form (name,name,...)
		/// The Next acceptable token is either , or )
		/// </summary>
		/// <param name="token"></param>
		internal void ProcessProperty3(Token token)
		{
			if (token.Symbol == Symbol.COMMA)
			{
				SetState(ParseState.PROPERTY2);
			}
			else if (token.Symbol == Symbol.RPAREN)
			{
				Popup();
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		/// <summary>
		/// The property is of the form name/name/...
		/// The Next acceptable token is either / or an operator
		/// If it's an operator, it's not part of the property, requeue it and end the
		/// property scan and begin the operator scan
		/// </summary>
		/// <param name="token"></param>

		internal void ProcessProperty4(Token token)
		{
			if (token.Symbol == Symbol.FORWARDSLASH)
			{
				SetState(ParseState.PROPERTY5);
			}
			else
			{
				lexer.Requeue(token);
				Popup();
			}
		}

		/// <summary>
		/// The property is of the form name/name...
		/// The only acceptable Next token is a name
		/// </summary>
		/// <param name="token"></param>
		internal void ProcessProperty5(Token token)
		{
			if (token.Symbol == Symbol.PROPERTY)
			{
				if (CurrentNode != null)
				{
					CurrentNode.Add(token.NonNullValue<string>());
					SetState(ParseState.PROPERTY4);
				}
				else
					throw new RqlFormatException($"Invalid RQL syntax at {token}.");
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}
		#endregion

		#region Initial Property Parsing
		/// <summary>
		/// A property must start with a PROPERTY name, or a (
		/// </summary>
		/// <param name="token"></param>
		internal void ProcessInitProperty1(Token token)
		{
			if (token.Symbol == Symbol.LPAREN)
			{
				SetState(ParseState.INITPROPERTY2);
			}
			else if (token.Symbol == Symbol.PROPERTY)
			{
				if (CurrentNode != null)
				{
					CurrentNode.Add(token.NonNullValue<string>());
					SetState(ParseState.INITPROPERTY4);
				}
				else
					throw new RqlFormatException($"Invalid RQL syntax at {token}.");
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		/// <summary>
		/// The property is of the form (name,name,...)
		/// At this point, the Next thing better be a PROPERTY name
		/// </summary>
		/// <param name="token"></param>
		internal void ProcessInitProperty2(Token token)
		{
			if (token.Symbol == Symbol.PROPERTY)
			{
				if (CurrentNode != null)
				{
					CurrentNode.Add(token.NonNullValue<string>());
					SetState(ParseState.INITPROPERTY3);
				}
				else
					throw new RqlFormatException($"Invalid RQL syntax at {token}.");
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		/// <summary>
		/// The property is of the form (name,name,...)
		/// The Next acceptable token is either , or )
		/// </summary>
		/// <param name="token"></param>
		internal void ProcessInitProperty3(Token token)
		{
			if (token.Symbol == Symbol.COMMA)
			{
				SetState(ParseState.INITPROPERTY2);
			}
			else if (token.Symbol == Symbol.RPAREN)
			{
				SetState(ParseState.INITPROPERTYOPERATOR);
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		/// <summary>
		/// The property is of the form name/name/...
		/// The Next acceptable token is either / or an operator
		/// If it's an operator, it's not part of the property, requeue it and end the
		/// property scan and begin the operator scan
		/// </summary>
		/// <param name="token"></param>

		internal void ProcessInitProperty4(Token token)
		{
			if (token.Symbol == Symbol.FORWARDSLASH)
			{
				SetState(ParseState.INITPROPERTY5);
			}
			else
			{
				lexer.Requeue(token);
				SetState(ParseState.INITPROPERTYOPERATOR);
			}
		}

		/// <summary>
		/// The property is of the form name/name...
		/// The only acceptable Next token is a name
		/// </summary>
		/// <param name="token"></param>
		internal void ProcessInitProperty5(Token token)
		{
			if (token.Symbol == Symbol.PROPERTY)
			{
				if (CurrentNode != null)
				{
					CurrentNode.Add(token.NonNullValue<string>());
					SetState(ParseState.INITPROPERTY4);
				}
				else
					throw new RqlFormatException($"Invalid RQL syntax at {token}.");
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessInitPropertyOperator(Token token)
		{
			if (token.Symbol == Symbol.EQ)
			{
				var node = new RqlNode(RqlOperation.EQ);
				node.Add(PopNode());
				SetNode(node, ParseState.INITCONDITIONAL);
			}
			else if (token.Symbol == Symbol.NE)
			{
				var node = new RqlNode(RqlOperation.NE);
				node.Add(PopNode());
				SetNode(node, ParseState.INITCONDITIONAL);
			}
			else if (token.Symbol == Symbol.LE)
			{
				var node = new RqlNode(RqlOperation.LE);
				node.Add(PopNode());
				SetNode(node, ParseState.INITCONDITIONAL);
			}
			else if (token.Symbol == Symbol.LT)
			{
				var node = new RqlNode(RqlOperation.LT);
				node.Add(PopNode());
				SetNode(node, ParseState.INITCONDITIONAL);
			}
			else if (token.Symbol == Symbol.GE)
			{
				var node = new RqlNode(RqlOperation.GE);
				node.Add(PopNode());
				SetNode(node, ParseState.INITCONDITIONAL);
			}
			else if (token.Symbol == Symbol.GT)
			{
				var node = new RqlNode(RqlOperation.GT);
				node.Add(PopNode());
				SetNode(node, ParseState.INITCONDITIONAL);
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}

		internal void ProcessInitConditional(Token token)
		{
			if (IsValue(token))
			{
				if (CurrentNode != null)
				{
					CurrentNode.Add(token.NonNullValue<object>());
					Popup();
				}
				else
					throw new RqlFormatException($"Invalid RQL syntax at {token}.");
			}
			else
				throw new RqlFormatException($"Invalid RQL syntax at {token}.");
		}
		#endregion

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		/// <summary>
		/// Disposes an RQL Node
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		/// <summary>
		/// Destructor of an RQL Node
		/// </summary>
		~RqlParser()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(false);
		}

		/// <summary>
		/// This code added to correctly implement the disposable pattern.
		/// </summary>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
