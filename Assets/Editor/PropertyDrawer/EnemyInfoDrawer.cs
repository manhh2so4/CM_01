using UnityEngine;
using UnityEditor; // Cần thiết cho Editor scripting

// Thuộc tính này liên kết Drawer với Struct bạn đã tạo
[CustomPropertyDrawer(typeof(EnemyInfo))]
public class EnemyInfoDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Bắt đầu nhóm thuộc tính. Cần thiết cho prefab overrides và context menu.
        label = EditorGUI.BeginProperty(position, label, property);

        // Vẽ label chính (tên biến trong script MonoBehaviour)
        // PrefixLabel trả về phần Rect còn lại sau khi vẽ label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Lưu mức thụt lề hiện tại và đặt lại về 0 để các trường con không bị thụt vào
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Tính toán các Rect cho từng trường (ID và Number)
        float labelWidth = 30f; // Chiều rộng cố định cho các label nhỏ "ID", "Num"
        float fieldWidth = (position.width - labelWidth * 2) / 2f; // Chia đều không gian còn lại cho 2 ô nhập liệu
        float spacing = 2f; // Khoảng cách nhỏ giữa các phần tử

        // Rect cho label "ID:"
        Rect idLabelRect = new Rect(position.x, position.y, labelWidth, position.height);
        // Rect cho ô nhập liệu ID
        Rect idFieldRect = new Rect(position.x + labelWidth, position.y, fieldWidth - spacing, position.height);
        // Rect cho label "Num:"
        Rect numLabelRect = new Rect(position.x + labelWidth + fieldWidth, position.y, labelWidth, position.height);
        // Rect cho ô nhập liệu Number
        Rect numFieldRect = new Rect(position.x + labelWidth * 2 + fieldWidth, position.y, fieldWidth - spacing, position.height);


        // Vẽ các label nhỏ và các trường nhập liệu IntField
        EditorGUI.LabelField(idLabelRect, "ID:");
        // Lấy SerializedProperty cho trường 'id' bên trong struct
        EditorGUI.PropertyField(idFieldRect, property.FindPropertyRelative("idEnemy"), GUIContent.none); // GUIContent.none để không vẽ label mặc định của trường

        EditorGUI.LabelField(numLabelRect, "Num:");
        // Lấy SerializedProperty cho trường 'number' bên trong struct
        EditorGUI.PropertyField(numFieldRect, property.FindPropertyRelative("count"), GUIContent.none);
        // Khôi phục lại mức thụt lề ban đầu
        EditorGUI.indentLevel = indent;

        // Kết thúc nhóm thuộc tính
        EditorGUI.EndProperty();
    }

    // (Tùy chọn) Ghi đè GetPropertyHeight để đảm bảo chiều cao đúng nếu cần nhiều dòng
    // Trong trường hợp này, một dòng là đủ nên không cần ghi đè hoặc trả về giá trị mặc định
    // public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    // {
    //     return EditorGUIUtility.singleLineHeight;
    // }
}