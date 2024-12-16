using HStrong.Saving;
using UnityEngine;

public class PickupSpawner : MonoBehaviour, ISaveable
{
    [SerializeField] InventoryItemSO item = null;
    [SerializeField] int number = 1;
    private void Awake()
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
        var spawnedPickup = item.SpawnPickup(transform.position, number);
        spawnedPickup.transform.SetParent(transform);
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
        bool shouldBeCollected = (bool)state;

        if (shouldBeCollected && !isCollected())
        {
            DestroyPickup();
        }

        if (!shouldBeCollected && isCollected())
        {
            SpawnPickup();
        }
    }
}