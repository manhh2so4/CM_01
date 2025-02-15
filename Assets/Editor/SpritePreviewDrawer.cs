using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SpritePreviewAttribute))]
public class SpritePreviewDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue is Sprite sprite)
        {
            EditorGUI.ObjectField(new Rect(position.x, position.y, position.width / 3, EditorGUIUtility.singleLineHeight), property, typeof(Sprite), GUIContent.none);
            if (sprite != null)
            {
                Texture2D mTex2d ;
                mTex2d = ViewTexture(sprite);

                float rito = (float)mTex2d.width / mTex2d.height;

                Rect spriteRect = new Rect(position.x + position.width /3 + 10, position.y, position.height*rito, position.height);          
                EditorGUI.DrawPreviewTexture(spriteRect, mTex2d);
            }
        }
        else
        {
            EditorGUI.ObjectField(new Rect(position.x, position.y, position.width /3, position.height), property, typeof(Sprite), GUIContent.none);
            //EditorGUI.LabelField(position, "This is not a Sprite or null");
        }
        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
    Texture2D ViewTexture(Sprite sprite)
    {
        Rect cropRect = sprite.rect;
        int x = Mathf.FloorToInt(cropRect.x);
        int y =  Mathf.FloorToInt(cropRect.y);
        int width = Mathf.FloorToInt(cropRect.width);
        int height = Mathf.FloorToInt(cropRect.height);
        Texture2D mTexture = new Texture2D(sprite.texture.width ,sprite.texture.height , sprite.texture.format, false);
        mTexture.LoadRawTextureData(sprite.texture.GetRawTextureData());
        Texture2D croppedTexture = new Texture2D(width, height);
        Color[] pixels ;
        pixels = mTexture.GetPixels(x, y, width, height);
        for (int i = 0; i < pixels.Length; i++)
        {
            if(pixels[i].a == 0){
                pixels[i] = Color.black;
            }
        }
        croppedTexture.SetPixels(pixels);

        croppedTexture.Apply();
        
        return croppedTexture;
    }
}
