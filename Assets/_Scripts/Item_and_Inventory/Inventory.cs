using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


public class Inventory : MonoBehaviour {
    public Core core;
    private CharacterStats charStats;
    public static Inventory Instance;

    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictianory;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictianory;

    public Action OnChangeCloth;


    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private Transform StatSlotParent;


    private UI_ItemSlot[] iteamSlot;
    protected UI_EquipmentSlot[] equipmentSlot;
    protected UI_StatSlot[] statSlots;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
    }
    private void Start()
    {
        Load_Components();
        inventory = new List<InventoryItem>();
        inventoryDictianory = new Dictionary<ItemData, InventoryItem>();

        equipment = new List<InventoryItem>();
        equipmentDictianory = new Dictionary<ItemData_Equipment, InventoryItem>();

        iteamSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        statSlots = StatSlotParent.GetComponentsInChildren<UI_StatSlot>();

    }
    private void UpdateSlotUI()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictianory)
            {
                if (item.Key.Type == equipmentSlot[i].slotType)
                {
                    equipmentSlot[i].UpdateSlot(item.Value);
                }
            }
        }
        for (int i = 0; i < iteamSlot.Length; i++)
        {
            iteamSlot[i].CleanUpSlot();
        }


        for (int i = 0; i < inventory.Count; i++)
        {
            iteamSlot[i].UpdateSlot(inventory[i]);
        }

        for (int i = 0; i < statSlots.Length; i++)
        {
            statSlots[i].UpdateStatValue();
        }

    }
    #region Equipment
    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment newEquipment = _item as ItemData_Equipment;
        InventoryItem newItem = new InventoryItem(newEquipment);

        ItemData_Equipment oldEquipment = null;

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictianory)
        {
            if (item.Key.Type == newEquipment.Type)
            {
                oldEquipment = item.Key;
            }
        }

        if(oldEquipment != null) {

            UnEquipment(oldEquipment);
            AddItem(oldEquipment);
        }

        if(newEquipment.Type == EquipmentType.body_armor || newEquipment.Type == EquipmentType.Pan_armor)
        {
            OnChangeCloth?.Invoke();
        }

        equipment.Add(newItem);
        equipmentDictianory.Add(newEquipment, newItem);
        newEquipment.AddModifiers(charStats);
        RemoveItem(newEquipment);
    }

    public void UnEquipment(ItemData_Equipment itemToRemove)
    {
        if (equipmentDictianory.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipment.Remove(value);
            equipmentDictianory.Remove(itemToRemove);
            itemToRemove.RemoveModifiers(charStats);
        }
        UpdateSlotUI();
    }
    #endregion

    #region Add and Remove
    public void AddItem(ItemData _item)
    {       
        AddToInventory(_item);
        UpdateSlotUI();
    }

    private void AddToInventory(ItemData _item)
    {
        if (inventoryDictianory.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictianory.Add(_item, newItem);
        }
    }

    public void RemoveItem(ItemData _item)
    {
        if (inventoryDictianory.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                inventory.Remove(value);
                inventoryDictianory.Remove(_item);
            }
            else
                value.RemoveStack();
        }
        UpdateSlotUI();
    }
    #endregion
    void Load_Components(){
        core = transform.parent.Find("Core").GetComponent<Core>();
        charStats = core.GetCoreComponent<CharacterStats>();
    }
}