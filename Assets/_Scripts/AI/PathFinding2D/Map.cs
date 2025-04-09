using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;
using Algorithms;
public enum TileType
{
    Empty,
    Block,
    OneWay
}
public class Map : MonoBehaviour {
    public PathFinderFast mPathFinder;
    public Vector3 position;
    byte[,] mGrid;
    private TileType[,] tiles;
    [SerializeField] Tilemap mapGround,mapOneWay;

    [Header("Tile Vieww")]
    [SerializeField] bool showTypeTile;
    [SerializeField] Vector2 Center;
    public Vector2i Offset;
    [SerializeField] int mWidth, mHeight;
    [Button]
    void Awake()
    {
        LoadComponent();
        SetData();
    }
    public void InitPathFinder()
	{
        mPathFinder = new PathFinderFast( mGrid, this);
		
		mPathFinder.Formula                 = HeuristicFormula.Manhattan;
		//if false then diagonal movement will be prohibited
        mPathFinder.Diagonals               = false;
		//if true then diagonal movement will have higher cost
        mPathFinder.HeavyDiagonals          = false;
		//estimate of path length
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

        float minX = Mathf.Min(boundsGround.min.x, boundsOneWay.min.x);
        float minY = Mathf.Min(boundsGround.min.y, boundsOneWay.min.y);

        float maxX = Mathf.Max(boundsGround.max.x, boundsOneWay.max.x);
        float maxY = Mathf.Max(boundsGround.max.y, boundsOneWay.max.y);
        Center = new Vector2((minX + maxX) / 2, (minY + maxY) / 2);

        Offset = new Vector2i( (int)minX, (int)minY);

        mWidth = (int)maxX - (int)minX;
        mHeight = (int)maxY - (int)minY;

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
        Gizmos.DrawWireCube(Center ,new Vector3(mWidth,mHeight,0));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3((float)mWidth/2,(float)mHeight/2,0) ,new Vector3(mWidth,mHeight,0));
        for(int y = 0; y < mHeight; y++){
            for(int x = 0; x < mWidth; x++){
                Vector3 position = new Vector3( x + size + Offset.x, y + size + Offset.y, 0);
                Color color = Color.white;
                switch( GetTile(x,y) ){
                    case TileType.Empty:
                        color = Color.white;
                        break;
                    case TileType.Block:
                        color = Color.red;
                        break;
                    case TileType.OneWay:
                        color = Color.green;
                        break;
                }
                Gizmos.color = color;
                Gizmos.DrawSphere(position, size/2);
            }
        }

    }
    void SetData(){
        tiles = new TileType[mWidth,mHeight];
        mGrid = new byte[ Mathf.NextPowerOfTwo(mWidth), Mathf.NextPowerOfTwo(mHeight) ];
        InitPathFinder();

        for(int y = 0; y < mHeight; y++){
            for(int x = 0; x < mWidth; x++){
                Vector3Int pos = new Vector3Int(x + Offset.x ,y + Offset.y, 0);
                if( mapGround.HasTile(pos) ){
                    SetTile(x,y,TileType.Block);
                }
                else if( mapOneWay.HasTile( pos) ){
                    SetTile(x,y,TileType.OneWay);
                }
                else{
                    SetTile(x,y,TileType.Empty);
                }
            }
        }
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
                mGrid[x, y] = 0;
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
    
}