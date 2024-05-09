using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    public int test = 1;
    private void Awake() {
        ChangeVar(test);
    }
    private void Reset() {
        ChangeVar(test);
    }
    void ChangeVar( int b){
        switch(b){
            case 0:
				Debug.Log("case 0");
				break;
			case 1:			
			case 2:					
            case 3:
                Debug.Log("case 3");
                break;				
            case 4:
                Debug.Log("case 4");
                break;				
            default:
       		 	Debug.Log("defaut");
        	    break;
        }
    }
}
