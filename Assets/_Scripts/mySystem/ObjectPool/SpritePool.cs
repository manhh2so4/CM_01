using UnityEngine;

public class SpritePool : MonoBehaviour, IObjectPoolItem
{
    public SpriteRenderer spriteRenderer;
    void Awake()
    {
        if(spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public Sprite Sprite{ set => spriteRenderer.sprite = value; }
    public Vector2 Position{ set => transform.localPosition = value; }
    public int SortingOrder{ set => spriteRenderer.sortingOrder = value; }
    public int SortingLayerID{ set => spriteRenderer.sortingLayerID = value; }

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