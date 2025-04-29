using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] List<CoreComponent> CoreComponents = new List<CoreComponent>();
	[SortingLayer] public int SortingLayerID = 0;
	public int uniqueID;
	public Vector2 size = new Vector2(1,1);
	//---------------- Height ----------------
	public float Height;
	public event Action OnChangeData;
	//-------------------------------------------
	void Start() {
		OnChangeData?.Invoke();
	}
	public void SetData() => OnChangeData?.Invoke();
    public void LogicUpdate() {
		foreach (CoreComponent component in CoreComponents) {
			component.LogicUpdate();
		}
	}
    public void AddComponent(CoreComponent component) {
		if (!CoreComponents.Contains(component)) {
			CoreComponents.Add(component);
		}
	}
    public T GetCoreComponent<T>() where T : CoreComponent {
		var comp = CoreComponents.OfType<T>().FirstOrDefault();
		if(comp) return comp;

		comp = GetComponentInChildren<T>();
		if(comp) return comp;
		//Common.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
		return null;
	}

    public T GetCoreComponent<T>(ref T value) where T : CoreComponent {
		value = GetCoreComponent<T>();
		return value;
	}
    
}
