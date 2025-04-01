using System;
using UnityEngine;
using HStrong.Shops;
public class ShopEvents{
    public event Action<Shop> onActiveShop;
    public void ActiveShop(Shop shop) => onActiveShop?.Invoke(shop);

    
}