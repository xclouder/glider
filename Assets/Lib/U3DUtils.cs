using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class U3DUtils : System.Object {

	public static void RemoveAllChildObjects(GameObject parent)
	{
		var children = new List<GameObject>();
		foreach (Transform child in parent.transform) children.Add(child.gameObject);
		children.ForEach(child => GameObject.Destroy(child));
	}

	public static GameObject CreateChild(GameObject parent, string childObjectName)
	{
		GameObject g = new GameObject (childObjectName);
		g.transform.parent = parent.transform;
		g.transform.localPosition = Vector3.zero;

		return g;
	}

	public static void SetAsChild(GameObject parent, GameObject child)
	{
		child.transform.parent = parent.transform;
	}

	public static Quaternion RotationFromPositionToPosition2D(Vector2 fromPos, Vector2 toPos)
	{
		
//		Vector2 v = toPos - fromPos;
//		Debug.Log ("v:" + v);
		float agl = AngelFromPositionToPosition2D (fromPos, toPos);

		return Quaternion.Euler(0, 0, -agl);
	}
//
	public static Quaternion RotationDirectionToDirection2D(Vector2 v1, Vector2 v2)
	{
		return Quaternion.FromToRotation (v1, v2);
	}

	public static float AngelFromPositionToPosition2D(Vector2 fromPos, Vector2 toPos)
	{
		Vector3 lookPos = toPos - fromPos;
		float targetAngle = Mathf.Atan2(lookPos.x, lookPos.y) * Mathf.Rad2Deg;

		return targetAngle;
	}

	public static void GameObjectLookAtPoint2D(GameObject go, Vector3 point)
	{
		Vector3 lookPos = point - go.transform.position;
//
//		go.transform.up = lookPos.normalized;

		go.transform.rotation = Quaternion.FromToRotation (Vector3.up, lookPos);

	}

	private static Dictionary<string,GameObject> _prefabsCacheDict= new Dictionary<string,GameObject>();
	public static GameObject LoadPrefab(string prefabPath,bool useCache=false){
		if(useCache && _prefabsCacheDict.ContainsKey(prefabPath)){
			return _prefabsCacheDict[prefabPath];
		}else{
			GameObject newObj = Resources.Load<GameObject> (prefabPath);
			if(useCache) _prefabsCacheDict.Add(prefabPath,newObj);
			return newObj;
		}
	}

    public static GameObject CreateGameObjectFromPrefab(GameObject prefabObj)
    {
        if (prefabObj == null)
        {
            Debug.LogError("prefabObj is null");
            return null;
        }

        return GameObject.Instantiate(prefabObj) as GameObject;
    }

	public static void AddLoadPrefabCache(string prefabPath,GameObject obj){
		if(!_prefabsCacheDict.ContainsKey(prefabPath))
			_prefabsCacheDict.Add(prefabPath,obj);
	}

	public static bool PrefabCacheContainsKey(string prefabPath){
		return _prefabsCacheDict.ContainsKey(prefabPath);
	}

}
