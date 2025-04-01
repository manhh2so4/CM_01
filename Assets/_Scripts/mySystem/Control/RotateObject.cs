using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 2f;
    [SerializeField] Sprite[] images;
    Image image;
    int index = 0;
    private CancellationTokenSource TokenSource;
    

    void Awake()
    {
        image = GetComponent<Image>();
        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        // TokenSource = new CancellationTokenSource();
        // RotateRoutine().Forget();
    }
    void Update()
    {
        if(TimeRate( 1f / rotationSpeed)){
            AnimSrpite();
        }
    }
    private float frameTimer = 0;
    bool TimeRate(float timeWait){        
        frameTimer += Time.deltaTime;
        if(frameTimer >= timeWait){
            frameTimer = 0;
            return true;
        }
        return false;
    }

    private async UniTaskVoid RotateRoutine()
    {
        while(true)
        {
            AnimSrpite();
            if (TokenSource.IsCancellationRequested)
            {
                break;
            }
            await UniTask.Delay(TimeSpan.FromSeconds(1f / rotationSpeed), ignoreTimeScale: false);
        }
    }

    private void AnimSrpite()
    {
        image.sprite = images[index];
        index++;
        if (index >= images.Length)
        {
            index = 0;
        }
    }

    void OnDisable()
    {
        // TokenSource?.Cancel();
        // TokenSource?.Dispose(); 
    }
}