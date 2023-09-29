# OrientDB Gremlin and Rest Client 

基於 [Gremlin Query Client](../GremlinQueryClient)，增加 [OrientDB](http://orientdb.org) 專用的函式，例如：Orientdb GraphSON 內容序列化、Orientdb REST Client

* [OrientDB 筆記參考](../../docs/OrientDB) 或是 [官方文件](https://orientdb.org/docs/3.2.x/)



## Usage

### ■ Gremlin Query

```c#
// OrientDB Config
var config = new OrientGremlinConfig().SetHost("localhost", 8182)
                                      .SetAccount("root", "<Your Password>");

// Connection OrientDB
using (var client = new JanusGraphGremlinClient(config)) 
{
    // Get Graph Traversal Source
    GraphTraversalSource source = client.GetTraversalSource();

    // Get Vertex
    var result = source.V()
                       .Has("Name", TextP.Containing("john"))
                       .Next();
}
```



---

### ■ REST Query

> 可以搭配 [Gremlin Query Builder](../GremlinQueryBuilder)，產生 Gremlin Query Language

* **Post Gremlin Command**

```c#
// OrientDB REST Config, OrientDB Rest API default port is 2480.
var config = new OrientDBConfig().SetHost("localhost", 2480)
                                 .SetAccount("root", "<Your Password>")
                                 .SetDatabase("<Graph Database Name>");

// OrientDB REST Client
var client = new OrientDBRestClient(config);

// Gremlin Command : "g.V().hasLabel('department').toList()"
string command = GraphTraversal.Graph()
                               .Vertex()
                               .HasLabel("department")
                               .ToList()
                               .ToString();

// Http Request
var request = PostCommandRequest.Create(command);

// Post Http Request
PostCommandResponse reposnse = await client.PostGremlinCommandAsync(request);
```

* **Post SQL Command**

```c#
// OrientDB REST Config, OrientDB Rest API default port is 2480.
var config = new OrientDBConfig().SetHost("localhost", 2480)
                                 .SetAccount("root", "<Your Password>")
                                 .SetDatabase("<Graph Database Name>");

// OrientDB REST Client
var client = new OrientDBRestClient(config);

// SQL Command
string command = "SELECT @rid FROM V WHERE SEARCH_INDEX(\"V.name\", \" *john*\") = true";

// Http Request
var request = PostCommandRequest.Create(command);

// Post Http Request
var result = await _client.PostSqlCommandAsync(request);
```

* **Post Batch Command**
    * `PostBatchRequest.CreateBatchGremlin` **(Gremlin Command)**
    * `PostBatchRequest.CreateBatchSqlCommands` **(SQL Command)**
    * `PostBatchRequest.CreateBatchJavaScriptCommands` **(JavaScript)**
    * `PostBatchRequest.CreateBatchSqlScripts` **(SQL Script)**

```c#
// OrientDB REST Config, OrientDB Rest API default port is 2480.
var config = new OrientDBConfig().SetHost("localhost", 2480)
                                 .SetAccount("root", "<Your Password>")
                                 .SetDatabase("<Graph Database Name>");

// OrientDB REST Client
var client = new OrientDBRestClient(config);

// Batch Commands
List<string> commands = new List<string>()
{
    // Gremlin Command : "g.V().drop()"
    GraphTraversal.Graph().Vertex().Drop().ToString(),
    
    // Gremlin Command : "g.addV('test').property('id', '0001')"
    GraphTraversal.Graph().AddVertex("test").Property("id", "0001").ToString(),
    
    // Gremlin Command : "g.V()"
    GraphTraversal.Graph().Vertex().ToString()
};

// Http Request
var request = PostBatchRequest.CreateBatchGremlin(commands, transaction: true);

// Post Http Request
PostBatchResponse reposnse = await client.PostBatchAsync(request);
```



## Reference

* [OrientDB REST API Document](https://orientdb.org/docs/3.2.x/misc/OrientDB-REST.html)



