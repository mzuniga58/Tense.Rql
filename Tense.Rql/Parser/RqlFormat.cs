namespace Tense.Rql
{
	/// <summary>
	/// Represents the format of an RQL Query
	/// </summary>
	public enum RqlFormat
	{
		/// <summary>
		///	The standard format of the form a=3&amp;select(a,b,c,d)
		/// </summary>
		Standard = 0,

		/// <summary>
		/// The normalized format of the form and(eq(a,3),select(a,b,c,d))
		/// </summary>
		Normalized = 1
	}
}
