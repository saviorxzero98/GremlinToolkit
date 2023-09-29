# Gremlin Query Toolkits

## Projects

| Name                                                         | Description                                                  |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| [GremlinQueryBuilder](src/GremlinQueryBuilder)               | 提供 [Gremlin](https://tinkerpop.apache.org/docs/current/reference/#_tinkerpop_documentation) Query Builder |
| [GremlinQueryClient](src/GremlinQueryClient)                 | 提供 Generic Gremlin Client，同時也提供一些擴充函式          |
| [JanusGraphClient](src/JanusGraphClient)                     | 基於 [Gremlin Query Client](../GremlinQueryClient)，增加 [JanusGraph](https://docs.janusgraph.org/) 專用的函式 |
| [JanusGraphManagement](src/JanusGraphManagement)             | 透過定義 JSON 來批示建置 JanusGraph  所需的 Schema 和 Index，批次快速執行 JanusGraph 設定的建置 |
| [JanusgraphManagement.Console](src/JanusgraphManagement.Console) | [JanusGraphManagement](src/JanusGraphManagement) 之 Console Application |
| [OrientDBClient](src/OrientDBClient)                         | 基於 [Gremlin Query Client](../GremlinQueryClient)，增加 [OrientDB](http://orientdb.org) 專用的函式 |



## Documents

* [Gremlin Query Language 筆記整理](docs/GremlinQueryLanguage)



