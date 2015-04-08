using UnityEngine;
using System.Collections;

public class UGuiUIManager : BaseUIManager {

	public override GameObject GetRootUIObjectFromCanvasObject(GameObject canvasUIObj)
	{
		var canvasWrapper = canvasUIObj.GetComponent<UICanvasWrapper>();

		if (canvasWrapper == null)
		{
			Debug.LogError("Cannot find UICanvasWrapper");
			return null;
		}

		var canvasObj = canvasWrapper.canvasObj;
		if (canvasObj == null)
		{
			Debug.LogError("CanvasObj is null");
			return null;
		}

		return canvasObj;
	}

	public override void AddChildUI(GameObject child, GameObject parent)
	{
		if (child == null || parent == null)
		{
			Debug.LogError("child or parent is null");
			return;
		}

		child.transform.SetParent(parent.transform, false);
	}

}
