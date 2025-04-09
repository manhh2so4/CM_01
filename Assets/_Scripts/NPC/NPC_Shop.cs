using System;
using System.Collections.Generic;
using HStrong.Shops;
using UnityEngine;

public class NPC_Shop : StaticNPC {

      [SerializeField] string shopName;
      public event Action onChange;
      [SerializeField] StockItemConfig[] stockConfigs;
      [Serializable]
      class StockItemConfig
      {
         public InventoryItemSO item;
         public int initialStock;
         [Range(0, 100)]
         public float DiscountPercent;
      }
      Dictionary<InventoryItemSO, int> transaction = new Dictionary<InventoryItemSO, int>();
      Dictionary<InventoryItemSO, int> stock = new Dictionary<InventoryItemSO, int>();
      protected override void Awake() {
         base.Awake();
         foreach (StockItemConfig config in stockConfigs)
         {
               stock[config.item] = config.initialStock;
         }
      }
      public IEnumerable<ShopItem> GetFilteredItems()
      {
         return GetAllItems();
      }

      public IEnumerable<ShopItem> GetAllItems() {
         foreach (StockItemConfig config in stockConfigs)
         {
               int price = (int)(config.item.GetPrice() * (1f - config.DiscountPercent/100));
               int quantityInTransaction = 0;
               transaction.TryGetValue(config.item, out quantityInTransaction);
               int currentStock = stock[config.item];
               yield return new ShopItem(config.item, currentStock, price , quantityInTransaction);
         }
      }
      public void ConfirmTransaction(InventoryItemSO item, int _quantity){
         Inventory inventory = PlayerManager.GetInventory();
         if(inventory == null) return;

         for(int i = 0; i < _quantity; i++){
               bool success = inventory.AddToFirstEmptySlot(item, 1);
         }
         onChange?.Invoke();
      }

      public bool CanBuy(ShopItem item, int quantity){
         if( item.GetCount() + quantity < 0) return false;

         Purse purse = PlayerManager.GetPurse();
         if( purse.GetBalance() < item.GetPrice() * quantity) return false;

         return true;
      }
      public void AddToTransaction(InventoryItemSO item, int quantity){

         if(!transaction.ContainsKey(item)){
               transaction[item] = 0;
         }

         transaction[item] += quantity;

         if(transaction[item] <= 0){
               transaction.Remove(item);
         }
         onChange?.Invoke();
      }
      public string GetShopName()
      {
         return shopName;
      }

      public override void Interact()
      {
         this.GameEvents().shopEvents.ActiveShop(this);
      }
}

