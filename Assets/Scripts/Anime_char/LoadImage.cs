using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;
public class LoadImage : MonoBehaviour
{
    [Header("Sprite------")]
    private static LoadImage instance;
    public Sprite[] spriteBody = new Sprite[18];
    public Sprite[] spriteHead = new Sprite[8];
    public Sprite[] spriteLeg = new Sprite[10];
    [Header("Body-------")]
    public  Texture2D[] imgBody ;
    public  Texture2D[] imgBodyImp ;
    public  Texture2D[] rootBody = new Texture2D[1];
    [Header("Head------")]
    public  Texture2D[] imgHead;
    public  Texture2D[] rootImageHead;
    [Header("Leg-------")]
    public  Texture2D[] imgLeg;
    public  Texture2D[] rootLeg = new Texture2D[1];
    [SerializeField] protected TexAo_SO mTexAo;
    [SerializeField] protected TexQuan_SO mQuan;
    [SerializeField] protected TexHead_SO mHead;
    public int[] Run = new int[]{2,3,4,5,6,7};
    public int[] Idle = new int[]{1,2};
    public int[] Jump = new int[]{};
    public int lvAo = 0;
    int lvAoCurrent = -1;
    public int lvHead = 0;
    int lvHeadCurrent = -1;
    public int lvQuan = 0;
    int lvQuanCurrent = -1;
    private void Reset() {
        rootLeg[0] = new Texture2D(1,1);
        rootBody[0] = new Texture2D(1,1);
        LoadrootImageHead();
        LoadTexAo_SO();
        LoadTexQuan_SO();
        LoadTexHead_SO();        
    }
    private void Start() {
        rootLeg[0] = new Texture2D(1,1);
        rootBody[0] = new Texture2D(1,1);
        LoadrootImageHead();
        LoadTexAo_SO();
        LoadTexQuan_SO();
        LoadTexHead_SO();    
    }
    private void FixedUpdate()  
    {
        LoadTexAo_SO();
        LoadTexQuan_SO();
        LoadTexHead_SO();   
    }
    void LoadrootImageHead(){
        string resPath = "Char_tex/Head/rootHead";
        this.rootImageHead = Resources.Load<TexHead_SO>(resPath).imgHead;
    }
     public void LoadTexAo_SO()
    {
        if (lvAoCurrent == lvAo && mTexAo != null) return;
        string resPath = "Char_tex/Ao/Ao_lv " + lvAo;
        this.mTexAo = Resources.Load<TexAo_SO>(resPath);
        Debug.Log(": Char_texture " + resPath);
        this.imgBody = rootBody.Concat(mTexAo.imgBody).ToArray();
        CvtSprite2(spriteBody,imgBody);
        lvAoCurrent = lvAo;

    }
     public void LoadTexQuan_SO()
    {
        if (lvQuanCurrent == lvQuan && mQuan != null) return;
        string resPath = "Char_tex/Quan/Quan_lv " + lvQuan;
        this.mQuan = Resources.Load<TexQuan_SO>(resPath);
        Debug.Log(": Char_texture " + resPath);
        this.imgLeg = rootLeg.Concat(mQuan.imgLeg).ToArray();
        CvtSprite2(spriteLeg,imgLeg);
        lvQuanCurrent = lvQuan;

    }
    public void LoadTexHead_SO()
    {
        if (lvHeadCurrent == lvHead && mHead != null) return;
        string resPath = "Char_tex/Head/Head_lv " + lvHead;
        this.mHead = Resources.Load<TexHead_SO>(resPath);

        Debug.Log(": Char_texture " + resPath);
        this.imgHead = mHead.imgHead.Concat(rootImageHead).ToArray();
        CvtSprite2(spriteHead,imgHead);
        lvHeadCurrent = lvHead;
    }

    void CvtSprite(){
        for (int i = 0; i < 18; i++)
        {
            spriteBody[i] = Sprite.Create(imgBody[i], new Rect(0, 0, imgBody[i].width, imgBody[i].height), new Vector2(0f,1f));
            spriteBody[i].name = "Body" + i.ToString();
        }
        for (int i = 0; i < spriteHead.Length; i++)
        {
            spriteHead[i] = Sprite.Create(imgHead[i], new Rect(0, 0, imgHead[i].width, imgHead[i].height), new Vector2(0f,1f));
            spriteHead[i].name = "Head" + i.ToString();
        }
        for (int i = 0; i < spriteLeg.Length; i++)
        {
            spriteLeg[i] = Sprite.Create(imgLeg[i], new Rect(0, 0, imgLeg[i].width, imgLeg[i].height), new Vector2(0f,1f));
            spriteLeg[i].name = "Leg" + i.ToString();
        }
    }
    void CvtSprite2(Sprite[] sprite,Texture2D[] text){
       for (int i = 0; i < sprite.Length; i++)
        {
            sprite[i] = Sprite.Create(text[i], new Rect(0, 0, text[i].width, text[i].height), new Vector2(0f,1f));
            sprite[i].name = i.ToString();
        } 
    }

}
