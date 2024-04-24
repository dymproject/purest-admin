<div align="center"><h3>PurestAdmin的服务端项目</h3></div>

项目基于.Net8开发，并会持续跟随官方LTS的版本更新

#### 完整项目地址

[https://gitee.com/dymproject/purest-admin](https://gitee.com/dymproject/purest-admin)

#### 项目说明

该项目由abp精简而来，沿用了abp framework的大部分功能，包括依赖注入、验证、授权等。结构重新划分后，使用更方便，项目分层如下

* PurestAdmin.Api.Host  项目入口、启动层，所有关于接口的设置都放在这里,比如请求中间件、授权策略等
* PurestAdmin.Application 应用层，会自动根据规则生成api接口，业务层
* PurestAdmin.Core 核心层，此层不参与任何业务内容，负责基础功能的实现，以及组件的封装、扩展等
* PurestAdmin.Multiplex 复合层，多元化的层，作用：为application和host层提供价值，放置一些后台功能以及复用的功能等
* PurestAdmin.Multiplex.Contracts 复合层的契约层，相当于复合层方法的抽象层，也不完全都是契约，也会存放常量或者枚举等
* PurestAdmin.SqlSugar ORM层，因为本项目使用了SqlSugar,并且也没打算支持替换，所以名字就叫PurestAdmin.SqlSugar，存放实体和扩展实体等数据库相关类，当然，如果你不擅长使用，可以另外使用其他的ORM框架，根据abp提供的规则，进行注入即可

#### 如何使用

- 1、本项目使用文档[http://docs.purestadmin.com](http://docs.purestadmin.com)
- 2、abp官方文档[https://docs.abp.io/](https://docs.abp.io/)
