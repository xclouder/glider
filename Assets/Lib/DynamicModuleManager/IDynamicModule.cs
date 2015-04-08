using System;

public interface IDynamicModule {

    string GetModuleId();

    void OnRegister(DynamicModuleManager mgr);

    void OnRegisterModule(IDynamicModule module , DynamicModuleManager mgr);
}
