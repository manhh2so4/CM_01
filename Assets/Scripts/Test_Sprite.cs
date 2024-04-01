using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Test_Sprite : MonoBehaviour
{
    public sbyte[] buffer;
    public sbyte[] buffer2;
    public byte[] bufferByte;
    private int posRead;
    public TextAsset textAsset;
    public void myReader()
	{
		textAsset = (TextAsset)Resources.Load("0", typeof(TextAsset));
		buffer = convertToSbyte2(textAsset);
        buffer2 = convertToSbyte(textAsset.bytes);
        //bufferByte = convertSbyteToByte(buffer2 );
        bufferByte = textAsset.bytes;        
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
    public static byte[] convertSbyteToByte(sbyte[] scr)
	{
		byte[] array = new byte[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			array[i] = (byte)scr[i];
		}
		return array;
	}

    public static sbyte[] convertToSbyte2(TextAsset scr)
	{
		ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
		byte[] bytes = aSCIIEncoding.GetBytes(scr.ToString());
		return convertToSbyte(bytes);
	}
     public static byte[] convertToSbyte3(TextAsset scr)
	{
		return Encoding.UTF8.GetBytes(scr.ToString());
	}
    private void Reset() {
        myReader();
    }
    void Start()
    {        
    }
}
