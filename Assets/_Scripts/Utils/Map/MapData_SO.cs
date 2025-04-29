using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "MapData ", menuName = "Data/MapData", order = 0)]
public class MapData_SO : ScriptableObject {
    public DataTile[] dataTileMaps ;
    public int OffsetXPosEnemy;

}
[System.Serializable]
public struct DataTile{
    [HideInInspector] public string name;
    public List<TileInfo> dataTileMaps;
}

[System.Serializable]
public struct TileInfo
{
    public Vector3Int position;
    public TileBase tileAsset;
    public Matrix4x4 transformMatrix;
}