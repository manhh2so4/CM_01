using UnityEngine;
using TMPro; // Hoặc using UnityEngine.UI cho Text Legacy
using System.Collections;
using NaughtyAttributes;
using System;
using UnityEngine.UI;

public class UI_NotifyText : MonoBehaviour, IObjectPoolItem
{
    public float slideInDuration = 0.5f; // Thời gian trượt vào
    public float displayDuration = 2.0f; // Thời gian hiển thị
    public float slideOutDuration = 0.5f; // Thời gian trượt ra
    public float slideDistance = 100f; // Khoảng cách trượt

    [SerializeField] RectTransform rectTransform;
    [SerializeField] TextMeshProUGUI notificationText;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Image icon;

    private Vector2 initialPosition;
    private Vector2 targetPosition;
    Action OnComplete;

    [Button]
    void Test(){
        Debug.Log("Height: " + notificationText.bounds.size.y);
    }
    public void ShowNotification(string message, int index, Action OnComplete, Sprite icon = null)
    {
        notificationText.text = message;

        if(icon != null){
            this.icon.sprite = icon;
            this.icon.SetNativeSize();
            this.icon.gameObject.SetActive(true);
        }
        else this.icon.gameObject.SetActive(false);
        this.OnComplete = OnComplete;

        float height = 50;
        targetPosition = new Vector2(0, -index*height);

        initialPosition = targetPosition + new Vector2(-slideDistance, 0);
        rectTransform.anchoredPosition = initialPosition;
        StartCoroutine(AnimateNotification());
    }

    IEnumerator AnimateNotification()
    {
        float elapsedTime = 0;
        while (elapsedTime < slideInDuration)
        {
            float t = elapsedTime / slideInDuration;
            float easedT = t * t * (3f - 2f * t);
            rectTransform.anchoredPosition = Vector2.Lerp(initialPosition, targetPosition, easedT);
            canvasGroup.alpha = Mathf.Lerp(0, 1, easedT );

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = targetPosition;

        yield return new WaitForSeconds(displayDuration);

        elapsedTime = 0;
        while (elapsedTime < slideOutDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, (elapsedTime / slideOutDuration));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        OnComplete?.Invoke();
        ReturnToPool();
    }
    void OnDisable(){
        StopAllCoroutines();
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