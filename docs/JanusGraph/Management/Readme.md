# JanusGraph Management

JanusGraph Management 提供一個管理 JanusGraph Index 和 Schema 的工具



## 1. 如何使用JanusGraph Management

### (1) 使用 Gremlin Console 

#### Step.1 下載/執行 Gremlin Console

* **Gremlin Console 使用步驟**
    1. 下載 **[Gremlin Console](https://tinkerpop.apache.org/downloads.html)**
    2. 解壓縮
    3. 調整設定檔 Remote.yaml，修改 `conf/remote.yaml`
        * 設定 Host 和 Port
    4. 執行 Gremlin Console，透過 BAT檔 或是 Shell檔啟用


* **Windows**

```shell
./gremlin.bat
```

* **Linux**

```shell
./gremlin.sh
```



---

#### Step.2 連接 JanusGraph Server

* 開啟  Gremlin Console 就能執行一些 Command
* 連線到 JanusGraph，使用前一個步驟設定的設定檔

```shell
:remote connect tinkerpop.server conf/remote.yaml session
```



---

##  2. 管理 Schema

* 新增資料時 JanusGraph  預設會依據資料自動建立對應的 Schema
    * 也可以手動建立對應的 Schema，手動的好處是避免建錯資料型態
    * JanusGraph  Schema 一旦建立後，就無法刪除，只能重新命名或是 Graph 整個砍掉
* JanusGraph Schema 主要分成 Property、Vertex 和 Edge
* Schema 管理
    * [Property](PropertySchemaManagement.md)
    * [Vertex](VertexSchemaManagement.md)
    * [Edge](EdgeSchemaManagement.md)



---

## 3. 管理 Index

* 提供一個圖形的索引 (Index)
    * 索引 (Index) 通常是建立在 Property 上
    * 由於 JanusGraph 不會自動建立索引 (Index)，因此需要手動建立
    * 查詢圖形資料時，有建立索引 (Index) 和沒有建立索引 (Index) 的查詢時間差異會非常大
* [Index 管理](IndexSchemaManagement.md)
* JanusGraph 建立索引 (Index) 的步驟非常繁瑣，因此這裡有提供了一個[自動建立的工具](../../src/JanusGraphManagement)



---

## 4. 其他雜項

- [其他雜項](MiscManagement.md)



---

## Reference

* [JanusGraph 官方文件](https://docs.janusgraph.org) 
    * [Indexing for Better Performance](https://docs.janusgraph.org/index-management/index-performance/)
    * [Index Lifecycle](https://docs.janusgraph.org/index-management/index-lifecycle/)
    * [Reindexing](https://docs.janusgraph.org/index-management/index-reindexing/)
    * [Removal](https://docs.janusgraph.org/index-management/index-removal/)
* [JanusGraph Management Java API 文件](https://www.javadoc.io/doc/org.janusgraph/janusgraph-core/latest/org/janusgraph/core/schema/JanusGraphManagement.html)
* [Getting Started with JanusGraph ─ Part.2 Indexes and Traversals](https://medium.com/@chris.hupman/getting-started-with-janusgraph-552f43beb9c0)
* [JanusGraph 建立索引步驟（composite index）踩坑總結](https://www.cnblogs.com/Uglthinx/p/9630779.html)



