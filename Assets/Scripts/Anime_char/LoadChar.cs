using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;
public class LoadChar : MonoBehaviour
{
    [Header("Body-------")]
    public Texture2D[] imgBody ;
    Texture2D[] rootBody = new Texture2D[1];
    [Header("Head------")]
    public Texture2D[] imgHead;
    Texture2D[] rootImageHead;
    [Header("Leg-------")]
    public Texture2D[] imgLeg;
    Texture2D[] rootLeg = new Texture2D[1];
    [Header("Wp-------")]
    [SerializeField] Texture2D[] imgWp = new Texture2D[6];
    [SerializeField] protected TexAo_SO mTexAo;
    [SerializeField] protected TexQuan_SO mQuan;
    [SerializeField] protected TexHead_SO mHead;
    public int lvAo = 0;
    int lvAoCurrent = -1;
    public int lvHead = 0;
    int lvHeadCurrent = -1;
    public int lvQuan = 0;
    int lvQuanCurrent = -1;
    public int wpType = 0;
    int wpTypeCurrent = -1;
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
        LoadTexWp();
        
    }
    private void FixedUpdate()  
    {
        LoadTexWp();
        LoadTexAo_SO();
        LoadTexQuan_SO();
        LoadTexHead_SO();   
    }
    void LoadrootImageHead(){
        string resPath = "Char_tex/Head/rootHead";
        this.rootImageHead = Resources.Load<TexHead_SO>(resPath).imgHead;
    }
    public void LoadTexWp()
    {
        if (wpTypeCurrent == wpType ) return;
        
        wpTypeCurrent = wpType;

    }
    public void LoadTexAo_SO()
    {
        if (lvAoCurrent == lvAo ) return;
        string resPath = "Char_tex/Ao/Ao_lv " + lvAo;
        this.mTexAo = Resources.Load<TexAo_SO>(resPath);
        Debug.Log(": Char_texture " + resPath);
        this.imgBody = rootBody.Concat(mTexAo.imgBody).ToArray();
        lvAoCurrent = lvAo;
    }
    public void LoadTexQuan_SO()
    {
        if (lvQuanCurrent == lvQuan ) return;
        string resPath = "Char_tex/Quan/Quan_lv " + lvQuan;
        this.mQuan = Resources.Load<TexQuan_SO>(resPath);
        Debug.Log(": Char_texture " + resPath);
        this.imgLeg = rootLeg.Concat(mQuan.imgLeg).ToArray();
        lvQuanCurrent = lvQuan;

    }
    public void LoadTexHead_SO()
    {
        if (lvHeadCurrent == lvHead ) return;
        string resPath = "Char_tex/Head/Head_lv " + lvHead;
        this.mHead = Resources.Load<TexHead_SO>(resPath);
        Debug.Log(": Char_texture " + resPath);
        this.imgHead = mHead.imgHead.Concat(rootImageHead).ToArray();
        lvHeadCurrent = lvHead;
    }
}
