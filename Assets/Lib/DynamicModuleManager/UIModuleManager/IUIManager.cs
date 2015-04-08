using UnityEngine;
using System.Collections;

public interface IUIManager {

	void Init();

	GameObject GetRootUIObjectFromCanvasObject(GameObject canvasObj);
	void AddChildUI(GameObject child, GameObject parent);

}
