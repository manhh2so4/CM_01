using System;
using UnityEngine;
using LitJson;
public class HSTool {
    public static Color HexToColor(string hex)
    {
        if (hex.StartsWith("#"))
        // Nếu có dấu "#" ở đầu, loại bỏ
        {
            hex = hex.Substring(1);
        }
        // Chuyển đổi thành giá trị RGB
        float r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
        float g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
        float b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
        return new Color(r, g, b);
    }
    public static JsonData GetItem(string data3){
        return JsonMapper.ToObject(data3);
    }
    
}
    

public static class Extension_Transform
{
    public static void RemoveAllChild(this Transform parent){
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Transform child = parent.GetChild(i);
            UnityEngine.Object.Destroy(child.gameObject);
        }
    }
    public static void RemoveImmediateChilds(this Transform parent){
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Transform child = parent.GetChild(i);
            UnityEngine.Object.DestroyImmediate(child.gameObject);
        }
    }
    public static void ReturnPoolChilds(this Transform parent){
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Transform child = parent.GetChild(i);
            if(child.TryGetComponent(out IObjectPoolItem item)){
                item.ReturnToPool();
            }
        }
    }
}
public static class Extension_GameObject{
    public static T ReplaceComponent<T>(this GameObject obj) where T : Component
    {
        if (obj == null)
        {
            Debug.LogError("Target GameObject không được null!");
            return null;
        }
         T existingComponent = obj.GetComponent<T>();

        if (existingComponent != null)
        {
            UnityEngine.Object.Destroy(existingComponent);
        }

        T newComponent = obj.gameObject.AddComponent<T>();

        return newComponent; 
    }
}


