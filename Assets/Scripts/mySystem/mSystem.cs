using UnityEngine;
public class mSystem
{
    float a = 0;
    bool change = true;
    public bool FrameRate(float speed){        
        a += Time.deltaTime;
        if(a >= speed){
            change = !change;
        }
        return change;
    }
}

