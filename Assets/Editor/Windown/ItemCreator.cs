using UnityEngine;
using UnityEditor;
using System.IO;

public class ItemCreator : EditorWindow
{
    private string filePath = "";
    private string SavePath = "";

    [MenuItem("Tools/Create Items from Text")]
    public static void ShowWindow()
    {
        GetWindow<ItemCreator>("Create Items");
    }

    private void OnGUI()
    {
        GUILayout.Label("Select Text File", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();

        filePath = EditorGUILayout.TextField("File Path:", filePath);
        if (GUILayout.Button("Select Text File", GUILayout.Width(100)))
        {
            filePath = EditorUtility.OpenFilePanel("Select Text File", "", "csv");
        }
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();

        SavePath = EditorGUILayout.TextField("SavePath", SavePath);

        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create Items"))
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                ReadCSVAndCreateScriptableObjects(filePath, SavePath);
            }
            else
            {
                Debug.LogError("Please select a text file first.");
            }
        }
    }
    void ReadCSVAndCreateScriptableObjects(string Path, string SavePath)
    {
        // Đọc file CSV
        string[] lines = File.ReadAllLines(Path);

        // Bỏ qua dòng đầu tiên (tiêu đề)
        for (int i = 1; i < lines.Length; i++)
        {
            string[] data = lines[i].Split(',');

            // Tạo một instance của ScriptableObject
            WeaponItemSO Equipable = ScriptableObject.CreateInstance<WeaponItemSO>();

            // Gán giá trị từ CSV vào ScriptableObject
            Equipable.SetName(data[0],data[1]);
            Equipable.SetTypeEquip(int.Parse(data[2]));
            Equipable.SetWeaponType(int.Parse(data[3]));
            Equipable.SetImageDraw( data[4]);

            string savepath = SavePath+"/" + data[0] + ".asset";
            UnityEditor.AssetDatabase.CreateAsset(Equipable, savepath);
        }

        // Làm mới AssetDatabase để hiển thị các file mới tạo
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
    }


    private void CreateItemsFromText(string path)
    {
        string[] lines = File.ReadAllLines(path);
        Item currentItem = null;

        foreach (string line in lines)
        {
            if (line.StartsWith("ItemName:"))
            {
                if (currentItem != null)
                {
                    SaveItem(currentItem);
                }

                currentItem = ScriptableObject.CreateInstance<Item>();
                currentItem.itemName = line.Replace("ItemName:", "").Trim();
            }
            else if (line.StartsWith("ItemDescription:"))
            {
                if (currentItem != null)
                {
                    currentItem.itemDescription = line.Replace("ItemDescription:", "").Trim();
                }
            }
            else if (line.StartsWith("ItemValue:"))
            {
                if (currentItem != null)
                {
                    int.TryParse(line.Replace("ItemValue:", "").Trim(), out currentItem.itemValue);
                }
            }
            Debug.Log("okok");
        }

        if (currentItem != null)
        {
            SaveItem(currentItem);
        }

        Debug.Log("Items created successfully.");
    }

    private void SaveItem(Item item)
    {
        string path = SavePath+"/" + item.itemName + ".asset";
        AssetDatabase.CreateAsset(item, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}