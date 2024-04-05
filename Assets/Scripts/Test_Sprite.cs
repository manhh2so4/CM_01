using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Test_Sprite : MonoBehaviour
{
    public sbyte[] buffer;
    public byte[] buffer2 = new byte[]{5,6,7,4,75,23,46,13,35,98,127,124,254,222,211};
    public byte[] bufferByte;
    private int posRead;
	public string string2;
    public TextAsset textAsset;
    public void myReader()
	{
		string @string = Resources.Load<TextAsset>("nj_effect").ToString();
		textAsset = Resources.Load<TextAsset>("nj_effect");
		buffer = convertToSbyte2(@string);

		//Debug.Log(@string);
		//string2 = textAsset.ToString().Substring(1, 2);
		
		StringToByteArray2(@string);
        //buffer2 = convertToSbyte(StringToByteArray(@textAsset.ToString()));
        //bufferByte = convertSbyteToByte(buffer2 );
        //bufferByte = textAsset.bytes;        
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

    public static sbyte[] convertToSbyte2(String scr)
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
			//array[i / 2] = Encoding.UTF8.GetBytes(hex.Substring(i, 2));;
			Debug.Log(array[i / 2]);
		}
		
	}
	public void ByteArrayToString(byte[] ba)
	{
		UTF8Encoding utf8 = new UTF8Encoding();
		string @text = Encoding.UTF8.GetString(ba);
		Debug.Log(@text);
		

        // Chuyển đổi chuỗi ký tự sang mảng byte UTF-8
        byte[] utf8Bytes = Encoding.UTF8.GetBytes(@text);
		Debug.Log(utf8Bytes.Length);
        for (int i = 0; i < utf8Bytes.Length; i++)
		{
			Debug.Log(utf8Bytes[i]);
		}

	}
    private void Reset() {
        //myReader();
		//ByteArrayToString(buffer2);
		Main22();
    }
    void Start()
    {        
    }

	static void Main22()
    {
        // Mảng số nguyên
        int[] numbers = { 5, 46, 13, 35, 210, 98, 255, 89, 38, 180, 45, 77 };

        // Bước 1: Chuyển mảng số nguyên thành một chuỗi UTF-8
        string @utf8String = ConvertIntArrayToString(numbers);
      	Debug.Log("UTF-8 String: " + @utf8String);


        int[] decodedNumbers = ConvertStringToIntArray(@utf8String,numbers);

        foreach (int num in decodedNumbers)
        {
           Debug.Log(num);
        }
    }

    static string ConvertIntArrayToString(int[] numbers)
    {
        byte[] byteArray = new byte[numbers.Length];
        for (int i = 0; i < numbers.Length; i++)
        {
            byteArray[i] = (byte)numbers[i];
        }
        return Encoding.UTF8.GetString(byteArray);
    }

    // Biên dịch chuỗi UTF-8 thành mảng số nguyên
    static int[] ConvertStringToIntArray(string utf8String,int[] num2)
    {
        byte[] byteArray = Encoding.UTF8.GetBytes(utf8String);

        int[] numbers = new int[byteArray.Length];
        for (int i = 0; i < byteArray.Length; i++)
        {	
			int a = 0;
            if(num2[0]>1000){
				short num = 0;
				for (int j = 0; j < 2; j++)
				{
					num = (short)(num << 8);
					num = (short)(num | (short)(0xFF & byteArray[a++]));				
				}
				numbers[i] = num;
				a++;			
			}else{
				numbers[i] = (int)byteArray[i];
				a++;
			}
        }
        return numbers;
    }
}
