# Gremlin Query Builder

提供 Gremlin Query Builder

## ■ Usage

### (1) Vertex

* **Gremlin Query**

```groovy
// Query 1
g.addV('person').property('name','stephen')

// Query 2: With Other Graph
tg.addV('person').property('name','stephen')

// Query 3: With Data
g.addV('person').property('id',1).property('name','stephen')

// Query 4: Get Vertex Values
g.V().has('person','name','stephen')
 .valueMap('ages','cname')

// Query 5: Gremlin Query Injection Protection
g.addV('person').property('name','jobs\'stephen')
```

* **Source  Code**

```c#
// Query 1 
string query1 = GraphTraversal.Graph()
                              .AddVertex("person")
                              .Property("name", "stephen")
                              .ToString();

// Query 2: Use Other Graph
string query2 = GraphTraversal.Graph("tg")
                              .AddVertex("person")
                              .Property("name", "stephen")
                              .ToString();

// Query 3: With Data
var data = new { id = 1, name = "stephen" };
string query3 = GraphTraversal.Graph()
                              .AddVertex("person", data)
                              .ToString();

// Query 4: Get Vertex Values
string query4 = GraphTraversal.Graph()
                              .Vertex().Has("person", "name", "stephen")
                              .ValueMap("ages", "cname")
                              .ToString();

// Query 5: Gremlin Query Injection Protection
string query5 = GraphTraversal.Graph()
                              .AddVertex("person")
                              .Property("name", "jobs'stephen")
                              .ToString();
```



### (2) Edge

### 

* **Gremlin Query**

```groovy
// Query 1
g.addE('create').property('weight',0.99)
 .from(V().has('person','name','jobs steve'))
 .to(V().has('product','name','iphone'))

// Query 2: Use Other Graph
tg.addE('create').property('weight',0.99)
  .from(V().has('person','name','jobs steve'))
  .to(V().has('product','name','iphone'))

// Query 3: With Data
g.addE('create').property('weight',0.99)
 .from(V().has('person','name','jobs steve'))
 .to(V().has('product','name','iphone'))
```

* **Source  Code**

```c#
// Query 1
string query1 = GraphTraversal.Graph()
                              .AddEdge("create").Property("weight", 0.99)
                              .FromVertex((v) => v.Has("person", "name", "jobs steve"))
                              .ToVertex((v) => v.Has("product", "name", "iphone"))
                              .ToString();

// Query 2: Use Other Graph
string query2 = GraphTraversal.Graph("tg")
                              .AddEdge("create").Property("weight", 0.99)
                              .FromVertex((v) => v.Has("person", "name", "jobs steve"))
                              .ToVertex((v) => v.Has("product", "name", "iphone"))
                              .ToString();

// Query 3: With Data
var data = new { weight = 0.99 };
string query3 = GraphTraversal.Graph()
                              .AddEdge("create", data)
                              .FromVertex("person", "name", "jobs steve")
                              .ToVertex("product", "name", "iphone")
                              .ToString();
```



