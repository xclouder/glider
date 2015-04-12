using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UINode {

	public string Prefab {get;set;}

	private IList<UINode> children;
	public IList<UINode> Children {get {return children;} set {children = value;}}
	public string ParentElementId {get;set;}
	public bool AllowMultiInstance {get;set;}

	public void AddChild(UINode n)
	{
		if (children == null)
		{
			children = new List<UINode>();
		}

		children.Add(n);
	}
}
