# 管理 Vertex Schema

## 1. Vertex 介紹

* [官方文件](https://docs.janusgraph.org/schema/#defining-vertex-labels)

---

## 2. 列出所有的 Vertex Schema

* 執行 Gremlin

```groovy
// 開啟 JanusGraph Management
mgmt = graph.openManagement();

// 列出所有的 Vertex Label
mgmt.printVertexLabels();
```

* 顯示的結果

```
---------------------------------------------------------------------------------------------------
Vertex Label Name              | Partitioned | Static                                             |
---------------------------------------------------------------------------------------------------
employee                       | false       | false                                              |
department                     | false       | false                                              |
---------------------------------------------------------------------------------------------------
```

---

## 3. 建立 Vertex Schema

**注意事項**

* **Vertex 一旦建立後，無法修改或是刪除**
    * [只能修改 Vertex Label Name](#4-修改-vertex-label-name)
    * **`無法修改 Vertex Partitioned 和 Static 屬性`**



```groovy
// 開啟 JanusGraph Management
mgmt = graph.openManagement();

// 建立 Vertex Label
mgmt.makeVertexLabel('<Vertex Label>').make();

// 提交
mgmt.commit();
```



---

## 4. 修改 Vertex Label Name



```groovy
// 開啟 JanusGraph Management
mgmt = graph.openManagement();

// 重新命名 Vertex Label
mgmt.changeName(mgmt.getVertexLabel('<Old Name>'), 'New Name');

// 提交
mgmt.commit();
```



---

## 5. 取得所有的 Vertex Label

* 執行 Gremlin

```groovy
// 開啟 JanusGraph Management
mgmt = graph.openManagement();

// 列出所有的 Vertex Label
mgmt.getVertexLabels();
```

* 顯示的結果

```
==>employee
==>department
```





