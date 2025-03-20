using NaughtyAttributes;
using UnityEngine;

public class Testing : MonoBehaviour {

    [Button]
    void Test() {
        PopupText textPopupInstance = PoolsContainer.GetObject(this.GetPrefab<PopupText>());
        textPopupInstance.transform.position = transform.position ;
        textPopupInstance.Setup(100, false, 1);
    }

}