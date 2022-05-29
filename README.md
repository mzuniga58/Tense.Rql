<h1>Tense.Rql</h1>
A library that supports RQL for REST Services. The *Tense.Rql* works in conjunction with the *Tense* package. 

The Resource Query Language (RQL) is a query language designed for use in URIs with object style data structures. The language provides powerful filtering capabilities to your endpoints. In addition, the RQL language can be used to support multi-update / multi-delete operations on resources.

The RQL language is used to describe the SQL Statement that will be used to perform the operation. In accordance with that goal, the *Tense.Rql* package contains the following features:

<h2>RqlNode</h2>
The <b>RqlNode</b> class is a structured representation of an RQL Statement. It has various functions to compile, inspect and manipulate the statement. An <b>RqlNode</b> is a hierarchical representation of the RQL statement. It consists of an Operation, and a set of parameters. Each parameter may be another RqlNode, or it may be an object value.

For example, the RQL Statement

BookId=1

is compiled into

![alt text](https://github.com/mzuniga58/Tense.Rql/blob/main/Images/RqlNode1.png "RqlNode")

Each node begins with an operation. In this case, the operation is <b>EQ</b>, representing equality. The <b>EQ</b> operation takes two paramters, and it will check whether parameter 1 is equal to parameter 2. The first parmater is another <b>RqlNode</b>. This node has the operation <b>PROPERTY</b>, and it takes one paramter. The <b>PROPERTY</b> operation means that its single parameter is the name of a member in a model. The parameter for this operation is an object value - a string in this case - that holds the value "BookId".

The second parameter of the parent node is an object value -- an int in this case -- holding the value 1.

This node can be used to generate the SQL clause:

```
BookId = 1
```

Which can then be placed inside of a WHERE clause.

<h3>RqlNode.Parse</h3>

<i>RqlNode</i> <b>RqlNode</b>.Parse(<i>statement</i>)

- <i>statement</i> - the RQL Statement to be compiled

The <b>RqlNode</b>.Parse function is used to parse, or compile an RQL Statement into an <b>RqlNode</b> representation.

<b>Returns</b>

An <b>RqlNode</b> that represents the RQL statement. The function alwasys returns an <b>RqlNode</b>. If the statement to be compiled is blank or whitespace, the function will return an <b>RqlNode</b> with an Operation of NOOP, and no parameters.

<b>Exceptions</b>

- <b>RqlFormatException</b> - An exception that details a Syntax error in the RQL Statement.

<h2>Change History</h2>
<table>
    <tr>
        <th>Date</th>
        <th>Description</th>
        <th>Version</th>
    </tr>
    <tr>
        <td>05/23/2022</td>
        <td>Fixed DateTime parsing bugs.</td>
        <td>0.0.8-alpha</td>
    </tr>  
    <tr>
        <td>05/26/2022</td>
        <td>Fixed Numeric parsing bugs.</td>
        <td>0.0.10-alpha</td>
    </tr>
    <tr>
        <td>05/28/2022</td>
        <td>Fixed syntax error reporting.</td>
        <td>0.0.11-alpha</td>
    </tr>
</table>