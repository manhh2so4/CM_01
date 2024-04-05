using System.Collections;
using UnityEngine;

public class Test_renderer : MonoBehaviour
{
    public Color borderColor = Color.black;
    public float borderWidth = 0.1f;
    public int cf = 0;
    public int zoomlv = 1;
	public float cx = Screen.width/2;
    public float cy = Screen.height/2;
	public int Frame = 24;
	public int nextFrame = -1;
    public Texture2D[] tex2D;

    private SpriteRenderer spriteRenderer;
    public static readonly int[][][] SkillInfo = new int[6][][]
    
	{
		new int[1][]
		{
			new int[3] { 0, 0, 0 },
		},
        new int[1][]
		{
			new int[3] { 1, 6, -6 },
		},
        new int[1][]
		{
			new int[3] { 2, 14, -10 },
		},
        new int[1][]
		{
			new int[3] { 3, 2, -17 },
		},
        new int[1][]
		{
			new int[3] { 4, -15, -16 },
		},
        new int[1][]
		{
			new int[3] { 5, 9, -12 },
		},
    };
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(MyCoroutine());
        
    }
        IEnumerator MyCoroutine()
    {
        while (true)
        {
            ++cf;
            if(cf==6){
                cf=0;
            }

            yield return new WaitForSeconds((float)Frame/60f);
        }
    }
    private void FixedUpdate() {
        
    }
    private void OnGUI() {
         LoadImage();
    }
    void LoadImage(){
        //if(nextFrame == cf) return;
        __drawRegion(tex2D[SkillInfo[cf][0][0]],SkillInfo[cf][0][1],SkillInfo[cf][0][2]);
		//nextFrame = cf;
    }	
    

    void __drawRegion(Texture2D image, float x, float y){
        int w = 0;
        int h = 0;
        if(image == null){
            w = 0;
            h = 0;

        }else{
            w = image.width;
            h = image.height;
        }
            float x0 = cx+x*4;
            float y0 = cy-y*4;
            x0 +=w/2;
        GUI.DrawTexture(new Rect(x0-w/2, y0-w/2, w, h), image);
    }
}