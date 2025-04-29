using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelBattle", menuName = "Data/BattleData", order = 0)]
public class BattleData : ScriptableObject {
    public Wave[] listWave;
}

[System.Serializable]
public class Wave{
    public string nameWave;
    public int CountEnemy => CountE();
    public EnemyInfo[] listEnemy;
    int CountE(){
        int count = 0;
        foreach(var enemy in this.listEnemy){
            count += enemy.count;
        }
        return count;
    }
}

[System.Serializable]
public struct EnemyInfo{
    const string name = "enemy";
    public int idEnemy;
    public int count;
    public EnemyInfo(int _idEnemy, int _count){
        idEnemy = _idEnemy;
        count = _count;
    }
}

