using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DetectTarget {
    
    public List<Collider2D> objDetected = new List<Collider2D>();
    Collider2D[] results;
    LayerMask layerDetectable;
    [SerializeField] int Count;
    public Camera mainCamera;
    int maxObjDetected;
    List<int> indexList = new List<int>();
    public DetectTarget(LayerMask _layerDetectable, int _maxObjDetected){
        layerDetectable = _layerDetectable;
        results = new Collider2D[_maxObjDetected];
        mainCamera = Camera.main;
        maxObjDetected = _maxObjDetected;
    }
    public void CheckForTargets( Vector2 center, float radius){

        Count = Physics2D.OverlapCircleNonAlloc( center, radius, results , layerDetectable);

        if( objDetected.Count >= maxObjDetected ) ClearObj();
        if( Count >= 0 ) ClearObj();
        for(int i = 0; i < Count; i++){
            if( results[i].TryGetComponent( out IInteractable interactable )){
                if( objDetected.Contains(results[i]) ) continue;

                // for(int j = 0; j < objDetected.Count; j++){

                //     if( objDetected[j] == results[i] ) continue;

                // }

                objDetected.Add(results[i]);
                
            }
        }
    }
    void checkobjDetected(){

    }


    public void ClearObj(){
        objDetected.Clear();
    }
}