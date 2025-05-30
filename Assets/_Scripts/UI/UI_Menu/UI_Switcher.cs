using UnityEngine;

public class UI_Switcher : MonoBehaviour {
    [SerializeField] GameObject entryPoint;
    private void Start() {
        SwitchTo(entryPoint);
    } 

    public void SwitchTo(GameObject toDisplay)
    {
        if (toDisplay.transform.parent != transform) return;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(child.gameObject == toDisplay);
        }
    }
    
}