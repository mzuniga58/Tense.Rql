﻿using System;
using System.Runtime.Serialization;

namespace Tense.Rql
{
	/// <summary>
	/// The exception that is thrown when the contents of a string do not form a valid
	/// RQL query, or the query is malformed.
	/// </summary>
	public class RqlFormatException : FormatException
	{
		/// <summary>
		///	Initializes a new instance of the <see cref="RqlFormatException"/> class.
		/// </summary>
		public RqlFormatException() : base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RqlFormatException"/> class with a specified
		///	error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public RqlFormatException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RqlFormatException"/> class with a specified
		///	error message and a reference to the inner exception that is the cause of this
		///	exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If 
		/// the innerException parameter is not a null reference (Nothing in Visual Basic), the 
		/// current exception is raised in a catch block that handles the inner exception.</param>
		public RqlFormatException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RqlFormatException"/> class with serialized
		///	data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		protected RqlFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
