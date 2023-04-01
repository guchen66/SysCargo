项目结构：使用WPF+Prism+SqlSugar+MahApps



### 1、数据库表结构

#### 1、用户表User

|    Id     |    int    |        自增        |
| :-------: | :-------: | :----------------: |
|   Name    | Navarcher |    用户名，主键    |
| Password  | navarcher |        密码        |
|   Role    |    int    | 0=管理员，1=操作员 |
| InserDate | DateTime  |      插入时间      |

#### 2、物资表Cargo

|    Id     |   int    |      自增      |
| :-------: | :------: | :------------: |
|   Name    | Nvarcher | 物资名称，主键 |
| TypeName  | Nvarcher |    物资类型    |
|  Amount   | Nvarcher |    物资个数    |
|   Price   |  float   |    物资单价    |
|    Tag    | Nvarcher |      备注      |
| InserDate | DateTime |    插入时间    |
|  UserId   |   int    |    操作员Id    |
| UserName  | Nvarcher |   操作员姓名   |

#### 3、物资操作流水表

|     Id     |   int    |      自增，主键      |
| :--------: | :------: | :------------------: |
|  CargoId   |   int    |        物资Id        |
| CargoName  | Nvarcher |       物资名称       |
|   Number   |   int    | 入库或出库的流水数量 |
|    Tag     | Nvarcher |         备注         |
| InsertDate | DateTime |       插入时间       |
|   UserId   |   int    |       操作员Id       |
|  UserName  | Nvarcher |      操作员姓名      |

#### 4、物资类型表

|     Id     |   int    | 自增，主键 |
| :--------: | :------: | :--------: |
|    Name    | Nvarcher |  物资类型  |
|    Tag     | Nvarcher |    备注    |
| InsertDate | DateTime |  插入时间  |
|   UserId   |   int    |  操作员Id  |
|  UserName  | Nvarcher | 操作员姓名 |



[toc]



































