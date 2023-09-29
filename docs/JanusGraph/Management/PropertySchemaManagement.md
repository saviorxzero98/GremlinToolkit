# 管理 Property Schema

## 1. Property Schema 介紹

* [官方文件](https://docs.janusgraph.org/schema/#defining-property-keys)

### (1) 支援的 Property Data Type

* [官方文件](https://docs.janusgraph.org/schema/#property-key-data-type)
* **Property Data Type**
    * String
    * Character
    * Boolean
    * Byte
    * Short
    * Integer
    * Long
    * Float
    * Double
    * Date
    * Geoshape
    * UUID
    * Object



---

### (2) Property Cardinality

* [官方文件](https://docs.janusgraph.org/schema/#property-key-cardinality)
*  Property Cardinality
    * **SINGLE**
        * Property Value 為一個值
    * **LIST**
        * Property Value 為一個 List **(允許有重複的值)**
    * **SET**
        * Property Value 為一個 HashSet **(不允許有重複的值)**



---

## 2. 列出所有的 Property Schema

* 執行 Gremlin

```groovy
// 開啟 JanusGraph Management
mgmt = graph.openManagement();

// 列出所有的 Property
mgmt.printPropertyKeys();
```

* 顯示的結果

```
---------------------------------------------------------------------------------------------------
Property Key Name              | Cardinality | Data Type                                          |
---------------------------------------------------------------------------------------------------
name                           | SINGLE      | class java.lang.String                             |
price                          | SINGLE      | class java.lang.Integer                            |
flags                          | SET         | class java.lang.String                             |
---------------------------------------------------------------------------------------------------
```



---

## 3. 建立 Property Schema

* **注意事項**
    * **Property 會被所有 Vertex 與 Edge 共用**
    * **Property 一旦建立後，無法修改或是刪除**
        * [只能修改 Property Key Name](#4-修改-property-key-name)
        * **`無法修改 Property Data Type`**
        * **`無法修改 Property Cardinality`**

* 建立 Property
    * Property Key Name
    * Property Data Type
    * Property Cardinality
        * **前面需加上 `org.janusgraph.core`**

```groovy
// 開啟 JanusGraph Management
mgmt = graph.openManagement();

// 建立 Property
mgmt.makePropertyKey('<Propery Key>').dataType(String.class).cardinality(org.janusgraph.core.Cardinality.SINGLE).make();

// 提交
mgmt.commit();
```



---

## 4. 修改 Property Key Name



```groovy
// 開啟 JanusGraph Management
mgmt = graph.openManagement();

// 重新命名 Property Key
mgmt.changeName(mgmt.getPropertyKey('<Old Name>'), 'New Name');

// 提交
mgmt.commit();
```











