
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(BoxCollider2D))] 
public class RaycastController : MonoBehaviour {
	BoxCollider2D boxCollider;
    Vector2 Center;
	Vector2 Size ;
	
	protected const float skinWidth = .05f;
    [SerializeField] protected int horizontalRayCount = 4;
	[SerializeField] protected int verticalRayCount = 4;

	protected float horizontalRaySpacing;
	protected float verticalRaySpacing;

	protected RaycastOrigins raycastOrigins;
	[Button]
	public virtual void Awake() {
		boxCollider = GetComponent<BoxCollider2D>();
		Center = boxCollider.offset;
		Size = boxCollider.size;
		CalculateRaySpacing ();
	}

	protected void UpdateRaycastOrigins(){

		Vector2 skin = Size;
		skin.Set(Size.x - skinWidth,Size.y - skinWidth);
		Vector2 max = (Vector2)transform.position + Center + skin/2;
		Vector2 min = (Vector2)transform.position + Center - skin/2;

		raycastOrigins.bottomLeft.Set(min.x ,min.y );
		raycastOrigins.bottomRight.Set(max.x , min.y );

		raycastOrigins.topLeft.Set(min.x , max.y );
		raycastOrigins.topRight.Set(max.x , max.y );
	}
	
    protected void CalculateRaySpacing(){

		horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
		horizontalRaySpacing = (Size.y - skinWidth) / (horizontalRayCount - 1);
		verticalRaySpacing = (Size.x - skinWidth) / (verticalRayCount - 1);

	}

	protected struct RaycastOrigins {
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}
    void OnDrawGizmos()
    {
        // Gizmos.color = Color.blue;
		// Gizmos.DrawWireCube( (Vector2)transform.position + Center , Size);

		// for (int i = 0; i < verticalRayCount; i ++) {
		//  	Gizmos.DrawRay(raycastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.up * -.5f);
		// }
		// for (int i = 0; i < horizontalRayCount; i ++) {
		// 	Gizmos.DrawRay(raycastOrigins.bottomLeft + Vector2.up * horizontalRaySpacing * i, Vector2.right * -.5f);
		// }
    }
}