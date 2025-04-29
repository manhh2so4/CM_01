using System;
using NaughtyAttributes;
using UnityEngine;
namespace HStrong.ProjectileSystem{
    public class Projectile : MonoBehaviour, IObjectPoolItem
    {
        public event Action OnInit;
        public event Action OnReset;
        public float life;
        float startTime;
        [HideInInspector] public Vector2 Dir;
        [HideInInspector] public float speed;
        [HideInInspector]public Transform target;
        [HideInInspector]public CharacterStats stats;
        [HideInInspector]public int damage;
  
        public void SetData(float speed, Vector2 _dir, string tag = "",CharacterStats stats = null)
        {
            
            this.speed = speed;
            this.Dir = _dir;
            SetData( tag, stats);

        }

        public void SetData(float speed, Transform target, string tag,CharacterStats stats)
        {
            this.speed = speed;
            this.target = target;
            SetData( tag, stats);
        }
        

        public void SetData(string tag, CharacterStats stats)
        {
            this.stats = stats;
            if(tag != "") gameObject.tag = tag;
            Init();
        }

        public void SetData(string tag, int damage)
        {
            this.damage = damage;
            gameObject.tag = tag;
            Init();
        }

        void Init()
        {
            startTime = 0;
            OnInit?.Invoke();
        }

        void Update() {

            if(life <= 0) return;
            
            startTime += Time.deltaTime;
            if( startTime > life){
                Destroy();
            }
        }

        void ReFresh()
        {
           target = null;
           speed = 0;
           Dir = Vector2.zero;
           stats = null;
           OnReset?.Invoke();
        }

        public void Destroy()
        {  
            ReFresh();
            ReturnItemToPool();
            
        }
#region CreatPool
        ObjectPool objectPool;
        void ReturnItemToPool()
        {
            ReturnToPool();
        }
        
        public void SetObjectPool(ObjectPool pool)
        {
            objectPool = pool;
        }

        public void Release()
        {
            objectPool = null;
        }

        public void ReturnToPool()
        {
            if (objectPool != null)
            {
                objectPool.ReturnObject(this);
                this.transform.SetParent(PoolsContainer.Instance.transform);
            }else
            {
                Destroy(gameObject);
            }
        }
 #endregion
    }
}