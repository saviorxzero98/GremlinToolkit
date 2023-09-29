# Gremlin Query Language

* **[Gremlin Playground](https://gremlify.com/)**



---

## ■ Traversal Steps

* [官方文件](https://tinkerpop.apache.org/docs/current/reference/#graph-traversal-steps)

![](https://tinkerpop.apache.org/docs/3.3.2/images/vertex-steps.png)



| Class      | Step     | Arguments     | Description                                                  |
| ---------- | -------- | ------------- | ------------------------------------------------------------ |
| **Graph**  | `V`      | 可指定 Vertex | 取得所有 Vertex                                              |
| **Graph**  | `E`      | 可指定 Edge   | 取得所有 Edge                                                |
| **Vertex** | `outE`   | 可指定 Edge   | 取得目前 Vertex 所有出去的 Edge                              |
| **Vertex** | `out`    | 可指定 Edge   | 取得目前 Vertex 所有出去的 Vertex                            |
| **Vertex** | `inE`    | 可指定 Edge   | 取得目前 Vertex 所有進來的 Edge                              |
| **Vertex** | `in`     | 可指定 Edge   | 取得目前 Vertex 所有進來的 Vertex                            |
| **Vertex** | `both`   | 可指定 Edge   | 取得目前 Vertex 所有進出的 Edge                              |
| **Edge**   | `outV`   | *N/A*         | 取得目前 Edge 的 Source Vertex                               |
| **Edge**   | `inV`    | *N/A*         | 取得目前 Edge 的 Target Vertex                               |
| **Edge**   | `bothV`  | *N/A*         | 取得目前 Edge 的 Source Vertex 和 Target Vertex              |
| **Edge**   | `otherV` | *N/A*         | Move to the vertex that was not the vertex that was moved from. |

* **Example**

```groovy
g.V()
g.E()
g.V().outE()
g.V().out()
g.V().inE()
g.V().in()
g.V().both()
g.E().outV()
g.E().inV()
g.E().otherV()
```



---

## ■ Search



| Class                                        | Step       | Arguments                                                    | Description                              |
| -------------------------------------------- | ---------- | ------------------------------------------------------------ | ---------------------------------------- |
| **Vertex**,<br />**Edge**                    | `has`      | ● Property Key<br />● Property Key + Value<br />● Property Key + Traversal<br />● Label + Property Key + Value<br />● Property Key + Predicate | 尋找符合指定條件的 Vertex、Edge          |
| **Vertex**,<br />**Edge**                    | `hasNot`   | Property Key                                                 | 尋找不符合指定條件的 Vertex、Edge        |
| **Vertex**,<br />**Edge**,<br />**Property** | `hasLabel` | Label                                                        | 尋找符合 Label 的 Vertex、Edge、Property |
| **Vertex**,<br />**Edge**,<br />**Property** | `hasId`    | id                                                           | 尋找符合 id 的 Vertex、Edge、Property    |
| **Property**                                 | `hasKey`   | ● Label<br />● Property Key                                  | 尋找符合 key 的 Vertex、Edge、Property   |
| **Property**                                 | `hasValue` | Value                                                        | 尋找符合 value 的 Vertex、Edge、Property |

* **Example**

```groovy
g.V().has('person', 'name', 'marty')
g.V().has('name')
g.V().hasNot('name')
g.V().hasLabel('person')
g.V().hasId(1)
g.V().properties().hasKey('name')
g.V().properties('name').hasValue('marty')
```



---

## ■ Read Data



| Class                                        | Step          | Arguments       | Description                                                  |
| -------------------------------------------- | ------------- | --------------- | ------------------------------------------------------------ |
| **Vertex**,<br />**Edge**                    | `properties`  | 可指定 Property | 取得 Vertex 或 Edge 的所有 Property                          |
| **Vertex**,<br />**Edge**                    | `propertyMap` | 可指定 Property | 取得 Vertex 或 Edge 的所有 Property **(Mapping)**            |
| **Vertex**,<br />**Edge**                    | `values`      | 可指定 Property | 取得 Vertex 或 Edge 的所有 Property 和 Property Value        |
| **Vertex**,<br />**Edge**                    | `valueMap`    | 可指定 Property | 取得 Vertex 或 Edge 的所有 Property 和 Property Value **(Mapping)** |
| **Vertex**,<br />**Edge**                    | `elementMap`  | 可指定 Property | 取得 Vertex 或 Edge 的所有 Property 和 Property Value **(Mapping)** |
| **Vertex**<br />**Edge**<br />**Property**   | `label`       | *N/A*           | 取得 Vertex、Edge 或 Property 的 Label                       |
| **Vertex**,<br />**Edge**,<br />**Property** | `id`          | *N/A*           | 取得 Vertex、Edge 或 Property 的 Id                          |
| **Property**                                 | `key`         | *N/A*           | 取得所有 Property 的 Key<br />**需搭配 `properties`**        |
| **Property**                                 | `value`       | *N/A*           | 取得所有 Property 的 Value<br />**需搭配 `properties`**      |
| **Vertex**,<br />**Edge**,<br />**Property** | `count`       | *N/A*           | 取得 Vertex、Edge 或 Property 的數量                         |

* **Example**

```groovy
g.V().properties()
g.V().propertyMap()
g.V().has('person', 'name', 'marty').values()
g.V().has('person', 'name', 'marty').valueMap()
g.V().has('person', 'name', 'marty').elementMap()
g.V().label()
g.V().id()
g.V().has('person', 'name', 'marty').properties().key()
g.V().has('person', 'name', 'marty').properties().value()
g.V().has('person', 'name', 'marty').values().count()
```



---

## ■ Insert / Update / Delete Data



| Class                                        | Step       | Arguments                       | Description                                                  |
| -------------------------------------------- | ---------- | ------------------------------- | ------------------------------------------------------------ |
| **Graph**,<br />**Edge**                     | `addV`     | ● Vertex Label<br />● Traversal | 新增 Vertex                                                  |
| **Vertex**,<br />**Edge**                    | `property` | Property Key + Value            | 新增/修改 Property 和 Property Value                         |
| **Vertex**                                   | `addE`     | ● Edge Label<br />● Traversal   | 新增 Edge<br />需搭配 `from` 和 `to` 來指定兩邊的連結的 Vertex |
|                                              | `from`     | Traversal                       | 來源 Vertex、Edge                                            |
|                                              | `to`       | Traversal                       | 目的 Vertex、Edge                                            |
| **Vertex**,<br />**Edge**,<br />**Property** | `drop`     | *N/A*                           | 刪除指定的 Vertex、Edge 或 Property<br />**注意：沒有明確指定對象，會刪除所有符合的項目** |

* **Example**

```groovy
g.addV('person')
g.addV('person').property('name','marty')

g.addE('create').property('weight', 0.99)
 .from(V().has('person', 'name', 'jobs steve'))
 .to(V().has('product', 'name', 'iphone'))

g.V().drop()

g.V().properties('name').drop()
```



---

## ■ Linq



| Class                     | Step                        | Arguments                  | Description                                    |
| ------------------------- | --------------------------- | -------------------------- | ---------------------------------------------- |
| **Vertex**,<br />**Edge** | `limit`                     | ● long                     | 取得指定資料筆數的資料 (從前面)                |
| **Vertex**,<br />**Edge** | `tail`                      | ● long                     | 取得指定資料筆數的資料 (從後面)                |
| **Vertex**,<br />**Edge** | `range`                     | ● long + long<br />● Scope | 指定取得資料的 Index 區間                      |
| **Vertex**,<br />**Edge** | `skip`                      | ● long                     | 忽略前面指定筆數的資料                         |
| **Vertex**,<br />**Edge** | `sample`                    | ● long                     | 隨機取出指定筆數的資料                         |
| **Vertex**,<br />**Edge** | `dedup`                     | *N/A*                      | 移除重複的資料                                 |
| **Vertex**,<br />**Edge** | `order`<br />`order` + `by` | *N/A*                      | 排序<br />可透過 `order` + `by` 指定排序的條件 |
|                           | `where`                     |                            | *進階用法，待補中...*                          |
|                           | `match`                     |                            | *進階用法，待補中...*                          |
|                           | `select`                    |                            | *進階用法，待補中...*                          |
|                           | `filter`                    |                            | *進階用法，待補中...*                          |

* **Example**

```groovy
g.V().limit(3)
g.V().tail(3)
g.V().range(2, 5)
g.V().skip(3)
g.V().sample(3)
g.V().dedup()
g.V().order();
g.V().order().by('name', desc)

```



---

## ■ Logic



| Class                                        | Step  | Arguments                 | Description                                  |
| -------------------------------------------- | ----- | ------------------------- | -------------------------------------------- |
| **Value**                                    | `is`  | ● object<br />● Predicate | 判斷是否符合條件<br />不符合條件則回覆空集合 |
| **Vertex**,<br />**Edge**,<br />**Property** | `not` | Traversal                 |                                              |
| **Vertex**,<br />**Edge**,<br />**Property** | `and` | Traversal                 |                                              |
| **Vertex**,<br />**Edge**,<br />**Property** | `or`  | Traversal                 |                                              |

* **Example**

```groovy

```





---

## ■ Math



| Class | Step   | Arguments | Description             |
| ----- | ------ | --------- | ----------------------- |
|       | `math` |           | *暫時用不到，待補中...* |
|       | `max`  |           | *暫時用不到，待補中...* |
|       | `min`  |           | *暫時用不到，待補中...* |
|       | `mean` |           | *暫時用不到，待補中...* |
|       | `sum`  |           | *暫時用不到，待補中...* |
|       |        |           |                         |

* **Example**

```groovy

```



---

## ■ Terminal Steps



| Class                     | Step        | Arguments | Description                                          |
| ------------------------- | ----------- | --------- | ---------------------------------------------------- |
| **Vertex**,<br />**Edge** | `hasNext()` | *N/A*     | 是否還有下一個<br />在 `gremlin-javascript` 上不支援 |
| **Vertex**,<br />**Edge** | `next()`    | *N/A*     | 返回一個結果                                         |
| **Vertex**,<br />**Edge** | `toList()`  | *N/A*     | 返回所有結果                                         |
| **Vertex**,<br />**Edge** | `iterate()` | *N/A*     | 不返回結果                                           |

* **Example**

```groovy
g.V().hasNext()
g.V().next()
g.V().list()
g.addV('person').iterate()
```



---

## ■ Predicate

* **Gremlin**

| Predicate                       | Description |
| ------------------------------- | ----------- |
| `P.eq(object)`                  | 等於        |
| `P.neq(object)`                 | 不等於      |
| `P.lt(number)`                  | 小於        |
| `P.lte(number)`                 | 小於或等於  |
| `P.gt(number)`                  | 大於        |
| `P.gte(number)`                 | 大於或等於  |
| `P.inside(number, number)`      |             |
| `P.outside(number, number)`     |             |
| `P.between(number, number)`     |             |
| `P.within(object...)`           |             |
| `P.without(object...)`          |             |
| `TextP.startingWith(string)`    |             |
| `TextP.endingWith(string)`      |             |
| `TextP.containing(string)`      |             |
| `TextP.notStartingWith(string)` |             |
| `TextP.notEndingWith(string)`   |             |
| `TextP.notContaining(string)`   |             |

* **JanusGraph 擴充**

| Predicate                          | Description |
| ---------------------------------- | ----------- |
| `TextP.textContains(string)`       |             |
| `TextP.textContainsPrefix(string)` |             |
| `TextP.textContainsRegex(string)`  |             |
| `TextP.textContainsFuzzy(string)`  |             |
| `TextP.textPrefix(string)`         |             |
| `TextP.textRegex(string)`          |             |
| `TextP.textFuzzy(string)`          |             |
| `GeoP.geoIntersect(GeoShape)`      |             |
| `GeoP.geoWithin(GeoShape)`         |             |
| `GeoP.geoDisjoint(GeoShape)`       |             |
| `GeoP.geoContains(GeoShape)`       |             |