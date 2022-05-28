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
<h2>Encoding Values</h2>
<p>The &lt;value&gt; argument is evaluated as in integer, double or string for most values, and translated to specialty values for specialty formats, such as Dates, Timespans or Unique Identitifers. For example, consider the following RQL statements:<p>
<pre><code>Age=18
FirstName=Joe
Cost=2.15</code></pre>
<p>In the first case, the value 18 is encoded as a signed interger (int) value, the value "Joe" is encoded as a string, and the value 2.15 is encoded as a double. However, at the time of SQL generation, the value is cast to the value of the column. So, for example, if <b>Cost</b> is a column defined in a database, and the data type of <b>Cost</b> is <i>money</i>, then the double value 2.15 will be cast as decimal(18,2) to match the column it references when the SQL Statement is generated. Likewise, a value that represents a date (such as "1/1/1960 14:32:25") would be encoded as a UTC Date. If the column to which it references is a DataTimeOffset, then the appropriate conversion will be done at the time of the SQL Statement generation.<p>
<p>Sometimes, for certain queries, the default values are not suffient. What if, for example, you wanted to write this statement:<p>
<pre><code>Total=7000000000
</code></pre>
<p>In this statement, <b>Total</b> is defined as a long, a 64-bit integer, and 7000000000 is a perfectly valid value for that column. But trying to cast 7000000000 into an integer value (which is what the RQL parser will try to do) will throw an exception, because the value 7000000000 is outside the range of valid 32-bit integers. To write this statement, we will need to tell the RQL parser that we need a long value here. We can do this in a few ways:<p>
<pre><code>Total=7000000000l
Total=7000000000L
Total=long:7000000000
Total=int64:7000000000</code></pre>
<p>The first value is valid, but not recommended. The last character is a lower case L, making the value a long value. It is not recommended simply becuase, depending on the font you are using, it can be difficult to tell if that last character is a lower case L, or the number 1. Because of that, I prefer to use the uppercase L, that way there is no doubt. Other valid value type identifiers are as follows:</p>
<ul>
<li><b>nnn</b> - 32-bit signed ingeter</li>
<li><b>0xnn</b> - 8 bit unsigned integer (the hexidecimal format)</li>
<li><b>0xnnnn</b> - 16-bit unsigned integer (the hexidecimal format)</li>
<li><b>0xnnnnnnnn</b> - 32-bit unsigned integer (the hexidecimal format)</li>
<li><b>0xnnnnnnnnnnnnnnnn</b> - 64-bit unsigned integer (the hexidecimal format)</li>
<li><b>nnnu</b> - 32-bit unsigned integer</li>
<li><b>nnnL or nnnl</b> - 64-bit signed integer</li>
<li><b>nnnUL or nnnul</b> - 64-bit unsigned integer</li>
<li><b>nnn.nn</b> - double value</li>
<li><b>nnnd or nnnD</b> - double value</li>
<li><b>nnnf or nnnF</b> - single (float) value</li>
<li><b>nnnm or nnnM</b> - decimal (money) value</li>
</ul>
<p>The other two forms are called the cast forms, and they simply take the raw value and cast them to the type specified. Here is a list of cast operators:</p>
<ul>
<li>Numeric cast operators</li>
<ul>
<li><b>byte</b> - an unsigned byte value, valid values are 0 - 255</li>
<li><b>uint8</b> - same as byte</li>
<li><b>sbyte</b> - a signed byte value, valid values are -127 to 128</li>
<li><b>int8</b> - same as sbyte</li>
<li><b>short</b> - a signed 16-bit integer value, valid values are -32,768 to 32,767</li>
<li><b>int16</b> - same as short</li>
<li><b>ushort</b> - an unsigned 16-bit integer value, valid values are 0 to 32,768</li>
<li><b>uint16</b> - same as ushort</li>
<li><b>int</b> - a signed 32-bit integer value, valid values are -2,147,483,648 to 2,147,483,647</li>
<li><b>integer</b> - same as int</li>
<li><b>int32</b> - same as int</li>
<li><b>uint</b> - an unsigned 32-bit integer value, valid values are 0 to 2,147,483,468</li>
<li><b>uint32</b> - same as uint</li>
<li><b>long</b> - a signed 64-bit integer value, valid values are -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807</li>
<li><b>int64</b> - same as long</li>
<li><b>ulong</b> - an unsigned 64-bit integer value, valid values are 0 to 9,223,372,036,854,775,808</li>
<li><b>uint64</b> - same as ulong</li>
<li><b>float</b> - a 32-bit floating point value</li>
<li><b>double</b> - a 64-bit floating point value</li>
<li><b>decimal</b> - a decimal value</li>
</ul>
<li>The Boolean operators</li>
<ul>
<li><b>bool</b> - a boolean value, either <i>true</i> or <i>false</i>
<li><b>boolean</b> - same as bool
</ul>
<li>The Date and Time operators</li>
<ul>
<li><b>DateTime</b> - a UTC DateTime, can take the form "mm/dd/yyyy hh:mm:ss.ffffff+/-hh:mm" or "yyyy/mm/ddThh:mm:ss.ffffff+/-hh:mm". The time and timezone information is optional.</li>
<li><b>utc</b> - a UTC DateTime.</li>T
<li><b>TimeSpan</b> - a time span, of the form d.hh:mm:ss.ffffff</li>
</ul>
<li>The Binary operators
<ul>
<li><b>bin</b> - a binary value, in the form of a base-64 encoded string</li>
<li><b>binary</b> - same as bin
</ul>
<li>String Operators</li>
<ul>
<li><b>str</b> - a string</li>
<li><b>string</b> - same as str</li>
<li><b>bstr</b> - written as bstr:&lt;encoding&gt;:value i.e., bstr:utf8:value - a base-64 encoded string</li>
<li><b>char</b> - a character value</li>
</ul>
<li>Miscellaneous operators</li>
<ul>
<li><b>guid</b> - a GUID (Globally Unique Identifier) value</li>
<li><b>uri</b> - a URI value
</ul>
</ul>
<p>A word about strings and uris. The values of strings and Uris need not be enclosed in quotes where the meaning of the string is unambigious. However, sometimes, the meaning isn't perfectly clear. Consider the following statement:</p>
<pre><code>Name=Tom &amp; Jerry's=Good Ice Creame
</code></pre>
<p>This statement can be interpreted in either of two ways. Either we are searching for the name "Tom &amp; Gerrys=Good Ice Creame", or we are searching for the name "Tom" and a column called "Jerry's" which should be equal to "Good Ice Creame". When the RQL parser sees the &amp; character, it interprets it as the end of the previous clause and the beginning of a new clause joined by the AND operation. Therefore, a statement like Name=Tom &amp; Jerry's would throw an RQL Format Exception, because it would interpret Jerry's as the beginning of the next clause, and that clause is incomplete. The same issue can happen with Uris. To avoid situations like this, you an enclose the the value in double quotes: Name="Tom &amp; Jerry's". This statement will compile corretly, because it will interpret "Tom &amp; Jerry's" as a single string.</p>
<h2>Logical Operators</h2>
<p>Now that we have the basics of relational operators, and understand how we can encode values, let's take a look at the logical operations. There are two, and they are fairly self-explanatory:</p>
<ul>
<li>AND</li>
<li>OR</li>
</ul>
<p>You can encode the <b>AND</b> and <b>OR</b> operations using the &amp; and | symbols respectively. For example, suppose we have a table with a member called <b>Status</b>. Suppose further that the <b>Status</b> member can take on any of three values: A - active, I - inactve and P - in process. Suppose further that there is a <b>Category</b> member. Now suppose I want the records in category 1 that are acive. The following RQL statement will give us those results:</p>
<pre><code>Category=1&Status=A
</code></pre>
<p>We can write the same statement using alphasymbols, or in normalized form:</p>
<pre><code>Category=eq-1&Status=eq=A
and(eq(Category,1),eq(Status,A))</code></pre>
<p>We can also combine logical operators using paranthesis. Suppose we want the records in category 1 that are active, combined with the records in category 2 that are in process:</p>
<pre><code>(Category=1&Status=A)|(Category=2&Status=P)
or(and(eq(Category,1),eq(Status,A)),and(eq(Category,2),eq(Status,P)))</code></pre>
<p>The normalized form is a bit difficult to read, but it does the same as the statement above it. It's easier to read if we break it down in a hierarchial form:</p>
<pre><code>
or(                                        ,                                          )
     and(                ,              )     and (                 ,               )
          eq(Category,1)   eq(Status,A)              eq(Category,2)    eq(Status,P)
