using System;
using System.Text;

namespace Tense.Rql
{
	internal class Lexer : IDisposable
	{
		internal MemoryStreamReader _input = new("");
		internal LexicalState _state = LexicalState.INITIAL_STATE;
		internal QueueStack<Token> _tokenStream = new();
		internal StringBuilder yytext = new();
		internal Bstr currentBStr = new();

		public Token Pull()
		{
			var token = _tokenStream.Pull();

			if (token! != null)
				return token;
			
			throw new RqlFormatException("Invalid Rql Syntax");
		}

		public Token Peek()
		{
			var token = _tokenStream.Peek();

			if (token! != null)
				return token;

			throw new RqlFormatException("Invalid Rql Syntax");
		}

		public Token LookForward(int n)
		{
			var token = _tokenStream.Lookforward(n);

			if (token! != null)
				return token;

			throw new RqlFormatException("Invalid Rql Syntax");
		}

		public void Push(Token token)
		{
			_tokenStream.Push(token);
		}

		public void Requeue(Token token)
		{
			_tokenStream.Put(token);
		}

		public bool EndOfStream
		{
			get
			{
				return _tokenStream.Count <= 0;
			}
		}

		public int Count
		{
			get { return _tokenStream.Count; }
		}

		public void Scan(string input)
		{
			_input = new MemoryStreamReader(input);
			_state = LexicalState.INITIAL_STATE;
			_tokenStream.Clear();

			if ( _input == null )
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");

			while (!_input.EndOfStream)
			{
				switch (_state)
				{
					case LexicalState.INITIAL_STATE:     //	Start of clause
						ProcessInitialState();
						break;

					case LexicalState.TEXTSTREAM:
						ProcessTextStream();
						break;

					case LexicalState.STRING1:
						ProcessString1();
						break;

					case LexicalState.STRING2:
						ProcessString2();
						break;

					case LexicalState.CHAR1:
						ProcessChar1();
						break;

					case LexicalState.CHARA:
						ProcessCharA();
						break;

					case LexicalState.TINY:
						ProcessTiny();
						break;

					case LexicalState.TINY2:
						ProcessTiny2();
						break;

					case LexicalState.UTINY:
						ProcessUTiny();
						break;

					case LexicalState.UTINY2:
						ProcessUTiny2();
						break;

					case LexicalState.SHORT:
						ProcessShort();
						break;

					case LexicalState.SHORT2:
						ProcessShort2();
						break;

					case LexicalState.USHORT:
						ProcessUShort();
						break;

					case LexicalState.USHORT2:
						ProcessUShort2();
						break;

					case LexicalState.INT:
						ProcessInt();
						break;

					case LexicalState.INT2:
						ProcessInt2();
						break;

					case LexicalState.UINT:
						ProcessUInt();
						break;

					case LexicalState.UINT2:
						ProcessUInt2();
						break;

					case LexicalState.LONG:
						ProcessLong();
						break;

					case LexicalState.LONG2:
						ProcessLong2();
						break;

					case LexicalState.ULONG:
						ProcessULong();
						break;

					case LexicalState.ULONG2:
						ProcessULong2();
						break;

					case LexicalState.NUMERIC1:
						ProcessUniaryOrDecimalPoint();
						break;

					case LexicalState.INTEGERPORTION:
						ProcessIntegerPortion();
						break;

					case LexicalState.DECIMALPORTION:
						ProcessDecimalPortion();
						break;

					case LexicalState.EXPONENTPORTION:
						ProcessExponentPortion();
						break;

					case LexicalState.EXPONENTPORTION2:
						ProcessExponentPortion2();
						break;

					case LexicalState.QSTRING1:
						ProcessQString1();
						break;

					case LexicalState.DOUBLE1:
						ProcessDouble1();
						break;

					case LexicalState.DOUBLE2:
						ProcessDouble2();
						break;

					case LexicalState.DOUBLE3:
						ProcessDouble3();
						break;

					case LexicalState.DOUBLE4:
						ProcessDouble4();
						break;

					case LexicalState.DOUBLE5:
						ProcessDouble5();
						break;

					case LexicalState.SINGLE1:
						ProcessSingle1();
						break;

					case LexicalState.SINGLE2:
						ProcessSingle2();
						break;

					case LexicalState.SINGLE3:
						ProcessSingle3();
						break;

					case LexicalState.SINGLE4:
						ProcessSingle4();
						break;

					case LexicalState.SINGLE5:
						ProcessSingle5();
						break;

					case LexicalState.DECIMAL1:
						ProcessDecimal1();
						break;

					case LexicalState.DECIMAL2:
						ProcessDecimal2();
						break;

					case LexicalState.DECIMAL3:
						ProcessDecimal3();
						break;

					case LexicalState.BOOL1:
						ProcessBool1();
						break;

					case LexicalState.BINARY1:
						ProcessBinary1();
						break;

					case LexicalState.BINARY2:
						ProcessBinary2();
						break;

					case LexicalState.BINARY3:
						ProcessBinary3();
						break;

					case LexicalState.DATEORTIMEORNUMERICORGUID:
						ProcessDateOrTimeOrNumericOrGuid();
						break;

					case LexicalState.GUIDORSTRING1:
						ProcessGuidOrString1();
						break;

					case LexicalState.GUIDORSTRING2:
						ProcessGuidOrString2();
						break;

					case LexicalState.GUIDORSTRING3:
						ProcessGuidOrString3();
						break;

					case LexicalState.GUIDORSTRING4:
						ProcessGuidOrString4();
						break;

					case LexicalState.GUIDORSTRING5:
						ProcessGuidOrString5();
						break;

					case LexicalState.GUID1:
						ProcessGuid1();
						break;

					case LexicalState.GUID2:
						ProcessGuid2();
						break;

					case LexicalState.GUID3:
						ProcessGuid3();
						break;

					case LexicalState.GUID4:
						ProcessGuid4();
						break;

					case LexicalState.GUID5:
						ProcessGuid5();
						break;

					case LexicalState.TIMESPAN1:
						ProcessTimeSpan1();
						break;

					case LexicalState.TIMESPAN2:
						ProcessTimeSpan2();
						break;

					case LexicalState.TIMESPAN3:
						ProcessTimeSpan3();
						break;

					case LexicalState.TIMESPAN4:
						ProcessTimeSpan4();
						break;

					case LexicalState.TIMESPAN5:
						ProcessTimeSpan5();
						break;

					case LexicalState.YMD1:
						ProcessYMD1();
						break;

					case LexicalState.YMD2:
						ProcessYMD2();
						break;

					case LexicalState.YMD3:
						ProcessYMD3();
						break;

					case LexicalState.MDY1:
						ProcessMDY1();
						break;
						
					case LexicalState.MDY2:
						ProcessMDY2();
						break;

					case LexicalState.MDY3:
						ProcessMDY3();
						break;

					case LexicalState.MDY4:
						ProcessMDY4();
						break;

					case LexicalState.MDY5:
						ProcessMDY5();
						break;

					case LexicalState.MDY6:
						ProcessMDY6();
						break;

					case LexicalState.MDY7:
						ProcessMDY7();
						break;

					case LexicalState.MDY8:
						ProcessMDY8();
						break;

					case LexicalState.MDY9:
						ProcessMDY9();
						break;

					case LexicalState.DATETIME1:
						ProcessDateTime1();
						break;

					case LexicalState.UTC1:
						ProcessUTC1();
						break;

					case LexicalState.UTC2:
						ProcessUTC2();
						break;

					case LexicalState.UTC3:
						ProcessUTC3();
						break;

					case LexicalState.UTC4:
						ProcessUTC4();
						break;

					case LexicalState.UTC5:
						ProcessUTC5();
						break;

					case LexicalState.UTC6:
						ProcessUTC6();
						break;

					case LexicalState.UTC7:
						ProcessUTC7();
						break;

					case LexicalState.UTC8:
						ProcessUTC8();
						break;

					case LexicalState.UTC9:
						ProcessUTC9();
						break;

					case LexicalState.UTC10:
						ProcessUTC10();
						break;

					case LexicalState.UTC11:
						ProcessUTC11();
						break;

					case LexicalState.UTC12:
						ProcessUTC12();
						break;

					case LexicalState.HEXVAL:
						ProcessHexVal();
						break;

					case LexicalState.UTINYHEX:
						ProcessUTinyHexVal();
						break;

					case LexicalState.USHORTHEXVAL:
						ProcessUShortHexVal();
						break;

					case LexicalState.UINTHEXVAL:
						ProcessUIntHexVal();
						break;

					case LexicalState.ULONGHEXVAL:
						ProcessULongHexVal();
						break;

					case LexicalState.TINYHEX:
						ProcessTinyHexVal();
						break;

					case LexicalState.SHORTHEXVAL:
						ProcessShortHexVal();
						break;

					case LexicalState.INTHEXVAL:
						ProcessIntHexVal();
						break;

					case LexicalState.LONGHEXVAL:
						ProcessLongHexVal();
						break;

					case LexicalState.BSTR1:
						ProcessBStr1();
						break;

					case LexicalState.BSTR2:
						ProcessBStr2();
						break;

					case LexicalState.QURI1:
						ProcessQUri1();
						break;

					case LexicalState.URI1:
						ProcessUri1();
						break;

					case LexicalState.URI2:
						ProcessUri2();
						break;

					default:
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
			}
		}

		private void ProcessQUri1()
		{
			if (_input != null)
			{
				if (_input.Peek() == '\\')
				{
					_input.Read();
					if (_input.EndOfStream)
						throw new RqlFormatException($"Unmattched \" in {yytext}. Aborting scan.");

					if (_input.Peek() == '\\')
						yytext.Append(_input.Read());
					else if (_input.Peek() == '"')
						yytext.Append(_input.Read());
					else if (_input.Peek() == 'r')
					{
						_input.Read();
						yytext.Append('\r');
					}
					else if (_input.Peek() == 't')
					{
						_input.Read();
						yytext.Append('\t');
					}
					else if (_input.Peek() == 'n')
					{
						_input.Read();
						yytext.Append('\n');
					}
					else
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
				else if (_input.Peek() == '"')
				{
					try
					{
						_input.Read();
						_state = LexicalState.INITIAL_STATE;

						if (yytext[0] == '/')
							_tokenStream.Push(new Token(Symbol.URI, new Uri(yytext.ToString(), UriKind.Relative)));
						else
							_tokenStream.Push(new Token(Symbol.URI, new Uri(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Invalid URI format: {yytext}. Aborting scan.");
					}
				}
				else
				{
					yytext.Append(_input.Read());
					if (_input.EndOfStream)
						throw new RqlFormatException($"Unmattched \" in {yytext}. Aborting scan.");
				}
			}
			else
			{
				throw new RqlFormatException($"Unmattched \" in {yytext}. Aborting scan.");
			}
		}

		private void ProcessUri1()
		{
			if (_input != null)
			{
				if (_input.Peek() == '/')
				{
					yytext.Append(_input.Read());
					_state = LexicalState.URI2;

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;

							if (yytext[0] == '/')
							{
								var theUri = new Uri(yytext.ToString(), UriKind.Relative);
								_tokenStream.Push(new Token(Symbol.URI, theUri));
							}
							else
							{
								_tokenStream.Push(new Token(Symbol.URI, new Uri(yytext.ToString())));
							}
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Invalid URI format: {yytext}. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == 'h' || _input.Peek() == 'H')
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

					if (_input.Peek() == 't' || _input.Peek() == 'T')
					{
						yytext.Append(_input.Read());

						if (_input.EndOfStream)
							throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

						if (_input.Peek() == 't' || _input.Peek() == 'T')
						{
							yytext.Append(_input.Read());

							if (_input.EndOfStream)
								throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

							if (_input.Peek() == 'p' || _input.Peek() == 'P')
							{
								yytext.Append(_input.Read());

								if (_input.EndOfStream)
									throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

								if (_input.Peek() == ':')
								{
									yytext.Append(_input.Read());

									if (_input.EndOfStream)
										throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

									if (_input.Peek() == '/')
									{
										yytext.Append(_input.Read());

										if (_input.EndOfStream)
											throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

										if (_input.Peek() == '/')
										{
											yytext.Append(_input.Read());

											if (_input.EndOfStream)
												throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

											_state = LexicalState.URI2;
										}
										else
											throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
									}
									else
										throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
								}
								else if (_input.Peek() == 's' || _input.Peek() == 'S')
								{
									yytext.Append(_input.Read());

									if (_input.EndOfStream)
										throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

									if (_input.Peek() == ':')
									{
										yytext.Append(_input.Read());

										if (_input.EndOfStream)
											throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

										if (_input.Peek() == '/')
										{
											yytext.Append(_input.Read());

											if (_input.EndOfStream)
												throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

											if (_input.Peek() == '/')
											{
												yytext.Append(_input.Read());

												if (_input.EndOfStream)
													throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

												_state = LexicalState.URI2;
											}
											else
												throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
										}
										else
											throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
									}
									else
										throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
								}
								else
									throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
							}
							else
								throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
						}
						else
							throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}
					else
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
				else
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessUri2()
		{
			if (_input != null)
			{
				if ((_input.Peek() >= 'a' && _input.Peek() <= 'z') ||
					(_input.Peek() >= 'A' && _input.Peek() <= 'Z') ||
					(_input.Peek() >= '0' && _input.Peek() <= '9'))
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;

							if (yytext[0] == '/')
							{
								var theUri = new Uri(yytext.ToString(), UriKind.Relative);
								_tokenStream.Push(new Token(Symbol.URI, theUri));
							}
							else
							{
								_tokenStream.Push(new Token(Symbol.URI, new Uri(yytext.ToString())));
							}
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Invalid URI format: {yytext}. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == ':' || _input.Peek() == '\\' || _input.Peek() == '/' || _input.Peek() == '.' ||
						 _input.Peek() == '%' || _input.Peek() == '?' || _input.Peek() == '=' || _input.Peek() == '<' ||
						 _input.Peek() == '>' || _input.Peek() == '!' || _input.Peek() == '(' || _input.Peek() == ')' ||
						 _input.Peek() == '-' || _input.Peek() == '+' || _input.Peek() == '_' || _input.Peek() == '[' ||
						 _input.Peek() == ']' || _input.Peek() == '|' || _input.Peek() == ',' || _input.Peek() == ';' ||
						 _input.Peek() == '`' || _input.Peek() == '~' || _input.Peek() == '"' || _input.Peek() == '\'' ||
						 _input.Peek() == '&' || _input.Peek() == '#' || _input.Peek() == '@' || _input.Peek() == '*')
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							if (yytext[0] == '/')
							{
								var theUri = new Uri(yytext.ToString(), UriKind.Relative);
								_tokenStream.Push(new Token(Symbol.URI, theUri));
							}
							else
							{
								_tokenStream.Push(new Token(Symbol.URI, new Uri(yytext.ToString())));
							}
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Invalid URI format: {yytext}. Aborting scan.");
						}
					}
				}
				else
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						if (yytext[0] == '/')
						{
							var theUri = new Uri(yytext.ToString(), UriKind.Relative);
							_tokenStream.Push(new Token(Symbol.URI, theUri));
						}
						else
						{
							_tokenStream.Push(new Token(Symbol.URI, new Uri(yytext.ToString())));
						}
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Invalid URI format: {yytext}. Aborting scan.");
					}
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessString1()
		{
			if (_input != null)
			{
				if (_input.Peek() == '"')
				{
					_input.Read();
					_state = LexicalState.QSTRING1;

					if (_input.EndOfStream)
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
				else if (IsStringSpecialChar())
				{
					_state = LexicalState.STRING2;
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
						yytext.Clear();
					}
				}
				else if (_input.Peek() == '\\')
				{
					_input.Read();

					if (_input.EndOfStream)
					{
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}

					if (_input.Peek() == 'r')
					{
						yytext.Append('\r');
						_input.Read();
					}
					else if (_input.Peek() == 'n')
					{
						yytext.Append('\n');
						_input.Read();
					}
					else if (_input.Peek() == 't')
					{
						yytext.Append('\t');
						_input.Read();
					}
					else if (_input.Peek() == 'b')
					{
						yytext.Append('\b');
						_input.Read();
					}
					else if (_input.Peek() == '"')
					{
						yytext.Append('\"');
						_input.Read();
					}
					else if (_input.Peek() == '\\')
					{
						yytext.Append('\\');
						_input.Read();
					}
					else if (_input.Peek() == 'u' || _input.Peek() == 'U' || _input.Peek() == 'x' || _input.Peek() == 'X')
					{
						_input.Read();

						StringBuilder num = new();

						for (int i = 0; i < 4; i++)
						{
							if (_input.EndOfStream)
								throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

							if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
								(_input.Peek() >= 'a' && _input.Peek() <= 'f') ||
								(_input.Peek() >= 'A' && _input.Peek() <= 'F'))
							{
								num.Append(_input.Read());
							}
							else
								break;
						}

						var character = System.Convert.ToChar(Convert.ToUInt32(num.ToString(), 16));
						yytext.Append(character);

						if (_input.EndOfStream)
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
							yytext.Clear();
						}
					}
					else
					{
						yytext.Append(_input.Read());
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
						yytext.Clear();
					}
				}
				else if ((_input.Peek() >= 'a' && _input.Peek() <= 'z') ||
						  (_input.Peek() >= 'A' && _input.Peek() <= 'Z') ||
						  (_input.Peek() >= '0' && _input.Peek() <= '9') ||
						  _input.Peek() == '_')
				{
					yytext.Append(_input.Read());
					_state = LexicalState.STRING2;

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
						yytext.Clear();
					}
				}
				else
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessString2()
		{
			if (_input != null)
			{
				if (IsStringSpecialChar())
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
						yytext.Clear();
					}
				}
				else if (_input.Peek() == '\\')
				{
					_input.Read();

					if (_input.EndOfStream)
					{
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}

					if (_input.Peek() == 'r')
					{
						yytext.Append('\r');
						_input.Read();
					}
					else if (_input.Peek() == 'n')
					{
						yytext.Append('\n');
						_input.Read();
					}
					else if (_input.Peek() == 't')
					{
						yytext.Append('\t');
						_input.Read();
					}
					else if (_input.Peek() == 'b')
					{
						yytext.Append('\b');
						_input.Read();
					}
					else if (_input.Peek() == '"')
					{
						yytext.Append('\"');
						_input.Read();
					}
					else if (_input.Peek() == '\\')
					{
						yytext.Append('\\');
						_input.Read();
					}
					else if (_input.Peek() == 'u' || _input.Peek() == 'U' || _input.Peek() == 'x' || _input.Peek() == 'X')
					{
						_input.Read();

						StringBuilder num = new();

						for (int i = 0; i < 4; i++)
						{
							if (_input.EndOfStream)
								throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

							if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
								(_input.Peek() >= 'a' && _input.Peek() <= 'f') ||
								(_input.Peek() >= 'A' && _input.Peek() <= 'F'))
							{
								num.Append(_input.Read());
							}
							else
								break;
						}

						var character = System.Convert.ToChar(Convert.ToUInt32(num.ToString(), 16));
						yytext.Append(character);

						if (_input.EndOfStream)
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
							yytext.Clear();
						}
					}
					else
					{
						yytext.Append(_input.Read());
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
						yytext.Clear();
					}
				}
				else if ((_input.Peek() >= 'a' && _input.Peek() <= 'z') ||
						  (_input.Peek() >= 'A' && _input.Peek() <= 'Z') ||
						  (_input.Peek() >= '0' && _input.Peek() <= '9') ||
						  _input.Peek() == '_')
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
						yytext.Clear();
					}
				}
				else
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private bool IsStringSpecialChar()
		{
			if (_input != null)
			{
				if (_input.Peek() == '?' || _input.Peek() == '~' || _input.Peek() == ';' ||
					_input.Peek() == '[' || _input.Peek() == ']' || _input.Peek() == '@' ||
					_input.Peek() == '#' || _input.Peek() == '$' || _input.Peek() == '^' ||
					_input.Peek() == '*' || _input.Peek() == '{' ||
					_input.Peek() == '}' || _input.Peek() == '\t' || _input.Peek() == '\r' ||
					_input.Peek() == '\n' || _input.Peek() == '\b' || _input.Peek() == '`' ||
					_input.Peek() == '%')
				{
					return true;
				}

				return false;
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessBStr1()
		{
			if (_input != null)
			{
				if (_input.Peek() != ':')
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
				else
				{
					_input.Read();
					currentBStr.Encoding = yytext.ToString();
					yytext.Clear();
					_state = LexicalState.BSTR2;
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessBStr2()
		{
			if (_input != null)
			{
				if ((_input.Peek() >= 'a' && _input.Peek() <= 'z') || (_input.Peek() >= 'A' && _input.Peek() <= 'Z') || (_input.Peek() >= '0' && _input.Peek() <= '9') || _input.Peek() == '+' || _input.Peek() == '/' || _input.Peek() == '=')
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
					{
						currentBStr.Value = yytext.ToString();
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.STRING, currentBStr));
						yytext.Clear();
					}
				}
				else
				{
					currentBStr.Value = yytext.ToString();
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.STRING, currentBStr));
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessCharA()
		{
			if (_input != null)
			{
				if (_input.Peek() == '\'')
				{
					_input.Read();
					_state = LexicalState.CHAR1;

					if (_input.EndOfStream)
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
				else if (_input.Peek() == '\\')
				{
					_input.Read();

					if (_input.EndOfStream)
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

					if (_input.Peek() == 'r')
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.CHAR, '\r'));
						yytext.Clear();
					}
					else if (_input.Peek() == 'n')
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.CHAR, '\n'));
						yytext.Clear();
					}
					else if (_input.Peek() == 't')
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.CHAR, '\t'));
						yytext.Clear();
					}
					else if (_input.Peek() == 'b')
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.CHAR, '\b'));
						yytext.Clear();
					}
					else if (_input.Peek() == '\\')
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.CHAR, '\\'));
						yytext.Clear();
					}
					else if (_input.Peek() == '\'')
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.CHAR, '\''));
						yytext.Clear();
					}
					else
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
				else
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.CHAR, _input.Read()));
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessChar1()
		{
			if (_input != null)
			{
				if (_input.Peek() == '\\')
				{
					_input.Read();

					if (_input.EndOfStream)
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

					if (_input.Peek() == 'r')
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.CHAR, '\r'));
						yytext.Clear();
					}
					else if (_input.Peek() == 'n')
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.CHAR, '\n'));
						yytext.Clear();
					}
					else if (_input.Peek() == 't')
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.CHAR, '\t'));
						yytext.Clear();
					}
					else if (_input.Peek() == 'b')
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.CHAR, '\b'));
						yytext.Clear();
					}
					else if (_input.Peek() == '\\')
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.CHAR, '\\'));
						yytext.Clear();
					}
					else if (_input.Peek() == '\'')
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.CHAR, '\''));
						yytext.Clear();
					}
					else
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
				else
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.CHAR, _input.Read()));
					yytext.Clear();
				}

				if (_input.EndOfStream)
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

				if (_input.Peek() != '\'')
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

				_input.Read();
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessTinyHexVal()
		{
			if (_input != null)
			{
				if ((_input.Peek() >= 'a' && _input.Peek() <= 'f') || (_input.Peek() >= 'A' && _input.Peek() <= 'F') || (_input.Peek() >= '0' && _input.Peek() <= '9'))
				{
					if (yytext.Length < 2)
					{
						yytext.Append(_input.Read());

						if (_input.EndOfStream)
						{
							if (yytext.Length == 2)
							{
								var int8 = Convert.ToSByte(yytext.ToString(), 16);
								_tokenStream.Push(new Token(Symbol.TINY, int8));
							}
							else
								throw new RqlFormatException($"Invalid length for hexidecimal value {yytext}. Hexidecimal values must be 2, 4, 8 or 16 characters in length. Aborting scan.");

							_state = LexicalState.INITIAL_STATE;
							yytext.Clear();
						}
					}
					else
						throw new RqlFormatException($"Invalid length for tiny hexidecimal value {yytext}. Tiny hexidecimal values must be 2 characters in length. Aborting scan.");
				}
				else
				{
					if (yytext.Length == 2)
					{
						var int8 = Convert.ToSByte(yytext.ToString(), 16);
						_tokenStream.Push(new Token(Symbol.TINY, int8));
					}
					else
						throw new RqlFormatException($"Invalid length for tiny hexidecimal value {yytext}. Tiny hexidecimal values must be 2 characters in length. Aborting scan.");

					_state = LexicalState.INITIAL_STATE;
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessShortHexVal()
		{
			if (_input != null)
			{
				if ((_input.Peek() >= 'a' && _input.Peek() <= 'f') || (_input.Peek() >= 'A' && _input.Peek() <= 'F') || (_input.Peek() >= '0' && _input.Peek() <= '9'))
				{
					if (yytext.Length < 4)
					{
						yytext.Append(_input.Read());

						if (_input.EndOfStream)
						{
							if (yytext.Length == 4)
							{
								var int16 = Convert.ToInt16(yytext.ToString(), 16);
								_tokenStream.Push(new Token(Symbol.SHORT, int16));
							}
							else
								throw new RqlFormatException($"Invalid length for short hexidecimal value {yytext}. Short hexidecimal values must be 8 characters in length. Aborting scan.");

							_state = LexicalState.INITIAL_STATE;
							yytext.Clear();
						}
					}
					else
						throw new RqlFormatException($"Invalid length for short hexidecimal value {yytext}. Short hexidecimal values must be 8 characters in length. Aborting scan.");
				}
				else
				{
					if (yytext.Length == 4)
					{
						var int16 = Convert.ToInt16(yytext.ToString(), 16);
						_tokenStream.Push(new Token(Symbol.SHORT, int16));
					}
					else
						throw new RqlFormatException($"Invalid length for short hexidecimal value {yytext}. Short hexidecimal values must be 8 characters in length. Aborting scan.");

					_state = LexicalState.INITIAL_STATE;
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessIntHexVal()
		{
			if (_input != null)
			{
				if ((_input.Peek() >= 'a' && _input.Peek() <= 'f') || (_input.Peek() >= 'A' && _input.Peek() <= 'F') || (_input.Peek() >= '0' && _input.Peek() <= '9'))
				{
					if (yytext.Length < 8)
					{
						yytext.Append(_input.Read());

						if (_input.EndOfStream)
						{
							if (yytext.Length == 8)
							{
								var int32 = Convert.ToInt32(yytext.ToString(), 16);
								_tokenStream.Push(new Token(Symbol.INTEGER, int32));
							}
							else
								throw new RqlFormatException($"Invalid length for Int32 hexidecimal value {yytext}. Int32 hexidecimal values must be 8 characters in length. Aborting scan.");

							_state = LexicalState.INITIAL_STATE;
							yytext.Clear();
						}
					}
					else
						throw new RqlFormatException($"Invalid length for Int32 hexidecimal value {yytext}. Int32 hexidecimal values must be 8 characters in length. Aborting scan.");
				}
				else
				{
					if (yytext.Length == 8)
					{
						var int32 = Convert.ToInt32(yytext.ToString(), 16);
						_tokenStream.Push(new Token(Symbol.INTEGER, int32));
					}
					else
						throw new RqlFormatException($"Invalid length for Int32 hexidecimal value {yytext}. Int32 hexidecimal values must be 8 characters in length. Aborting scan.");

					_state = LexicalState.INITIAL_STATE;
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessLongHexVal()
		{
			if (_input != null)
			{
				if ((_input.Peek() >= 'a' && _input.Peek() <= 'f') || (_input.Peek() >= 'A' && _input.Peek() <= 'F') || (_input.Peek() >= '0' && _input.Peek() <= '9'))
				{
					if (yytext.Length < 16)
					{
						yytext.Append(_input.Read());

						if (_input.EndOfStream)
						{
							if (yytext.Length == 16)
							{
								var int32 = Convert.ToInt64(yytext.ToString(), 16);
								_tokenStream.Push(new Token(Symbol.LONG, int32));
							}
							else
								throw new RqlFormatException($"Invalid length for long hexidecimal value {yytext}. Long hexidecimal values must be 16 characters in length. Aborting scan.");

							_state = LexicalState.INITIAL_STATE;
							yytext.Clear();
						}
					}
					else
						throw new RqlFormatException($"Invalid length for long hexidecimal value {yytext}. Long hexidecimal values must be 16 characters in length. Aborting scan.");
				}
				else
				{
					if (yytext.Length == 16)
					{
						var int32 = Convert.ToInt64(yytext.ToString(), 16);
						_tokenStream.Push(new Token(Symbol.LONG, int32));
					}
					else
						throw new RqlFormatException($"Invalid length for long hexidecimal value {yytext}. Long hexidecimal values must be 16 characters in length. Aborting scan.");

					_state = LexicalState.INITIAL_STATE;
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessUTinyHexVal()
		{
			if (_input != null)
			{
				if ((_input.Peek() >= 'a' && _input.Peek() <= 'f') || (_input.Peek() >= 'A' && _input.Peek() <= 'F') || (_input.Peek() >= '0' && _input.Peek() <= '9'))
				{
					if (yytext.Length < 2)
					{
						yytext.Append(_input.Read());

						if (_input.EndOfStream)
						{
							if (yytext.Length == 2)
								_tokenStream.Push(new Token(Symbol.UTINY, Convert.ToByte(yytext.ToString(), 16)));
							else
								throw new RqlFormatException($"Invalid length for hexidecimal value {yytext}. Hexidecimal values must be 2, 4, 8 or 16 characters in length. Aborting scan.");

							_state = LexicalState.INITIAL_STATE;
							yytext.Clear();
						}
					}
					else
						throw new RqlFormatException($"Invalid length for tiny hexidecimal value {yytext}. Tiny hexidecimal values must be 2 characters in length. Aborting scan.");
				}
				else
				{
					if (yytext.Length == 2)
						_tokenStream.Push(new Token(Symbol.UTINY, Convert.ToByte(yytext.ToString(), 16)));
					else
						throw new RqlFormatException($"Invalid length for tiny hexidecimal value {yytext}. Tiny hexidecimal values must be 2 characters in length. Aborting scan.");

					_state = LexicalState.INITIAL_STATE;
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessUShortHexVal()
		{
			if (_input != null)
			{
				if ((_input.Peek() >= 'a' && _input.Peek() <= 'f') || (_input.Peek() >= 'A' && _input.Peek() <= 'F') || (_input.Peek() >= '0' && _input.Peek() <= '9'))
				{
					if (yytext.Length < 4)
					{
						yytext.Append(_input.Read());

						if (_input.EndOfStream)
						{
							if (yytext.Length == 4)
								_tokenStream.Push(new Token(Symbol.USHORT, Convert.ToUInt16(yytext.ToString(), 16)));
							else
								throw new RqlFormatException($"Invalid length for short hexidecimal value {yytext}. Short hexidecimal values must be 4 characters in length. Aborting scan.");

							_state = LexicalState.INITIAL_STATE;
							yytext.Clear();
						}
					}
					else
						throw new RqlFormatException($"Invalid length for short hexidecimal value {yytext}. Short hexidecimal values must be 4 characters in length. Aborting scan.");
				}
				else
				{
					if (yytext.Length == 4)
						_tokenStream.Push(new Token(Symbol.USHORT, Convert.ToUInt16(yytext.ToString(), 16)));
					else
						throw new RqlFormatException($"Invalid length for short hexidecimal value {yytext}. Short hexidecimal values must be 4 characters in length. Aborting scan.");

					_state = LexicalState.INITIAL_STATE;
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessUIntHexVal()
		{
			if (_input != null)
			{
				if ((_input.Peek() >= 'a' && _input.Peek() <= 'f') || (_input.Peek() >= 'A' && _input.Peek() <= 'F') || (_input.Peek() >= '0' && _input.Peek() <= '9'))
				{
					if (yytext.Length < 8)
					{
						yytext.Append(_input.Read());

						if (_input.EndOfStream)
						{
							if (yytext.Length == 8)
								_tokenStream.Push(new Token(Symbol.UINTEGER, Convert.ToUInt32(yytext.ToString(), 16)));
							else
								throw new RqlFormatException($"Invalid length for int32 hexidecimal value {yytext}. Int32 hexidecimal values must be 8 characters in length. Aborting scan.");

							_state = LexicalState.INITIAL_STATE;
							yytext.Clear();
						}
					}
					else
						throw new RqlFormatException($"Invalid length for int32 hexidecimal value {yytext}. Int32 hexidecimal values must be 8 characters in length. Aborting scan.");
				}
				else
				{
					if (yytext.Length == 8)
						_tokenStream.Push(new Token(Symbol.UINTEGER, Convert.ToUInt32(yytext.ToString(), 16)));
					else
						throw new RqlFormatException($"Invalid length for int32 hexidecimal value {yytext}. Int32 hexidecimal values must be 8 characters in length. Aborting scan.");

					_state = LexicalState.INITIAL_STATE;
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessULongHexVal()
		{
			if (_input != null)
			{
				if ((_input.Peek() >= 'a' && _input.Peek() <= 'f') || (_input.Peek() >= 'A' && _input.Peek() <= 'F') || (_input.Peek() >= '0' && _input.Peek() <= '9'))
				{
					if (yytext.Length < 16)
					{
						yytext.Append(_input.Read());

						if (_input.EndOfStream)
						{
							if (yytext.Length == 16)
								_tokenStream.Push(new Token(Symbol.ULONG, Convert.ToUInt64(yytext.ToString(), 16)));
							else
								throw new RqlFormatException($"Invalid length for long hexidecimal value {yytext}. Long hexidecimal values must be 16 characters in length. Aborting scan.");

							_state = LexicalState.INITIAL_STATE;
							yytext.Clear();
						}
					}
					else
						throw new RqlFormatException($"Invalid length for long hexidecimal value {yytext}. Long hexidecimal values must be 16 characters in length. Aborting scan.");
				}
				else
				{
					if (yytext.Length == 16)
						_tokenStream.Push(new Token(Symbol.ULONG, Convert.ToUInt64(yytext.ToString(), 16)));
					else
						throw new RqlFormatException($"Invalid length for long hexidecimal value {yytext}. Long hexidecimal values must be 16 characters in length. Aborting scan.");

					_state = LexicalState.INITIAL_STATE;
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessHexVal()
		{
			if (_input != null)
			{
				if ((_input.Peek() >= 'a' && _input.Peek() <= 'f') || (_input.Peek() >= 'A' && _input.Peek() <= 'F') || (_input.Peek() >= '0' && _input.Peek() <= '9'))
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
					{
						if (yytext.Length == 2)
							_tokenStream.Push(new Token(Symbol.UTINY, Convert.ToByte(yytext.ToString(), 16)));
						else if (yytext.Length == 4)
							_tokenStream.Push(new Token(Symbol.USHORT, Convert.ToUInt16(yytext.ToString(), 16)));
						else if (yytext.Length == 8)
							_tokenStream.Push(new Token(Symbol.UINTEGER, Convert.ToUInt32(yytext.ToString(), 16)));
						else if (yytext.Length == 16)
							_tokenStream.Push(new Token(Symbol.ULONG, Convert.ToUInt64(yytext.ToString(), 16)));
						else
							throw new RqlFormatException($"Invalid length for hexidecimal value {yytext}. Hexidecimal values must be 2, 4, 8 or 16 characters in length. Aborting scan.");

						_state = LexicalState.INITIAL_STATE;
						yytext.Clear();
					}
				}
				else
				{
					if (yytext.Length == 2)
						_tokenStream.Push(new Token(Symbol.UTINY, Convert.ToByte(yytext.ToString(), 16)));
					else if (yytext.Length == 4)
						_tokenStream.Push(new Token(Symbol.USHORT, Convert.ToUInt16(yytext.ToString(), 16)));
					else if (yytext.Length == 8)
						_tokenStream.Push(new Token(Symbol.UINTEGER, Convert.ToUInt32(yytext.ToString(), 16)));
					else if (yytext.Length == 16)
						_tokenStream.Push(new Token(Symbol.ULONG, Convert.ToUInt64(yytext.ToString(), 16)));
					else
						throw new RqlFormatException($"Invalid length for hexidecimal value {yytext}. Hexidecimal values must be 2, 4, 8 or 16 characters in length. Aborting scan.");

					_state = LexicalState.INITIAL_STATE;
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessDateTime1()
		{
			if (yytext.Length < 4 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length <= 2 && (_input.Peek() >= '-' || _input.Peek() <= '/'))
			{
				yytext.Append(_input.Read());
				_state = LexicalState.MDY1;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if ((yytext.Length > 2  && yytext.Length <=4) && (_input.Peek() >= '-' || _input.Peek() <= '/'))
			{
				yytext.Append(_input.Read());
				_state = LexicalState.YMD1;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
		}

		private void ProcessUTC1()
		{
			if (yytext.Length < 4 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length <= 2 && (_input.Peek() >= '-' || _input.Peek() <= '/'))
			{
				yytext.Append(_input.Read());
				_state = LexicalState.UTC2;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if ((yytext.Length > 2 && yytext.Length <= 4) && (_input.Peek() >= '-' || _input.Peek() <= '/'))
			{
				yytext.Append(_input.Read());
				_state = LexicalState.UTC10;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
		}

		private void ProcessUTC2()
		{
			// m/  mm/
			// m-  mm			
			var index1 = yytext.ToString().LastIndexOf('-');
			var index2 = yytext.ToString().LastIndexOf('/');
			var index = index1 > index2 ? index1 : index2;

			if (yytext.Length - index - 1 < 2 &&
				 _input.Peek() >= '0' &&
				 _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if ( yytext.Length - index - 1 >= 1 && (_input.Peek() == '-' || _input.Peek() == '/'))
			{
				yytext.Append(_input.Read());
				_state = LexicalState.UTC3;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
		}

		private void ProcessUTC3()
		{
			//	mm/0 - mm/dd/
			//	mm-0 - mm-dd/
			var index1 = yytext.ToString().LastIndexOf('-');
			var index2 = yytext.ToString().LastIndexOf('/');
			var index = index1 > index2 ? index1 : index2;

			if (yytext.Length - index - 1 < 4 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
				{
					_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext} 00:00:00+0000").UtcDateTime));
					yytext.Clear();
				}
			}
			else if ((yytext.Length - index - 1 >= 1 && yytext.Length - index - 1 <= 4) && (_input.Peek() == 'z' || _input.Peek() == 'Z'))
			{
				yytext.Append(_input.Read());
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
			else if (yytext.Length - index - 1 > 1 && (_input.Peek() == 'g' || _input.Peek() == 'G'))
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");

				if (_input.Peek() == 'm' || _input.Peek() == 'M')
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");

					if (_input.Peek() == 't' || _input.Peek() == 'T')
					{
						yytext.Append(_input.Read());
						_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
						yytext.Clear();
						_state = LexicalState.INITIAL_STATE;
					}
					else
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
				}
				else
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");

			}
			else if ((yytext.Length - index - 1 >= 1 && yytext.Length - index - 1 <= 4) && _input.Peek() == ' ')
			{
				yytext.Append(_input.Read());
				_state = LexicalState.UTC4;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if ((yytext.Length - index - 1 >= 1 && yytext.Length - index - 1 <= 4) && (_input.Peek() == '+' || _input.Peek() == '+'))
			{
				yytext.Append(_input.Read());
				_state = LexicalState.UTC8;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if ((yytext.Length - index - 1 >= 1 && yytext.Length - index - 1 <= 4))
			{
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext} 00:00:00+0000").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
			else
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");

		}

		private void ProcessUTC4()
		{
			//	mm/dd/yyyy<sp>
			//	mm-dd-yyyy<sp>
			var index = yytext.ToString().LastIndexOf(' ');
			if ( _input.Peek() == ' ' || _input.Peek() == '\t')
            {
				_input.Read();	//	Skip whitespace
            }
			else if (yytext.Length - index - 1 == 0 && (_input.Peek() == 'g' || _input.Peek() == 'G'))
            {
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");

				if (_input.Peek() == 'm' || _input.Peek() == 'M')
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");

					if (_input.Peek() == 't' || _input.Peek() == 'T')
					{
						yytext.Append(_input.Read());
						_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
						yytext.Clear();
						_state = LexicalState.INITIAL_STATE;
					}
					else
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
				}
				else
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");

			}
			else if (yytext.Length - index - 1 == 0 && (_input.Peek() == 'z' || _input.Peek() == 'Z'))
            {
				yytext.Append(_input.Read());
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
			else if (yytext.Length - index - 1 < 2 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 >= 1 && _input.Peek() == ':')
			{
				yytext.Append(_input.Read());
				_state = LexicalState.UTC5;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 >= 1 && (_input.Peek() == '-' || _input.Peek() == '+' ))
			{
				yytext.Append(_input.Read());
				_state = LexicalState.UTC8;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
		}

		private void ProcessUTC5()
		{
			//	mm-dd-yyyy hh:
			//	mm/dd/yyyy hh:

			var index = yytext.ToString().LastIndexOf(':');

			if (yytext.Length - index - 1 < 2 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 >- 1 && _input.Peek() >= ':' )
			{
				yytext.Append(_input.Read());
				_state = LexicalState.UTC6;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
		}

		private void ProcessUTC6()
		{
			//	mm/dd/yyyy hh:mm:
			var index = yytext.ToString().LastIndexOf(':');

			if (_input.Peek() == ' ' || _input.Peek() == '\t')
            {
				_input.Read();		//	Skip Whitespace
            }
			if (yytext.Length - index - 1 < 2 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
				{
					_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}+0000").UtcDateTime));
					yytext.Clear();
				}
			}
			else if (yytext.Length - index - 1 > 1 && _input.Peek() == '.')
			{
				yytext.Append(_input.Read());
				_state = LexicalState.UTC7;

				if (_input.EndOfStream)
				{
					_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}+0000").UtcDateTime));
					yytext.Clear();
				}
			}
			else if (yytext.Length - index - 1 > 1 && (_input.Peek() == '+' || _input.Peek() == '-'))
			{
				yytext.Append(_input.Read());
				_state = LexicalState.UTC8;

				if (_input.EndOfStream)
				{
					_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}0000").UtcDateTime));
					yytext.Clear();
				}
			}
			else if (yytext.Length - index - 1 > 1 && (_input.Peek() == 'z' || _input.Peek() == 'Z'))
			{
				yytext.Append(_input.Read());
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
			else if (yytext.Length - index - 1 > 1 && (_input.Peek() == 'g' || _input.Peek() == 'G'))
			{
				yytext.Append(_input.Read());
				if ( _input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");

				else if (_input.Peek() == 'm' || _input.Peek() == 'M')
				{
					yytext.Append(_input.Read());
					if (_input.EndOfStream)
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");

					else if (_input.Peek() == 't' || _input.Peek() == 'T')
					{
						yytext.Append(_input.Read());
						_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
						yytext.Clear();
						_state = LexicalState.INITIAL_STATE;
					}
					else
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
				}
				else
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else
			{
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}+0000").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
		}

		private void ProcessUTC7()
		{
			//	mm/dd/yyyy hh:mm:ss.

			var index = yytext.ToString().LastIndexOf('.');

			if ( _input.Peek() == ' ' || _input.Peek() == '\t')
            {
				_input.Read();	//	Skip whitespace
            }
			else if (yytext.Length - index - 1 < 6 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
				{
					_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}+0000").UtcDateTime));
					yytext.Clear();
				}
			}
			else if (yytext.Length - index - 1 >= 1 && (_input.Peek() == '-' || _input.Peek() == '+'))
			{
				yytext.Append(_input.Read());
				_state = LexicalState.UTC8;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 >= 1 && (_input.Peek() == 'z' || _input.Peek() == 'Z'))
			{
				yytext.Append(_input.Read());
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse(yytext.ToString()).UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
			else if (yytext.Length - index - 1 >= 1 && 
				    (_input.Peek() == 'g' || _input.Peek() == 'G') &&
					!yytext.ToString().Contains('T') &&
					!yytext.ToString().Contains('t'))
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
				else if (_input.Peek() == 'm' || _input.Peek() == 'M')
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
					else if (_input.Peek() == 't' || _input.Peek() == 'T')
					{
						yytext.Append(_input.Read());
						_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse(yytext.ToString()).UtcDateTime));
						yytext.Clear();
						_state = LexicalState.INITIAL_STATE;
					}
				}
			}
			else
			{
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}+0000").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
		}

		private void ProcessUTC8()
		{
			//	mm/dd/yyyy hh:mm:ss.ssssss+

			var index1 = yytext.ToString().LastIndexOf('+');
			var index2 = yytext.ToString().LastIndexOf('-');
			var index = index1 > index2 ? index1 : index2;

			if (yytext.Length - index - 1 < 4 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());
				
				if (_input.EndOfStream)
				{
					_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse(yytext.ToString()).UtcDateTime));
					yytext.Clear();
				}
			}
			else if (yytext.Length - index - 1 >= 1 && _input.Peek() == ':' )
			{
				yytext.Append(_input.Read());
				_state = LexicalState.UTC9;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else
			{
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse(yytext.ToString()).UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
		}

		private void ProcessUTC9()
		{
			//	mm/dd/yyyy hh:mm:ss.ffffff+00:

			var index = yytext.ToString().LastIndexOf(':');

			if (yytext.Length - index - 1 < 2 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
				{
					_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse(yytext.ToString()).UtcDateTime));
					yytext.Clear();
				}
			}
			else
			{
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse(yytext.ToString()).UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
		}

		private void ProcessUTC10()
		{
			//	yyyy/
			//	yyyy-

			var index1 = yytext.ToString().LastIndexOf('-');
			var index2 = yytext.ToString().LastIndexOf('/');
			var index = index1 > index2 ? index1 : index2;

			if (yytext.Length - index - 1 < 2 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 >= 1 && (_input.Peek() == '/' || _input.Peek() == '-'))
			{
				yytext.Append(_input.Read());
				_state = LexicalState.UTC11;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else
			{
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
		}

		private void ProcessUTC11()
		{
			//	yyyy-mm-
			//	yyyy/mm/

			var index1 = yytext.ToString().LastIndexOf('-');
			var index2 = yytext.ToString().LastIndexOf('/');
			var index = index1 > index2 ? index1 : index2;

			if ( _input.Peek() == ' ' || _input.Peek() == '\t')
            {
				_input.Read();	//	Skip whitespace
            }
			else if (yytext.Length - index - 1 < 2 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
				{
					_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext} 00:00:00+0000").UtcDateTime));
					yytext.Clear();
				}
			}
			else if (yytext.Length - index - 1 >= 1 && (_input.Peek() == 'T' || _input.Peek() == 't'))
			{
				yytext.Append(_input.Read());
				_state = LexicalState.UTC12;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 >= 1 && (_input.Peek() == 'z' || _input.Peek() == 'Z'))
			{
				yytext.Append(_input.Read());
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
			else if (yytext.Length - index - 1 >= 1 && (_input.Peek() == '+' || _input.Peek() == '-'))
			{
				yytext.Append(_input.Read());
				_state = LexicalState.UTC8;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 >= 1 && (_input.Peek() == 'g' || _input.Peek() == 'G'))
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
				else if (_input.Peek() == 'm' || _input.Peek() == 'M')
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
					else if (_input.Peek() == 't' || _input.Peek() == 'T')
					{
						yytext.Append(_input.Read());
						_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
						yytext.Clear();
						_state = LexicalState.INITIAL_STATE;
					}
					else
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
				}
				else
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 >= 1)
			{
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext} 00:00:00+0000").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
			else
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
		}

		private void ProcessUTC12()
		{
			//	yyyy-mm-ddT
			var index1 = yytext.ToString().LastIndexOf('t');
			var index2 = yytext.ToString().LastIndexOf('T');
			var index = index1 > index2 ? index1 : index2;

			if (yytext.Length - index - 1 < 2 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
				{
					_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
					yytext.Clear();
				}
			}
			else if (yytext.Length - index - 1 >= 1 && _input.Peek() >= ':')
			{
				yytext.Append(_input.Read());
				_state = LexicalState.UTC5;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
		}



		private void ProcessMDY1()
		{
			// m/  mm/
			// m-  mm-
			var index1 = yytext.ToString().LastIndexOf('-');
			var index2 = yytext.ToString().LastIndexOf('/');
			var index = index1 > index2 ? index1 : index2;

			if (yytext.Length - index - 1 < 2 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if ( yytext.Length - index - 1 >= 1 && (_input.Peek() == '-' || _input.Peek() == '/' ))
            {
				yytext.Append(_input.Read());
				_state = LexicalState.MDY2;
            }
			else
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
		}

		private void ProcessMDY2()
		{
			//	mm/0 - mm/dd/
			//	mm-0 - mm-dd-

			var index1 = yytext.ToString().LastIndexOf('-');
			var index2 = yytext.ToString().LastIndexOf('/');
			var index = index1 > index2 ? index1 : index2;

			if (yytext.Length - index - 1 < 4 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
				{
					var sign = TimeZoneInfo.Local.BaseUtcOffset.Hours < 0 ? "" : "+";
					_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext} 00:00:00{sign}{TimeZoneInfo.Local.BaseUtcOffset.Hours:00}:{TimeZoneInfo.Local.BaseUtcOffset.Minutes:00}").UtcDateTime));
					yytext.Clear();
				}
			}
			else if (yytext.Length - index - 1 >= 3 && (_input.Peek() == 'z' || _input.Peek() == 'Z'))
			{
				yytext.Append(_input.Read());
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
			else if (yytext.Length - index - 1 >= 3 && (_input.Peek() == 'g' || _input.Peek() == 'G'))
			{
				yytext.Append(_input.Read());
				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
				else if (yytext.Length - index - 1 >= 3 && (_input.Peek() == 'm' || _input.Peek() == 'M'))
				{
					yytext.Append(_input.Read());
					if (_input.EndOfStream)
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
					else if (yytext.Length - index - 1 >= 3 && (_input.Peek() == 't' || _input.Peek() == 'T'))
					{
						yytext.Append(_input.Read());
						_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
						yytext.Clear();
						_state = LexicalState.INITIAL_STATE;
					}
					else
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
				}
				else
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 >= 3 && _input.Peek() == ' ')
			{
				yytext.Append(_input.Read());
				_state = LexicalState.MDY3;
			}
			else
			{
				var sign = TimeZoneInfo.Local.BaseUtcOffset.Hours < 0 ? "" : "+";
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext} 00:00:00{sign}{TimeZoneInfo.Local.BaseUtcOffset.Hours:00}:{TimeZoneInfo.Local.BaseUtcOffset.Minutes:00}").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
		}

		private void ProcessMDY3()
		{
			//	mm/dd/yyyy<sp>
			var index = yytext.ToString().LastIndexOf(' ');

			if (_input.Peek() == ' ' || _input.Peek() == '\t')
            {
				_input.Read();	//	Skip whitespace
			}
			else if (yytext.Length - index - 1 == 0 && (_input.Peek() == 'z' || _input.Peek() == 'Z'))
			{
				yytext.Append(_input.Read());
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
			else if (yytext.Length - index - 1 == 0 && (_input.Peek() == 'g' || _input.Peek() == 'G'))
			{
				yytext.Append(_input.Read());
				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
				else if (_input.Peek() == 'm' || _input.Peek() == 'M')
				{
					yytext.Append(_input.Read());
					if (_input.EndOfStream)
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
					else if (_input.Peek() == 't' || _input.Peek() == 'T')
					{
						yytext.Append(_input.Read());
						_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
						yytext.Clear();
						_state = LexicalState.INITIAL_STATE;
					}
					else
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
				}
				else
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 < 2 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if ( _input.Peek() == ':' )
            {
				yytext.Append(_input.Read());
				_state = LexicalState.MDY4;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
		}

		private void ProcessMDY4()
		{
			//	mm/dd/yyyy hh:

			var index = yytext.ToString().LastIndexOf(':');

			if (yytext.Length - index - 1 < 2 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 >= 1 && _input.Peek() == ':')
			{
				yytext.Append(_input.Read());
				_state = LexicalState.MDY5;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
		}

		private void ProcessMDY5()
		{
			//	mm/dd/yyyy hh:mm:
			var index = yytext.ToString().LastIndexOf(':');

			if ( _input.Peek() == ' ' || _input.Peek() == '\t')
            {
				_input.Read();		// Skip whitespace
            }
			else if (yytext.Length - index - 1 < 2 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
				{
					var sign = TimeZoneInfo.Local.BaseUtcOffset.Hours < 0 ? "" : "+";
					var dateString = $"{yytext}{sign}{TimeZoneInfo.Local.BaseUtcOffset.Hours:00}:{TimeZoneInfo.Local.BaseUtcOffset.Minutes:00}";
					_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse(dateString).UtcDateTime));
					yytext.Clear();
				}
			}
			else if (yytext.Length - index - 1 >= 1 && (_input.Peek() == 'z' || _input.Peek() == 'Z'))
			{
				yytext.Append(_input.Read());
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
			else if (yytext.Length - index - 1 >= 1 && (_input.Peek() == 'g' || _input.Peek() == 'G'))
			{
				yytext.Append(_input.Read());
				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
				else if (yytext.Length - index - 1 >= 1 && (_input.Peek() == 'm' || _input.Peek() == 'M'))
				{
					yytext.Append(_input.Read());
					if (_input.EndOfStream)
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
					else if (yytext.Length - index - 1 >= 1 && (_input.Peek() == 't' || _input.Peek() == 'T'))
					{
						yytext.Append(_input.Read());
						_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
						yytext.Clear();
						_state = LexicalState.INITIAL_STATE;
					}
					else
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
				}
				else
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 >= 1 && _input.Peek() == '.')
			{
				yytext.Append(_input.Read());
				_state = LexicalState.MDY6;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else
			{
				var sign = TimeZoneInfo.Local.BaseUtcOffset.Hours < 0 ? "" : "+";
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}{sign}{TimeZoneInfo.Local.BaseUtcOffset.Hours:00}:{TimeZoneInfo.Local.BaseUtcOffset.Minutes:00}").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
		}

		private void ProcessMDY6()
		{
			//	mm/dd/yyyy hh:mm:ss.

			var index = yytext.ToString().LastIndexOf('.');

			if (_input.Peek() == ' ' || _input.Peek() == '\t')
			{
				_input.Read();      //	Skip whitespace
			}
			else if (yytext.Length - index - 1 < 6 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
				{
					var sign = TimeZoneInfo.Local.BaseUtcOffset.Hours < 0 ? "" : "+";
					_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}{sign}{TimeZoneInfo.Local.BaseUtcOffset.Hours:00}:{TimeZoneInfo.Local.BaseUtcOffset.Minutes:00}").UtcDateTime));
					yytext.Clear();
				}
			}
			else if (yytext.Length - index - 1 > 1 && (_input.Peek() == 'z' || _input.Peek() == 'Z'))
			{
				yytext.Append(_input.Read());
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
			else if (yytext.Length - index - 1 > 1 && (_input.Peek() == 'g' || _input.Peek() == 'G'))
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
				else if (_input.Peek() == 'm' || _input.Peek() == 'M')
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
					else if (_input.Peek() == 't' || _input.Peek() == 'T')
					{
						yytext.Append(_input.Read());
						_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
						yytext.Clear();
						_state = LexicalState.INITIAL_STATE;
					}
					else
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
				}
				else
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (_input.Peek() == '-' || _input.Peek() == '+')
			{
				yytext.Append(_input.Read());
				_state = LexicalState.MDY7;
			}
			else
			{
				var sign = TimeZoneInfo.Local.BaseUtcOffset.Hours < 0 ? "" : "+";
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}{sign}{TimeZoneInfo.Local.BaseUtcOffset.Hours:00}:{TimeZoneInfo.Local.BaseUtcOffset.Minutes:00}").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
		}

		private void ProcessMDY7()
		{
			//	mm/dd/yyyy hh:mm:ss.ffffff+
			//	mm/dd/yyyy hh:mm:ss.ffffff-

			var index1 = yytext.ToString().LastIndexOf('-');
			var index2 = yytext.ToString().LastIndexOf('+');
			var index = index1 > index2 ? index1 : index2;

			if (yytext.Length - index - 1 < 2 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 >= 1 && _input.Peek() >= ':')
			{
				yytext.Append(_input.Read());
				_state = LexicalState.MDY8;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 == 2 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());
				_state = LexicalState.MDY9;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
		}

		private void ProcessMDY8()
		{
			//	mm/dd/yyyy hh:mm:ss.ffffff+00:
			//	mm/dd/yyyy hh:mm:ss.ffffff-00:

			var index = yytext.ToString().LastIndexOf(':');

			if (yytext.Length - index - 1 < 2 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
				{
					_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
					yytext.Clear();
				}
			}
			else if ( yytext.Length-index-1 >= 1 && yytext.Length-index-1 <= 2 && (_input.Peek() < '0' || _input.Peek() > '9'))
            {
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
			else
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
		}

		private void ProcessMDY9()
		{
			//	mm/dd/yyyy hh:mm:ss.ffffff+000
			//	mm/dd/yyyy hh:mm:ss.ffffff-000

			var index1 = yytext.ToString().LastIndexOf('-');
			var index2 = yytext.ToString().LastIndexOf('+');
			var index = index1 > index2 ? index1 : index2;

			if (yytext.Length - index - 1 < 4 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
				{
					_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
					yytext.Clear();
				}
			}
			else if (yytext.Length - index - 1 == 4)
			{
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
			else
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
		}

		private void ProcessYMD1()
		{
			//	yyyy/
			//	yyyy-

			var index1 = yytext.ToString().LastIndexOf('-');
			var index2 = yytext.ToString().LastIndexOf('/');
			var index = index1 > index2 ? index1 : index2;

			if (yytext.Length - index - 1 < 2 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 >= 1 && (_input.Peek() == '/' || _input.Peek() == '-'))
			{
				yytext.Append(_input.Read());
				_state = LexicalState.YMD2;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else
			{
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
		}

		private void ProcessYMD2()
		{
			//	yyyy-mm-
			//	yyyy/mm/

			var index1 = yytext.ToString().LastIndexOf('-');
			var index2 = yytext.ToString().LastIndexOf('/');
			var index = index1 > index2 ? index1 : index2;

			if (_input.Peek() == ' ' || _input.Peek() == '\t')
			{
				_input.Read();      //	Skip Whitespace	
			}
			else if (yytext.Length - index - 1 < 2 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
				{
					var sign = TimeZoneInfo.Local.BaseUtcOffset.Hours < 0 ? "" : "+";
					_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}T00:00:00{sign}{TimeZoneInfo.Local.BaseUtcOffset.Hours:00}:{TimeZoneInfo.Local.BaseUtcOffset.Minutes:00}").UtcDateTime));
					yytext.Clear();
				}
			}
			else if (yytext.Length - index - 1 >= 1 && (_input.Peek() == 'z' || _input.Peek() == 'Z'))
			{
				yytext.Append(_input.Read());
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
			else if (yytext.Length - index - 1 >= 1 && (_input.Peek() == 'g' || _input.Peek() == 'G'))
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
				else if (_input.Peek() == 'm' || _input.Peek() == 'M')
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
						throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
					else if (_input.Peek() == 't' || _input.Peek() == 'T')
					{
						yytext.Append(_input.Read());
						_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}").UtcDateTime));
						yytext.Clear();
						_state = LexicalState.INITIAL_STATE;
					}
				}
				else
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 >= 1 && (_input.Peek() == 'T' || _input.Peek() == 't'))
			{
				yytext.Append(_input.Read());
				_state = LexicalState.YMD3;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 >= 1)
			{
				var sign = TimeZoneInfo.Local.BaseUtcOffset.Hours < 0 ? "" : "+";
				_tokenStream.Push(new Token(Symbol.DATETIME, DateTimeOffset.Parse($"{yytext}T00:00:00{sign}{TimeZoneInfo.Local.BaseUtcOffset.Hours:00}:{TimeZoneInfo.Local.BaseUtcOffset.Minutes:00}").UtcDateTime));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
			else
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
		}

		private void ProcessYMD3()
		{
			//	yyyy-mm-ddT
			var index1 = yytext.ToString().LastIndexOf('t');
			var index2 = yytext.ToString().LastIndexOf('T');
			var index = index1 > index2 ? index1 : index2;

			if (yytext.Length - index - 1 < 2 && _input.Peek() >= '0' && _input.Peek() <= '9')
			{
				yytext.Append(_input.Read());

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else if (yytext.Length - index - 1 >= 1 && _input.Peek() >= ':' )
			{
				yytext.Append(_input.Read());
				_state = LexicalState.MDY4;

				if (_input.EndOfStream)
					throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
			}
			else
				throw new RqlFormatException($"Invalid Datetime format {yytext}. Aborting scan.");
		}

		private void ProcessTimeSpan1()
		{
			if (_input != null)
			{
				//	00:
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(_input.Read());
					_state = LexicalState.TIMESPAN2;
				}
				else
					throw new RqlFormatException($"Invalid Timespan format {yytext}. Aborting scan.");
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessTimeSpan2()
		{
			if (_input != null)
			{
				//	00:0
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(_input.Read());
					_state = LexicalState.TIMESPAN3;
				}
				else
					throw new RqlFormatException($"Invalid Timespan format {yytext}. Aborting scan.");
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessTimeSpan3()
		{
			if (_input != null)
			{
				//	00:00
				if (_input != null && _input.Peek() == ':')
				{
					yytext.Append(_input.Read());
					_state = LexicalState.TIMESPAN4;
				}
				else
					throw new RqlFormatException($"Invalid Timespan format {yytext}. Aborting scan.");
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessTimeSpan4()
		{
			if (_input != null)
			{
				//	00:00:
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(_input.Read());
					_state = LexicalState.TIMESPAN5;
				}
				else
					throw new RqlFormatException($"Invalid Timespan format {yytext}. Aborting scan.");
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessTimeSpan5()
		{
			if (_input != null)
			{
				//	00:00:0
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(_input.Read());
					_state = LexicalState.INITIAL_STATE;

					try
					{
						_tokenStream.Push(new Token(Symbol.TIMESPAN, TimeSpan.Parse(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Invalid Timespan format {yytext}. Aborting scan.");
					}
				}
				else
					throw new RqlFormatException($"Invalid Timespan format {yytext}. Aborting scan.");
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessBinary1()
		{
			if (_input != null)
			{
				if (_input.Peek() == '\"')
				{
					_input.Read();

					if (_input.EndOfStream)
					{
						throw new RqlFormatException($"{yytext}. Missing matching quote. Aborting scan.");
					}

					_state = LexicalState.BINARY2;
				}
				else if ((_input.Peek() >= 'a' && _input.Peek() <= 'z') || (_input.Peek() >= 'A' && _input.Peek() <= 'Z') || (_input.Peek() >= '0' && _input.Peek() <= '9') || _input.Peek() == '+' || _input.Peek() == '/' || _input.Peek() == '=')
				{
					yytext.Append(Convert.ToString(_input.Read()));
					_state = LexicalState.BINARY3;

					if (_input.EndOfStream)
					{
						throw new RqlFormatException($"{yytext}. Not a valid BASE 64 string. Aborting scan.");
					}
				}
				else
				{
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessBinary2()
		{
			if (_input != null)
			{
				if (_input.Peek() == '\"')
				{
					_input.Read();

					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.BINARY, Convert.FromBase64String(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"{yytext}. Not a valid BASE 64 string. Aborting scan.");
					}
				}
				else if ((_input.Peek() >= 'a' && _input.Peek() <= 'z') || (_input.Peek() >= 'A' && _input.Peek() <= 'Z') || (_input.Peek() >= '0' && _input.Peek() <= '9') || _input.Peek() == '+' || _input.Peek() == '/' || _input.Peek() == '=')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						throw new RqlFormatException($"{yytext}. Missing matching quote. Aborting scan.");
					}
				}
				else
				{
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessBinary3()
		{
			if (_input != null)
			{
				if ((_input.Peek() >= 'a' && _input.Peek() <= 'z') || (_input.Peek() >= 'A' && _input.Peek() <= 'Z') || (_input.Peek() >= '0' && _input.Peek() <= '9') || _input.Peek() == '+' || _input.Peek() == '/' || _input.Peek() == '=')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.BINARY, Convert.FromBase64String(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"{yytext}. Not a valid BASE 64 string. Aborting scan.");
						}
					}
				}
				else
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.BINARY, Convert.FromBase64String(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"{yytext}. Not a valid BASE 64 string. Aborting scan.");
					}
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessBool1()
		{
			if (_input != null)
			{
				if (_input.Peek() == '0')
				{
					_input.Read();
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.BOOLEAN, false));
					yytext.Clear();
				}
				else if (_input.Peek() == '1')
				{
					yytext.Append(Convert.ToString(_input.Read()));
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.BOOLEAN, true));
					yytext.Clear();
				}
				else if (_input.Peek() == 'y' || _input.Peek() == 'Y')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.BOOLEAN, true));
						yytext.Clear();
					}
					else if (_input.Peek() == 'e' || _input.Peek() == 'E')
					{
						yytext.Append(Convert.ToString(_input.Read()));

						if (_input.EndOfStream)
						{
							yytext.Append(Convert.ToString(_input.Read()));
							throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
						}
						else if (_input.Peek() == 's' || _input.Peek() == 'S')
						{
							yytext.Append(Convert.ToString(_input.Read()));
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.BOOLEAN, true));
							yytext.Clear();
						}
						else
						{
							yytext.Append(Convert.ToString(_input.Read()));
							throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
						}
					}
					else
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.BOOLEAN, true));
						yytext.Clear();
					}
				}
				else if (_input.Peek() == 'n' || _input.Peek() == 'N')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.BOOLEAN, false));
						yytext.Clear();
					}
					else if (_input.Peek() == 'o' || _input.Peek() == 'O')
					{
						yytext.Append(Convert.ToString(_input.Read()));
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.BOOLEAN, false));
						yytext.Clear();
					}
					else
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.BOOLEAN, false));
						yytext.Clear();
					}
				}
				else if (_input.Peek() == 't' || _input.Peek() == 'T')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.BOOLEAN, true));
						yytext.Clear();
					}
					else if (_input.Peek() == 'r' || _input.Peek() == 'R')
					{
						yytext.Append(Convert.ToString(_input.Read()));

						if (_input.EndOfStream)
						{
							yytext.Append(Convert.ToString(_input.Read()));
							throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
						}
						else if (_input.Peek() == 'u' || _input.Peek() == 'U')
						{
							yytext.Append(Convert.ToString(_input.Read()));

							if (_input.EndOfStream)
							{
								yytext.Append(Convert.ToString(_input.Read()));
								throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
							}
							else if (_input.Peek() == 'e' || _input.Peek() == 'E')
							{
								yytext.Append(Convert.ToString(_input.Read()));
								_state = LexicalState.INITIAL_STATE;
								_tokenStream.Push(new Token(Symbol.BOOLEAN, true));
								yytext.Clear();
							}
							else
							{
								yytext.Append(Convert.ToString(_input.Read()));
								throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
							}
						}
						else
						{
							yytext.Append(Convert.ToString(_input.Read()));
							throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
						}
					}
					else
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.BOOLEAN, true));
						yytext.Clear();
					}
				}
				else if (_input.Peek() == 'f' || _input.Peek() == 'F')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.BOOLEAN, false));
						yytext.Clear();
					}
					else if (_input.Peek() == 'a' || _input.Peek() == 'A')
					{
						yytext.Append(Convert.ToString(_input.Read()));

						if (_input.EndOfStream)
						{
							yytext.Append(Convert.ToString(_input.Read()));
							throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
						}
						else if (_input.Peek() == 'l' || _input.Peek() == 'L')
						{
							yytext.Append(Convert.ToString(_input.Read()));

							if (_input.EndOfStream)
							{
								yytext.Append(Convert.ToString(_input.Read()));
								throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
							}
							else if (_input.Peek() == 's' || _input.Peek() == 'S')
							{
								yytext.Append(Convert.ToString(_input.Read()));

								if (_input.EndOfStream)
								{
									yytext.Append(Convert.ToString(_input.Read()));
									throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
								}
								else if (_input.Peek() == 'e' || _input.Peek() == 'E')
								{
									yytext.Append(Convert.ToString(_input.Read()));
									_state = LexicalState.INITIAL_STATE;
									_tokenStream.Push(new Token(Symbol.BOOLEAN, false));
									yytext.Clear();
								}
								else
								{
									yytext.Append(Convert.ToString(_input.Read()));
									throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
								}
							}
							else
							{
								yytext.Append(Convert.ToString(_input.Read()));
								throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
							}
						}
						else
						{
							yytext.Append(Convert.ToString(_input.Read()));
							throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
						}
					}
					else
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.BOOLEAN, false));
						yytext.Clear();
					}
				}
				else
				{
					yytext.Append(Convert.ToString(_input.Read()));
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessSingle1()
		{
			if (_input != null)
			{
				//	The input character is guaranteed to be 0-9, '+', '-' ro '.'
				var c = _input.Read();

				if (_input.EndOfStream && (c == '+' || c == '-' || c == '.'))
				{
					//	If nothing follows a +, - or ., then the character is meaningless
					yytext.Append(Convert.ToString(c));
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
				else if (_input.EndOfStream)
				{
					try
					{
						//	it's not +,- or ., therefore, it must be 0-9, therfore, and nothing follows,
						//	therefore, it is a valid integer
						_state = LexicalState.INITIAL_STATE;
						yytext.Append(Convert.ToString(c));
						_tokenStream.Push(new Token(Symbol.SINGLE, Convert.ToSingle(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as single. Aborting scan.");
					}
				}
				else
				{
					//	Include the character in the string, unless it is a unary plus.
					//	There is no reason to include unary plus
					if (c != '+')
						yytext.Append(Convert.ToString(c));

					//	If this is a ., then the numeric valud is either decimal or double.
					//	Set the state to reflect that
					if (c == '.')
						_state = LexicalState.SINGLE3;

					//	Continue with the parseing, where this value could be an integer or decimal or double.
					_state = LexicalState.SINGLE2;
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessSingle2()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.SINGLE, Convert.ToSingle(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as single. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == '.')
				{
					yytext.Append(Convert.ToString(_input.Read()));
					_state = LexicalState.SINGLE3;

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.SINGLE, Convert.ToSingle(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as single. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == 'f' || _input.Peek() == 'F')
				{
					_input.Read();

					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.SINGLE, Convert.ToSingle(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as single. Aborting scan.");
					}
				}
				else if (_input.Peek() == 'e' || _input.Peek() == 'E')
				{
					yytext.Append(Convert.ToString(_input.Read()));
					_state = LexicalState.SINGLE4;

					if (_input.EndOfStream)
					{
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}
				}
				else
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.SINGLE, Convert.ToSingle(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as single. Aborting scan.");
					}
				}
			}
			else
            {
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}

		}

		private void ProcessSingle3()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.SINGLE, Convert.ToSingle(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as single. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == 'f' || _input.Peek() == 'F')
				{
					_input.Read();

					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.SINGLE, Convert.ToSingle(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as single. Aborting scan.");
					}
				}
				else if (_input.Peek() == 'e' || _input.Peek() == 'E')
				{
					yytext.Append(Convert.ToString(_input.Read()));
					_state = LexicalState.SINGLE4;

					if (_input.EndOfStream)
					{
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}
				}
				else
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.SINGLE, Convert.ToSingle(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as single. Aborting scan.");
					}
				}
			}
            else
			{ 
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessSingle4()
		{
			if (_input != null)
			{
				if (_input.Peek() == '+' || _input.Peek() == '-')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as single. Aborting scan.");
					}

					_state = LexicalState.SINGLE5;
				}
				else if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.SINGLE, Convert.ToSingle(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as single. Aborting scan.");
						}
					}
					else
						_state = LexicalState.SINGLE5;
				}
				else
				{
					yytext.Append(_input.Read());
					throw new RqlFormatException($"Unable to cast {yytext} as single. Aborting scan.");
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessSingle5()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.SINGLE, Convert.ToSingle(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as single. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == 'f' || _input.Peek() == 'F')
				{
					_input.Read();

					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.SINGLE, Convert.ToSingle(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as single. Aborting scan.");
					}
				}
				else
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.SINGLE, Convert.ToSingle(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as single. Aborting scan.");
					}
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessDecimal1()
		{
			if (_input != null)
			{
				//	The input character is guaranteed to be 0-9, '+', '-' ro '.'
				var c = _input.Read();

				if (_input.EndOfStream && (c == '+' || c == '-' || c == '.'))
				{
					//	If nothing follows a +, - or ., then the character is meaningless
					yytext.Append(Convert.ToString(c));
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
				else if (_input.EndOfStream)
				{
					try
					{
						//	it's not +,- or ., therefore, it must be 0-9, therfore, and nothing follows,
						//	therefore, it is a valid integer
						_state = LexicalState.INITIAL_STATE;
						yytext.Append(Convert.ToString(c));
						_tokenStream.Push(new Token(Symbol.DECIMAL, Convert.ToDecimal(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as decimal. Aborting scan.");
					}
				}
				else
				{
					//	Include the character in the string, unless it is a unary plus.
					//	There is no reason to include unary plus
					if (c != '+')
						yytext.Append(Convert.ToString(c));

					//	If this is a ., then the numeric valud is either decimal or double.
					//	Set the state to reflect that
					if (c == '.')
						_state = LexicalState.DECIMAL3;

					//	Continue with the parseing, where this value could be an integer or decimal or double.
					_state = LexicalState.DECIMAL2;
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessDecimal2()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.DECIMAL, Convert.ToDecimal(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as decimal. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == '.')
				{
					yytext.Append(Convert.ToString(_input.Read()));
					_state = LexicalState.DECIMAL3;

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.DECIMAL, Convert.ToDecimal(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as decimal. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == 'm' || _input.Peek() == 'M')
				{
					try
					{
						_input.Read();
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.DECIMAL, Convert.ToDecimal(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as decimal. Aborting scan.");
					}
				}
				else
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.DECIMAL, Convert.ToDecimal(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as decimal. Aborting scan.");
					}
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessDecimal3()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.DECIMAL, Convert.ToDecimal(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as decimal. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == 'm' || _input.Peek() == 'M')
				{
					try
					{
						_input.Read();
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.DECIMAL, Convert.ToDecimal(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as decimal. Aborting scan.");
					}
				}
				else
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.DECIMAL, Convert.ToDecimal(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as decimal. Aborting scan.");
					}
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessDouble1()
		{
			if (_input != null)
			{
				//	The input character is guaranteed to be 0-9, '+', '-' ro '.'
				var c = _input.Read();

				if (_input.EndOfStream && (c == '+' || c == '-' || c == '.'))
				{
					//	If nothing follows a +, - or ., then the character is meaningless
					yytext.Append(Convert.ToString(c));
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
				else if (_input.EndOfStream)
				{
					try
					{
						//	it's not +,- or ., therefore, it must be 0-9, therfore, and nothing follows,
						//	therefore, it is a valid integer
						_state = LexicalState.INITIAL_STATE;
						yytext.Append(Convert.ToString(c));
						_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}
				}
				else
				{
					//	Include the character in the string, unless it is a unary plus.
					//	There is no reason to include unary plus
					if (c != '+')
						yytext.Append(Convert.ToString(c));

					//	If this is a ., then the numeric valud is either decimal or double.
					//	Set the state to reflect that
					if (c == '.')
						_state = LexicalState.DOUBLE3;

					//	Continue with the parseing, where this value could be an integer or decimal or double.
					_state = LexicalState.DOUBLE2;
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessDouble2()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == '.')
				{
					yytext.Append(Convert.ToString(_input.Read()));
					_state = LexicalState.DOUBLE3;

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == 'd' || _input.Peek() == 'D')
				{
					_input.Read();

					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}
				}
				else if (_input.Peek() == 'e' || _input.Peek() == 'E')
				{
					yytext.Append(Convert.ToString(_input.Read()));
					_state = LexicalState.DOUBLE4;

					if (_input.EndOfStream)
					{
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}
				}
				else
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessDouble3()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == 'd' || _input.Peek() == 'D')
				{
					_input.Read();

					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}
				}
				else if (_input.Peek() == 'e' || _input.Peek() == 'E')
				{
					yytext.Append(Convert.ToString(_input.Read()));
					_state = LexicalState.DOUBLE4;

					if (_input.EndOfStream)
					{
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}
				}
				else
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessDouble4()
		{
			if (_input != null)
			{
				if (_input.Peek() == '+' || _input.Peek() == '-')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}

					_state = LexicalState.DOUBLE5;
				}
				else if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
						}
					}
					else
						_state = LexicalState.DOUBLE5;
				}
				else
				{
					yytext.Append(_input.Read());
					throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessDouble5()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == 'd' || _input.Peek() == 'D')
				{
					_input.Read();

					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}
				}
				else
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessQString1()
		{
			if (_input != null)
			{
				if (_input.Peek() == '"')
				{
					_input.Read();

					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
					yytext.Clear();
				}
				else if (_input.Peek() == '\\')
				{
					_input.Read();

					if (_input.EndOfStream)
					{
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}

					if (_input.Peek() == 'r')
					{
						yytext.Append('\r');
						_input.Read();
					}
					else if (_input.Peek() == 'n')
					{
						yytext.Append('\n');
						_input.Read();
					}
					else if (_input.Peek() == 't')
					{
						yytext.Append('\t');
						_input.Read();
					}
					else if (_input.Peek() == 'b')
					{
						yytext.Append('\b');
						_input.Read();
					}
					else if (_input.Peek() == '"')
					{
						yytext.Append('\"');
						_input.Read();
					}
					else if (_input.Peek() == '\\')
					{
						yytext.Append('\\');
						_input.Read();
					}
					else if (_input.Peek() == 'u' || _input.Peek() == 'U' || _input.Peek() == 'x' || _input.Peek() == 'X')
					{
						_input.Read();

						StringBuilder num = new();

						for (int i = 0; i < 4; i++)
						{
							if (_input.EndOfStream)
								throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");

							if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
								(_input.Peek() >= 'a' && _input.Peek() <= 'f') ||
								(_input.Peek() >= 'A' && _input.Peek() <= 'F'))
							{
								num.Append(_input.Read());
							}
							else
								break;
						}

						var character = System.Convert.ToChar(Convert.ToUInt32(num.ToString(), 16));
						yytext.Append(character);

						if (_input.EndOfStream)
						{
							throw new RqlFormatException($"No matching \" found: {yytext}. Aborting scan.");
						}
					}
					else
					{
						yytext.Append(_input.Read());
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}

					if (_input.EndOfStream)
					{
						throw new RqlFormatException($"No matching \" found: {yytext}. Aborting scan.");
					}
				}
				else
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
					{
						throw new RqlFormatException($"No matching \" found: {yytext}. Aborting scan.");
					}
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessDateOrTimeOrNumericOrGuid()
		{
			//	all numeric numbers at this point
			yytext.Append(Convert.ToString(_input.Read()));

			if ( _input.EndOfStream )
			{
				_state = LexicalState.INITIAL_STATE;
				_tokenStream.Push(new Token(Symbol.INTEGER, Convert.ToInt32(yytext.ToString())));
				yytext.Clear();
			}

			//	Is it a hexidecimal number format?
			else if (yytext.Length == 1 && yytext[0] == '0' && (_input.Peek() == 'x' || _input.Peek() == 'X'))
			{
				yytext.Append(Convert.ToString(_input.Read()));
				_state = LexicalState.HEXVAL;
				yytext.Clear();
			}

			//	Is it a timespan format?
			else if ((yytext.Length == 1 || yytext.Length == 2) && _input.Peek() == ':')
			{
				yytext.Append(_input.Read());
				_state = LexicalState.TIMESPAN1;
			}

			//	Is it a datetime of the form mdy?
			else if ((yytext.Length == 1 || yytext.Length == 2) && (_input.Peek() == '-' || _input.Peek() == '/'))
			{
				if (Convert.ToInt32(yytext.ToString()) > 0 && Convert.ToInt32(yytext.ToString()) <= 12)
				{
					yytext.Append(_input.Read());
					_state = LexicalState.MDY1;
				}
				else
					throw new RqlFormatException("Datetime format is invalid.");
			}
			//	Is it a datetime of the form ymd?
			else if (yytext.Length > 2 && (_input.Peek() == '-' || _input.Peek() == '/'))
			{
				yytext.Append(_input.Read());
				_state = LexicalState.YMD1;
			}
			//	Is it a double format?
			else if (_input.Peek() == 'd' || _input.Peek() == 'D')
			{
				//	nnnnd or nnnnD
				var c = _input.Read();

				if (_input.EndOfStream)
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
					yytext.Clear();
				}
				else if ((_input.Peek() >= 'a' && _input.Peek() <= 'f') || (_input.Peek() >= 'A' && _input.Peek() <= 'F') || (_input.Peek() >= '0' && _input.Peek() <= '9'))
				{
					yytext.Append(c);
					_state = LexicalState.GUID1;
				}
				else
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
					yytext.Clear();
				}
			}

			//	Is it a float format?
			else if (_input.Peek() == 'f' || _input.Peek() == 'F')
			{
				//	nnnnd or nnnnD
				var c = _input.Read();

				if (_input.EndOfStream)
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.SINGLE, Convert.ToSingle(yytext.ToString())));
					yytext.Clear();
				}
				else if ((_input.Peek() >= 'a' && _input.Peek() <= 'f') || (_input.Peek() >= 'A' && _input.Peek() <= 'F') || (_input.Peek() >= '0' && _input.Peek() <= '9'))
				{
					yytext.Append(c);
					_state = LexicalState.GUID1;
				}
				else
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.SINGLE, Convert.ToSingle(yytext.ToString())));
					yytext.Clear();
				}
			}

			//	Is it an unsigned integer format?
			else if (_input.Peek() == 'u' || _input.Peek() == 'U')
			{
				_input.Read();

				if (_input.EndOfStream)
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.UINTEGER, Convert.ToUInt32(yytext.ToString())));
					yytext.Clear();
				}
				else if (_input.Peek() == 'l' || _input.Peek() == 'L')
				{
					_input.Read();
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.ULONG, Convert.ToUInt64(yytext.ToString())));
					yytext.Clear();
				}
				else
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.UINTEGER, Convert.ToUInt32(yytext.ToString())));
					yytext.Clear();
				}
			}

			//	Is it a decimal format?
			else if (_input.Peek() == 'm' || _input.Peek() == 'M')
			{
				_input.Read();
				_state = LexicalState.INITIAL_STATE;
				_tokenStream.Push(new Token(Symbol.DECIMAL, Convert.ToDecimal(yytext.ToString())));
				yytext.Clear();
			}

			//	Is it a long format?
			else if (_input.Peek() == 'l' || _input.Peek() == 'L')
			{
				_input.Read();

				if (_input.EndOfStream)
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.LONG, Convert.ToInt64(yytext.ToString())));
					yytext.Clear();
				}
				else if (_input.Peek() == 'u' || _input.Peek() == 'U')
				{
					_input.Read();
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.ULONG, Convert.ToUInt64(yytext.ToString())));
					yytext.Clear();
				}
				else
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.LONG, Convert.ToInt64(yytext.ToString())));
					yytext.Clear();
				}
			}

			//	Is it a GUID format?
			else if ( (yytext.Length == 8 && _input.Peek() == '-') ||
				      (_input.Peek() >= 'a' && _input.Peek() <= 'f') ||
					  (_input.Peek() >= 'A' && _input.Peek() <= 'F'))
			{
				_state = LexicalState.GUID1;
			}

			//	Is it a decimal number (double, float or decimal)
			else if (_input.Peek() == '.')
			{
				yytext.Append(_input.Read());
				_state = LexicalState.DECIMALPORTION;

				if (_input.EndOfStream)
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}
				}
			}

			//	Is this the end of the numeric format?
			else if (_input.Peek() < '0' || _input.Peek() > '9')
			{
				try
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.INTEGER, Convert.ToInt32(yytext.ToString())));
					yytext.Clear();
				}
				catch (Exception)
				{
					throw new RqlFormatException($"Unable to cast {yytext} as int. Aborting scan.");
				}
			}
		}

		private void ProcessGuidOrString1()
		{
			if (yytext.Length < 8)
			{
				if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
					(_input.Peek() >= 'a' && _input.Peek() <= 'f') ||
					(_input.Peek() >= 'A' && _input.Peek() <= 'F'))
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
					{
						_tokenStream.Push(new Token(Symbol.PROPERTY, yytext.ToString()));
						_state = LexicalState.INITIAL_STATE;
						yytext.Clear();
					}
				}
				else if (_input.Peek() == '_' ||
						(_input.Peek() >= 'g' && _input.Peek() <= 'z') ||
						(_input.Peek() >= 'G' && _input.Peek() <= 'Z'))
				{
					_state = LexicalState.TEXTSTREAM;
				}
				else if (IsStringSpecialChar())
				{
					_state = LexicalState.STRING2;
				}
				else
				{
					_tokenStream.Push(new Token(Symbol.PROPERTY, yytext.ToString()));
					yytext.Clear();
					_state = LexicalState.INITIAL_STATE;
				}
			}
			else if (_input.Peek() == '-')
			{
				yytext.Append(_input.Read());
				_state = LexicalState.GUIDORSTRING2;

				if (_input.EndOfStream)
				{
					_tokenStream.Push(new Token(Symbol.PROPERTY, yytext.ToString()));
					yytext.Clear();
					_state = LexicalState.INITIAL_STATE;
				}
			}
			else if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
				     (_input.Peek() >= 'a' && _input.Peek() <= 'z') ||
				     (_input.Peek() >= 'A' && _input.Peek() <= 'Z') || _input.Peek() == '_')
			{
				yytext.Append(_input.Read());
				_state = LexicalState.TEXTSTREAM;

				if (_input.EndOfStream)
				{
					_tokenStream.Push(new Token(Symbol.PROPERTY, yytext.ToString()));
					yytext.Clear();
					_state = LexicalState.INITIAL_STATE;
				}
			}
			else if (IsStringSpecialChar())
			{
				_state = LexicalState.STRING2;
			}
			else
			{
				_tokenStream.Push(new Token(Symbol.PROPERTY, yytext.ToString()));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
		}

		private void ProcessGuidOrString2()
		{
			//	12345678-0123
			//	4a0bb476-8ab5-4265-a698-6f999b09867b

			if (yytext.Length < 13)
			{
				if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
					(_input.Peek() >= 'a' && _input.Peek() <= 'f') ||
					(_input.Peek() >= 'A' && _input.Peek() <= 'F'))
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
					{
						_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
						yytext.Clear();
						_state = LexicalState.INITIAL_STATE;
					}
				}
				else if (_input.Peek() == '_' ||
						(_input.Peek() >= 'g' && _input.Peek() <= 'z') ||
						(_input.Peek() >= 'G' && _input.Peek() <= 'Z'))
				{
					_state = LexicalState.TEXTSTREAM;
				}
				else if (IsStringSpecialChar())
				{
					_state = LexicalState.STRING2;
				}
				else
				{
					_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
					yytext.Clear();
					_state = LexicalState.INITIAL_STATE;
				}
			}
			else if (_input.Peek() == '-')
			{
				yytext.Append(_input.Read());
				_state = LexicalState.GUIDORSTRING3;

				if (_input.EndOfStream)
				{
					_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
					yytext.Clear();
					_state = LexicalState.INITIAL_STATE;
				}
			}
			else if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
				 (_input.Peek() >= 'a' && _input.Peek() <= 'z') ||
				 (_input.Peek() >= 'A' && _input.Peek() <= 'Z') || _input.Peek() == '_')
			{
				_state = LexicalState.TEXTSTREAM;
			}
			else if (IsStringSpecialChar())
			{
				_state = LexicalState.STRING2;
			}
			else
			{
				_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
		}

		private void ProcessGuidOrString3()
		{
			//	12345678-0123-5678
			//	4a0bb476-8ab5-4265-a698-6f999b09867b

			if (yytext.Length < 18)
			{
				if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
					(_input.Peek() >= 'a' && _input.Peek() <= 'f') ||
					(_input.Peek() >= 'A' && _input.Peek() <= 'F'))
				{
					yytext.Append(_input.Read());
				}
				else if (_input.Peek() == '_' ||
						(_input.Peek() >= 'g' && _input.Peek() <= 'z') ||
						(_input.Peek() >= 'G' && _input.Peek() <= 'Z'))
				{
					_state = LexicalState.TEXTSTREAM;
				}
				else if (IsStringSpecialChar())
				{
					_state = LexicalState.STRING2;
				}
				else
				{
					_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
					yytext.Clear();
					_state = LexicalState.INITIAL_STATE;
				}
			}
			else if (_input.Peek() == '-')
			{
				yytext.Append(_input.Read());
				_state = LexicalState.GUIDORSTRING4;

				if (_input.EndOfStream)
				{
					_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
					yytext.Clear();
					_state = LexicalState.INITIAL_STATE;
				}
			}
			else if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
				 (_input.Peek() >= 'a' && _input.Peek() <= 'z') ||
				 (_input.Peek() >= 'A' && _input.Peek() <= 'Z') || _input.Peek() == '_')
			{
				_state = LexicalState.TEXTSTREAM;
			}
			else if (IsStringSpecialChar())
			{
				_state = LexicalState.STRING2;
			}
			else
			{
				_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
		}

		private void ProcessGuidOrString4()
		{
			//	12345678-0123-5678-0123
			//	4a0bb476-8ab5-4265-a698-6f999b09867b

			if (yytext.Length < 23)
			{
				if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
					(_input.Peek() >= 'a' && _input.Peek() <= 'f') ||
					(_input.Peek() >= 'A' && _input.Peek() <= 'F'))
				{
					yytext.Append(_input.Read());
				}
				else if (_input.Peek() == '_' ||
						(_input.Peek() >= 'g' && _input.Peek() <= 'z') ||
						(_input.Peek() >= 'G' && _input.Peek() <= 'Z'))
				{
					_state = LexicalState.TEXTSTREAM;
				}
				else if (IsStringSpecialChar())
				{
					_state = LexicalState.STRING2;
				}
				else
				{
					_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
					yytext.Clear();
					_state = LexicalState.INITIAL_STATE;
				}
			}
			else if (_input.Peek() == '-')
			{
				yytext.Append(_input.Read());
				_state = LexicalState.GUIDORSTRING5;

				if (_input.EndOfStream)
				{
					_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
					yytext.Clear();
					_state = LexicalState.INITIAL_STATE;
				}
			}
			else if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
				 (_input.Peek() >= 'a' && _input.Peek() <= 'z') ||
				 (_input.Peek() >= 'A' && _input.Peek() <= 'Z') || _input.Peek() == '_')
			{
				_state = LexicalState.TEXTSTREAM;
			}
			else if (IsStringSpecialChar())
			{
				_state = LexicalState.STRING2;
			}
			else
			{
				_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
				yytext.Clear();
				_state = LexicalState.INITIAL_STATE;
			}
		}

		private void ProcessGuidOrString5()
		{
			//	12345678-0123-5678-0123-567890123456
			//	4a0bb476-8ab5-4265-a698-6f999b09867b

			if (yytext.Length < 36)
			{
				if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
					(_input.Peek() >= 'a' && _input.Peek() <= 'f') ||
					(_input.Peek() >= 'A' && _input.Peek() <= 'F'))
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.UNIQUEIDENTIFIER, Guid.Parse(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception error)
						{
							throw new RqlFormatException($"{error.Message} Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == '_' ||
						(_input.Peek() >= 'g' && _input.Peek() <= 'z') ||
						(_input.Peek() >= 'G' && _input.Peek() <= 'Z'))
				{
					_state = LexicalState.TEXTSTREAM;
				}
				else if (IsStringSpecialChar())
				{
					_state = LexicalState.STRING2;
				}
				else
				{
					_tokenStream.Push(new Token(Symbol.STRING, yytext.ToString()));
					yytext.Clear();
					_state = LexicalState.INITIAL_STATE;
				}
			}
			else if (_input.Peek() == '-')
			{
				_state = LexicalState.TEXTSTREAM;
			}
			else if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
				 (_input.Peek() >= 'a' && _input.Peek() <= 'z') ||
				 (_input.Peek() >= 'A' && _input.Peek() <= 'Z') || _input.Peek() == '_')
			{
				_state = LexicalState.TEXTSTREAM;
			}
			else if (IsStringSpecialChar())
			{
				_state = LexicalState.STRING2;
			}
			else
			{
				_state = LexicalState.INITIAL_STATE;
				_tokenStream.Push(new Token(Symbol.UNIQUEIDENTIFIER, Guid.Parse(yytext.ToString())));
				yytext.Clear();
			}
		}

		private void ProcessGuid1()
		{
			if (_input != null)
			{
				//	12345678
				//	4a0bb476-8ab5-4265-a698-6f999b09867b

				if (yytext.Length < 8)
				{
					if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
						(_input.Peek() >= 'a' && _input.Peek() <= 'f') ||
						(_input.Peek() >= 'A' && _input.Peek() <= 'F'))
					{
						yytext.Append(_input.Read());
					}
					else
					{
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}
				}
				else if (_input.Peek() == '-')
				{
					yytext.Append(_input.Read());
					_state = LexicalState.GUID2;
				}
				else
				{
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessGuid2()
		{
			if (_input != null)
			{
				//	12345678-0123
				//	4a0bb476-8ab5-4265-a698-6f999b09867b

				if (yytext.Length < 13)
				{
					if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
						(_input.Peek() >= 'a' && _input.Peek() <= 'f') ||
						(_input.Peek() >= 'A' && _input.Peek() <= 'F'))
					{
						yytext.Append(_input.Read());
					}
					else
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
				else if (_input.Peek() == '-')
				{
					yytext.Append(_input.Read());
					_state = LexicalState.GUID3;
				}
				else
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessGuid3()
		{
			if (_input != null)
			{
				//	12345678-0123-5678
				//	4a0bb476-8ab5-4265-a698-6f999b09867b

				if (yytext.Length < 18)
				{
					if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
						(_input.Peek() >= 'a' && _input.Peek() <= 'f') ||
						(_input.Peek() >= 'A' && _input.Peek() <= 'F'))
					{
						yytext.Append(_input.Read());
					}
					else
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
				else if (_input.Peek() == '-')
				{
					yytext.Append(_input.Read());
					_state = LexicalState.GUID4;
				}
				else
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessGuid4()
		{
			if (_input != null)
			{
				//	12345678-0123-5678-0123
				//	4a0bb476-8ab5-4265-a698-6f999b09867b

				if (yytext.Length < 23)
				{
					if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
						(_input.Peek() >= 'a' && _input.Peek() <= 'f') ||
						(_input.Peek() >= 'A' && _input.Peek() <= 'F'))
					{
						yytext.Append(_input.Read());
					}
					else
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
				else if (_input.Peek() == '-')
				{
					yytext.Append(_input.Read());
					_state = LexicalState.GUID5;
				}
				else
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessGuid5()
		{
			if (_input != null)
			{
				//	12345678-0123-5678-0123-567890123456
				//	4a0bb476-8ab5-4265-a698-6f999b09867b

				if (yytext.Length < 36)
				{
					if ((_input.Peek() >= '0' && _input.Peek() <= '9') ||
						(_input.Peek() >= 'a' && _input.Peek() <= 'f') ||
						(_input.Peek() >= 'A' && _input.Peek() <= 'F'))
					{
						yytext.Append(_input.Read());

						if (_input.EndOfStream)
						{
							try
							{
								_state = LexicalState.INITIAL_STATE;
								_tokenStream.Push(new Token(Symbol.UNIQUEIDENTIFIER, Guid.Parse(yytext.ToString())));
								yytext.Clear();
							}
							catch (Exception error)
							{
								throw new RqlFormatException($"{error.Message} Aborting scan.");
							}
						}
					}
					else
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
				else
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.UNIQUEIDENTIFIER, Guid.Parse(yytext.ToString())));
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessUniaryOrDecimalPoint()
		{
			if (_input != null)
			{
				//	The input character is guaranteed to be '+', '-' or '.'
				var c = _input.Peek();

				if (c != '+')
					yytext.Append(_input.Read());
				else
					_input.Read();

				//	Don't push the uinary +
				if (_input.EndOfStream)
				{
					//	If nothing follows a +, - or ., then the character is meaningless
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
				else if (c == '+' && (_input.Peek() < '0' || _input.Peek() > '9'))
				{
					//	what follows the + is not numeric, therefore, the + is not a uniary plus.
					//	therfore, it's just a plus symbol. Return it as such, and reset the state
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.PLUS, yytext.ToString()));
					yytext.Clear();
				}

				else if (c == '-' && (_input.Peek() < '0' || _input.Peek() > '9'))
				{
					//	what follows the - is not numeric, therefore, the - is not a uniary minus.
					//	therfore, it's just a minus symbol. Return it as such, and reset the state
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.MINUS, yytext.ToString()));
					yytext.Clear();
				}
				else
				{
					//	If this is a ., then the numeric valud is either decimal or double.
					//	Set the state to reflect that
					if (c == '.')
						_state = LexicalState.DECIMALPORTION;

					//	Continue with the parseing, where this value could be an integer or decimal or double.
					_state = LexicalState.INTEGERPORTION;
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessIntegerPortion()
		{
			if (_input != null)
			{
				if ((_input.Peek() >= '0' && _input.Peek() <= '9'))
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.INTEGER, Convert.ToInt32(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as int. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == '.')
				{
					yytext.Append(Convert.ToString(_input.Read()));
					_state = LexicalState.DECIMALPORTION;

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == 'f' || _input.Peek() == 'F')
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_input.Read();
						_tokenStream.Push(new Token(Symbol.SINGLE, Convert.ToSingle(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as single. Aborting scan.");
					}
				}
				else if (_input.Peek() == 'd' || _input.Peek() == 'D')
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_input.Read();
						_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}
				}
				else if (_input.Peek() == 'u' || _input.Peek() == 'U')
				{
					_input.Read();

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.UINTEGER, Convert.ToUInt32(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as uint. Aborting scan.");
						}
					}
					else if (_input.Peek() == 'l' || _input.Peek() == 'L')
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_input.Read();
							_tokenStream.Push(new Token(Symbol.ULONG, Convert.ToUInt64(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as ulong. Aborting scan.");
						}
					}
					else
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_input.Read();
							_tokenStream.Push(new Token(Symbol.UINTEGER, Convert.ToUInt32(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as uint. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == 'l' || _input.Peek() == 'L')
				{
					_input.Read();

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.LONG, Convert.ToInt64(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as long. Aborting scan.");
						}
					}
					else if (_input.Peek() == 'u' || _input.Peek() == 'U')
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_input.Read();
							_tokenStream.Push(new Token(Symbol.ULONG, Convert.ToUInt64(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as ulong. Aborting scan.");
						}
					}
					else
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.LONG, Convert.ToInt64(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as long. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == 'm' || _input.Peek() == 'M')
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_input.Read();
						_tokenStream.Push(new Token(Symbol.DECIMAL, Convert.ToDecimal(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as decimal. Aborting scan.");
					}
				}
				else if (_input.Peek() == 'd' || _input.Peek() == 'D')
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_input.Read();
						_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}
				}
				else if (_input.Peek() == 'e' || _input.Peek() == 'E')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}

					_state = LexicalState.EXPONENTPORTION;
				}
				else
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.INTEGER, Convert.ToInt32(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as int. Aborting scan.");
					}
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessDecimalPortion()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == 'm' || _input.Peek() == 'M')
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_input.Read();
						_tokenStream.Push(new Token(Symbol.DECIMAL, Convert.ToDecimal(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as decimal. Aborting scan.");
					}
				}
				else if (_input.Peek() == 'd' || _input.Peek() == 'D')
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;

						_input.Read();
						_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}
				}
				else if (_input.Peek() == 'f' || _input.Peek() == 'F')
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;

						_input.Read();
						_tokenStream.Push(new Token(Symbol.SINGLE, Convert.ToSingle(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as single. Aborting scan.");
					}
				}
				else if (_input.Peek() == 'e' || _input.Peek() == 'E')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}

					_state = LexicalState.EXPONENTPORTION;
				}
				else
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessExponentPortion()
		{
			if (_input != null)
			{
				if (_input.Peek() == '+' || _input.Peek() == '-')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}

					_state = LexicalState.EXPONENTPORTION2;
				}
				else if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
						}
					}
					else
						_state = LexicalState.EXPONENTPORTION2;
				}
				else
				{
					yytext.Append(_input.Read());
					throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessExponentPortion2()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(_input.Read());

					if (_input.EndOfStream)
					{
						try
						{
							_state = LexicalState.INITIAL_STATE;
							_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
							yytext.Clear();
						}
						catch (Exception)
						{
							throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
						}
					}
				}
				else if (_input.Peek() == 'd' || _input.Peek() == 'D')
				{
					_input.Read();

					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}
				}
				else if (_input.Peek() == 'f' || _input.Peek() == 'F')
				{
					_input.Read();

					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.SINGLE, Convert.ToSingle(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as single. Aborting scan.");
					}
				}
				else
				{
					try
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.DOUBLE, Convert.ToDouble(yytext.ToString())));
						yytext.Clear();
					}
					catch (Exception)
					{
						throw new RqlFormatException($"Unable to cast {yytext} as double. Aborting scan.");
					}
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessULong()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.ULONG, Convert.ToUInt64(yytext.ToString())));
						yytext.Clear();
					}

					if (yytext.Length == 1 && yytext[0] == '0' && (_input.Peek() == 'x' || _input.Peek() == 'X'))
					{
						yytext.Clear();
						_input.Read();
						_state = LexicalState.ULONGHEXVAL;
					}
					else
						_state = LexicalState.ULONG2;
				}
				else
				{
					yytext.Append(Convert.ToString(_input.Read()));
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessULong2()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.ULONG, Convert.ToUInt64(yytext.ToString())));
						yytext.Clear();
					}
				}
				else if (_input.Peek() == 'u' || _input.Peek() == 'U')
				{
					var cx = _input.Read();

					if (_input.Peek() == 'l' || _input.Peek() == 'L')
					{
						_input.Read();
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.ULONG, Convert.ToUInt64(yytext.ToString())));
						yytext.Clear();
					}
					else
					{
						yytext.Append(cx);
						yytext.Append(Convert.ToString(_input.Read()));
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}
				}
				else if (_input.Peek() == 'l' || _input.Peek() == 'L')
				{
					var cx = _input.Read();

					if (_input.Peek() == 'u' || _input.Peek() == 'U')
					{
						_input.Read();
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.ULONG, Convert.ToUInt64(yytext.ToString())));
						yytext.Clear();
					}
					else
					{
						yytext.Append(cx);
						yytext.Append(Convert.ToString(_input.Read()));
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}
				}
				else
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.ULONG, Convert.ToUInt64(yytext.ToString())));
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessLong()
		{
			if (_input != null)
			{
				if ((_input.Peek() >= '0' && _input.Peek() <= '9') || _input.Peek() == '-' || _input.Peek() == '+')
				{
					var c = _input.Read();

					if (_input.EndOfStream && (c == '+' || c == '-'))
					{
						yytext.Append(Convert.ToString(c));
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}
					else if (_input.EndOfStream)
					{
						yytext.Append(Convert.ToString(c));
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.LONG, Convert.ToInt64(yytext.ToString())));
						yytext.Clear();
					}
					else
					{
						if (c != '+')
							yytext.Append(Convert.ToString(c));

						if (yytext.Length == 1 && yytext[0] == '0' && (_input.Peek() == 'x' || _input.Peek() == 'X'))
						{
							_input.Read();
							yytext.Clear();
							_state = LexicalState.LONGHEXVAL;
						}
						else
							_state = LexicalState.LONG2;
					}
				}
				else
				{
					yytext.Append(Convert.ToString(_input.Read()));
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessLong2()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.LONG, Convert.ToInt64(yytext.ToString())));
						yytext.Clear();
					}
				}
				else if (_input.Peek() == 'l' || _input.Peek() == 'L')
				{
					_input.Read();
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.LONG, Convert.ToInt64(yytext.ToString())));
					yytext.Clear();
				}
				else
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.LONG, Convert.ToInt64(yytext.ToString())));
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessUInt()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.UINTEGER, Convert.ToUInt32(yytext.ToString())));
						yytext.Clear();
					}
					else if (_input.Peek() == 'u' || _input.Peek() == 'U')
					{
						_input.Read();
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.UINTEGER, Convert.ToUInt32(yytext.ToString())));
						yytext.Clear();
					}
					else if (yytext.Length == 1 && yytext[0] == '0' && (_input.Peek() == 'x' || _input.Peek() == 'X'))
					{
						_input.Read();
						yytext.Clear();
						_state = LexicalState.UINTHEXVAL;
					}
					else
						_state = LexicalState.UINT2;
				}
				else
				{
					yytext.Append(Convert.ToString(_input.Read()));
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessUInt2()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.UINTEGER, Convert.ToUInt32(yytext.ToString())));
						yytext.Clear();
					}
				}
				else if (_input.Peek() == 'u' || _input.Peek() == 'U')
				{
					_input.Read();
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.UINTEGER, Convert.ToUInt32(yytext.ToString())));
					yytext.Clear();
				}
				else
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.UINTEGER, Convert.ToUInt32(yytext.ToString())));
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessInt()
		{
			if (_input != null)
			{
				if ((_input.Peek() >= '0' && _input.Peek() <= '9') || _input.Peek() == '-' || _input.Peek() == '+')
				{
					var c = _input.Read();

					if (_input.EndOfStream && (c == '+' || c == '-'))
					{
						yytext.Append(Convert.ToString(c));
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}
					else if (_input.EndOfStream)
					{
						yytext.Append(Convert.ToString(c));
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.INTEGER, Convert.ToInt32(yytext.ToString())));
						yytext.Clear();
					}
					else
					{
						if (c != '+')
							yytext.Append(Convert.ToString(c));

						if (yytext.Length == 1 && yytext[0] == '0' && (_input.Peek() == 'x' || _input.Peek() == 'X'))
						{
							_input.Read();
							yytext.Clear();
							_state = LexicalState.INTHEXVAL;
						}
						else
							_state = LexicalState.INT2;
					}
				}
				else
				{
					yytext.Append(Convert.ToString(_input.Read()));
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessInt2()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.INTEGER, Convert.ToInt32(yytext.ToString())));
						yytext.Clear();
					}
				}
				else
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.INTEGER, Convert.ToInt32(yytext.ToString())));
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessUShort2()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.USHORT, Convert.ToUInt16(yytext.ToString())));
						yytext.Clear();
					}
				}
				else
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.USHORT, Convert.ToUInt16(yytext.ToString())));
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessUShort()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.USHORT, Convert.ToUInt16(yytext.ToString())));
						yytext.Clear();
					}
					else
					{
						if (yytext.Length == 1 && yytext[0] == '0' && (_input.Peek() == 'x' || _input.Peek() == 'X'))
						{
							_input.Read();
							yytext.Clear();
							_state = LexicalState.USHORTHEXVAL;
						}
						else
							_state = LexicalState.USHORT2;
					}
				}
				else
				{
					yytext.Append(Convert.ToString(_input.Read()));
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessShort2()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.SHORT, Convert.ToInt16(yytext.ToString())));
						yytext.Clear();
					}
				}
				else
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.SHORT, Convert.ToInt16(yytext.ToString())));
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessShort()
		{
			if (_input != null)
			{
				if ((_input.Peek() >= '0' && _input.Peek() <= '9') || _input.Peek() == '-' || _input.Peek() == '+')
				{
					var c = _input.Read();

					if (_input.EndOfStream && (c == '+' || c == '-'))
					{
						yytext.Append(Convert.ToString(c));
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}
					else if (_input.EndOfStream)
					{
						yytext.Append(Convert.ToString(c));
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.SHORT, Convert.ToInt16(yytext.ToString())));
						yytext.Clear();
					}
					else
					{
						if (c != '+')
							yytext.Append(Convert.ToString(c));

						if (yytext.Length == 1 && yytext[0] == '0' && (_input.Peek() == 'x' || _input.Peek() == 'X'))
						{
							_input.Read();
							yytext.Clear();
							_state = LexicalState.SHORTHEXVAL;
						}
						else
							_state = LexicalState.SHORT2;
					}
				}
				else
				{
					yytext.Append(Convert.ToString(_input.Read()));
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessUTiny2()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.UTINY, Convert.ToByte(yytext.ToString())));
						yytext.Clear();
					}
				}
				else
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.UTINY, Convert.ToByte(yytext.ToString())));
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessUTiny()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.UTINY, Convert.ToByte(yytext.ToString())));
						yytext.Clear();
					}
					else if (yytext[0] == '0' && (_input.Peek() == 'x' || _input.Peek() == 'X'))
					{
						_input.Read();
						yytext.Clear();
						_state = LexicalState.UTINYHEX;

					}
					else
						_state = LexicalState.UTINY2;
				}
				else
				{
					yytext.Append(Convert.ToString(_input.Read()));
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessTiny2()
		{
			if (_input != null)
			{
				if (_input.Peek() >= '0' && _input.Peek() <= '9')
				{
					yytext.Append(Convert.ToString(_input.Read()));

					if (_input.EndOfStream)
					{
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.TINY, Convert.ToSByte(yytext.ToString())));
						yytext.Clear();
					}
				}
				else
				{
					_state = LexicalState.INITIAL_STATE;
					_tokenStream.Push(new Token(Symbol.TINY, Convert.ToSByte(yytext.ToString())));
					yytext.Clear();
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessTiny()
		{
			if (_input.Peek() == ' ' || _input.Peek() == '\t' )
            {
				_input.Read();	//	Skip whitespace
            }
			else if (_input != null)
			{
				if ((_input.Peek() >= '0' && _input.Peek() <= '9') || _input.Peek() == '-' || _input.Peek() == '+')
				{
					var c = _input.Read();
						
					if (_input.EndOfStream && (c == '+' || c == '-'))
					{
						yytext.Append(Convert.ToString(c));
						throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
					}
					else if (_input.EndOfStream)
					{
						yytext.Append(Convert.ToString(c));
						_state = LexicalState.INITIAL_STATE;
						_tokenStream.Push(new Token(Symbol.TINY, Convert.ToSByte(yytext.ToString())));
						yytext.Clear();
					}
					else
					{
						if (c != '+')
							yytext.Append(Convert.ToString(c));

						if (yytext.Length == 1 && yytext[0] == '0' && (_input.Peek() == 'x' || _input.Peek() == 'X'))
						{
							_input.Read();
							yytext.Clear();
							_state = LexicalState.TINYHEX;
						}
						else
							_state = LexicalState.TINY2;
					}
				}
				else
				{
					yytext.Append(Convert.ToString(_input.Read()));
					throw new RqlFormatException($"Unrecognized token {yytext}. Aborting scan.");
				}
			}
			else
			{
				throw new RqlFormatException($"Unexpected end of stream. Aborting scan.");
			}
		}

		private void ProcessTextStream()
		{
			if ((_input.Peek() >= 'a' && _input.Peek() <= 'z') || (_input.Peek() >= 'A' && _input.Peek() <= 'Z') || (_input.Peek() >= '0' && _input.Peek() <= '9') || _input.Peek() == '_')
			{
				yytext.Append(Convert.ToString(_input.Read()));

				if (_input.EndOfStream)
					ParseReservedWords();
			}
			else if (IsStringSpecialChar())
			{
				_state = LexicalState.STRING2;
			}
			else if (_input.Peek() == '\\')
			{
				_state = LexicalState.STRING2;
			}
			else if (_input.Peek() == ':')
			{
				if (string.Equals(yytext.ToString(), "byte", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.UTINY;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "sbyte", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.TINY;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "int8", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.TINY;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "uint8", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.UTINY;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "short", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.SHORT;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "int16", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.SHORT;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "ushort", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.USHORT;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "uint16", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.USHORT;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "int32", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.INT;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "int", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.INT;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "integer", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.INT;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "uint32", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.UINT;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "uint", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.UINT;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "uinteger", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.UINT;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "int64", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.LONG;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "long", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.LONG;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "uint64", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.ULONG;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "ulong", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.ULONG;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "double", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.DOUBLE1;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "float", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.SINGLE1;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "decimal", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.DECIMAL1;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "boolean", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.BOOL1;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "bool", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.BOOL1;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "bin", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.BINARY1;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "binary", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.BINARY1;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "char", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.CHARA;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "bstr", StringComparison.OrdinalIgnoreCase))
				{
					currentBStr = new Bstr();
					_input.Read();
					_state = LexicalState.BSTR1;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "guid", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.GUID1;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "datetime", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.DATETIME1;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "utc", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.UTC1;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "timespan", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.TIMESPAN1;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "string", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.STRING1;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "str", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();
					_state = LexicalState.STRING1;
					yytext.Clear();
				}
				else if (string.Equals(yytext.ToString(), "uri", StringComparison.OrdinalIgnoreCase))
				{
					_input.Read();

					if (_input.Peek() == '"')
					{
						_input.Read();
						_state = LexicalState.QURI1;
					}
					else
						_state = LexicalState.URI1;

					yytext.Clear();
				}
				else
				{
					yytext.Append(Convert.ToString(_input.Read()));
					throw new Exception($"Unrecognized token starting at {yytext}. Aborting scan.");
				}
			}
			else if (_input.Peek() == ' ')
			{
				yytext.Append(Convert.ToString(_input.Read()));

				if (_input.EndOfStream)
					ParseReservedWords();
			}
			else
			{
				ParseReservedWords();
			}
		}

		private void ProcessInitialState()
		{
			if (_input.Peek() == ' ' || _input.Peek() == '\t' || _input.Peek() == '\r' || _input.Peek() == '\b' || _input.Peek() == '\n')
			{
				_input.Read();		//	Skip Whitespace
			}
			else if (IsStringSpecialChar())
			{
				_state = LexicalState.STRING2;
			}
			else if (_input.Peek() == '\\')
			{
				_state = LexicalState.STRING2;
			}
			else if (_input.Peek() == '/')
			{
				_tokenStream.Push(new Token(Symbol.FORWARDSLASH, Convert.ToString(_input.Read())));
				yytext.Clear();
			}
			else if (_input.Peek() == '\'')
			{
				_input.Read();

				if (_input.EndOfStream)
				{
					yytext.Append(Convert.ToString("'"));
					throw new Exception($"Unrecognized token {yytext}. Scan aborted.");
				}

				_state = LexicalState.CHAR1;
			}
			else if (_input.Peek() == '"')
			{
				_input.Read();
				yytext.Clear();

				if ((_input.Peek() == 'h' || _input.Peek() == 'H') &&
					 (_input.LookForward(1) != null && (_input.LookForward(1) == 't' || _input.LookForward(1) == 'T')) &&
					 (_input.LookForward(2) != null && (_input.LookForward(2) == 't' || _input.LookForward(2) == 'T')) &&
					 (_input.LookForward(3) != null && (_input.LookForward(3) == 'p' || _input.LookForward(3) == 'P')) &&
					 (_input.LookForward(4) != null && _input.LookForward(4) == ':'))
				{
					_state = LexicalState.QURI1;
				}
				else if ((_input.Peek() == 'h' || _input.Peek() == 'H') &&
					 (_input.LookForward(1) != null && (_input.LookForward(1) == 't' || _input.LookForward(1) == 'T')) &&
					 (_input.LookForward(2) != null && (_input.LookForward(2) == 't' || _input.LookForward(2) == 'T')) &&
					 (_input.LookForward(3) != null && (_input.LookForward(3) == 'p' || _input.LookForward(3) == 'P')) &&
					 (_input.LookForward(4) != null && (_input.LookForward(4) == 's' || _input.LookForward(4) == 'S')) &&
					 (_input.LookForward(5) != null && _input.LookForward(5) == ':'))
				{
					_state = LexicalState.QURI1;
				}
				else if (_input.Peek() == '/')
				{
					_state = LexicalState.QURI1;
				}
				else
				{
					_state = LexicalState.QSTRING1;
				}
			}
			else if (_input.Peek() == '(')
			{
				_tokenStream.Push(new Token(Symbol.LPAREN, Convert.ToString(_input.Read())));
				yytext.Clear();
			}
			else if (_input.Peek() == ')')
			{
				_tokenStream.Push(new Token(Symbol.RPAREN, Convert.ToString(_input.Read())));
				yytext.Clear();
			}
			else if (_input.Peek() == ',')
			{
				_tokenStream.Push(new Token(Symbol.COMMA, Convert.ToString(_input.Read())));
				yytext.Clear();
			}
			else if (_input.Peek() == '&')
			{
				_tokenStream.Push(new Token(Symbol.AND, Convert.ToString(_input.Read())));
				yytext.Clear();
			}
			else if (_input.Peek() == '|')
			{
				_tokenStream.Push(new Token(Symbol.OR, Convert.ToString(_input.Read())));
				yytext.Clear();
			}
			else if (_input.Peek() == '>')
			{
				yytext.Append(Convert.ToString(_input.Read()));

				if (!_input.EndOfStream && _input.Peek() == '=')
				{
					yytext.Append(Convert.ToString(_input.Read()));
					_tokenStream.Push(new Token(Symbol.GE, yytext.ToString()));
					yytext.Clear();
				}
				else
				{
					_tokenStream.Push(new Token(Symbol.GT, yytext.ToString()));
					yytext.Clear();
				}
			}
			else if (_input.Peek() == '<')
			{
				yytext.Append(Convert.ToString(_input.Read()));

				if (!_input.EndOfStream && _input.Peek() == '=')
				{
					yytext.Append(Convert.ToString(_input.Read()));
					_tokenStream.Push(new Token(Symbol.LE, yytext.ToString()));
					yytext.Clear();
				}
				else if (!_input.EndOfStream && _input.Peek() == '>')
				{
					yytext.Append(Convert.ToString(_input.Read()));
					_tokenStream.Push(new Token(Symbol.NE, yytext.ToString()));
					yytext.Clear();
				}
				else
				{
					_tokenStream.Push(new Token(Symbol.LT, yytext.ToString()));
					yytext.Clear();
				}
			}
			else if (_input.Peek() == '!')
			{
				yytext.Append(Convert.ToString(_input.Read()));

				if (!_input.EndOfStream && _input.Peek() == '=')
				{
					yytext.Append(Convert.ToString(_input.Read()));
					_tokenStream.Push(new Token(Symbol.NE, yytext.ToString()));
					yytext.Clear();
				}
				else
				{
					yytext.Append(Convert.ToString(_input.Read()));
					throw new Exception($"Unrecognized token {yytext}. Scan aborted.");
				}
			}
			else if (_input.Peek() == '=')
			{
				yytext.Append(_input.Read());
				var position = _input.Position;

				if (_input.Peek() == 'N' || _input.Peek() == 'n')
				{
					yytext.Append(_input.Read());

					if (_input.Peek() == 'E' || _input.Peek() == 'e')
					{
						yytext.Append(_input.Read());

						if (_input.Peek() == '=')
						{
							yytext.Append(_input.Read());
							_tokenStream.Push(new Token(Symbol.NE, yytext.ToString()));
							yytext.Clear();
						}
						else
						{
							_input.Position = position;
							_tokenStream.Push(new Token(Symbol.EQ, "="));
							yytext.Clear();
						}
					}
					else
					{
						_input.Position = position;
						_tokenStream.Push(new Token(Symbol.EQ, "="));
						yytext.Clear();
					}
				}
				else if (_input.Peek() == 'L' || _input.Peek() == 'l')
				{
					yytext.Append(_input.Read());

					if (_input.Peek() == 'E' || _input.Peek() == 'e')
					{
						yytext.Append(_input.Read());

						if (_input.Peek() == '=')
						{
							yytext.Append(_input.Read());
							_tokenStream.Push(new Token(Symbol.LE, yytext.ToString()));
							yytext.Clear();
						}
						else
						{
							_input.Position = position;
							_tokenStream.Push(new Token(Symbol.EQ, "="));
							yytext.Clear();
						}
					}
					else if (_input.Peek() == 'T' || _input.Peek() == 't')
					{
						yytext.Append(_input.Read());

						if (_input.Peek() == '=')
						{
							yytext.Append(_input.Read());
							_tokenStream.Push(new Token(Symbol.LT, yytext.ToString()));
							yytext.Clear();
						}
						else
						{
							_input.Position = position;
							_tokenStream.Push(new Token(Symbol.EQ, "="));
							yytext.Clear();
						}
					}
					else
					{
						_input.Position = position;
						_tokenStream.Push(new Token(Symbol.EQ, "="));
						yytext.Clear();
					}
				}
				else if (_input.Peek() == 'G' || _input.Peek() == 'g')
				{
					yytext.Append(_input.Read());

					if (_input.Peek() == 'E' || _input.Peek() == 'e')
					{
						yytext.Append(_input.Read());

						if (_input.Peek() == '=')
						{
							yytext.Append(_input.Read());
							_tokenStream.Push(new Token(Symbol.GE, yytext.ToString()));
							yytext.Clear();
						}
						else
						{
							_input.Position = position;
							_tokenStream.Push(new Token(Symbol.EQ, "="));
							yytext.Clear();
						}
					}
					else if (_input.Peek() == 'T' || _input.Peek() == 't')
					{
						yytext.Append(_input.Read());

						if (_input.Peek() == '=')
						{
							yytext.Append(_input.Read());
							_tokenStream.Push(new Token(Symbol.GT, yytext.ToString()));
							yytext.Clear();
						}
						else
						{
							_input.Position = position;
							_tokenStream.Push(new Token(Symbol.EQ, "="));
							yytext.Clear();
						}
					}
					else
					{
						_input.Position = position;
						_tokenStream.Push(new Token(Symbol.EQ, "="));
						yytext.Clear();
					}
				}
				else if (_input.Peek() == 'E' || _input.Peek() == 'e')
				{
					yytext.Append(_input.Read());

					if (_input.Peek() == 'Q' || _input.Peek() == 'q')
					{
						yytext.Append(_input.Read());

						if (_input.Peek() == '=')
						{
							yytext.Append(_input.Read());
							_tokenStream.Push(new Token(Symbol.EQ, yytext.ToString()));
							yytext.Clear();
						}
						else
						{
							_input.Position = position;
							_tokenStream.Push(new Token(Symbol.EQ, "="));
							yytext.Clear();
						}
					}
					else
					{
						_input.Position = position;
						_tokenStream.Push(new Token(Symbol.EQ, "="));
						yytext.Clear();
					}
				}
				else
				{
					_tokenStream.Push(new Token(Symbol.EQ, yytext.ToString()));
					yytext.Clear();
				}
			}
			else if (_input.Peek() == '-' || _input.Peek() == '+' || _input.Peek() == '.')
			{
				_state = LexicalState.NUMERIC1;
			}
			else if (_input.Peek() >= '0' && _input.Peek() <= '9')
			{
				_state = LexicalState.DATEORTIMEORNUMERICORGUID;
			}
			else if ((_input.Peek() >= 'a' && _input.Peek() <= 'f') || (_input.Peek() >= 'A' && _input.Peek() <= 'F'))
			{
				_state = LexicalState.GUIDORSTRING1;
			}
			else if ((_input.Peek() >= 'g' && _input.Peek() <= 'z') || (_input.Peek() >= 'G' && _input.Peek() <= 'Z') || _input.Peek() == '_')
			{
				if ((_input.Peek() == 'h' || _input.Peek() == 'H') &&
					 (_input.LookForward(1) != null && (_input.LookForward(1) == 't' || _input.LookForward(1) == 'T')) &&
					 (_input.LookForward(2) != null && (_input.LookForward(2) == 't' || _input.LookForward(2) == 'T')) &&
					 (_input.LookForward(3) != null && (_input.LookForward(3) == 'p' || _input.LookForward(3) == 'P')) &&
					 (_input.LookForward(4) != null && _input.LookForward(4) == ':'))
				{
					_state = LexicalState.URI1;
				}
				else if ((_input.Peek() == 'h' || _input.Peek() == 'H') &&
					 (_input.LookForward(1) != null && (_input.LookForward(1) == 't' || _input.LookForward(1) == 'T')) &&
					 (_input.LookForward(2) != null && (_input.LookForward(2) == 't' || _input.LookForward(2) == 'T')) &&
					 (_input.LookForward(3) != null && (_input.LookForward(3) == 'p' || _input.LookForward(3) == 'P')) &&
					 (_input.LookForward(4) != null && (_input.LookForward(4) == 's' || _input.LookForward(4) == 'S')) &&
					 (_input.LookForward(5) != null && _input.LookForward(5) == ':'))
				{
					_state = LexicalState.URI1;
				}
				else
				{
					_state = LexicalState.TEXTSTREAM;
				}
			}
			else
			{
				yytext.Append(Convert.ToString(_input.Read()));
				throw new Exception($"Unrecognized token {yytext}. Scan aborted.");
			}
		}

		private void ParseReservedWords()
		{
			if (string.Equals(yytext.ToString(), "limit", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.LIMIT, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "select", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.SELECT, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "sort", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.SORT, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "contains", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.CONTAINS, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "excludes", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.EXCLUDES, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "like", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.LIKE, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "eq", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.EQOP, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "lt", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.LTOP, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "le", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.LEOP, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "gt", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.GTOP, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "ge", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.GEOP, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "ne", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.NEOP, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "and", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.ANDOP, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "or", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.OROP, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "distinct", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.DISTINCT, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "First", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.First, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "one", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.ONE, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "count", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.COUNT, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "values", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.VALUES, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "sum", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.SUM, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "max", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.MAX, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "min", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.MIN, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "mean", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.MEAN, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "true", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.BOOLEAN, true));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "false", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.BOOLEAN, false));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "in", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.IN, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "out", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.OUT, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "aggregate", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.AGGREGATE, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else if (string.Equals(yytext.ToString(), "null", StringComparison.OrdinalIgnoreCase))
			{
				_tokenStream.Push(new Token(Symbol.NULL, null));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
			else
			{
				_tokenStream.Push(new Token(Symbol.PROPERTY, yytext.ToString()));
				yytext.Clear();

				_state = LexicalState.INITIAL_STATE;
			}
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					if (_input != null)
					{
						_input.Close();
						_input.Dispose();
					}
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		~Lexer()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(false);
		}

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
