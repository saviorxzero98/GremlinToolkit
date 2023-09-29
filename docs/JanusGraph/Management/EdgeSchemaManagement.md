# 管理 Edge Schema

## 1. Edge 介紹

* [官方文件](https://docs.janusgraph.org/schema/#defining-edge-labels)

### (1) Edge Label Multiplicity

* [官方文件](https://docs.janusgraph.org/schema/#edge-label-multiplicity)
* Edge 分成以下幾類
    * **MULTI**
        * 預設值
    * **SIMPLE**
    * **MANY2ONE**
    * **ONE2MANY**
    * **ONE2ONE**



---

## 2. 列出所有的 Edge Schema

* 執行 Gremlin

```groovy
// 開啟 JanusGraph Management
mgmt = graph.openManagement();

// 列出所有的 Vertex Label
mgmt.printEdgeLabels();
```

* 顯示的結果

```
---------------------------------------------------------------------------------------------------
Edge Label Name                | Directed    | Unidirected | Multiplicity                         |
---------------------------------------------------------------------------------------------------
has                            | true        | false       | MULTI                                |
owned                          | true        | false       | MULTI                                |
---------------------------------------------------------------------------------------------------
```

---

## 3. 建立 Edge Schema

**注意事項**

* **Edge 一旦建立後，無法修改或是刪除**
    * [只能修改 EdgeLabel Name](#4-修改-edge-label-name)
    * **`無法修改 Edge Multiplicity 屬性`**

* **建立 Edge Label**

```groovy
// 開啟 JanusGraph Management
mgmt = graph.openManagement();

// 建立 Edge Label
mgmt.makeEdgeLabel('<Edge Label>').make();

// 提交
mgmt.commit();
```

* **建立 Edge Label (指定 Multiplicity)**

```groovy
// 開啟 JanusGraph Management
mgmt = graph.openManagement();

// 建立 Edge Label
mgmt.makeEdgeLabel('<Edge Label>').multiplicity(MULTI).make();

// 提交
mgmt.commit();
```



---

## 4. 修改 Edge Label Name



```groovy
// 開啟 JanusGraph Management
mgmt = graph.openManagement();

// 重新命名 Vertex Label
mgmt.changeName(mgmt.getEdgeLabel('<Old Name>'), 'New Name');

// 提交
mgmt.commit();
```




