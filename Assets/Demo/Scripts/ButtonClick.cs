using UnityEngine;
using System.Collections;

public class ButtonClick : MonoBehaviour {

	public void Click()
	{
		var textObj = SceneController.Current.ElementCenter.Get(TestElementScanner.UIElement_Test) as UnityEngine.UI.Text;

		Debug.Log("I get the content of TextComponent by ElementCenter! The content is:" + textObj.text);
	}

}
