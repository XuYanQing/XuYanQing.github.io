### 1.简答题
``` 1.解释游戏对象和资源之间的区别```

GameObject其实是Component的容器，他们时包含关系。默认的创建的空GameObject的位置，旋转和缩放。组件有不同的至和属性，在构建游戏是可以在编辑器里调整，或者在运行游戏时由脚本来调整。有两种主要的至和属性：值和引用。值和引用。值通常在检视视图中的编辑，引用通常通过将任何其他类型的组件，游戏对象或资源拖动到引用下来指定。
###


```
void Awake(){
		Debug.Log ("awake");
	}
	// Use this for initialization
	void Start () {
		Debug.Log ("start");
	}

	// Update is called once per frame
	void Update () {
		Debug.Log ("update");
	}

	void FixedUpdate(){
		Debug.Log ("FixedUpdate");
	}

	void LateUpdate(){
		Debug.Log ("LateUpdate");
	}

	void OnGUI(){
		Debug.Log ("onGUI");
	}

	void Reset(){
		Debug.Log ("onReset");
	}

	void OnDisable(){
		Debug.Log ("onDisable");
	}

	void OnDestroy(){
		Debug.Log ("onDestroy");
	}
```
``` 
2.了解GameObject,Transform,Component对象。
```
如之前在游戏对象 (GameObjects) 中所述，游戏对象包含组件 (Components)。我们将通过讨论游戏对象 (GameObject) 及其最常见的组件 (Component) - 变换组件 (Transform Component) 来探讨这种关系。打开任意 Unity 场景，创建一个新的游戏对象 (GameObject)（在 Windows 中使用 Shift-Control-N，或在 Mac 中使用 Shift-Command-N），选择该游戏对象并在检视器 (Inspector) 中查看。 

```
3.UML图
```
![image](https://camo.githubusercontent.com/be959da3e431524c0f76db0c1690f103dc05668b/687474703a2f2f6d2e717069632e636e2f7073623f2f563133397947474f304f683941652f674a714966646655356a56414a56626e79546951445250714665336841744a525766454b4e4c6649375a77212f622f64465942414141414141414126626f3d67514b41416741414a141414442794d212672663d7669657765725f3426743d35)

- 查找对象
```
public static GameObject Find(String name)
```

- 添加子对象
```
public static GameObject CreatePrimitive(PrimitiveTypetype)
```

- 遍历对象树
```
foreach (Transform child in transform){
    Debug.Log(child.gameObject.name);
}
```
- 清除所有子对象
```
foreach(Transform child in transform){
    Destroy(child.gameObject);
}
```


```
3.预设有什么好处？与对象克隆关系
```

在Unity3D当中，为了快速复制出游戏对象，主要有克隆游戏对象与创建预制两种方法。
两者区别在于：
        1、克隆游戏对象需要场景中有被克隆对象，而创建预制只需事先创建预制即可，允许场景中一开始并不存在该游戏对象。
        2、克隆出来的游戏对象并不会随着被克隆体的变化而发生变化，但是使用预制创建出来的对象会随着预制的改变而发生改变。


```
4.尝试解释组合模式
```
组合模式允许用户将对象组合成树形结构来表现”部分-整体“的层次结构，使得客户以一致的方式处理单个对象以及对象的组合。组合模式实现的最关键的地方是——简单对象和复合对象必须实现相同的接口。这就是组合模式能够将组合对象和简单对象进行一致处理的原因。
BroadcastMessage() 使用示例：
```
//父类对象（table）方法：
void Start () {
    Debug.Log("Table Start");
    this.BroadcastMessage("testBroad", "hello sons!");
}
//子类对象（chair1）方法：
public void testBroad(string str)
{
    print("chair1 received: " + str);
}
//子类对象（chair2）方法：
public void testBroad(string str)
{
    print("chair2 received: " + str);
}
```
![image](https://blog.csdn.net/bkjs626/article/details/79685557)



### 编程实践
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
	private int turn =1 ;
	private int[,] state = new int[3, 3];
	public Texture2D img;
	public Texture2D img1;
	public Texture2D img2;

	void Start()
	{
		Reset ();
	}


	void OnGUI()
	{
		GUIStyle fontStyle = new GUIStyle ();
		GUIStyle fontStyle1 =new GUIStyle ();
		GUIStyle fontStyle2 = new GUIStyle ();
		fontStyle.fontSize = 40;
		fontStyle1.normal.background = img;
		fontStyle2.fontSize = 30;
		fontStyle2.normal.textColor = new Color (255, 255, 255);

		GUI.Label (new Rect (0, 0, 1024, 781), "", fontStyle1);
		GUI.Label (new Rect (230, 120, 100, 100), "Welcome to Game", fontStyle);

		GUI.Label (new Rect (50, 150, 200, 100), img1);
		GUI.Label (new Rect (600, 150, 200, 100), img2);

		if (GUI.Button (new Rect (350, 500, 100, 50), "RESET")) {
			Reset ();
		}
		int result = check ();
		if (result == 1) {
			GUI.Label (new Rect (50, 250, 100, 50), "black win", fontStyle2);
		} else if (result == 2) {
			GUI.Label (new Rect (600, 250, 100, 50), "white win", fontStyle2);
		}
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (state [i, j] == 1)
					GUI.Button (new Rect (280 + i * 80, 220 + j * 80, 80, 80), img1);
				if (state [i, j] == 2)
					GUI.Button (new Rect (280 + i * 80, 220 + j * 80, 80, 80), img2);
				if (GUI.Button (new Rect (280 + i * 80, 220 + j * 80, 80, 80), "")) { 
					if (result == 0) {
						if (turn == 1)
							state [i, j] = 1;
						else
							state [i, j] = 2;
						turn = -turn;
					}
				}
			}
		}
	}

	void Reset()
	{
		turn = 1;
		for (int i = 0; i < 3; i++)
			for (int j = 0; j < 3; j++)
				state [i, j] = 0;
	}

	int check()
	{
		for (int i = 0; i < 3; i++) {
			if (state [i, 0] != 0 && state [i, 0] == state [i, 1] && state [i, 1] == state [i, 2]) {
				return state [i, 0];
			}
		}

		for (int j = 0; j < 3; j++) {
			if (state [0, j] != 0 && state [0, j] == state [1, j] && state [1, j] == state [2, j])
				return state [0, j];
		}

		if (state [1, 1] != 0 && state [0, 0] == state [1, 1] && state [1, 1] == state [2, 2] ||
		    state [0, 2] == state [1, 1] && state [1, 1] == state [2, 0]) {
			return state [1, 1];
		}
		return 0;
		
		
	}

}
![image](http://b114.photo.store.qq.com/psb?/V12TRLoM3pOvmN/uzwUCFY206iV0tOtYk6j0ZkfJWt12UE7f9Ye4SwOJk4!/c/dHIAAAAAAAAA&bo=KgOAAioDgAIBACc!)

![image](http://b114.photo.store.qq.com/psb?/V12TRLoM3pOvmN/qrs7ec.VaW*XXIiyWB2BJphWo1n7*q5c4DSsgKvf1Cc!/c/dHIAAAAAAAAA&bo=SgOAAkoDgAIBACc!)