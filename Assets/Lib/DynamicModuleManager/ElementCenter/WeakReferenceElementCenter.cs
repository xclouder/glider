using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeakReferenceElementCenter : IElementCenter {

	private IDictionary<string, System.WeakReference> elementDict;

	public void Add(string key, Object obj)
	{
		if (elementDict == null)
		{
			elementDict = new Dictionary<string, System.WeakReference>();
		}

		var refObj = new System.WeakReference(obj);
		if (elementDict.ContainsKey(key))
		{
			Debug.LogWarning("key:" + key + " existed, overwrite it");
		}
		elementDict[key] = refObj;

	}

	public Object Get(string key)
	{
		if (elementDict == null)
		{
			return null;
		}

		if (!elementDict.ContainsKey(key))
		{
			Debug.LogError("key:" + key + " not exist.");
			return null;
		}

		return elementDict[key].Target as Object;
	}

}
