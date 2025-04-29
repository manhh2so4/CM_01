using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;
using Algorithms;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum TileType
{
    Empty,
    Block,
    OneWay
}

public class Map : Singleton<Map> {
    public PathFinderFast mPathFinder;
    byte[,] mGrid;
    TileType[,] tiles;
    Tilemap mapGround,mapOneWay;
    PolygonCollider2D polygonCollider;

    [Header("Tile Vieww")]
    [SerializeField] bool showTypeTile;
    public Vector2i Offset;
    int mWidth, mHeight;

    //----------- Data ------------
    public MapData_SO mapData_SO;
    public List<Vector2> PosEnemy;
    int offsetX => mapData_SO.OffsetXPosEnemy;

    [Button]
    protected override void Awake()
    {
        base.Awake();
        LoadComponent();
        SetData();
    }

    public void InitPathFinder()
	{
        mPathFinder = new PathFinderFast( mGrid, this);
		mPathFinder.Formula                 = HeuristicFormula.Manhattan;

        mPathFinder.Diagonals               = false;

        mPathFinder.HeavyDiagonals          = false;

        mPathFinder.HeuristicEstimate       = 6;
        mPathFinder.PunishChangeDirection   = false;
        mPathFinder.TieBreaker              = false;
        mPathFinder.SearchLimit             = 1000000;
        mPathFinder.DebugProgress           = false;
        mPathFinder.DebugFoundPath          = false;
    }
    void LoadComponent()
    {
        mapGround = transform.Find("Ground").GetComponent<Tilemap>();
        mapOneWay = transform.Find("Platform").GetComponent<Tilemap>();
        mapGround.CompressBounds();
        mapOneWay.CompressBounds();
        BoundsInt boundsGround = mapGround.cellBounds;
        BoundsInt boundsOneWay = mapOneWay.cellBounds;

        
        int minX = Mathf.Min(boundsGround.min.x, boundsOneWay.min.x) + 2;
        int minY = Mathf.Min(boundsGround.min.y, boundsOneWay.min.y) ;

        int maxX = Mathf.Max(boundsGround.max.x, boundsOneWay.max.x) - 2;
        int maxY = Mathf.Max(boundsGround.max.y, boundsOneWay.max.y) + 6;

        PosEnemy.Clear();

        foreach(var pos in boundsGround.allPositionsWithin){
            if(mapGround.GetColliderType(pos) == Tile.ColliderType.Sprite){
                if( (pos.x > minX + offsetX) && (pos.x < maxX - offsetX) ){
                    if( mapGround.HasTile( new Vector3Int(pos.x, pos.y+1, 0) ) ) continue;
                    PosEnemy.Add(new Vector2(pos.x +0.5f ,pos.y + 1f));
                }
            }
        }
        foreach(var pos in boundsOneWay.allPositionsWithin){
            if(mapOneWay.GetColliderType(pos) == Tile.ColliderType.Sprite){
                if( (pos.x > minX + offsetX) && (pos.x < maxX -offsetX ) ){
                    PosEnemy.Add(new Vector2(pos.x +0.5f ,pos.y + 1.1f));
                }
            }
        }

        Offset = new Vector2i( (int)minX, (int)minY);

        mWidth = (int)maxX - (int)minX;
        mHeight = ( (int)maxY - (int)minY );

        polygonCollider = GetComponent<PolygonCollider2D>();
        Vector2 point1 = new Vector2(minX,maxY);
        Vector2 point2 = new Vector2(maxX,maxY);
        Vector2 point3 = new Vector2(maxX,minY);
        Vector2 point4 = new Vector2(minX,minY);

        polygonCollider.pathCount = 1;
        polygonCollider.SetPath(0, new Vector2[]{point1,point2,point3,point4});

        for (int y = boundsGround.min.y; y < boundsGround.max.y ; y++)
        {
            for (int x = boundsGround.min.x; x < boundsGround.max.x; x++)
            {
                mapGround.SetColliderType(new Vector3Int(x, y,0), Tile.ColliderType.Grid);
            }
        }

    }
    
    void SetData(){
        tiles = new TileType[mWidth,mHeight];
        mGrid = new byte[ Mathf.NextPowerOfTwo(mWidth), Mathf.NextPowerOfTwo(mHeight)];
        InitPathFinder();

        for(int y = 0; y < mHeight; y++){
            for(int x = 0; x < mWidth; x++){
                Vector3Int pos = new Vector3Int(x + Offset.x ,y + Offset.y, 0);
                if( mapGround.HasTile(pos) ){
                    SetTile(x,y,TileType.Block);
                    if(mapOneWay.GetColliderType(pos) == Tile.ColliderType.Sprite){
                    }
                }
                else if( mapOneWay.HasTile( pos) && mapOneWay.GetColliderType(pos) == Tile.ColliderType.Sprite){
                    SetTile(x,y,TileType.OneWay);
                }
                else{
                    SetTile(x,y,TileType.Empty);
                }
            }
        }
    }

    public static List<Vector2> GetPosEnemy(){
        return Instance.PosEnemy;
    }
    
