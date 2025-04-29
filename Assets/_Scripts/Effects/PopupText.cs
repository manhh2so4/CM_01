using TMPro;
using UnityEngine;
public enum PopupTextType{
    Exp,
    Damage,
    CritDamage,
    Heal,

}
public class PopupText : MonoBehaviour,IObjectPoolItem {
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
    public void Setup(int value, PopupTextType _type, int dir = 0){

        ResetValue();

        textMesh.SetText( (value > 0 ? "+" : "") + value.ToString() );

        textMesh.fontSize = 5;
        
        textMesh.color = GetColorType(_type);
        
        textMesh.sortingOrder = 1;
        float randomX = Random.Range(-.6f, 0.6f);
        float randomY = Random.Range(.2f, 0.4f);
        moveVector = new Vector3(randomX,.3f) * 6;
        moveVector.x *= -dir;

    }
    Color GetColorType(PopupTextType _type){
        switch (_type)
        {
            case PopupTextType.Exp:
                textMesh.fontSize -= 1;
                return Color.cyan;

            case PopupTextType.Damage:
            
                return Color.yellow;

            case PopupTextType.CritDamage:
                textMesh.fontSize += 2;
                return Color.red;

            case PopupTextType.Heal:
                return Color.green;

            default:
                return Color.white;
        }
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

                ReturnToPool();
            }
        }
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