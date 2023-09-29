# OrientDB 安裝

## 1. 透過 Docker

<h3>(1) 透過 Docker 安裝 & 執行</h3>

> 預設 Storage 為 InMemory

```shell
docker run -d --name orientdb -p 2424:2424 -p 2480:2480 -e ORIENTDB_ROOT_PASSWORD=root orientdb:latest
```





---

## 2. 下載安裝包

<h3>(1) 下載/安裝 OpenJDK 11</h3>

<h4>● Windows Server</h4>

* 下載 OpenJDK (下面任選一連結下載)
    * [Microsoft Build of OpenJDK](https://docs.microsoft.com/zh-tw/java/openjdk/download)
    * [RedHat OpenJDK](https://developers.redhat.com/products/openjdk/download)
    * [Adopt OpenJDK](https://adoptopenjdk.net/)
    * [IBM OpenJDK](https://www.ibm.com/support/pages/java-sdk-downloads)
    * [ojdkbuild OpenJDK](https://github.com/ojdkbuild/ojdkbuild)
    * [BellSoft Liberica OpenJDK](https://bell-sw.com/pages/downloads/#/java-11-lts)
    * [Azul Zulu OpenJDK](https://www.azul.com/downloads/?package=jdk)



<h4> ● Ubnutu</h4>

```shell
# 更新套件清單
sudo apt-get update

# 安裝 OpenJDK 11
sudo apt-get install openjdk-11-jdk

# 確認 Java 版本
java -version
```



<h3>(2) 下載/解壓縮 OrientDB</h3>

* [OrientDB with Gremlin Server](https://orientdb.org/download)



<h3>(3) 調整設定檔</h3>

> **設定檔放在 config 目錄下**

<h4>● Gremlin Server 設定</h4>

<h5>A. 新增一個新的 Graph</h5>

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
    * **host** ─ 設定 Gremlin Host 
    * **port** ─ 設定 Port
    * **graphs** ─ 設定 Graph，可以同時設定多個 Graph
        * **Key** ：為 Graph Name，需要調整 Step.2 Graph Name 設定
        * **Value** ： 指向 Step.1 建立的 Properties 檔案
    * **scriptEngines.gremlin-groovy.plugins**
        * **org.apache.tinkerpop.gremlin.jsr223.ScriptFileGremlinPlugin**
            * **file** ： 加入  Step.2 建立的 Groovy 檔案
    * **authentication** ─ 帳號密碼驗證
        * 預設使用 OGremlinServerAuthenticator

```yaml
host: localhost
port: 8182

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
authentication: {
  authenticator: com.orientechnologies.tinkerpop.server.auth.OGremlinServerAuthenticator
}
```



<h4>● OrientDB Server 設定</h4>

* 修改 `orientdb-server-config.xml`
    * **[詳細設定參數 (官方文件)](https://orientdb.org/docs/3.2.x/admin/Configuration.html)**
    * **[常見的設定](02-Configuration.md)**



<h3>(4) 啟動 Server</h3>

* **Windows Server**
    * 目錄需要切換到 OrientDB 的 bin 目錄，執行 `server.bat` 檔

* **Linux Server**
    * 目錄需要切換到 OrientDB 的 bin 目錄，執行 `server.sh` 檔



<h3>(5) 防火牆相關</h3>

* OrientDB 使用的 Port：
    * **OrientDB Http API** ：`2480`
    * **OrientDB Binary API** ：`2424`
    * **Gremlin API** ：`8182` (預設)



<h3>(6) 檢查是否啟動成功</h3>

* 使用瀏覽器開啟 `http://{Host Url}:2480` 便會進入到管理介面 (Studio)

