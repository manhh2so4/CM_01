using UnityEngine;
using UnityEditor; // Cần cho các lớp Editor
using UnityEngine.Networking; // Cần cho UnityWebRequest
using System.Collections; // Cần cho IEnumerator
using System.IO;
using Unity.EditorCoroutines.Editor;
using System.Threading.Tasks;
using System.Runtime.CompilerServices; // Cần cho việc xử lý đường dẫn và thư mục

// Đặt tên file là SkillDataImporter.cs và đặt trong thư mục "Editor"
public class SkillDataImporter
{
    private const string FOLDER_PATH = "TextLoad/FX_skill/";
    [MenuItem("Tools/Process My ScriptableObjects")]
    private static void RunProcessingTool()
    {
        Sprite_FXSkill_SO[] foundObjects = Resources.LoadAll<Sprite_FXSkill_SO>(FOLDER_PATH);
        Common.Log("foundObjects: " + foundObjects.Length);
        foreach(Sprite_FXSkill_SO item in foundObjects){
            item.LoadData();
        }
    }
}