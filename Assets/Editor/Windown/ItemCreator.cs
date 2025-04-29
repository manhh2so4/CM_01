using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using UnityEngine.Networking;
using Unity.EditorCoroutines.Editor;

public class ItemCreator : EditorWindow
{
    private string SavePath = "";
    private static string PublicCsvUrl = "";

    [MenuItem("Tools/Create Items from Text")]
    public static void ShowWindow()
    {
        GetWindow<ItemCreator>("Create Items");
    }

    private void OnGUI()
    {
        GUILayout.Label("Select Text File", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();

        PublicCsvUrl = EditorGUILayout.TextField("Link URL:", PublicCsvUrl);
        // if (GUILayout.Button("Select Text File", GUILayout.Width(100)))
        // {
        //     filePath = EditorUtility.OpenFilePanel("Select Text File", "", "csv");
        // }
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();

        SavePath = EditorGUILayout.TextField("SavePath", SavePath);

        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create Items"))
        {
            if (!string.IsNullOrEmpty(PublicCsvUrl))
            {
                EditorCoroutineUtility.StartCoroutineOwnerless(FetchAndProcessData());
            }
            else
            {
                Debug.LogError("Please select a URL.");
            }
        }
    }
    private static IEnumerator FetchAndProcessData()
    {
        Debug.Log("Load URL :" + PublicCsvUrl);
        UnityWebRequest request = UnityWebRequest.Get(PublicCsvUrl);
        // Gửi yêu cầu và đợi phản hồi (sẽ tạm dừng Editor một chút)
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("<color=green>Successfully fetched data from Google Sheet.</color>");
            string csvText = request.downloadHandler.text;
            Debug.Log(csvText);
            //ParseAndCreateScriptableObjects(csvText);
        }
        else
        {
            Debug.LogError($"<color=red>Error fetching data: {request.error}</color>");
            EditorUtility.DisplayDialog("Import Error", $"Failed to fetch data from Google Sheet.\nError: {request.error}\n\nCheck URL and internet connection.", "OK");
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