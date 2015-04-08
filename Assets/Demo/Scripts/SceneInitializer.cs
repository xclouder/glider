using UnityEngine;
using System.Collections;

public class SceneInitializer : MonoBehaviour {

	void Awake()
	{
		var c = SceneController.Create();
		c.LoadUIWithDescription("TestUI");
	}
}
