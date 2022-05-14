namespace Tense.Rql
{
	/// <summary>
	/// RQL Operations
	/// </summary>
	public enum RqlOperation
	{
		/// <summary>
		/// No operation - used as a placeholder during the construction of an RQL Node
		/// </summary>
		NOOP,

		/// <summary>
		/// limit(start,size)
		/// </summary>
		LIMIT,

		/// <summary>
		/// select(property,property,...)
		/// </summary>
		SELECT,

		/// <summary>
		/// sort(+/-property,+/-property,...)
		/// </summary>
		SORT,

		/// <summary>
		/// and(operation,operation,...)
		/// </summary>
		AND,

		/// <summary>
		/// or(operation,operation,...)
		/// </summary>
		OR,

		/// <summary>
		/// in(property,value,value,...)
		/// </summary>
		IN,

		/// <summary>
		/// le(property,value)
		/// </summary>
		LE,

		/// <summary>
		/// lt(property,value)
		/// </summary>
		LT,

		/// <summary>
		/// ge(property,value)
		/// </summary>
		GE,

		/// <summary>
		/// gt(property,value)
		/// </summary>
		GT,

		/// <summary>
		/// ne(property,value)
		/// </summary>
		NE,

		/// <summary>
		/// eq(property,value)
		/// </summary>
		EQ,

		/// <summary>
		/// like(property,value)
		/// </summary>
		LIKE,

		/// <summary>
		/// contains(property,value)
		/// </summary>
		CONTAINS,

		/// <summary>
		/// excludes(property,value)
		/// </summary>
		EXCLUDES,

		/// <summary>
		/// distinct()
		/// </summary>
		DISTINCT,

		/// <summary>
		/// first()
		/// </summary>
		FIRST,

		/// <summary>
		/// one()
		/// </summary>
		ONE,

		/// <summary>
		/// out(property,value,value...)
		/// </summary>
		OUT,

		/// <summary>
		/// propert (name,name,...)
		/// </summary>
		PROPERTY,

		/// <summary>
		/// sort property -  { ascending/descending, propergy, prop
		/// </summary>
		SORTPROPERTY,

		/// <summary>
		/// count()
		/// </summary>
		COUNT,

		/// <summary>
		/// values(property)
		/// </summary>
		VALUES,

		/// <summary>
		/// max(property)
		/// </summary>
		MAX,

		/// <summary>
		/// min(property)
		/// </summary>
		MIN,

		/// <summary>
		/// mean([property)
		/// </summary>
		MEAN,

		/// <summary>
		/// sum(property)
		/// </summary>
		SUM,

		/// <summary>
		/// aggregate(property,property,...,function,function,...)
		/// </summary>
		AGGREGATE
	}
}
