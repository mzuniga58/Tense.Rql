using System;

namespace Tense.Rql
{
	internal class Token
	{
		public Symbol Symbol { get; set; }
		private object? TokenValue { get; set; }

		public Token(Symbol s, object? v)
		{
			Symbol = s;
			TokenValue = v;
		}

		public static Token EndOfStreamToken()
		{
			return new Token(Symbol.EndOfStream, null);
		}

		public T? Value<T>() where T : class
		{
			if (typeof(T) == typeof(object))
				return (T?)TokenValue;

			return (T)Convert.ChangeType(TokenValue, typeof(T));
		}

		public T NonNullValue<T>() 
		{
			if ( TokenValue == null )
				throw new ArgumentNullException(nameof(TokenValue));

			if (typeof(T) == typeof(object))
				return (T)TokenValue;

			return (T)Convert.ChangeType(TokenValue, typeof(T));
		}

		public override string ToString()
		{
			return $"{Symbol} ({TokenValue})";
		}
	}
}
