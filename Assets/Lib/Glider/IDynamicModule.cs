using System;

public interface IDynamicModule {

    string GetModuleId();

    void Init();

    void OnRegister(DynamicModuleManager mgr);

    void OnRegisterModule(IDynamicModule module , DynamicModuleManager mgr);

    void OnAllModuleInitDone(DynamicModuleManager mgr);

}