</code></pre>
<h2>The Aggregation Operators</h2>
<p>Along with the relational and logical operators, RQl also supports aggregation. Think of this as the GROUP BY clause in SQL. The aggregation operator are:<p>
<ul>
<li><b>MAX</b> - delivers the maximum value of a member in a set</li>
<li><b>MIN</b> - delivers teh minimum value of a member in a set</li>
<li><b>MEAN</b> - delivers the mean, or average, value of a member in a set</li>
<li><b>COUNT</b> - delivers the total number of members in a set</li>
<li><b>SUM</b> - delivers the sum of the members in a set</li>
</ul>
<p>Used by themselves, these operators will return one record with the specified value. If these operators are used, they can only be used with other aggregation operators. Again, think of the GROUP BY clause in SQL. For example, suppose we have a list of customers purchases, and suppose that the customer purchaes record has the members PurchaseDate, ProductId, Age (the age of the customer in years), Quantity, Cost, List, and Discount. Now, suppose we wanted to get the average cost of all purchases.</p>
<pre><code>mean(Cost)
</code></pre>
<p>This query will return one record with one value, the average cost of all purchases. If we wanted the aveerage cost and the average list price:</p>
<pre><code>mean(Cost)&mean(List)
</code></pre>
<p>But, now suppose we wanted those values broken down by age. To do that we need to introduce another operator, the <b>AGGREGATE</b> operator. The aggregate operator takes a list of properties, and a list of aggregate operations. The properties are included in the GROuP By clause of the SQL statement, and the aggregate operations are the values in the SELECT clause.</p>
<pre></code>aggregate(Age,mean(Cost),Mean(List))</code></pre>
<p>This query will return one record for each Age value, and that record will contain two values, the average cost and averge list price for that age.</p>
<h2>Paging Operator</h2>
<p>A collection of items is returned in a <b>PageSet</b> class, and is limited to batch-size defined in your service. For example, suppose we have table of customers, and we are fortunate enough to have 100,000 customers. The endpoint to retrive the list of customers is /customers. You can call this endpoint, with no filters, and it will return the entire list. But it won't do it all in one shot. Instead of returning 100,000 records, it will only return the first 100 records (assuming your batch-size is 100). To get the remaining records, you have to specify a start and page size. In RQl, we use the <b>LIMIT</b> operator.</p>
<pre><code>limit(&lt;start&gt;,&lt;pagesize&gt;)
</code></pre>
<p>The start value is a one based index value. For example, suppose I called /customers?limit(1,10). This statement would return the first 10 customers in the list. The call /customers?limit(11,10) would return the next 10 customers, starting with the 11th customer. Unless otherwise specified, the returned set will be ordered by the primary key(s) of the table in question.</p>
<h2>The Sort operator</h2>
<p>You can specifiy the order of a collection using the <b>SORT</b> operator. The syntax is:</p>
<pre><code>sort(+/-member,+/-member,...+/-member)</code></pre>
<p>Here, <i>member</i> is the name of a member in the table. If preceeded with the + symbol (or no symbol at all) the result set will be sorted by the values of that member in ascending order. Placing an - before the member causes the set to be sorted by the values of that member in descending order.</p>
<p>You can combine these operators (and all the following operators) using the &amp; symbol (or place them inside an and operator in normalized form).</p>
<pre><code>Status=A&sort(Age,Name)&limit(1,10)</code></pre>
<p>Unlike logical operators, the order of these operators does not matter.</p>
<h2>Choosing your returned columns</h2>
<p>Sometimes, you don't need to get all the members of a record. If you are constructing a list of items on a Web Page, all you really need is the id and the name of that record. We can limit the members returned using the <b>SELECT</b> operator (not to be confused with the SELECT clause of a SQL Statement).</p>
<pre><code>select(Id,FirstName,LastName)</code></pre>
<p>A customer record, for example, is likely to have many columns: Id, FirstName, LastName, Address1, Address2, City, State, PostalCode, PhoneNumber, etc. If we were to apply the above select operator, then only the Id, FirstName and LastName members would be returned for each customer in the list.</p>
