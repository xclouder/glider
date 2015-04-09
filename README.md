# Glider

![Glider](http://upload-images.jianshu.io/upload_images/301341-e9d20d531cb25f2c.png)

Glider是为Unity3D开发的一个动态模块管理插件，几个意思呢？

她的诞生主要为了解决以下问题：

1. 场景文件合并困难，如何能让多人协同编辑同一个场景的内容时避免冲突？

2. 场景中关联不大的模块，偶尔需要互相引用。如何获取这些对象而不耦合？

3. MonoBehaviour只有Awake()、Start()两个时机做脚本的初始化，如何解决模块加载顺序问题？

4. 场景能否放手交给美术同学来编辑，逻辑组件、UI能否动态加载？

#核心思想
Glider使用**动态加载**的思想来解决如上问题，不同人员可以将场景中某一个部分做成Prefab，通过一定规则动态加载到场景中。加载过程中，将可能会被其他模块使用到的对象放到“对象仓库”中。加载过程交由Glider来控制，各模块能收到加载完成的通知。

#特性
* 支持动态加载UI，彻底分离UI与场景，可兼容各种UI框架(uGUI、NGUI、Toolkit2D等)，目前已支持uGUI
* 可扩展，如果你喜欢可以将所有游戏对象做成动态模块进行加载
* 统一对象仓库，方便组件之间互相通讯

#运行环境
* Unity3D 5.0及以上版本

#主要模块
#### SceneController
场景管理器，提供方便的方法来加载UI、访问对象仓库。

#### IDynamicModuleManager
动态模块管理器，负责注册模块(IDynamicModule)，并通知相关事件。

####IElementCenter
对象仓库，目前实现类为WeakReferenceElementCenter，提供对象的存取接口。

#### UIModuleManager
UI管理器，实现IDynamicModule接口，在SceneController中作为一个默认的模块加载。

#如何使用动态UI
加载一个动态UI，可以使用SceneController.LoadUIWithDescription(string uiDesFilePath)方法，其中uiDesFilePath指向的是UI描述的配置，其内容如下：

文件：MazeUI.txt
```
{
	"Prefab": "",
	"CanvasPrefab": "Maze/MazeCanvasWrapper",
	"Children": [
		{
			"Prefab": "Maze/HpDisplayPanel",
		},
		{
			"Prefab": "Maze/BagPanel",
		},
		{
			"Prefab": "Maze/GameOverPanel",
		},
		{
			"Prefab": "Maze/Header",
		},
		{
			"Prefab": "Maze/SkillCtrPanel",
		}
	]
}
```

这个配置将会解析成UIDescription对象，里面的UI节点会解析成UINode对象。UINode代表的UIPrefab默认会被加载到父节点中。

UIModuleManager加载UI时，有一些约定：  

1. 制作UIPrefab的粒度，建议控制在Panel层面。例如一个弹窗界面，按钮、标题、文字内容等逻辑比较相关，做成一个Prefab会方便不少，虽然Glider能够支持更细的粒度(如果你有特殊需求的话)。

2. 目录约定。UIPrefabs需要放到"Resources/UIPrefabs/"目录下，UI配置文件需要放在"Resources/UIPrefabs/"目录下。例如：  
DynamicUI/  
　　Resources/  
　　　　UIDescriptions/  
　　　　　　MazeUI.txt  
　　　　UIPrefabs/  
　　　　　　Maze/  
　　　　　　　　HpDisplayPanel.prefab  
　　　　　　　　BagPanel.prefab  
　　　　　　　　MazeCanvasWrapper.prefab  
　　　　　　　　......  


#示例
这里我们简单描述怎样通过配置来加载一个UI

```
void Awake()
{
    var c = SceneController.Create();
    c.LoadUIWithDescription("MazeUI");
}
```

在Resources/UIDescriptions/MazeUI.txt文件中，有如下内容
```
{
	"Prefab": "",
	"CanvasPrefab": "Maze/MazeCanvasWrapper",
	"Children": [
		{
			"Prefab": "Maze/HpDisplayPanel",
		},
		{
			"Prefab": "Maze/BagPanel",
		},
		{
			"Prefab": "Maze/GameOverPanel",
		},
		{
			"Prefab": "Maze/Header",
		},
		{
			"Prefab": "Maze/SkillCtrPanel",
		}
	]
}
```
其中Prefab表示创建的UIPrefab位置，这是一个相对于Resources/UIPrefabs/ 的路径。

详情请参考Demo

#开源协议
遵守MIT协议

