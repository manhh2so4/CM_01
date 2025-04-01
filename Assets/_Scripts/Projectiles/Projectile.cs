using System;
using NaughtyAttributes;
using UnityEngine;
namespace HStrong.ProjectileSystem{
    public class Projectile : MonoBehaviour, IObjectPoolItem
    {
        public event Action OnInit;
        public event Action OnReset;
        [HideInInspector]public Transform target;
        public float speed;
        [HideInInspector] public CharacterStats stats;
        public Vector2 Dir;
        [HideInInspector] public int damage;
  
        public void SetProjectile(float speed, Vector2 _dir, string tag,CharacterStats stats)
        {
            
            this.speed = speed;
            this.Dir = _dir;
            SetProjectile( tag, stats);

        }

        public void SetProjectile(float speed, Transform target, string tag,CharacterStats stats)
        {
            this.speed = speed;
            this.target = target;
            SetProjectile( tag, stats);
        }


        public void SetProjectile(string tag, CharacterStats stats)
        {
            this.stats = stats;
            gameObject.tag = tag;
            Init();
        }
        public void SetProjectile(string tag, int damage)
        {
            this.damage = damage;
            gameObject.tag = tag;
            Init();
        }
        private
        void Init()
        {
            OnInit?.Invoke();
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
            if (objectPool != null)
            {
                objectPool.ReturnObject(this);
                this.transform.SetParent(PoolsContainer.Instance.transform);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void SetObjectPool(ObjectPool pool)
        {
            objectPool = pool;
        }

        public void Release()
        {
            objectPool = null;
        }
        #endregion
    }
}