<div align="center"><h1>Purest Admin</h1></div>
<div align="center"><h2>努力变成一款最适合进行二开的极简框架</h2></div>

#### 介绍
* 基于 .NET 8 + vue3 实现的极简rabc权限管理系统后端
* 后端基于精简后的abp框架，前端基于vue-pure-admin，前端表格框架vxe-table
* 项目采用dbfirst模式，使用powerdesigner设计数据模型
* 后端ORM使用.net届最受欢迎的框架：SqlSugar，且文档永久免费
* 核心功能包括最常见的：用户管理、角色管理、组织机构管理、字典管理、等基本功能

#### 开源初衷

* 1、从业也算是很多年了，磕磕绊绊的总算立足于社会，成家立业。在这漫长的职业生涯里，一直感受到的是行业的温暖。有同事朋友们的帮助和关怀，还有大佬们对知识的无私奉献，所以希望能把行业的温暖传播给更多的人
* 2、现有的很多优秀的开源项目，并不复合我的设计思路（重度强迫症） :smiley: ，如果你觉得项目还行，请给个star吧


#### 设计思路

先说说现有的前后端分离系统，大多数其实在我看来都是（假）分离。尤其是菜单及按钮权限这个部分，在此不再赘述。说一下我的设计思路：

* 1、后端不再关心前端的任何实现，只针对功能，开放接口，不再关心菜单按钮等前端问题，降低耦合，同时，可以通过功能模块，控制其他使用者（比如第三方的调用）的权限
* 2、前端根据功能接口、实现想要的功能，所有的业务点由后端控制

* 表现为：把传统的菜单、按钮这些以功能的形式剥离开，后端不再关心前端的按钮、菜单问题，前端通过功能接口，设计自己的路由以及权限控制。

#### 演示地址

- 暂时下线了。后面会重新部署

#### 基本功能


- 1、登陆
- 2、用户管理
- 3、角色管理
- 4、功能管理
- 5、组织机构
- 6、字典管理
- 7、配置管理

#### 未来规划

- 继续完善一些常用且不掺杂业务的基本功能
- 开发一版winform的前端
- 开发一版wpf的前端
 

#### 特别注意
 - **Furion分支是后端基于Furion开发的，由于Furion文档收费，以后不再进行维护了**


#### 截图

| ![用户管理](https://foruda.gitee.com/images/1694076900312622994/8793fa24_1438846.png "屏幕截图")  | ![角色管理](https://foruda.gitee.com/images/1694076921535694292/7210560b_1438846.png "屏幕截图")  | ![功能管理](https://foruda.gitee.com/images/1694076933911129502/a26b87b3_1438846.png "屏幕截图")  |
|---|---|---|
| ![组织机构](frontend/src/assets/organization.png) | ![字典管理](frontend/src/assets/dict.png) | ![系统配置管理](frontend/src/assets/systemconfig.png) |
|![修改个人信息、密码](frontend/src/assets/userinfo.png)|---|---|
|---|---|---|


#### 如何使用

前端建议使用pnpm,clone之后 install（需要node环境，版本16+）

* 前端文档传送门 [Pure Admin 保姆级文档](https://yiming_chang.gitee.io/pure-admin-doc/pages/introduction) 
* 后端文档传送门 [ABP 文档](https://docs.abp.io/zh-Hans/abp/latest/Tutorials/Todo/Index?UI=NG&DB=EF)
* SqlSugar [SqlSugar .Net ORM 5.X 官网 、文档、教程](https://www.donet5.com/Home/Doc)


#### 特别鸣谢
- 👉 ABP：  [https://docs.abp.io/zh-Hans/abp/latest/Tutorials/Todo/Index?UI=NG&DB=EF)
- 👉 SqlSugar：[https://gitee.com/dotnetchina/SqlSugar](https://gitee.com/dotnetchina/SqlSugar)
- 👉 IdGenerator：[https://github.com/yitter/idgenerator](https://github.com/yitter/idgenerator)
- 👉 vue-pure-admin：[https://gitee.com/yiming_chang/vue-pure-admin](https://gitee.com/yiming_chang/vue-pure-admin)
- 👉 vxe-table：[https://gitee.com/xuliangzhan_admin/vxe-table](https://gitee.com/xuliangzhan_admin/vxe-table)
- 👉 有幸使用过、未能一一在此列举的框架以及好朋友们

#### 其他
如有其他问题、请私信或者留言
