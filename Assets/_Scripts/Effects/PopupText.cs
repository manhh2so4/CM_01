using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour,IPrefab,IObjectPoolItem {
    private const float DISAPPEAR_TIMER_MAX = .5f;

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector; 
    private float ScaleAmount = 1f;
    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }
    public void Setup(int dameAmount, bool isCriticalHit, int dir){

        ResetValue();
        textMesh.SetText((-dameAmount).ToString());

        if(!isCriticalHit){
            textMesh.fontSize = 5;
            textColor = Color.yellow;
        }else{
            textMesh.fontSize = 6;
            textColor = Color.red;
        }

        textMesh.color = textColor;
        
        textMesh.sortingOrder = 1;
        float randomX = Random.Range(-.6f, 0.6f);
        float randomY = Random.Range(.2f, 0.4f);
        moveVector = new Vector3(randomX,.3f) * 6;
        moveVector.x *= -dir;

    }
    void ResetValue()
    {
        transform.localScale = Vector3.one;
        disappearTimer = DISAPPEAR_TIMER_MAX;

    }

    void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;
        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f) {

            transform.localScale += Vector3.one * ScaleAmount * Time.deltaTime;
        } else {

            transform.localScale -= Vector3.one * ScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;

        if (disappearTimer < 0) {

            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a < 0) {

                ReturnItemToPool();
            }
        }
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