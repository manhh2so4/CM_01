using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class FindPathHandle : MonoBehaviour {
    //--------Input Action--------
    public int amountJump;
    public bool isFollowPath;
    public int moveInput;
    public bool jumpInput;
    //------------------------
    public PlayerInputHandler inputPlayer ;
    Movement movement;
    public Map map;
    public List<Vector2i> mPath = new List<Vector2i>();
    public int mCurrentNodeId = -1;
    
    Vector2 prevDest, currentDest, nextDest;
    Vector2 pathPosition;
    [SerializeField] bool destOnGround, reachedY, reachedX;

    public void SetMovement(Movement _movement){
        movement = _movement;
    }
    void OnEnable(){
        inputPlayer.OnClickPos += OnClickPos;
    }
    void OnDisable(){
        inputPlayer.OnClickPos -= OnClickPos;
    }
    void Update()
    {
        UpdatePath();
    }
    [Button]
    void Jump(){
        amountJump = 3;
        jumpInput = true;
    }

    void OnClickPos(Vector2i _pos){
        Vector2i destination = _pos - map.Offset;
        for(int i = 0; i < 100; i++){
            destination.y--;
            if( map.IsGround(destination.x, destination.y)){ 
                destination.y++;
                break;
            }
        }
        MoveTo(destination);
        pos.Set(destination.x + map.Offset.x, destination.y + map.Offset.y);
    }

    public void MoveTo(Vector2i destination)
    {
        Vector2i startTile = map.GetMapTileAtPoint(transform.position);
        List<Vector2i> path = map.mPathFinder.FindPath( startTile , destination , 1, 1, 3 );

        mPath.Clear();

        if (path != null && path.Count > 1)
        {
            for (var i = path.Count - 1; i >= 0; --i)
                mPath.Add(path[i]);
            
            mCurrentNodeId = 1;
            amountJump = GetJumpFramesForNode(0);
            isFollowPath = true;

        }else{
            mCurrentNodeId = -1;
        }
    }
    public bool ReachedNodeOnXAxis(Vector2 pathPosition, Vector2 prevDest, Vector2 currentDest)
    {
        return (prevDest.x <= currentDest.x && pathPosition.x >= currentDest.x )
            || (prevDest.x >= currentDest.x && pathPosition.x <= currentDest.x )
            || (Mathf.Abs( pathPosition.x - currentDest.x ) <= StaticValue.cBotMaxPositionError);
    }
    public bool ReachedNodeOnYAxis(Vector2 pathPosition, Vector2 prevDest, Vector2 currentDest)
    {
        return (prevDest.y <= currentDest.y && pathPosition.y >= currentDest.y)
            || (prevDest.y >= currentDest.y && pathPosition.y <= currentDest.y)
            || (Mathf.Abs( pathPosition.y - currentDest.y ) <= StaticValue.cBotMaxPositionError);
    }
    public void GetContext(out Vector2 prevDest, out Vector2 currentDest, out Vector2 nextDest, out bool destOnGround, out bool reachedX, out bool reachedY)
    {
        prevDest = new Vector2( mPath[mCurrentNodeId - 1].x, mPath[mCurrentNodeId - 1].y );
        currentDest = new Vector2( mPath[mCurrentNodeId].x, mPath[mCurrentNodeId].y );

        nextDest = currentDest;

        if ( mPath.Count > mCurrentNodeId + 1 )
        {
            nextDest = new Vector2( mPath[mCurrentNodeId + 1].x , mPath[mCurrentNodeId + 1].y );
        }

        destOnGround = false;

        for (int x = mPath[mCurrentNodeId].x ; x < mPath[mCurrentNodeId].x + movement.mWidth ; ++x )
        {
            if( map.IsGround(x, mPath[mCurrentNodeId].y - 1))
            {
                destOnGround = true;
                break;
            }
        }

        pathPosition = (Vector2)transform.position - new Vector2( map.Offset.x + 0.5f, map.Offset.y) ;

        reachedX = ReachedNodeOnXAxis(pathPosition, prevDest, currentDest);
        reachedY = ReachedNodeOnYAxis(pathPosition, prevDest, currentDest);

        //snap the character if it reached the goal but overshot it by more than cBotMaxPositionError
        if ( reachedX && Mathf.Abs(pathPosition.x - currentDest.x) > StaticValue.cBotMaxPositionError && movement.isGround() )
        {
            if(Mathf.Abs(pathPosition.x - currentDest.x) > StaticValue.cBotMaxPositionError*3){
                reachedX = false;
            }else{
                Debug.Log("snap");
                pathPosition.x = currentDest.x;
                transform.position = pathPosition + new Vector2(map.Offset.x + 0.5f, map.Offset.y);
            }
        }

        if(destOnGround && !movement.isGround()){
            reachedY = false;
        }
    }

    public int GetJumpFramesForNode(int prevNodeId)
    {
        int nextNode = prevNodeId + 1;

        if ( mPath[nextNode].y - mPath[prevNodeId].y > 0 && movement.isGround() )
        {
            int jumpHeight = 1;
            for (int i = nextNode; i < mPath.Count; ++i)
            {
                if (mPath[i].y - mPath[prevNodeId].y >= jumpHeight)
                    jumpHeight = mPath[i].y - mPath[prevNodeId].y;

                if (mPath[i].y - mPath[prevNodeId].y < jumpHeight || map.IsGround(mPath[i].x, mPath[i].y - 1))
                    return jumpHeight;
            }
        }
        return 0;
    }

    public void UpdatePath(){

        if( isFollowPath == false ) return;

        if(mCurrentNodeId == -1 || mPath.Count <= mCurrentNodeId)  return;

        GetContext(out prevDest, out currentDest, out nextDest, out destOnGround, out reachedX, out reachedY);
        moveInput = 0;
        jumpInput = false;
        float sqrDistance = (pathPosition - nextDest).sqrMagnitude;
        

        if (reachedX && reachedY)
        {
            int prevNodeId = mCurrentNodeId;
            mCurrentNodeId++;

            if( mCurrentNodeId >= mPath.Count ){
                
                if( sqrDistance < StaticValue.cBotMaxPositionError ){

                    Debug.Log("end find path");

                    mCurrentNodeId = -1;
                    moveInput = 0;
                    jumpInput = false;
                    isFollowPath = false;
                    return;
                }
                return;
            }

            if( movement.isGround() ) amountJump = GetJumpFramesForNode(prevNodeId);

        }else if( !reachedX ){
            //moveInput = Mathf.Clamp(currentDest.x - pathPosition.x, -1, 1);

            if( currentDest.x - pathPosition.x > StaticValue.cBotMaxPositionError ) //    * |
                moveInput = 1;
            else if( pathPosition.x - currentDest.x > StaticValue.cBotMaxPositionError ) //  | *
                moveInput = -1;

        }else if( !reachedY && ( mPath.Count > mCurrentNodeId + 1 ) && !destOnGround
            //&& (sqrDistance >= StaticValue.cBotMaxPositionError*2 )
            ){

            //moveInput = Mathf.Clamp(nextDest.x - pathPosition.x, -1, 1);
            if( nextDest.x - pathPosition.x > StaticValue.cBotMaxPositionError ) //  * |
                moveInput = 1;
            else if( pathPosition.x - nextDest.x > StaticValue.cBotMaxPositionError ) //  | *
                moveInput = -1;

            if( Mathf.Abs(currentDest.x - nextDest.x) < StaticValue.cBotMaxPositionError || Mathf.Abs(currentDest.x - prevDest.x) < StaticValue.cBotMaxPositionError ){
                moveInput = 0;
            }

            if( ReachedNodeOnXAxis(pathPosition, currentDest, nextDest ) && ReachedNodeOnYAxis( pathPosition, currentDest, nextDest ))
            {
                mCurrentNodeId ++;
                return;
            }

        }

        if( amountJump > 0 &&  movement.isGround()){
            jumpInput = true;
        }
        
        if( movement.Velocity.magnitude < 2f ){
            if(TimeAction(1f)){
                Debug.Log("MoveTo");
                MoveTo(mPath[mPath.Count-1]);
            }
        }else{
            timeAction = 0;
        }

    }

    float timeAction = 0;
    protected bool TimeAction(float timeWait){        
        timeAction += Time.deltaTime;
        if(timeAction >= timeWait){
            timeAction = 0;
            return true;
        }
        return false;
    }
#if UNITY_EDITOR
    Vector2 pos;
    void OnDrawGizmos()
    {

        DrawPath();
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere( pos , .1f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere( pathPosition + new Vector2( map.Offset.x , map.Offset.y) , .1f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere( currentDest + new Vector2( map.Offset.x , map.Offset.y) + Vector2.one * 0.5f , .1f );
        Gizmos.color = Color.red;
        Gizmos.DrawSphere( prevDest + new Vector2( map.Offset.x , map.Offset.y) + Vector2.one * 0.5f , .1f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere( nextDest + new Vector2( map.Offset.x , map.Offset.y) + Vector2.one * 0.5f , .1f );

        DrawInput();
        
    }
    void DrawPath()
    {

        if (mPath != null && mPath.Count > 0)
        {
            Vector2i start = mPath[0];

            float size = 0.5f;

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(  new Vector3(start.x + map.Offset.x + size,start.y + map.Offset.y + size, 0f ), .05f);

            for (var i = 1; i < mPath.Count; ++i)
            {
                var end = mPath[i];
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere( new Vector3(end.x + map.Offset.x + size, end.y + map.Offset.y + size, 0f), 0.05f);

                Gizmos.color = Color.red;
                Gizmos.DrawLine( new Vector3(start.x + map.Offset.x + size, start.y + map.Offset.y + size, 0f),
                                 new Vector3(end.x + map.Offset.x + size, end.y + map.Offset.y + size, 0f) );

                start = end;
            }

        }
    }
    void DrawInput(){
        Gizmos.color = Color.red;
        Gizmos.DrawRay( transform.position + transform.up * 0.6f , Vector2.right * moveInput);
        if(jumpInput){
            Gizmos.color = Color.green;
            Gizmos.DrawRay( transform.position + transform.up * 0.6f , Vector2.up*1);
        }
    }
#endif
}