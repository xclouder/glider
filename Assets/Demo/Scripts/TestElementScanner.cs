using UnityEngine;
using System.Collections;

public class TestElementScanner : BaseElementScanner {

	public static string UIElement_Test = "com.jidanke.test";

	public UnityEngine.UI.Text textObj;

	public override void Scan(GameObject uiObject, IElementCenter eleCenter)
	{
		eleCenter.Add(UIElement_Test, textObj);
	}

}
