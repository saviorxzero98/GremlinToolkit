# Gremlin Query Client

提供 Gremlin Query Client，同時也提供一些擴充函式

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



