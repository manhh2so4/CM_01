using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyBig ", menuName = "GameData/EnemyBig_SO")]
public class EnemyBoss_SO : ScriptableObject
{
        public Texture2D TEXTURE2D;
        public ImageInfor[] imageInfors;
        public FrameBoss[] frameBoss;
        public int[] frameBossMove;
        public BossAttack[] BossAttacks;

        public float speed_move;
        public float speed_attack;
        public float RangeMove;

}

[System.Serializable]
public class BossAttack{
        public int[] attack;
}
