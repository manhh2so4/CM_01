using UnityEngine;

public class LineRendererExample : MonoBehaviour
{
    private LineRenderer lineRenderer;
    
    [Header("Line Settings")]
    public Color startColor = Color.red;
    public Color endColor = Color.yellow;
    public float startWidth = 0.1f;
    public float endWidth = 0.1f;
    public int numberOfPoints = 5;
    public bool useWorldSpace = true;
    
    [Header("Movement Settings")]
    public bool animateLine = true;
    public float moveSpeed = 1.0f;
    public float radius = 2.0f;
    
    void Start()
    {
        // Thêm và khởi tạo LineRenderer component
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        
        // Thiết lập vật liệu và shader
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        
        // Thiết lập các thuộc tính của LineRenderer
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.positionCount = numberOfPoints;
        lineRenderer.useWorldSpace = useWorldSpace;
        
        // Các thiết lập khác (không bắt buộc)
        lineRenderer.numCapVertices = 10; // Làm tròn các đầu mút
        lineRenderer.numCornerVertices = 10; // Làm tròn các góc
        lineRenderer.loop = false; // Không nối điểm cuối với điểm đầu
        
        // Thiết lập vị trí ban đầu
        SetupLine();
    }
    
    void Update()
    {
        if (animateLine)
        {
            AnimateLine();
        }
    }
    
    void SetupLine()
    {
        // Thiết lập các điểm cho đường thẳng
        if (numberOfPoints <= 0)
            return;
            
        for (int i = 0; i < numberOfPoints; i++)
        {
            float progress = (float)i / (numberOfPoints - 1);
            float angle = progress * Mathf.PI * 2.0f;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            
            lineRenderer.SetPosition(i, new Vector3(x, 0, z));
        }
    }
    
    void AnimateLine()
    {
        // Di chuyển các điểm để tạo hiệu ứng animation
        for (int i = 0; i < numberOfPoints; i++)
        {
            Vector3 position = lineRenderer.GetPosition(i);
            float angle = Time.time * moveSpeed + (float)i / numberOfPoints * Mathf.PI * 2.0f;
            
            position.x = Mathf.Cos(angle) * radius;
            position.z = Mathf.Sin(angle) * radius;
            
            lineRenderer.SetPosition(i, position);
        }
    }
    
    // Phương thức để thêm một điểm mới vào LineRenderer
    public void AddPoint(Vector3 position)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
    }
    
    // Phương thức để xóa tất cả các điểm
    public void ClearPoints()
    {
        lineRenderer.positionCount = 0;
    }
}