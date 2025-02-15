using UnityEngine;
using UnityEngine.UI;

public class UI_RefSprite : MonoBehaviour {
    GameObject refObject;
    SpriteRenderer sprite;
    Image image;
    void Awake(){
        sprite = refObject.GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
    }
    void Update(){
        image.sprite = sprite.sprite;
    }
    
}