using System;
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
	public string string2;
    public TextAsset textAsset;
    public void myReader()
	{
		textAsset = Resources.Load<TextAsset>("nj_part");;
		buffer = convertToSbyte2(textAsset);
		Debug.Log(textAsset.ToString());
		string2 = textAsset.ToString().Substring(1, 2);
		
		StringToByteArray2(@textAsset.ToString());
        //buffer2 = convertToSbyte(StringToByteArray(@textAsset.ToString()));
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
	public static byte[] StringToByteArray(string hex)
	{
		int length = hex.Length;
		byte[] array = new byte[length / 2];
		for (int i = 0; i < length; i += 2)
		{
			array[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
		}
		return array;
	}
	public void StringToByteArray2(string hex)
	{
		int length = hex.Length;
		byte[] array = new byte[length / 2];
		for (int i = 0; i < length; i += 2)
		{
			array[i / 2] = Convert.ToByte("hex",16);
			Debug.Log(array[i / 2]);
		}
		
	}
    private void Reset() {
        myReader();

    }
    void Start()
    {        
    }
}
