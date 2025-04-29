using System.Collections;
using UnityEngine;

public class BaseBuffStat : MonoBehaviour,IObjectPoolItem{
    [SerializeField] protected float duration;
    protected const float tickInterval = 0.5f;
    [SerializeField] protected float CountDownTime;
    public BuffType buffType;
    public virtual void StartBuff(int valuePerTick, float duration,Stat stat,Sprite icon = null){
        this.duration = duration;
        StartCoroutine(PlussOverTime(valuePerTick,stat));
    }

    protected virtual IEnumerator PlussOverTime(int valuePerTick,Stat stat)
    {
        float timer = 0f;
        bool infiniteDuration = duration <= 0;
        while (infiniteDuration || timer < duration)
        {
            CountDown(duration - timer);
            stat.CurrentValue += valuePerTick;
            yield return new WaitForSeconds(tickInterval);
            
            if (!infiniteDuration)
            {
                timer += tickInterval;
            }
        }

        if (!infiniteDuration)
        {
            Remove();
        }
    }
    protected virtual void CountDown(float time){
        CountDownTime = time;
    }
    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }
    public void Remove(){
        ReturnToPool();
    }
#region CreatPool
    ObjectPool objectPool;
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