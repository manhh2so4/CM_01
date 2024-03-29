using UnityEngine;

public class Test_renderer : MonoBehaviour
{
    public Color borderColor = Color.black;
    public float borderWidth = 0.1f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        DrawBorder();
    }

    void DrawBorder()
    {
        Texture2D texture = new Texture2D(spriteRenderer.sprite.texture.width, spriteRenderer.sprite.texture.height);
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                if (x==0 || y==0 || x==texture.width-1 || y==texture.height-1)
                {
                    texture.SetPixel(x, y, borderColor);
                }
                else
                {
                    texture.SetPixel(x, y, spriteRenderer.sprite.texture.GetPixel(x, y));
                }
            }
        }
        texture.Apply();

        spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), spriteRenderer.sprite.pixelsPerUnit);
    }
}