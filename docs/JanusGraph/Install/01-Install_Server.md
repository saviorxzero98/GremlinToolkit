# 安裝 JanusGraph Server

## 1. Docker

<h4>(1) 透過 Docker 安裝 & 執行</h4>

> 預設 Storage 為 InMemory

```shell
sudo docker run -it -p 8182:8182 janusgraph/janusgraph
```



<h4>(2) 設定相關參數</h4>

* **Step.1** 安裝 Apache Cassandra

```shell
sudo docker run --name cassandra-3.11.11 -p 7000:7000 -p 7001:7001 -p 7199:7199 -p 9042:9042 -p 9160:9160 -d cassandra:3.11.11
```

* **Step.2** 安裝 Elasticsearch

```shell
sudo docker run --name es-7.14.1 -p 9200:9200 -p 9300:9300 -d elasticsearch:7.14.1
```

* **Step.3** 安裝相關套件

```shell
# 更新套件清單
sudo apt-get update

# 安裝 Unzip
sudo apt-get install unzip

# 安裝 OpenJDK 8
sudo apt-get install openjdk-8-jdk

# 確認 Java 版本
java -version
```

* **Step.4** 下載 [JanusGraph 0.6.0](https://github.com/JanusGraph/janusgraph/releases)，並解壓縮

```shell
unzip janusgraph-0.6.0.zip
```

* **Step.5** 進入 JanusGraph 目錄，並執行 JanusGraph

```shell
./bin/janusgraph-server.sh console ./conf/gremlin-server/gremlin-server-cql-es.yaml
```



---

## 2. From Source Code

<h4>(1) 程式碼</h4>

* **[Github](https://github.com/janusgraph/janusgraph/)**



<h4>(2) 安裝 OpenJDK 8 (Ubuntu)</h4>

```shell
# 更新套件清單
sudo apt update

# 安裝 OpenJDK 8
sudo apt install -y openjdk-8-jdk
sudo apt install openjdk-8-jre

# 確認 Java 版本
java -version
```



<h4>(3) 修改設定檔</h4>

待補中...



<h4>(4) 啟動 Server</h4>

```shell
# 目錄需要切換到 JanusGraph Server 目錄
sudo bin/gremlin-server.sh start
```

