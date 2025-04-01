using HStrong.Saving;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] InventoryItemSO item = null;
    [SerializeField] int number = 1;
    private void Start()
    {
        SpawnPickup();
    }
    public Pickup GetPickup() 
    { 
        return GetComponentInChildren<Pickup>();
    }
    public bool isCollected() 
    { 
        return GetPickup() == null;
    }
    private void SpawnPickup()
    {
        item.SpawnPickup(transform.position, number);
    }
    private void DestroyPickup()
    {
        if (GetPickup())
        {
            Destroy(GetPickup().gameObject);
        }
    }
    public object CaptureState()
    {
        return isCollected();
    }

    public void RestoreState(object state)
    {
        // bool shouldBeCollected = (bool)state;

        // if (shouldBeCollected && !isCollected())
        // {
        //     DestroyPickup();
        // }

        // if (!shouldBeCollected && isCollected())
        // {
        //     SpawnPickup();
        // }
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if(item != null)
        {
            gameObject.name = item.GetDisplayName();
        }
    }
#endif
}