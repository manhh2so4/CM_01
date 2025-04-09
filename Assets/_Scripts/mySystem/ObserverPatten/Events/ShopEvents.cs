using System;
using UnityEngine;
using HStrong.Shops;
public class ShopEvents{
    public event Action<NPC_Shop> onActiveShop;
    public void ActiveShop(NPC_Shop shop) => onActiveShop?.Invoke(shop);

    
}