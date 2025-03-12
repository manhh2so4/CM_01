using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;
[CustomNodeEditor(typeof(StartNode))]
public class StartNodeEditor : NodeEditor{
    public override int GetWidth() {
        return 100;
    }
    public override Color GetTint() {
        
        Color col = Color.green;
        col *= .3f;
        col.a = 1f;
        return col;       
    }
}

[CustomNodeEditor(typeof(ExitNode))]
public class ExitNodeNodeEditor : NodeEditor{
    public override int GetWidth() {
        return 100;
    }
    public override Color GetTint() {
        
        Color col = Color.red;
        col *= .3f;
        col.a = 1f;
        return col;       
    }

}