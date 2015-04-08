using UnityEngine;
using System.Collections.Generic;

public class DynamicModuleManager {

    private IDictionary<string, IDynamicModule> moduleDict;

	public void Register(IDynamicModule module)
    {
        if (module == null)
        {
            Debug.LogError("module is null, ignore");
            return;
        }

        string moduleId = module.GetModuleId();
        if (string.IsNullOrEmpty(moduleId))
        {
            Debug.LogError("moduleId is null, ignore");
            return;
        }

        if (moduleDict == null)
        {
            moduleDict = new Dictionary<string, IDynamicModule>();
        }

        if (moduleDict.ContainsKey(moduleId))
        {
            Debug.LogError("moduleId:" + moduleId + " exist, ignore.");
            return;
        }

        //notify other modules
        foreach (var kvp in moduleDict)
        {
            kvp.Value.OnRegisterModule(module, this);
        }

        //register module
        moduleDict.Add(moduleId, module);

        //notify registered
        module.OnRegister(this);
    }


}
