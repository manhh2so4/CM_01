using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Draw_Char : MonoBehaviour
{
    public int cf = 0;
	private int current = -1;
    [SerializeField] GameObject LegGO;
    [SerializeField] GameObject HeadGO;
    [SerializeField] GameObject BodyGO;
	[SerializeField] GameObject DustGO;
    [Header("Body-------")]
    Texture2D[] imgBody ;
    Sprite[] spriteBody;
    Texture2D[] rootBody = new Texture2D[1];
    [Header("Head------")]
	Sprite[] spriteHead;
    Texture2D[] imgHead;
    Texture2D[] rootImageHead;
    [Header("Leg-------")]
	Sprite[] spriteLeg;
    Texture2D[] imgLeg;
    Texture2D[] rootLeg = new Texture2D[1];
    [Header("Wp-------")]
    [SerializeField] Texture2D[] imgWp = new Texture2D[6];
    [SerializeField] protected TexAo_SO mTexAo;
    [SerializeField] protected TexQuan_SO mQuan;
    [SerializeField] protected TexHead_SO mHead;
	//--------- effect char --------
	Sprite[] spriteDust;
	SmallEffect_SO mDust;
	int x = 0;
	Sprite[] spriteWaterSplash;
	SmallEffect_SO mWaterSplash;

	//--------- Data Char draw --------
    public int lvAo = 0;
    int lvAoCurrent = -1;
    public int lvHead = 0;
    int lvHeadCurrent = -1;
    public int lvQuan = 0;
    int lvQuanCurrent = -1;
    public int wpType = 0;
    int wpTypeCurrent = -1;
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
	void LoadEffect_Trigger(){
        string resPath = "Effec_Load/Small_Eff/" + "dust";
        this.mDust = Resources.Load<SmallEffect_SO>(resPath);
        mPaint.LoadSprite(ref spriteDust,mDust.textures, mPaint.BOTTOM|mPaint.HCENTER);

		string resPath1 = "Effec_Load/Small_Eff/" + "WaterSplash";
        this.mWaterSplash = Resources.Load<SmallEffect_SO>(resPath1);
        mPaint.LoadSprite(ref spriteWaterSplash,mWaterSplash.textures, mPaint.VCENTER|mPaint.HCENTER);
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
        mTexAo = Resources.Load<TexAo_SO>(resPath);
        imgBody = rootBody.Concat(mTexAo.imgBody).ToArray();
		mPaint.LoadSprite(ref spriteBody,imgBody,0);
        lvAoCurrent = lvAo;
    }
    public void LoadTexQuan_SO()
    {
        if (lvQuanCurrent == lvQuan ) return;
        string resPath = "Char_tex/Quan/Quan_lv " + lvQuan;
        this.mQuan = Resources.Load<TexQuan_SO>(resPath);
        //Debug.Log(": Char_texture " + resPath);
        this.imgLeg = rootLeg.Concat(mQuan.imgLeg).ToArray();
		mPaint.LoadSprite(ref spriteLeg,imgLeg,0);
        lvQuanCurrent = lvQuan;

    }
    public void LoadTexHead_SO()
    {
        if (lvHeadCurrent == lvHead ) return;
        string resPath = "Char_tex/Head/Head_lv " + lvHead;
        this.mHead = Resources.Load<TexHead_SO>(resPath);
        //Debug.Log(": Char_texture " + resPath);
        this.imgHead = mHead.imgHead.Concat(rootImageHead).ToArray();
		mPaint.LoadSprite(ref spriteHead,imgHead,0);
        lvHeadCurrent = lvHead;
    }
   private readonly int[][][] CharInfo = new int[30][][]
    
	{
		new int[4][]
		{
			new int[3] { 0, -10, 32 },
			new int[3] { 1, -7, 7 },
			new int[3] { 1, -11, 15 },
			new int[3] { 1, -9, 45 }
		},
		new int[4][]
		{
			new int[3] { 0, -10, 33 },
			new int[3] { 1, -7, 7 },
			new int[3] { 1, -11, 16 },
			new int[3] { 1, -9, 46 }
		},
		new int[4][]
		{
			new int[3] { 1, -10, 33 },
			new int[3] { 2, -10, 11 },
			new int[3] { 2, -9, 16 },
			new int[3] { 1, -12, 49 }
		},
		new int[4][]
		{
			new int[3] { 1, -10, 32 },
			new int[3] { 3, -11, 9 },
			new int[3] { 3, -11, 16 },
			new int[3] { 1, -13, 47 }
		},
		new int[4][]
		{
			new int[3] { 1, -10, 34 },
			new int[3] { 4, -9, 9 },
			new int[3] { 4, -8, 16 },
			new int[3] { 1, -12, 47 }
		},
		new int[4][]
		{
			new int[3] { 1, -10, 34 },
			new int[3] { 5, -11, 11 },
			new int[3] { 5, -10, 17 },
			new int[3] { 1, -13, 49 }
		},
		new int[4][]
		{
			new int[3] { 1, -10, 33 },
			new int[3] { 6, -9, 9 },
			new int[3] { 6, -8, 16 },
			new int[3] { 1, -12, 47 }
		},
		new int[4][]
		{
			new int[3] { 0, -9, 36 },
			new int[3] { 7, -5, 15 },
			new int[3] { 7, -10, 21 },
			new int[3] { 1, -8, 49 }
		},
		new int[4][]
		{
			new int[3] { 4, -13, 26 },
			new int[3],
			new int[3],
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 5, -13, 25 },
			new int[3],
			new int[3],
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 6, -12, 26 },
			new int[3],
			new int[3],
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 7, -13, 25 },
			new int[3],
			new int[3],
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 0, -9, 35 },
			new int[3] { 8, -4, 13 },
			new int[3] { 8, -14, 27 },
			new int[3] { 1, -9, 49 }
		},
		new int[4][]
		{
			new int[3] { 0, -9, 31 },
			new int[3] { 9, -11, 8 },
			new int[3] { 10, -10, 17 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -7, 33 },
			new int[3] { 9, -11, 8 },
			new int[3] { 11, -8, 15 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -8, 32 },
			new int[3] { 9, -11, 8 },
			new int[3] { 12, -8, 14 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -7, 32 },
			new int[3] { 9, -11, 8 },
			new int[3] { 13, -12, 15 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 0, -11, 31 },
			new int[3] { 9, -11, 8 },
			new int[3] { 14, -15, 18 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -9, 32 },
			new int[3] { 9, -11, 8 },
			new int[3] { 15, -13, 19 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -9, 31 },
			new int[3] { 9, -11, 8 },
			new int[3] { 16, -7, 22 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -9, 32 },
			new int[3] { 9, -11, 8 },
			new int[3] { 17, -11, 18 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 3, -12, 34 },
			new int[3] { 8, -4, 13 },
			new int[3] { 8, -15, 25 },
			new int[3] { 1, -10, 46 }
		},
		new int[4][]
		{
			new int[3] { 0, -9, 32 },
			new int[3] { 8, -4, 9 },
			new int[3] { 10, -10, 18 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -7, 34 },
			new int[3] { 8, -4, 9 },
			new int[3] { 11, -8, 16 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -8, 33 },
			new int[3] { 8, -4, 9 },
			new int[3] { 12, -8, 15 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -7, 33 },
			new int[3] { 8, -4, 9 },
			new int[3] { 13, -12, 16 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 0, -11, 32 },
			new int[3] { 7, -5, 9 },
			new int[3] { 14, -15, 19 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -9, 33 },
			new int[3] { 7, -5, 9 },
			new int[3] { 15, -13, 20 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -9, 32 },
			new int[3] { 7, -5, 9 },
			new int[3] { 16, -7, 23 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -9, 33 },
			new int[3] { 7, -5, 9 },
			new int[3] { 17, -11, 19 },
			new int[3]
		}
	};
    private void Awake() {
		LoadCompnents();		        
        LoadTexAo_SO();
        LoadTexQuan_SO();
        LoadTexHead_SO();
		LoadEffect_Trigger();   				
	}
	private void Reset() {
		LoadCompnents();
        LoadTexAo_SO();
        LoadTexQuan_SO();
        LoadTexHead_SO();        
	}
	void LoadCompnents(){
		rootLeg[0] = new Texture2D(1,1);
        rootBody[0] = new Texture2D(1,1);
		LoadrootImageHead();
		LegGO = transform.Find("Leg").gameObject;
		HeadGO = transform.Find("Head").gameObject;
		BodyGO = transform.Find("Body").gameObject;
		DustGO = transform.Find("Dust").gameObject;
	}
	private void Update() {
        PaintChar();
    }
    private void PaintChar(){
		if(cf == current) return;
        mPaint.Paint(BodyGO,spriteBody[CharInfo[cf][2][0]], CharInfo[cf][2][1],CharInfo[cf][2][2]);
        mPaint.Paint(HeadGO,spriteHead[CharInfo[cf][0][0]], CharInfo[cf][0][1],CharInfo[cf][0][2]);
        mPaint.Paint(LegGO,spriteLeg[CharInfo[cf][1][0]], CharInfo[cf][1][1],CharInfo[cf][1][2]);
		current = cf;
	}
    public void PainDust(int index){
		if(index == 0){
            x = 0;
        }
		x +=3;
		mPaint.Paint(DustGO,spriteDust[index],-5-x,0);
	}
	public void OffDust(){
		DustGO.SetActive(false);
	}
	public void OnDust(){
		DustGO.SetActive(true);
	}
}
