
using System.Collections.Generic;
using System.Linq;
using Cathei.BakingSheet;
using Cathei.BakingSheet.Unity;
using Google.Apis.Sheets.v4.Data;
using UnityEditor;
using UnityEngine;

public class LevelPlayerSheet : Sheet<LevelPlayerSheet.Row>
{
    public class Row : SheetRow
    {
        public int Level { get; set; }
        public int xpToNextLevel { get; set; }
        public int HP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
    }
}

[System.Serializable]
public class SheetLevelPlayerContainer : BaseSheetContainer{
    public LevelPlayerSheet levelPlayer {get; set;}

    //public SkillPassiveSheet SkillPassive { get; set; }
    public override void BakeData(){
        string assetPath = "Assets/Data/LevelPlayer.asset";
        LevelPlayer_SO levelPlayerSO = AssetDatabase.LoadAssetAtPath<LevelPlayer_SO>( assetPath );
        if(levelPlayerSO == null){
            levelPlayerSO = ScriptableObject.CreateInstance<LevelPlayer_SO>();
            AssetDatabase.CreateAsset(levelPlayerSO, assetPath);
        }
        LevelPlayer[] levelPlayers = new LevelPlayer[levelPlayer.Count];
        for(int i = 0; i < levelPlayer.Count; i++){
            levelPlayers[i] = new LevelPlayer(levelPlayer[i].Level, levelPlayer[i].xpToNextLevel, levelPlayer[i].HP, levelPlayer[i].ATK, levelPlayer[i].DEF);
        }


        levelPlayerSO.levelPlayer = levelPlayers;
        EditorUtility.SetDirty(levelPlayerSO);
        
    }

}