using System.Collections;
using System.Collections.Generic;
using HStrong.Saving;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour,ISaveable {
    public enum StateBattle { Idle, Active, BattleOver}
    [SerializeField] BattleData levelBattle;
    [SerializeField] StateBattle stateBattle;
    [SerializeField] Enemy enemyPrefab;
    //----------------- var Combat
    List<Vector2> ListPos ;
    [SerializeField] List<EnemyEntity> ListEnemys;
    [SerializeField] InventoryItemSO itemKey;
    int Count;

    void Awake(){
        stateBattle = StateBattle.Idle;
    }
    void OnEnable()
    {
        this.GameEvents().miscEvents.onKillEnemy += IsDead;
    }
    void OnDisable()
    {
        this.GameEvents().miscEvents.onKillEnemy -= IsDead;
        
    }

    void IsDead(EnemyEntity _enemy){
        if(ListEnemys.Contains(_enemy)){
            ListEnemys.Remove(_enemy);
            if(ListEnemys.Count == 0){
                DropKey(_enemy.transform.position);
            }
        }
    }
    void DropKey(Vector2 pos){
        Vector2 randomVelocity = new Vector2( Random.Range(-4, 4), Random.Range(5, 8));
        itemKey.SpawnPickup(pos, 1 ,randomVelocity);
    }
    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("OnTriggerEnter2D");
        if(other.gameObject.CompareTag("Player")){
            TriggerEvent();
        }
    }

    void TriggerEvent(){
        if(stateBattle == StateBattle.Idle){
            stateBattle = StateBattle.Active;
            StartBattle();
        }
    }


    [Button]
    void StartBattle(){
        Debug.Log("StartBattle");
        stateBattle = StateBattle.Active;
        StartCoroutine(Battle());
    }

    IEnumerator Battle(){
        ListPos = new List<Vector2>( Map.GetPosEnemy() );
        Count = ListPos.Count;
        ListEnemys.Clear();
        
        foreach(var wave in levelBattle.listWave){  
            foreach(var enemy in levelBattle.listWave[0].listEnemy){
                for(int i = 0; i < enemy.count; i++){
                    Enemy enemyadd = SpawnEnemy(enemy.idEnemy, GetRandomPos());
                    ListEnemys.Add(enemyadd);
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        yield return null;
    }
    
    Vector2 GetRandomPos(){
        if(Count <= 0) Count = ListPos.Count;

        int randomIndex = Random.Range(0, Count);
        Vector2 randomPos = ListPos[randomIndex];
        ListPos[randomIndex] = ListPos[Count - 1];
        Count--;

        return randomPos;
    }

    Enemy SpawnEnemy(int idEnemy, Vector2 pos){

        Enemy enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
        enemy.Load_Enemy(idEnemy);
        return enemy;

    }

    [System.Serializable]
    class BattleRestore{
        //public StateBattle _State;
        public bool _isActive;
        public int[] _dataEnemys;
        public SerializableVector3[] _dataPos;
    }

    public object CaptureState()
    {
        BattleRestore battleRestore = new BattleRestore();

        battleRestore._isActive = stateBattle == StateBattle.Active;
        
        battleRestore._dataEnemys = new int[ListEnemys.Count];
        battleRestore._dataPos = new SerializableVector3[ListEnemys.Count];
        for(int i = 0; i < ListEnemys.Count; i++){
            battleRestore._dataEnemys[i] = ListEnemys[i].idEnemy;
            battleRestore._dataPos[i] = new SerializableVector3(ListEnemys[i].transform.position);
        }
        
        return battleRestore;
    }

    public void RestoreState(object state)
    {
        BattleRestore battleRestore = (BattleRestore)state;

        stateBattle = battleRestore._isActive ? StateBattle.Active : StateBattle.Idle;
        
        if( stateBattle == StateBattle.Active){
            ListEnemys.Clear();
            
            for(int i = 0; i < battleRestore._dataEnemys.Length; i++){
                Enemy enemy = SpawnEnemy(battleRestore._dataEnemys[i], battleRestore._dataPos[i].ToVector() );
                ListEnemys.Add(enemy);
            }
        }
    }

#if UNITY_EDITOR
    // void OnDrawGizmos(){
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawLine(transform.position + new Vector3(spawnRange/2, 0, 0), transform.position + new Vector3(-spawnRange/2, 0, 0));
    // }
#endif

}