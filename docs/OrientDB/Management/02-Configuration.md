# OrientDB Server 參數設定

> 修改 config 目錄下的 `orientdb-server-config.xml` 檔案

## (1) 允許 REST API 使用 Gremlin 語法

* **設定 `orientdb-server-config.xml`**
    * **加入一個 Parameter `allowedLanguages` ，Value 為 `Gremlin`**

```xml
<orient-server>
    <handler class="com.orientechnologies.orient.server.handler.OServerSideScriptInterpreter">
        <parameters>
                <parameter name="enabled" value="true"/>
                <parameter name="allowedLanguages" value="SQL"/>
                <parameter name="allowedLanguages" value="Gremlin"/>
            </parameters>
    </handler>
</orient-server>
```



# Gremlin Server 參數設定

> 修改 config 目錄下的 `gremlin-server.yaml` 檔案

* **[詳細設定參數 (官方文件)](https://orientdb.org/docs/3.2.x/admin/Configuration.html)**

## (1) 設定 Host & Port

* **設定 `gremlin-server.yaml`**
    * **host** ─ 設定 Gremlin Host 
    * **port** ─ 設定 Port

```yaml
host: localhost
port: 8182
```



## (2) 新增 Graph

* **Step.1 建立 Graph Properties 檔**
    * 範例：`myGraph.properties`
    * 主要修改 `orient-db-name`，對應 `databases` 下面的資料庫
    * **`orient-user` 和 `orient-pass` 用途目前還不清楚，Gremlin 的帳密還需要進一步釐清**

```properties
gremlin.graph=org.apache.tinkerpop.gremlin.orientdb.OrientEmbeddedFactory
orient-db-name=graphdb
orient-user=admin
orient-pass=admin
```

* **Step.2 建立 Graph Groovy 檔**
    * 範例：`myGraph.groovy`
    * 只要調整這一行
        * 調整 Traversal Source 的名稱 `g`，Gremlin 查詢時使用的
        * 調整 Graph Instance `graph`，對應 Step.3 `gremlin-server.yaml` 的 Graph Name 設定

```groovy
// define the default TraversalSource to bind queries to - this one will be named "g".
globals << [g : graph.traversal()]
```

* **Step.3 設定 `gremlin-server.yaml`**
    * **graphs** ─ 設定 Graph，可以同時設定多個 Graph
        * **Key** ：為 Graph Name，需要調整 Step.2 Graph Name 設定
        * **Value** ： 指向 Step.1 建立的 Properties 檔案
    * **scriptEngines.gremlin-groovy.plugins**
        * **org.apache.tinkerpop.gremlin.jsr223.ScriptFileGremlinPlugin**
            * **file** ： 加入  Step.2 建立的 Groovy 檔案

```yaml
graphs: {
  graph : ../config/myGraph.properties,
  demoGraph: ../config/demodb.properties
}
scriptEngines: {
  gremlin-groovy: {
    plugins: { 
      org.apache.tinkerpop.gremlin.server.jsr223.GremlinServerGremlinPlugin: {},
      org.apache.tinkerpop.gremlin.orientdb.jsr223.OrientDBGremlinPlugin: {},
      org.apache.tinkerpop.gremlin.jsr223.ImportGremlinPlugin: {
        classImports: [java.lang.Math], 
        methodImports: [java.lang.Math#*]
      },
      org.apache.tinkerpop.gremlin.jsr223.ScriptFileGremlinPlugin: {
        files: [
          ../config/myGraph.groovy,
          ../config/demodb.groovy
        ]
      }
    }
  }
}
```

* **Step.4 重新啟動 Server**



## (3) 設定 Gremlin Server 帳密

* **設定 `gremlin-server.yaml`**
    * **authentication** ─ 帳號密碼驗證
        * 預設使用 OGremlinServerAuthenticator
        * 如果要停止帳號密碼的驗證就將這幾行刪除
        * **[Gremlin Server Security 設定](https://tinkerpop.apache.org/docs/3.4.12/reference/#security)**

```yaml
authentication: {
  authenticator: com.orientechnologies.tinkerpop.server.auth.OGremlinServerAuthenticator
}
```





