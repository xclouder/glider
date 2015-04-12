using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneController : MonoBehaviour {

    private static string SCENE_CONTROLLER_NAME = "SceneController";

    public string UIName;

    public static SceneController Current
    {
        get {
            var go = GameObject.FindObjectOfType(typeof(SceneController)) as SceneController;//GameObject.FindWithTag(SCENE_CONTROLLER_NAME);

            return go;
        }
    }

    void Awake()
    {
        var s = GetComponent<SceneController>();
        s.Init(UIName);
    }

    public IElementCenter ElementCenter {get {return elementCenter;}}

	public void LoadUIWithDescription(string uiDesPath)
	{
		string path = "UIDescriptions/" + uiDesPath;

		UIDescriptionParser parser = new UIDescriptionParser();

		var json = LoadTextFromPath(path);

		UIDescription uiDes = parser.ParseFromJson(json);

		LoadUIWithDescription(uiDes);
	}

	private string LoadTextFromPath(string path)
	{
		var json = Resources.Load(path) as TextAsset;
		return System.Text.Encoding.UTF8.GetString(json.bytes);
	}

	public void LoadUIWithDescription(UIDescription uiDes)
	{
		//load canvas & root ui
		LoadCanvas(uiDes.CanvasPrefab);
        if (uiDes.Prefab != null)
        {
            LoadUI(uiDes.Prefab);
        }

		//load children if exists
		if (uiDes.Children != null && uiDes.Children.Count > 0)
		{
			foreach (var c in uiDes.Children)
			{
				GameObject parent = null;
				if (!string.IsNullOrEmpty(c.ParentElementId))
				{
					parent = elementCenter.Get(c.ParentElementId) as GameObject;
				}

				LoadUI(c, parent);
			}
		}
	}

	private IElementCenter elementCenter;
	public void LoadCanvas(string canvasPrefabPath)
	{
		UIManager.LoadCanvas(canvasPrefabPath, elementCenter);
	}

	public void LoadUI(string uiPrefabPath, GameObject parentUI = null)
	{
		UIManager.LoadUI(uiPrefabPath, elementCenter, parentUI);
	}

	public void LoadUI(UINode uiNode, GameObject parentUI = null)
	{
        Debug.Log("Load UINode:" + uiNode.Prefab);
		GameObject ui = UIManager.LoadUI(uiNode.Prefab, elementCenter, parentUI);

		if (uiNode.Children != null && uiNode.Children.Count > 0)
		{
			GameObject parent = ui;
			foreach (var c in uiNode.Children)
			{
				if (c.ParentElementId != null)
				{
					var tmp = elementCenter.Get(c.ParentElementId) as GameObject;
					if (tmp != null)
					{
						parent = tmp;
					}
				}
				LoadUI(c.Prefab, parent);
			}
		}

	}

    public void Init(string uiDesPath = null)
    {
        SetupModuleManager();
		elementCenter = new WeakReferenceElementCenter();

        OnWillRegisterDynamicModule(ModuleManager);

        ICollection<IDynamicModule> modules = this.ModuleManager.GetAllModules();
        foreach (var m in modules)
        {
            m.Init();
        }

        if (!string.IsNullOrEmpty(uiDesPath))
        {
            LoadUIWithDescription(uiDesPath);
        }

        foreach (var m in modules)
        {
            m.OnAllModuleInitDone(this.ModuleManager);
        }
    }

    public virtual void OnWillRegisterDynamicModule(DynamicModuleManager mgr)
    {

    }

	public UIModuleManager UIManager
	{
		get;set;
	}

    private void SetupModuleManager()
    {
        this.ModuleManager = new DynamicModuleManager();

		//register common modules
		var uiModule = new UIModuleManager();
		
        ModuleManager.Register(uiModule);
		UIManager = uiModule;
    }

    private DynamicModuleManager ModuleManager
    {
        get;set;
    }
	
}
