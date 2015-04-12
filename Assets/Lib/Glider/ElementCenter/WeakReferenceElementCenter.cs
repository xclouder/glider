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

    public T Get<T>(string key)
    {
        var obj = Get(key);

        if (obj is T) {
            return (T)System.Convert.ChangeType(obj, typeof(T));
        } else {
            Debug.LogError("element for key:" + key + " got a type:" + obj.GetType().ToString() + " but you need type:" + typeof(T).ToString());
            return default(T);
        }
    }

}
