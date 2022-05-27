<h1>Resource Query Language Reference</h1>
<p>The Resource Query Language (RQL) is a query language designed for use in URIs with object style data structures. <b>Tense.Rql</b> includes the RQL specification and provides a C# implementation of query parsing and model translations. Supporting packages, such as <b>Tense.Rql.SqlServer</b> provide SQL Statement generators that can translate an RQL query into its corresponding SQL Statement. RQL can be thought as basically a set of nestable named operators which each have a set of arguments. RQL is designed to have an extremely simple, but extensible grammar that can be written in a URL friendly query string.</p>
<h2>General Syntax</h2>
<p>The RQL grammar is based around standard URI delimiters, and the standard rules for encoding strings with URL encoding (%xx) are observed. RQL is also a superset of the Feed Item Query Language (FIQL, pronounced "fickle"). It supports parenthesized nested operations (also called the normalized form), as well as alpha relational operations (i.e., =lt is the same as &lt;).<p>
<h3>Relational Operations</h3>
<p>The relational operations include the standard set:</p>
<ul>
<li><b>=</b> - can also be written as <b>=eq</b></li>
<li><b>&lt;</b> - can also be written as <b>=lt</b></li>
<li><b>&lt;=</b> - can also be written as <b>=le</b></li>
<li><b>&gt;</b> - can also be written as <b>=gt</b></li>
<li><b>&gt;=</b> - can also be written as <b>=ge</b></li>
<li><b>!=</b> - can also be written as <b>=ne</b></li>
</ul>
<p>Each of these operations can also be written in the paranthesized nested form of &lt;op&gt;(&lt;arg&gt;,&lt;arg&gt;). For example, if we wanted to create a filter that returns all the records in a dataset where Age is greater than 18, we could write:</p>
<pre><code>Age&gt;18
Age=gt=18
gt(Age,18)</code></pre>
<p>The meaning of each of the above statements are identitcal. The syntax for a relational operator is:</p>
<pre><code>&lt;property&gt;&lt;symboloperator&gt;&lt;value&gt;
&lt;property&gt;&lt;alphaoperator&gt;=&lt;value&gt;
&lt;operator&gt;(&lt;property&gt;,&lt;value&gt;)
</code></pre>
<p>The &lt;value&gt; argument is evaluated as in integer, double or string for most values, and translated to specialty values for specialty formats, such as Dates, Timespans or Unique Identitifers. For example, consider the following RQL statements:<p>
<pre><code>Age=18
FirstName=Joe
Cost=2.15</code></pre>
<p>In the first case, the value 18 is encoded as a signed interger (int) value, the value "Joe" is encoded as a string, and the value 2.15 is encoded as a double. However, at the timei of SQL generation, the value is cast to the value of the column. So, for example, if Cost is a column defined in a database, and the data type of Cost is money, then the double value 2.15 will be cast as decimal(18,2) to match the column it references when the SQL Statement is generated. Likewise, a value that represents a date (such as "1/1/1960 14:32:25") would be encoded as a UTC Date. If the column to which it references is a DataTimeOffset, then the appropriate conversion will be done at the time of the SQL Statement generation.<p>
<p>Sometimes, for certain queries, the default values are not suffient. What if, for example, you wanted to write this statement:<p>
<pre><code>Total=7000000000
</code></pre>
<p>In this statement, Total is defined as a long, a 64-bit integer, and 7000000000 is a perfectly valid value for that column. But trying to cast 7000000000 into an integer value (which is what the RQL parser will try to do) will throw an exception, because the value 7000000000 is outside the range of valid 32-bit integers. To write this statement, we will need to tell the RQL parser that we need a long value here. We can do this is a few ways:<p>
<pre><code>Total=7000000000l
Total=7000000000L
Total=long:7000000000
Total=int64:7000000000</code></pre>
<p>The first value is valid, but not recommended. The last character is a lower case L, making the value a long value. It is not recommended simply becuase it's difficult to tell if that last character is a lower case L, or the number 1. Because of that, I prefer to use the uppercase L, that way there is no doubt. The other two forms are called the cast forms, and they simply take the raw value and cast them to the type specified. Here is a list of cast operators:</p>
<ul>
<li>Numeric cast operators</li>
<ul>
<li>byte - an unsigned byte value, valid values are 0 - 255</li>
<li>uint8 - same as byte</li>
<li>sbyte - a signed byte value, valid values are -127 to 128</li>
<li>int8 - same as sbyte</li>
<li>short - a signed 16-bit integer value, valid values are -32,768 to 32,767</li>
<li>int16 - same as short</li>
<li>ushort - an unsigned 16-bit integer value, valid values are 0 to 32,768</li>
<li>uint16 - same as ushort</li>
<li>int - a signed 32-bit integer value, valid values are -2,147,483,648 to 2,147,483,647</li>
<li>integer - same as int</li>
<li>int32 - same as int</li>
<li>uint - an unsigned 32-bit integer value, valid values are 0 to 2,147,483,468</li>
<li>uint32 - same as uint</li>
<li>long - a signed 64-bit integer value, valid values are -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807</li>
<li>int64 - same as long</li>
<li>ulong - an unsigned 64-bit integer value, valid values are 0 to 9,223,372,036,854,775,808</li>
<li>uint64 - same as ulong</li>
<li>float - a 32-bit floating point value</li>
<li>double - a 64-bit floating point value</li>
<li>decimal - a decimal value</li>
</ul>
<li>The Boolean operators</li>
<ul>
<li>bool - a boolean value, either <i>true</i> or <i>false</i>
<li>boolean - same as bool
</ul>
</ul>
