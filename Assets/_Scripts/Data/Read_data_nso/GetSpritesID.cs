using System.Collections.Generic;
using UnityEngine;

public static class GetSpritesID {

    static readonly Dictionary<int, Sprite> spritesID = new Dictionary<int, Sprite>();

    public static void SetSprite(){
        spritesID.Clear();
        Sprite[] sprites = Resources.LoadAll<Sprite>("ToSlice/");
        foreach(Sprite sprite in sprites){
            spritesID.Add( int.Parse(sprite.name), sprite);
        }
        
    }

    public static Dictionary<int, Sprite> Get(){
        if(spritesID.Count == 0){
            SetSprite();
            Debug.Log(spritesID.Count );
        }
        return spritesID;
    }

}