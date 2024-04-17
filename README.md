<div align="center"><h1>Purest Admin</h1></div>
<div align="center"><h2>打造一款最适合进行二开的极简.Net框架</h2></div>

#### 项目介绍
* 基于 .NET 8 + vue3 实现的极简rabc权限管理系统后端
* 后端基于精简后的abp框架。前端基于vue-pure-admin，表格框架vxe-table
* 项目采用dbfirst模式，使用powerdesigner设计数据模型
* 后端ORM使用.net届最受欢迎的框架：SqlSugar（注意：文档永久免费）
* 核心功能包括最常见的：用户管理、角色管理、组织机构管理、字典管理、等基本功能

#### 设计思路

* 后端不再关心前端的任何实现，只针对功能，开放接口，同时通过“功能管理”，控制用户调用接口的的权限
* 前端根据调用接口，获取功能编码，通过唯一的“function code”控制界面以及路由等界面权限

#### 框架结构

这里主要介绍后端结构，项目精简了abp的项目结构，前端请参照Pure Admin文档查看结构

* PurestAdmin.Api.Host  项目入口、启动层，所有关于接口的设置都放在这里,比如请求中间件、授权策略等
* PurestAdmin.Application 应用层，会自动根据规则生成api接口，业务层
* PurestAdmin.Core 核心层，项目中所有层都要依赖此层，此层不参与任何业务内容，只负责外部package的引用，以及组件的封装、扩展等
* PurestAdmin.Multiplex 复合层，多元化的层，作用：为application和host层提供价值，别的层不合适存放的东西都可以放在这一层（比如获取当前用户、枚举等）
* PurestAdmin.Multiplex.Contracts 复合层的契约层，这里定义的接口需要在Multiplex层实现，相当于复合层方法的抽象层
* PurestAdmin.SqlSugar ORM层，存放实体和扩展实体

#### 演示地址

- [http://www.purestadmin.com](http://www.purestadmin.com) 被墙了。需要自行过墙

#### 完成功能

- 1、登陆验证
- 2、用户管理
- 3、角色管理
- 4、功能管理
- 5、组织机构
- 6、字典管理
- 7、配置管理
- 8、在线用户
- 9、请求日志
- 10、个人信息


#### 如何使用
* client-vue 存放vue前端项目，建议使用pnpm（需要node环境，版本16+）,项目自己封装了vxe-table，可自行替换。
* api 存放后端接口，项目结构已经重新划分，和abp已经不一样了，参照框架结构介绍
* relationship-model 存放数据关系模型以及数据始化的sql文件


* 前端文档传送门 [Pure Admin 保姆级文档](https://yiming_chang.gitee.io/pure-admin-doc/pages/introduction) 
* 后端文档传送门 [ABP 文档](https://docs.abp.io/zh-Hans/abp/latest/Tutorials/Todo/Index?UI=NG&DB=EF)
* SqlSugar [SqlSugar .Net ORM 5.X 官网 、文档、教程](https://www.donet5.com/Home/Doc)


#### 当前工作

-  **配套的wpf的前端(开发中)** 

#### 截图


| ![系统首页](screenshot/welcome.png)|![个人信息](screenshot/userinfo.png)|![项目配置](screenshot/projectsetting.png) |
|---|---|---|
| ![用户管理](screenshot/userlist.png) | ![角色管理](screenshot/rolelist.png)  | ![功能管理](screenshot/functionlist.png) |
| ![组织机构](screenshot/organizationlist.png) | ![字典管理](screenshot/dictionarylist.png) |![配置管理](screenshot/systemconfiglist.png) |


#### 特别鸣谢
- 👉 ABP：  [https://docs.abp.io/zh-Hans/abp/latest](https://docs.abp.io/zh-Hans/abp/latest)
- 👉 SqlSugar：[https://gitee.com/dotnetchina/SqlSugar](https://gitee.com/dotnetchina/SqlSugar)
- 👉 IdGenerator：[https://github.com/yitter/idgenerator](https://github.com/yitter/idgenerator)
- 👉 Ip2region：[https://github.com/lionsoul2014/ip2region](https://github.com/lionsoul2014/ip2region)
- 👉 vue-pure-admin：[https://gitee.com/yiming_chang/vue-pure-admin](https://gitee.com/yiming_chang/vue-pure-admin)
- 👉 vxe-table：[https://gitee.com/xuliangzhan_admin/vxe-table](https://gitee.com/xuliangzhan_admin/vxe-table)
- 👉 有幸使用、未能一一在此列举的框架以及好朋友们

#### 其他
开源之路充满挑战，但每一步都凝结着作者的汗水与智慧。如果您觉得这个项目对您有帮助，不妨给它点个Star，给予一点小小的支持。您的每一个鼓励，都是我继续前行的动力，让我有更多的热情和信心去完善和优化这个项目。感谢您的支持与关注！


