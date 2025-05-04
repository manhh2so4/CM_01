using UnityEngine;

[CreateAssetMenu(fileName = "LevelPlayer_SO", menuName = "LevelPlayer_SO", order = 0)]
public class LevelPlayer_SO : ScriptableObject {
    public LevelPlayer[] levelPlayer;
}
[System.Serializable]
public class LevelPlayer{
    public int level;
    public int xpToNextLevel;
    public int HP;
    public int ATK;
    public int DEF;
    public LevelPlayer(int _level, int _xpToNextLevel, int _HP, int _ATK, int _DEF){
        this.level = _level;
        this.xpToNextLevel = _xpToNextLevel;
        this.HP = _HP;
        this.ATK = _ATK;
        this.DEF = _DEF;
    }
}

