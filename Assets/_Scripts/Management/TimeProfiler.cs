using UnityEngine;
using System.Diagnostics;
public class TimeProfiler : MonoBehaviour {
    private static TimeProfiler instance;
    public static TimeProfiler Instance
    {
        get
        {
            if (instance == null)
            {
                
                //instance = ;
            }
            return instance;
        }
    }
}