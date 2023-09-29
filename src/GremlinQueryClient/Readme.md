# Gremlin Query Client

提供 Generic Gremlin Client，同時也提供一些擴充函式

* [Gremlin Query Language 語法可以參考這裡](../../docs/GremlinQueryLanguage) 或是 [官方文件](https://tinkerpop.apache.org/docs/current/reference/#_tinkerpop_documentation)

## Usage

```c#
// Gremlin Config
var config = new GenericGremlinConfig() 
{
    Host = "localhost",
    Port = 8182
};

// Connection Gremlin
using (var client = new GenericGremlinClient(config)) 
{
    // Get Graph Traversal Source
    GraphTraversalSource source = client.GetTraversalSource();

    // Get Vertex
    var result = source.V()
                       .Has("Name", "john")
                       .Next();
}
```



