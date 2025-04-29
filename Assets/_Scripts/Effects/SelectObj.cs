using UnityEngine;

public class SelectObj : MonoBehaviour {
    Vector3 startPosition;
    float speed = 10f;
    float height = .05f;

    public void setUp(float _height , Transform parent){
        
        transform.parent = parent;
        startPosition = new Vector3(0, _height, 0);

        transform.parent = parent;
        transform.localPosition = startPosition;

    }
    void OnEnable()
    {
        
        startPosition = transform.localPosition;

    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * height;
        transform.localPosition = new Vector3(startPosition.x, newY, startPosition.z);
    }
}