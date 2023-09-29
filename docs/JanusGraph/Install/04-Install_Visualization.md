# 安裝 JanusGraph 視覺化介面

* 目前候選的視覺化介面有以下幾個
    1. **[Graphexp](#1-graphexp)**
    2. **[Graph Explorer](#2-graph-explorer)** `(Invana Studio 前一個版本)`
    3. **[Invana Studio](#3-invana-studio)** `(Graph Explorer 後一個版本)`
    4. **[Gremlin Visualizer](#4-gremlin-visualizer)**



## 1. Graphexp

![](https://github.com/bricaud/graphexp/raw/master/images/graphexp2018.png)



<h3>安裝 & 執行</h3>

1. 從[Github](https://github.com/bricaud/graphexp)下載程式碼
2. 直接使用瀏覽器開啟目錄下的 `index.html` 檔案
3. 在 UI 下方設定 Server 的 IP 和 Port



---

## 2. Graph Explorer

> Invana Studio 前一個版本
>
> * **目前因為是 Beta 版本，因此是免費的，但未來變為正式版本時可能收費**

![](https://github.com/invanalabs/invana-studio/raw/pre-beta-1/docs/screenshots/1.png)



<h3>(1) 安裝 (使用 Docker)</h3>

```shell
# 安裝 Invana Engine，請修改 Server Url 和 Port
sudo docker run -p 8000:8000 -d -e GREMLIN_SERVER_URL=ws://localhost:8182/gremlin --name invana-engine invanalabs/invana-engine

# 安裝 Graph Explorer
sudo docker run -p 8888:8888 -d --name graph-explorer invanalabs/graph-explorer
```



<h3>(2) 移除 (使用 Docker)</h3>

```shell
# 移除 Invana Engine
sudo docker rm invana-engine

# 移除 Graph Explorer
sudo docker rm graph-explorer
```



## 3. Invana Studio

> Graph Explorer 後一個版本
>
> * **目前因為是 Beta 版本，因此是免費的，但未來變為正式版本時可能收費**

![](https://github.com/invanalabs/invana-studio/raw/v0.0.1/screenshot.png)



<h3>(1) 安裝</h3>

* **(1) 使用安裝包**
    * 請到[官方網站下載](https://invana.io/get-started.html)



* **(2) 使用 Docker**

```shell
# 安裝 Invana Engine，請修改 Server Url 和 Port
sudo docker run -p 8200:8200 -d -e GREMLIN_SERVER_URL=ws://ip-address:8182/gremlin --name invana-engine invanalabs/invana-engine

# 安裝 Invana Studio
sudo docker run -p 8888:8888 -d --name invana-studio invanalabs/invana-studio
```



<h3>(2) 移除 (使用 Docker)</h3>

```shell
# 移除 Invana Engine
sudo docker rm invana-engine invanalabs/invana-engine

# 移除 Graph Explorer
sudo docker rm invana-studio invanalabs/invana-studio
```



---

## 4. Gremlin Visualizer

![](https://raw.githubusercontent.com/prabushitha/Readme-Materials/master/Gremlin-Visualizer.png)



待補中...