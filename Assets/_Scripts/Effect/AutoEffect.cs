using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AutoEffect : MonoBehaviour
{
    public sbyte idEff = 1;
    public int index = 0;
    public int posRead = 5;
    private MyImage img;
    public sbyte[] buffer;
    public static Effect_temp EffAtutoTemp;
    public TextAsset textAsset;
    public Texture2D mText;
    public Sprite[] spriteTest ;
     public Sprite spriteTest2;
    public SpriteRenderer SREffec;
    public void myReader()
	{
		textAsset = (TextAsset)Resources.Load("1", typeof(TextAsset));
		buffer = convertToSbyte2(textAsset);   
	}
    public static sbyte[] convertToSbyte2(TextAsset scr)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(scr.ToString());
        return convertToSbyte(bytes);
	}
    public static sbyte[] convertToSbyte(byte[] scr)
	{
		sbyte[] array = new sbyte[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			array[i] = (sbyte)scr[i];
		}
		return array;
	}
     private void Reset() {
        myReader();
        //LoadImgae(ref spriteTest);
        SREffec = GetComponent<SpriteRenderer>();
        
    }
    private void Start() {
        myReader();
        SREffec = GetComponent<SpriteRenderer>();
    }
    void DrawImg(){
        spriteTest2 = Sprite.Create(mText, new Rect(0f, 74f, 64f, 26f), new Vector2(0f, 1f));
    }
    void LoadImgae(ref Sprite[] sp){
            
			int c = readSbyte();
			for (int i = 0; i < 8; i++)
			{
				sbyte id = readSbyte();
				int x = readSbyte();
				int y = readSbyte()*2;
				int width = readSbyte()*2;
				int height = readSbyte()*2;
                spriteTest2 = Sprite.Create(mText, new Rect(0,0,width,height), new Vector2(0f, 1f));
               //spriteTest[i].name = "img" + id.ToString();
			}
    }

    void OnGUI()
    {
        // Xác định vị trí và kích thước của nút bấm
        Rect buttonRect = new Rect(10, 10, 150, 50);

        // Vẽ nút bấm
        if (GUI.Button(buttonRect, "Click Me"))
        {
           sbyte id = readSbyte();
			int x = readSbyte()*2;
			int y = readSbyte()*2;
			int width = readSbyte()*2;
			int height = readSbyte()*2;
            Debug.Log("dasda" + new Rect(x,y,width,height) );
            spriteTest2 = Sprite.Create(mText, new Rect(x,mText.height-y,width,height), new Vector2(0f, 1f)); // Hành động sẽ được thực hiện khi nút bấm được nhấn
            SREffec.sprite = spriteTest2;
        }
    }
    sbyte readSbyte(){
        if (posRead < buffer.Length)
		{
            posRead++;
            //Debug.Log("dasda" +posRead );
			return buffer[posRead];
		}
		posRead = buffer.Length;
		return 0;
    }

}
