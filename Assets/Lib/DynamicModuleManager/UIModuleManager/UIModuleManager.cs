using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIModuleManager : BaseDynamicModule {

	public static string UI_MANAGER_MODULE_ID = "com.jidanke.modules.ui";

	public static string UI_ROOT_ELEMENT_ID = "ui_root";
	private IUIManager concreteUIManager;

	public override string GetModuleId()
	{
		return UI_MANAGER_MODULE_ID;
	}

	public override void OnRegister(DynamicModuleManager mgr)
	{

	}

	public void Init()
	{
		concreteUIManager = new UGuiUIManager();
		concreteUIManager.Init();
	}

	public GameObject LoadCanvas(string canvasPrefabPath, IElementCenter elementCenter)
	{
		var canvasUIObj = CreateGameObjectFromPrefab(canvasPrefabPath);
		var rootUIObj = concreteUIManager.GetRootUIObjectFromCanvasObject(canvasUIObj);

		elementCenter.Add(UI_ROOT_ELEMENT_ID, rootUIObj);

		return rootUIObj;
	}

	public GameObject LoadUI(string prefabPath, IElementCenter elementCenter, GameObject parentUI = null)
	{
		if (string.IsNullOrEmpty(prefabPath))
		{
			Debug.LogError("prefabPath is null");
			return null;
		}

		var go = CreateGameObjectFromPrefab(prefabPath);

		if (go == null)
		{
			Debug.LogError("UI in path:" + prefabPath + " cannot be load.");
			return null;
		}

		var scanner = go.GetComponent<IElementScanner>();
		if (scanner != null)
		{
			scanner.Scan(go, elementCenter);
		}

		if (parentUI == null)
		{
			parentUI = elementCenter.Get(UI_ROOT_ELEMENT_ID) as GameObject;
		}

		if (parentUI == null)
		{
			Debug.LogError("no rootUI loaded, please load rootUI first.");
		}
		else
		{
			concreteUIManager.AddChildUI(go, parentUI);
		}

		return go;
	}

//	public GameObject LoadUI(UIDescription uiDes, IElementCenter elementCenter, string parentElementId = null)
//	{
//		/*
//		if(uiDes.IsRootUI)
//		{
//			var canvasUIObj = CreateGameObjectFromPrefab(uiDes.CanvasPrefabPath);
//			var rootUIObj = concreteUIManager.GetRootUIObjectFromCanvasObject(canvasUIObj);
//
//			elementCenter.Add(UI_ROOT_ELEMENT_ID, rootUIObj);
//		}
//
//		var go = CreateGameObjectFromPrefab(uiDes.UIPrefabPath);
//		if (go == null)
//		{
//			Debug.LogError("UI in path:" + uiDes.UIPrefabPath + " cannot be load.");
//		}
//
//		var explorer = uiDes.ElementExplorer;
//		if (explorer != null)
//		{
//			explorer.Explore(go, elementCenter);
//		}
//
//		GameObject parentUI = null;
//		if (!string.IsNullOrEmpty(parentElementId))
//		{
//			parentUI = elementCenter.Get(parentElementId) as GameObject;
//		}
//		else
//		{
//			parentUI = elementCenter.Get(UI_ROOT_ELEMENT_ID) as GameObject;
//		}
//
//		concreteUIManager.AddChildUI(go, parentUI);
//
//		return go;
//		*/
//		return null;
//	}

	private GameObject CreateGameObjectFromPrefab(string prefabPath)
	{
        if (string.IsNullOrEmpty(prefabPath))
        {
            Debug.LogError("prefabPath is null");
            return null;
        }
		var prefab = LoadPrefab(prefabPath);
		return GameObject.Instantiate(prefab) as GameObject;
	}

	private static Dictionary<string,GameObject> _prefabsCacheDict= new Dictionary<string,GameObject>();
	private static GameObject LoadPrefab(string prefabPath, bool useCache = false){
		if(useCache && _prefabsCacheDict.ContainsKey(prefabPath)){
			return _prefabsCacheDict[prefabPath];
		}else{
            Debug.Log("load prefab:" + prefabPath);
			GameObject newObj = Resources.Load<GameObject> (prefabPath);
			if(useCache) _prefabsCacheDict.Add(prefabPath, newObj);
			return newObj;
		}
	}


}
