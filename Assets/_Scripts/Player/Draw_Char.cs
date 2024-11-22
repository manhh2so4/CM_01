using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Draw_Char : MonoBehaviour
{
    public int cf = 0;
	private int current = -1;

    [SerializeField] UnityEngine.GameObject LegGO;
    [SerializeField] UnityEngine.GameObject HeadGO;
    [SerializeField] UnityEngine.GameObject BodyGO;
	[SerializeField] UnityEngine.GameObject DustGO;
	[SerializeField] UnityEngine.GameObject WpGO;

    SpriteInfo[] partBody = new SpriteInfo[18];
	SpriteInfo[] partHead = new SpriteInfo[8];
	SpriteInfo[] partLeg = new SpriteInfo[10];
    SpriteInfo[] partWP = new SpriteInfo[2];

	[SerializeField] protected Tex_NjPart_SO mTexAo;
    [SerializeField] protected Tex_NjPart_SO mQuan;
    [SerializeField] protected Tex_NjPart_SO mHead;
	[SerializeField] private Tex_NjPart_SO mWp;

	//--------- effect char --------
	Sprite[] spriteDust;
	SmallEffect_SO mDust;
	int x = 0;
	Sprite[] spriteWaterSplash;
	SmallEffect_SO mWaterSplash;

	//--------- Data Char draw --------
    public int lvHead = 0;
    int lvHeadCurrent = -1;

    public int wpType = 0;

    private void FixedUpdate()  
    {
        //LoadTexWp();
        LoadTexHead_SO();   
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
        partWP = mWp.spriteInfos;
    }
	void UpdateColth(){
		for (int i = 0; i < Inventory.Instance.equipment.Count; i++)
		{
			ItemData_Equipment newEquipment = Inventory.Instance.equipment[i].data as ItemData_Equipment;
			if(newEquipment.Type == EquipmentType.body_armor){
				partBody = newEquipment.tex_NjPart_SO.spriteInfos;
			}
			if(newEquipment.Type == EquipmentType.Pan_armor){
				partLeg = newEquipment.tex_NjPart_SO.spriteInfos;
			}	
		}
	}
	private void OnEnable() {
		
	}
	private void Start() {
		Inventory.Instance.OnChangeCloth += UpdateColth;
	}
	private void OnDisable() {
		//Inventory.Instance.OnChangeCloth -= UpdateColth;
	}
    public void LoadTexHead_SO()
    {
        if (lvHeadCurrent == lvHead ) return;
        string resPath = "Char_tex/Head/Head_id " + lvHead;
        this.mHead = Resources.Load<Tex_NjPart_SO>(resPath);
		partHead = mHead.spriteInfos;
        lvHeadCurrent = lvHead;
    }
   
   private readonly int[][][] CharInfo2 = new int[34][][]
    
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
		},
		new int[4][]
		{
			new int[3] { 0, -5, 34},
			new int[3] { 8, -0, 10 },
			new int[3] { 9, -5, 21 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 0, -4, 34 },
			new int[3] { 7, -0, 10 },
			new int[3] { 11, -5, 18 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 1, -10, 36 },
			new int[3] { 2, -8, 10 },
			new int[3] { 9, -10, 21 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 1, -9, 36 },
			new int[3] { 3, -8, 10 },
			new int[3] { 9, -8, 21 },
			new int[3]
		}	
	};
    private void Awake() {
		LoadCompnents();		        
        LoadTexHead_SO();
		LoadTexWp();
		LoadEffect_Trigger();   				
	}
	private void Reset() {
		LoadCompnents();
        LoadTexHead_SO(); 
		Debug.Log(partBody[1].sprite.rect.height);
		

	}
	void LoadCompnents(){
		LegGO = transform.Find("Leg").gameObject;
		HeadGO = transform.Find("Head").gameObject;
		BodyGO = transform.Find("Body").gameObject;		
		WpGO = transform.Find("Wp").gameObject;
		partLeg = mQuan.spriteInfos;
		partBody = mTexAo.spriteInfos;
	}
	private void Update() {
        PaintChar();
    }
    private void PaintChar(){
		if(cf == current) return;
        mPaint.Paint(BodyGO, partBody[CharInfo2[cf][2][0]].sprite, CharInfo2[cf][2][1] + partBody[CharInfo2[cf][2][0]].dx, CharInfo2[cf][2][2] - partBody[CharInfo2[cf][2][0]].dy, 0);
        mPaint.Paint(HeadGO, partHead[CharInfo2[cf][0][0]].sprite, CharInfo2[cf][0][1] + partHead[CharInfo2[cf][0][0]].dx, CharInfo2[cf][0][2] - partHead[CharInfo2[cf][0][0]].dy, 0);
        mPaint.Paint(LegGO,  partLeg[CharInfo2[cf][1][0]].sprite,  CharInfo2[cf][1][1] + partLeg[CharInfo2[cf][1][0]].dx,  CharInfo2[cf][1][2] - partLeg[CharInfo2[cf][1][0]].dy, 0);
		mPaint.Paint(WpGO,   partWP[CharInfo2[cf][3][0]].sprite,   CharInfo2[cf][3][1] + partWP[CharInfo2[cf][3][0]].dx,   CharInfo2[cf][3][2] - partWP[CharInfo2[cf][3][0]].dy, 0);
		current = cf;
	}
    public void PainDust(int index){
		if(index == 0){
            x = 0;
        }
		x +=3;
		mPaint.Paint(DustGO,spriteDust[index],-5-x,0,3);
	}
	public void OffDust(){
		DustGO.SetActive(false);
	}
	public void OnDust(){
		DustGO.SetActive(true);
	}
}
