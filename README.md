﻿## Smartflow
### .NET平台下工作流现状

目前，属于.NET平台下研发的工作流管理平台真是太少，可供选择真心不多，官方我们之前也采用过，没有用好，到处是问题，而且还没有提供在线的流程设计器，使用起来极其不方便。市面上免费开源工作流管理平台又很少，基本都是属于要收取一定的费用。真正免费的又不是很好用，收费的又太高，所以笔者自已闭门造车，打造一款基于.NET平台免费开源工作流管理平台，为开源尽一点自已的绵薄之力。
 
### Smartflow 工作流管理平台介绍
笔者基于.NET平台，研发了Smartflow工作流平台，Smartflow工作流平台目前包含工作流引擎、工作流流程设计器，支持流程在线设计。工作流引擎负责提供对流程的解析，并驱动流程的流转，是工作流平台核心部件。流程设计器是基于SVG研发，支持所有主流浏览器，IE浏览器只支持IE9以上的版本。工作流平台研发所采用的技术框架和工具是Vs2013+
Dapper+ASP.NET MVC4.0+.NETFX4.0。为了便于后续扩展，支持其他的数据库的访问，笔者经过慎重考虑采用Dapper
组件， 了解该组件的，应该知道他是一款，半ORM框架，对原生SQL语句支持比较友好，且支持所有主流数据库系统访问。
所以，你不用担心Smartflow工作流管理平台对跨库访问能力。目前，默认只支持 MSSQLSERVER数据库，若想支持其他的数
据，请修改工作流引擎中DapperFactory 工厂类，提供数据库访问接口,如下代码片段：
```C#
  public static IDbConnection CreateConnection(DatabaseCategory dbc, string connectionString)
  {
	IDbConnection connection = null;
	switch (dbc)
	{
		case DatabaseCategory.SQLServer:
			connection = DatabaseService.CreateInstance(new SqlConnection(connectionString));
			break;
		case DatabaseCategory.Oracle:
			//ms 提供
			connection = DatabaseService.CreateInstance(new OracleConnection(connectionString));
			break;
		case DatabaseCategory.MySQL:
			//需要自已提供Dll
			//connection = DatabaseService.CreateInstance(new SqlConnection(connectionString));
			break;
	}
	return connection;
   }
```
工作流平台目前实现功能点如下：
1.	支持流程流转；<br/>
2.	支持流程分支；<br/>
3.	支持流程撤销；<br/>
4.	支持流程原路回退；<br/>
5.	支持流程节点角色绑定；<br/>
6.	支持流程在线设计；<br/>
7.	提供友好授权验证接口；<br/>
...

### Smartflow 工作流管理平台未来
笔者会对Smartflow 工作流平台，一直维护到底。未来，我会对工作流管理平台增加更多有趣的功能，以便你能将工作流平台更加快速的融入到业务系统中。期望，能打造成符合中国特色工作流管理平台，造福更多企业和开发人员。
### 关于我
入行七年多的时间，一直从事研发工作，主要从事的ERP 管理类型信息系统研发。 七年时间也不短，一直碌碌无为，于是我决定现在要做点什么事情，由于之前接触过工作流研发方面事情，所以我就想做个免费开源工作流管理平台。工作流管理平台的技术含量并不高，但是要想把他做好，也是需要花很大的精力。如果你觉得能帮助到你，欢迎帮忙推荐。生活不易，若有商务合作的欢迎联系，我的联系方式237552006@qq.com。