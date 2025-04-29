using System.Collections;
using System.Collections.Generic;
using HStrong.ProjectileSystem;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy ", menuName = "GameData/Enemy_SO")]
public class Enemy_SO : ScriptableObject
{
        [Header("Infor")]
        [SpritePreview] public Sprite[] Sprites;
        public Projectile projectile;
        public int idEnemy;      
        public int type;
        [Header("Property")]
        public string Name;
        public float speedMove;
        public int speedAtk = 1;
        public int damage = 1;
        public int Hp = 100;
        public float timeReSpont;
        [Header("Set Drop")]
        public int countDrop;

        [field : SerializeField] public DropInfo[] dropInfo {get; private set;}
 
}
