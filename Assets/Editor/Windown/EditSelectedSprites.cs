using UnityEngine;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using System.Collections.Generic;

public class EditSelectedSprites : EditorWindow
{
    private string prefix = ""; // Tiền tố
    private string suffix = ""; // Hậu tố
    private int startNumber = 1; // Số bắt đầu
    private string newName = "Sprite"; // Tên mới
    private List<Sprite> sprites = new List<Sprite>(); // Mảng chứa các sprite

    [MenuItem("Tools/Sprite Rename Tool")]
    static void Init()
    {
        // Mở cửa sổ tool
        EditSelectedSprites window = GetWindow<EditSelectedSprites>("Sprite Rename Tool");
        window.Show();
    }

    void OnGUI()
    {
        // Hiển thị các trường nhập liệu
        GUILayout.Label("Rename Settings", EditorStyles.boldLabel);
        prefix = EditorGUILayout.TextField("Prefix", prefix);
        suffix = EditorGUILayout.TextField("Suffix", suffix);
        startNumber = EditorGUILayout.IntField("Start Number", startNumber);
        newName = EditorGUILayout.TextField("Base Name", newName);

        // Vùng kéo thả
        GUILayout.Label("Kéo và thả Sprite vào đây", EditorStyles.helpBox);
        Rect dropArea = GUILayoutUtility.GetRect(0, 50, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, "Drag and Drop Sprites Here");

        // Xử lý sự kiện kéo thả
        HandleDragAndDrop(dropArea);

        // Hiển thị danh sách các sprite đã thêm
        GUILayout.Label("Danh sách Sprite:", EditorStyles.boldLabel);
        for (int i = 0; i < sprites.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField($"Sprite {i + 1}", sprites[i], typeof(Sprite), false);
            if (GUILayout.Button("Xóa", GUILayout.Width(50)))
            {
                sprites.RemoveAt(i);
                i--; // Điều chỉnh chỉ số sau khi xóa
            }
            EditorGUILayout.EndHorizontal();
        }

        // Nút để thực hiện đổi tên
        if (GUILayout.Button("Rename Sprites"))
        {
            RenameSprites();
        }
    }

    void HandleDragAndDrop(Rect dropArea)
    {
        Event evt = Event.current;

        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dropArea.Contains(evt.mousePosition))
                    return;

                // Hiển thị biểu tượng khi kéo thả
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();
                    

                    // Lấy tất cả các sprite được kéo thả
                    foreach (Object draggedObject in DragAndDrop.objectReferences)
                    {
                        
                        if (draggedObject.GetType() == typeof(Sprite)){
                            sprites.Add((Sprite)draggedObject);
                            Debug.Log($"Đã thêm {draggedObject.name} vào danh sách đổi tên.");
                        }
                        else if (draggedObject is Texture2D)
                        {
                            Debug.Log("draggedObject is " + "Texture");
                            string assetPath = AssetDatabase.GetAssetPath(draggedObject);
                            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);

                            if (sprite != null)
                            {
                                sprites.Add(sprite);
                                Debug.Log($"Đã thêm Sprite từ Texture: {draggedObject.name}");
                            }
                            else
                            {
                                Debug.LogWarning($"Texture {draggedObject.name} không phải là Sprite.");
                            }
                        }
                        
                    }
                }
                break;
        }
    }

    void RenameSprites()
    {
        if (sprites.Count == 0)
        {
            Debug.LogWarning("Không có sprite nào trong danh sách.");
            return;
        }


        // Đổi tên từng sprite
        for (int i = 0; i < sprites.Count; i++)
        {

            
            Rect spriteRect = sprites[i].rect;
            List<Vector2[]> physicsShapes = new List<Vector2[]>
            {
                new Vector2[]
                {
                    new Vector2(spriteRect.xMin, spriteRect.yMin),
                    new Vector2(spriteRect.xMax, spriteRect.yMin),
                    new Vector2(spriteRect.xMax, spriteRect.yMax),
                    new Vector2(spriteRect.xMin, spriteRect.yMax)
                }
            };
            sprites[i].OverridePhysicsShape(physicsShapes);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}