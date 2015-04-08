using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIDescriptionParser {

	private static string kUIPrefabsDefaultFolder = "UIPrefabs/";

	public UIDescription ParseFromJson(string jsonText)
	{

		if (string.IsNullOrEmpty(jsonText))
		{
			Debug.LogError("jsonText is null");
			return null;
		}

		Dictionary<string, object> config = MiniJSON.Json.Deserialize(jsonText) as Dictionary<string, object>;
		if (config == null)
		{
			Debug.LogError("parse uidescription config failed.");
			return null;
		}

		UIDescription des = new UIDescription();
        des.CanvasPrefab = GetRealPrefabPath(GetStringFromDict("CanvasPrefab", config));
		des.Prefab = GetRealPrefabPath(GetStringFromDict("Prefab", config));

		if (config.ContainsKey("Children"))
		{
			IList<UINode> nodes = ParseNodes(config["Children"]);
			des.Children = nodes;
		}

		return des;
	}

	private IList<UINode> ParseNodes(object rawNodes)
	{
		IList<object> rawChildren = rawNodes as IList<object>;
		IList<UINode> nodeList = null;

		if (rawChildren == null)
		{
			Debug.LogError("invalid config for Children");
		}
		else
		{
			nodeList = new List<UINode>();

			foreach (var c in rawChildren)
			{
				var cDict = c as IDictionary<string, object>;
				if (cDict == null)
				{
					Debug.LogError("cDict should be a dict");
					continue;
				}
				
				UINode node = ParseUINode(cDict);
				nodeList.Add(node);
			}
		}

		return nodeList;
	}

	private string GetRealPrefabPath(string prefabName)
	{
		if (string.IsNullOrEmpty(prefabName))
		{
			Debug.LogError("prefabName is null");
			return null;
		}

		return kUIPrefabsDefaultFolder + prefabName;
	}

	private UINode ParseUINode(IDictionary<string, object> dict)
	{
		if (dict == null)
		{
			Debug.LogError("UINode dict is null");
			return null;
		}

		UINode n = new UINode();
		n.ParentElementId = GetStringFromDict("ParentElementId", dict);
		n.Prefab = GetRealPrefabPath(GetStringFromDict("Prefab", dict));
		n.AllowMultiInstance = GetBoolFromDict("AllowMultiInstance", dict);

		if (dict.ContainsKey("Children"))
		{
			IList<UINode> nodes = ParseNodes(dict["Children"]);
			n.Children = nodes;
		}

		return n;
	}

	private string GetStringFromDict(string key, IDictionary<string, object> dict, string defaultVal = null)
	{
		if (dict == null)
		{
			Debug.LogError("dict is null");
			return null;
		}

		if (!dict.ContainsKey(key))
		{
			return defaultVal;
		}

		var val = dict[key] as string;
		if (val == null)
		{
			Debug.LogError("value in dict with key:" + key + " exist, but the value isnot a string.");
		}
		return val;
	}

	private bool GetBoolFromDict(string key, IDictionary<string, object> dict, bool defaultVal = false)
	{
		if (dict == null)
		{
			Debug.LogError("dict is null");
			return false;
		}
		
		if (!dict.ContainsKey(key))
		{
			return defaultVal;
		}
		
		var val = System.Convert.ToBoolean(dict[key]);
		return val;
	}
}
