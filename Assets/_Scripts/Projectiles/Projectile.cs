using System;
using NaughtyAttributes;
using UnityEngine;
namespace HStrong.ProjectileSystem{
    public class Projectile : MonoBehaviour, IObjectPoolItem
    {
        public event Action OnInit;

        public Transform target;
        public float speed;
        public CharacterStats stats;
        public Vector2 Dir;
  
        public virtual void SetProjectile(float speed, Vector2 _dir, string tag,CharacterStats stats)
        {
            
            this.speed = speed;
            this.Dir = _dir;
            SetProjectile( tag, stats);

        }

        public virtual void SetProjectile(float speed, Transform target, string tag,CharacterStats stats)
        {
            this.speed = speed;
            this.target = target;
            SetProjectile( tag, stats);
        }

        public virtual void SetProjectile(string tag,CharacterStats stats)
        {
            this.stats = stats;
            gameObject.tag = tag;
            Init();
        }
        public void Init()
        {
            OnInit?.Invoke();
        }
        public void Destroy()
        {
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