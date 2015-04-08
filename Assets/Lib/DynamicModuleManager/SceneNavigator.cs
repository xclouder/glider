using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneNavigator {

//    private static IDictionary<string, Types> sceneControllerClassDict;

    private static SceneController CreateSceneController(System.Type sceneControllerType)
    {
        GameObject go = new GameObject("SceneController");
        Debug.Log("new obj added");
        return go.AddComponent(sceneControllerType) as SceneController;
    }

	public static SceneController To(string sceneName, Hashtable paramters = null, bool useAsync = false)
    {

        Debug.Log("load scene:" + sceneName);
        Application.LoadLevel (sceneName);

  
        SceneController controller = CreateSceneController(typeof(SceneController));
        
        if (controller == null)
        {
            Debug.LogError("create sceneController failed");
        }

        return null;
    }

}
