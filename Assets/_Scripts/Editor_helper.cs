using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LitJson;
public class Editor_helper
{
    public static TextAsset textJson;
    private static JsonData data; 
    [SerializeField] public static nj_Image[] nj_Images;

    [MenuItem("EditorHelper/SliceSprites")]
    static void SliceSprites()
    {
        
        string folderPath = "ToSlice";

        Object[] spriteSheets = Resources.LoadAll(folderPath, typeof(Texture2D));
        Debug.Log("spriteSheets.Length: " + spriteSheets.Length);

        

        //----------------------------------
        textJson = Resources.Load<TextAsset>("nj_image");
        data = JsonMapper.ToObject(textJson.ToString());
        nj_Images = new nj_Image[data.Count];
        Debug.Log(data.Count + "  nj_image");
        for (int i = 0; i < data.Count ; i++)
        {   
            nj_Image imageId = new nj_Image();
            imageId.ID =  int.Parse(data[i][0].ToString()); 
            JsonData data2 = GetItem(data[i][1].ToString());
                ImageInfor imgInfor = new ImageInfor();
                imgInfor.ID = int.Parse(data2[0].ToString());
                imgInfor.x0 = int.Parse(data2[1].ToString());
                imgInfor.y0 = int.Parse(data2[2].ToString());
                imgInfor.w = int.Parse(data2[3].ToString());
                imgInfor.h = int.Parse(data2[4].ToString());                              
            imageId.inforImg = imgInfor;          
            nj_Images[i] = imageId;                            
        }
        //----------------------------------


        for (int z = 0; z < spriteSheets.Length; z++)
        {
            
            Debug.Log("z: " + z + " spriteSheets[z]: " + spriteSheets[z]);
            string path = AssetDatabase.GetAssetPath(spriteSheets[z]);
            TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
            ti.isReadable = true;
            
            ti.spriteImportMode = SpriteImportMode.Multiple;

            List<SpriteMetaData> newData = new List<SpriteMetaData>();

            Texture2D spriteSheet = spriteSheets[z] as Texture2D;
            int h = spriteSheet.height;
            int w = spriteSheet.width;
            Debug.Log(nj_Images.Length);
            for (int i = 0; i < nj_Images.Length; i++)
            {
                if(nj_Images[i].inforImg.ID == z){
                        SpriteMetaData smd = new SpriteMetaData();
                        int x0 = nj_Images[i].inforImg.x0*4;
                        int y0 = nj_Images[i].inforImg.y0*4;
                        int w0 = nj_Images[i].inforImg.w*4;
                        int h0 = nj_Images[i].inforImg.h*4;
                        if(x0>h || y0 > w){
                            continue;
                        }

                        smd.pivot = new Vector2(0.5f, 0.5f);
                        smd.alignment = 9;
                        smd.name = (nj_Images[i].ID-1).ToString();
                        
                        smd.rect = new Rect(x0, h - y0 - h0 , w0, h0);
                        newData.Add(smd);
                    
                }
            }
            ti.spritesheet = newData.ToArray();
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }
        Debug.Log("Done Slicing!");
    }
    
    static JsonData GetItem(string data3){
        return JsonMapper.ToObject(data3);
    }
}