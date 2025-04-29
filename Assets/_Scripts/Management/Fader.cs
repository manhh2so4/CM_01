using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        Transform image;
        public static Fader Instance;

        private void Awake() {
            if(Instance == null) Instance = this;
            else Destroy(gameObject);
            canvasGroup = GetComponent<CanvasGroup>();
            image = transform.Find("Image");
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }

        public IEnumerator FadeOut(float time)
        {
            yield return Fade(1, time);
            
        }

        public IEnumerator FadeIn(float time)
        {
            yield return Fade(0, time);
        }

        public IEnumerator Fade(float target, float time)
        {
            yield return FadeRoutine(target, time);
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                if(image.gameObject.activeSelf == false){
                    if( canvasGroup.alpha > 0 ) image.gameObject.SetActive(true);
                }else{
                    if( canvasGroup.alpha < 0.01f ) image.gameObject.SetActive(false);
                }
                yield return null;
            }
        }
    }