using UnityEngine;
using System.Collections;

public interface IElementCenter {

	void Add(string key, Object obj);
	Object Get(string key);

    T Get<T>(string key);
}
