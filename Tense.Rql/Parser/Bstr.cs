using System;

namespace Tense.Rql
{
	/// <summary>
	/// Represents a string encoded in binary format 
	/// </summary>
	public class Bstr
	{
		/// <summary>
		/// The encoding used to encode the string
		/// </summary>
		public string Encoding { get; set; } = string.Empty;

		/// <summary>
		/// The binary value of the string, represented as Base 64 String
		/// </summary>
		public string Value { get; set; } = string.Empty;

		/// <summary>
		/// Gets the unencoded value of the string
		/// </summary>
		/// <returns></returns>
		public string Decode()
		{
			return System.Text.Encoding.GetEncoding(Encoding).GetString(Convert.FromBase64String(Value));
		}

		/// <summary>
		/// Encodes a string using he specified encoding
		/// </summary>
		/// <param name="encoding">The encoding used to encode the string</param>
		/// <param name="value">The value to encode</param>
		/// <returns>A <see cref="Bstr"/> object that represents the encoded string.</returns>
		public static Bstr Encode(string encoding, string value)
		{
			return new Bstr() {
				Encoding = encoding,
				Value = Convert.ToBase64String(System.Text.Encoding.GetEncoding(encoding).GetBytes(value))
			};
		}

		/// <summary>
		/// Returns the array of bytes represented by the encoded string
		/// </summary>
		/// <returns>The array of <see cref="byte"/>s that the string represents.</returns>
		public byte[] GetBytes()
        {
			return Convert.FromBase64String(Value);
		}

		/// <summary>
		/// Converts the value of the current <see cref="Bstr"/> object to its equivalent string representation.
		/// </summary>
		/// <returns>A string representation of the value of the current <see cref="Bstr"/> object.</returns>
		public override string ToString()
		{
			return Decode();
		}
	}
}
