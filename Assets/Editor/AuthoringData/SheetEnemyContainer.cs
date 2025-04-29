
using System.Collections.Generic;
using System.Linq;
using Cathei.BakingSheet;
using Cathei.BakingSheet.Unity;
using UnityEditor;
using UnityEngine;

public class EnemySheet : Sheet<EnemySheet.Row>
{
    public class Row : SheetRow
    {
        public int idEnemy { get; set; }
        public int type { get; set; }
        public string Name { get; set; }
        public int speedMove { get; set; }
        public int speedAtk { get; set; }
        public int Hp { get; set; }
        public int timeReSpont { get; set; }
        public int damage { get; set; }
    }

}
[System.Serializable]
public class SheetEnemyContainer : BaseSheetContainer{
    public EnemySheet Enemy { get; set;}
    public override void BakeData(){
        EnemySheet.Row[] enemySheet = Enemy.ToArray();
        string assetPath = "Assets/Resources/Enemy_Load/Enemy/";
        foreach( var enemy in enemySheet ){
            
            string namePAth = assetPath + enemy.Id + ".asset";
            Enemy_SO enemySO = AssetDatabase.LoadAssetAtPath<Enemy_SO>( namePAth );

            if (enemySO == null)
            {
                enemySO = ScriptableObject.CreateInstance<Enemy_SO>();
                AssetDatabase.CreateAsset(enemySO, namePAth);
            }

            enemySO.idEnemy = enemy.idEnemy;
            enemySO.Name = enemy.Name;
            enemySO.Hp = enemy.Hp;
            enemySO.type = enemy.type;
            enemySO.speedMove = enemy.speedMove;
            enemySO.speedAtk = enemy.speedAtk;
            enemySO.timeReSpont = enemy.timeReSpont;
            enemySO.damage = enemy.damage;

            EditorUtility.SetDirty(enemySO);
        }
    } 
}
public class Enemy_SOEditor : BaseSheetContainer{
    static string folderPathSprite = "Assets/_Sprites/Enemy/EnemyDefau/";
    static string folderPathEnemy = "Enemy_Load/Enemy/";
    [MenuItem("Tools/LoadSpritesEnemy")]
    static void LoadSpritesFromFolder(){
        string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { folderPathSprite });
        Sprite[] sprites = new Sprite[guids.Length];
        for(int i = 0; i < guids.Length; i++){
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            sprites[i] = AssetDatabase.LoadAssetAtPath<Sprite>(path);
        }
        Enemy_SO[] foundObjects = Resources.LoadAll<Enemy_SO>(folderPathEnemy);
        foreach(var enemy in foundObjects){

            List<Sprite> listSprite = new List<Sprite>();
            foreach(var sprite in sprites){
                if(  GetIdEnemy(sprite.name) == enemy.idEnemy ){
                    
                    listSprite.Add(sprite);
                }
            }

            enemy.Sprites = listSprite.ToArray();
            listSprite.Clear();
            EditorUtility.SetDirty(enemy);
        }
    }
    public static int GetIdEnemy(string text){
        int index = text.IndexOf('_');
        if (index == -1)
        {
            return -1;
        }
        return int.Parse(text.Substring(0, index));
    }
}

