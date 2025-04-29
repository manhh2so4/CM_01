// using UnityEngine;
// using UnityEditor;
// using UnityEngine.Tilemaps;
// using Cysharp.Threading.Tasks.Triggers;

// [CustomEditor(typeof(TilemapCollider2D))]
// public class TilemapCollider2DEditor : Editor {

//     private Tile.ColliderType selectedColliderType = Tile.ColliderType.None;
//     private Tile.ColliderType lastSelectedType = Tile.ColliderType.None;
    


//     public override void OnInspectorGUI() {
        
//         selectedColliderType = ( Tile.ColliderType)EditorGUILayout.EnumPopup("Collider Types:", selectedColliderType);

//         if (selectedColliderType != lastSelectedType)
//         {
//             UpdateAllTilesColliderType();

//         }
//         if(GUILayout.Button("Generate Geometry"))
//         {
//             GetColliderType();
//         }

//         lastSelectedType = selectedColliderType;
//         //EditorUtility.SetDirty(this);

//         EditorGUILayout.Space();
//         EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
//         EditorGUILayout.Space();

//         base.OnInspectorGUI();
//     }
//     void GetColliderType(){
//         Tilemap tilemap = ((TilemapCollider2D)target).GetComponent<Tilemap>();
//         BoundsInt bounds = tilemap.cellBounds;
      
//         for (int y = bounds.min.y; y < bounds.max.y; y++)
//         {
//             for (int x = bounds.min.x; x < bounds.max.x; x++)
//             {
//                 Common.Log($"x: {x}, y: {y}, colliderType: {tilemap.GetColliderType(new Vector3Int(x, y,0))}");
//             }
//         }
//     }
//     private void UpdateAllTilesColliderType()
//     {
//         Tilemap tilemap = ((TilemapCollider2D)target).GetComponent<Tilemap>();
//         tilemap.CompressBounds();
//         BoundsInt bounds = tilemap.cellBounds;
      
//         for (int y = bounds.min.y; y < bounds.max.y ; y++)
//         {
//             for (int x = bounds.min.x; x < bounds.max.x; x++)
//             {
//                 tilemap.SetColliderType(new Vector3Int(x, y,0), selectedColliderType);
//                 EditorUtility.SetDirty(tilemap);
//             }
//         }
//         tilemap.RefreshAllTiles();
//     }
// }