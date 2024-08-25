using UnityEngine;

public class CoreComp<T> where T : CoreComponent {
    private Core core;
    private T comp;
    public T Comp => comp ? comp : core.GetCoreComponent(ref comp);
    public CoreComp(Core _core){
        if( _core == null){
            Debug.LogWarning($"Core is Null for componet {typeof(T)}");
        }
        this.core = _core;
    }
}