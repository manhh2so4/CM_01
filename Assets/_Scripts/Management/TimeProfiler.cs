using UnityEngine;
using System.Diagnostics;
public class CallTime{

    static Stopwatch stopwatch = new Stopwatch();
    public static void Begin(){
        
        stopwatch.Restart();
    }
    public static void End(string Name = null){

        stopwatch.Stop();

        double elapsedMilliseconds = stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000;
        double roundedMilliseconds = System.Math.Round(elapsedMilliseconds, 3);
        UnityEngine.Debug.Log(" executed " + Name + " : " + roundedMilliseconds + " ms" );
        stopwatch.Reset();
    }

}
public class SetTime{
    Stopwatch stopwatch; 
    public SetTime(){
        stopwatch = new Stopwatch();
        stopwatch.Start();
    }
    public void End(string Name = null){
        stopwatch.Stop();
        double elapsedMilliseconds = stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000;
        double roundedMilliseconds = System.Math.Round(elapsedMilliseconds, 3);
        UnityEngine.Debug.Log( Name + " : " + roundedMilliseconds + " ms");
        stopwatch.Reset();
    }
}