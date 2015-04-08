using UnityEngine;
using System.Collections;

public abstract class BaseUIManager : IUIManager {

//	public abstract GameObject LoadUI(string prefabPath);

	public virtual void Init()
	{

	}
	
	public abstract GameObject GetRootUIObjectFromCanvasObject(GameObject canvasObj);

	public abstract void AddChildUI(GameObject child, GameObject parent);

}
