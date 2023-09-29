# Gremlin 相關



## ● Gremlin Console

### ■ [其他] 安裝 Plugin

* **安裝 [OrientDB Gremlin Plugin](https://github.com/orientechnologies/orientdb-gremlin)**

```
:install com.orientechnologies orientdb-gremlin

```

* **使用 OrientDB Gremlin Plugin**

```
:plugin use tinkerpop.orientdb
```



### ■ [雜項] OrientGraph (Groovy Script)

* **OrientGraph 可用函式**

    * **開啟 OrientGraph，為 static 函式**

        * `OrientGraph.open(String url, String user, String password)`

    * **取得 Vertex / Edge**

        * `vertices(Object... vertexIds)`

        * `edges(Object... edgeIds)`

    * **新增 Vertex**

        * `addVertex(Object... keyValues)`

    * **取得 Index**

        * `getIndexedVertices(OIndex index, Iterator<Object> valueIter)`
        * `getVertexIndexedKeys(final String label)`
        * `getIndexedEdges(OIndex index, Iterator<Object> valueIter)`
        * `getEdgeIndexedKeys(final String label)`
        * `getIndexedKeys(String className)`
        * `getIndexedKeys(final Class<? extends Element> elementClass, String label)`
        * ` getIndexedKeys(final Class<? extends Element> elementClass)`
        * `lookupInIndex(OIndex index, Object value)`
        * `getIndexManager()`

    * **建立 Index**

        * `createVertexIndex(final String key, final String label, final Configuration configuration)`
        * `createEdgeIndex(final String key, final String label, final Configuration configuration)`
        * `createIndex(final String key, String className, final Configuration configuration)`

    * **執行 SQL 或 Gremlin**

        * `executeSql(String sql, Object... params)`
        * `querySql(String sql, Object... params)`
        * `execute(String language, String script, Map params)`

    * **取得 Schema**

        * `getSchema()`



---

## ● Gremlin.NET

###  ■ [問題排解] Deserializer for "orient:ORecordId" not found

* **問題原因**
    * **Vertex Id & Edge Id** 的 `@type` 的值是 `orient:ORecordId`

* **建立 Orientdb Serializer**

```c#
public class OrientdbSerializer
{
    public const string ORecordIdKey = "orient:ORecordId";

    public static IMessageSerializer GetSerializer()
    {
        var dictionary = new Dictionary<string, IGraphSONDeserializer>
        {
            { ORecordIdKey, new ORidDeserializer() },
        };
        var reader = new GraphSON3Reader(dictionary);
        var messageSerializer = new GraphSON3MessageSerializer(graphSONReader: reader);
        return messageSerializer;
    }

    public class ORidDeserializer : IGraphSONDeserializer
    {
        public dynamic Objectify(JsonElement graphsonObject, GraphSONReader reader)
        {
            return graphsonObject.ToString();
        }
    }
}
```

* **在建立 Gremlin Clinet 時，加入 Orientdb Serializer**

```c#
// 新增 Orientdb Serializer
IMessageSerializer messageSerializer = OrientdbSerializer.GetSerializer();

// 建立 Gremlin Server
var server = new GremlinServer("localhost", 8182);

// 建立 Gremlin Client
var client = new GremlinClient(server, messageSerializer);
```



