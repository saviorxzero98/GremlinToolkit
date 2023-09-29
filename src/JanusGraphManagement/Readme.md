# JanusGraph Management

* 透過定義 JSON 來批示建置 JanusGraph  所需的 Schema 和 Index，批次快速執行 JanusGraph 設定的建置
    * Vertex Schema 建立
    * Edge Schema 建立
    * Property Schema 建立
    * Index 建立、啟用
    * Index 停用 **(coming soon...)**
* 實作方面是透過 Submit JanusGraph Management Gremlin Script 來管理 JanusGraph Schema 和 Index
    * 使用  [JanusGraphClient](../JanusGraphClient)
* [JanusGraph Management 筆記](../../docs/JanusGraph/Management) 或是 [官方文件](https://docs.janusgraph.org)



## Schema Example

> 為一個 JSON 格式

* **Name** ：Schema Name
* **Connection** ：JanusGraph 連線設定
* **Properties** ：建立的 Property
* **Vertices **：建立的 Vertex
* **Edges** ：建立的 Edge
* **CompositeIndexes** ：建立的 Composite Index
* **MixedIndexes** ：建立的 Mixed Index

```json
{
    "Name": "My JanusGraph",
    "Connection": {
        "Type": "janusgraph",
        "Host": "localhost",
        "Port": 8182,
        "EnableSsl": false,
        "UserName": "",
        "Password": "",
        "GraphName": "graph",
        "IsEncryptedPassword": false
    },
    "Properties": [
        {
            "Key": "tags",
            "DataType": "string",
            "Cardinality": "SET"
        },
        {
            "Key": "name",
            "DataType": "string"
        }
    ],

    "Vertices": [
        {
            "Label": "user"
        },
        {
            "Label": "book"
        }
    ],

    "Edges": [
        {
            "Label": "owned"
        }
    ],

    "CompositeIndexes": [
        {
            "Name": "index_name",
            "PropertyKey": "name"
        }
    ],

    "MixedIndexes": [
        {
            "Name": "index_tags",
            "PropertyKey": "tags",
            "MappingType": "TEXTSTRING"
        }
    ]
}
```