    void SetTile(int x,int y,TileType tileType){

        if (x < 0 || x >= mWidth || y < 0 || y >= mHeight){
            Common.LogError("Out of bounds Size Map");
            return;
        }

        tiles[x,y] = tileType;
        switch(tileType){
            case TileType.Empty:
                mGrid[x, y] = 1;
                break;
            case TileType.Block:
                mGrid[x, y] = 0;
                break;
            case TileType.OneWay:
                mGrid[x, y] = 1;
                break;
        }

    }

    public TileType GetTile(int x, int y) 
	{
        if (x < 0 || x >= mWidth || y < 0 || y >= mHeight)
            return TileType.Block;

		return tiles[x, y]; 
	}
    public Vector2i GetMapTileAtPoint( Vector2 point )
	{
		return new Vector2i( Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y) ) - Offset;
	}
    public bool IsOneWayPlatform(int x, int y)
    {
        if (x < 0 || x >= mWidth
            || y < 0 || y >= mHeight)

            return false;

        return (tiles[x, y] == TileType.OneWay);
    }
    public bool IsGround(int x, int y)
    {
        if (x < 0 || x >= mWidth
           || y < 0 || y >= mHeight)

            return false;

        return ( tiles[x, y] == TileType.OneWay || tiles[x, y] == TileType.Block );
    }

    public bool IsObstacle(int x, int y)
    {
        if (x < 0 || x >= mWidth
            || y < 0 || y >= mHeight)

            return true;

        return ( tiles[x, y] == TileType.Block );
    }
    [ContextMenu("Load_Map")]
    void LoadMap(){
        var allTilemaps = GetComponentsInChildren<Tilemap>();
        for(int i = 0; i < allTilemaps.Length; i++){
            LoadTile(allTilemaps[i], mapData_SO.dataTileMaps[i]);
        }
    }
    void LoadTile(Tilemap _tilemap, DataTile _dataTile){
        _tilemap.ClearAllTiles();
        foreach(var tileInfo in _dataTile.dataTileMaps){
            _tilemap.SetTile(tileInfo.position, tileInfo.tileAsset);
            _tilemap.SetTransformMatrix(tileInfo.position, tileInfo.transformMatrix);
        }
    }

#if UNITY_EDITOR
    
    [Button]
    void SaveTiles(){
        var allTilemaps = GetComponentsInChildren<Tilemap>();
        mapData_SO.dataTileMaps = new DataTile[allTilemaps.Length];
        for(int i = 0; i < allTilemaps.Length; i++){
            DataTile temp = new DataTile();
            temp.name = allTilemaps[i].name;
            temp.dataTileMaps = GetTileInfo(allTilemaps[i]);
            mapData_SO.dataTileMaps[i] = temp;
        }
        EditorUtility.SetDirty(mapData_SO);
    }
    List<TileInfo> GetTileInfo(Tilemap _tilemap){
        BoundsInt bounds = _tilemap.cellBounds;
        List<TileInfo> tileInfos = new List<TileInfo>();
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = _tilemap.GetTile(pos);
            Matrix4x4 matrix = _tilemap.GetTransformMatrix(pos);
            if(tile != null){
                TileInfo tileInfo = new TileInfo();
                tileInfo.position = pos;
                tileInfo.transformMatrix = matrix;
                tileInfo.tileAsset = tile;

                tileInfos.Add( tileInfo );
            }
        }
        return tileInfos;
    }

    void OnDrawGizmos()
    {
        ShowValue();
    }
    void ShowValue(){

        float size = 0.5f;
        if(mGrid == null) return; 
        if(!showTypeTile) return;
        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube( new Vector3(0,0) ,new Vector3( mGrid.GetUpperBound(0) + 1, mGrid.GetUpperBound(1) + 1,0));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3((float)mWidth/2,(float)mHeight/2,0) ,new Vector3(mWidth,mHeight,0));

        // for(int y = 0; y < mGrid.GetUpperBound(1) + 1 ; y++){
        //     for(int x = 0; x < mGrid.GetUpperBound(0) + 1; x++){
        //         Vector3 position = new Vector3( x + size + Offset.x, y + size + Offset.y, 0);
        //         Color color = Color.white;
        //         switch( GetTile(x,y) ){
        //             case TileType.Empty:
        //                 continue;
        //             case TileType.Block:
        //                 color = Color.red;
        //                 break;
        //             case TileType.OneWay:
        //                 color = Color.green;
        //                 break;
        //         }
        //         Gizmos.color = color;
        //         Gizmos.DrawSphere(position, size/2);
        //     }
        // }

        if( PosEnemy.Count > 0 ) {
            foreach(var pos in PosEnemy){
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(pos, size/2);
            }
        }
    }
    [ContextMenu("Remove_Tiles")]
    void ResetMap(){
        var allTilemaps = GetComponentsInChildren<Tilemap>();
        foreach(var tilemap in allTilemaps){
            tilemap.ClearAllTiles();
        }
    }
    
#endif
    
}