using UnityEngine;
using TMPro;
using HStrong.Shops;
using UnityEngine.UI;
public class ShopUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI shopName;
    [SerializeField] GameObject shopContener;
    [SerializeField] Transform ListItemContainer;
    [SerializeField] ItemShop_UI ItemShop_UIFrefab;
    [SerializeField] TextMeshProUGUI totalField;
    [SerializeField] Button Buy1;
    [SerializeField] TextMeshProUGUI Buy1Text;
    [SerializeField] Button Buy10;
    [SerializeField] TextMeshProUGUI Buy10Text;
    [SerializeField] Button Buy100;
    [SerializeField] TextMeshProUGUI Buy100Text;
    
    //Shopper shopper = null;
    Shop currentShop;
    ShopItem itemCurent;
    void Awake()
    {
        Buy1.onClick.AddListener(() => Buy(1));
        Buy10.onClick.AddListener(() => Buy(10));
        Buy100.onClick.AddListener(() => Buy(100));
    }
    private void OnEnable() {
        this.GameEvents().shopEvents.onActiveShop += ShopChanged;
    }
    private void OnDisable() {
        this.GameEvents().shopEvents.onActiveShop -= ShopChanged;

    }
    private void ShopChanged(Shop shop)
    {
        currentShop = shop;
        shopContener.SetActive(currentShop != null);

        if (currentShop == null) return;
        shopName.text = currentShop.GetShopName();

        currentShop.onChange += RefreshUI;
        RefreshUI();
    }
    void RefreshUI(){ 
        foreach (Transform child in ListItemContainer){
            Destroy(child.gameObject);
        }
        foreach (var item in currentShop.GetAllItems()){
           ItemShop_UI itemShop_UI = Instantiate(ItemShop_UIFrefab, ListItemContainer);
           itemShop_UI.Setup(currentShop, item, () => SetButton(item));
        }
    }
    void SetButton(ShopItem _item){
        this.itemCurent = _item;

        Buy1.interactable = currentShop.CanBuy(itemCurent,1);
        Buy10.interactable = currentShop.CanBuy(itemCurent,10);
        Buy100.interactable = currentShop.CanBuy(itemCurent,100);

        Buy1Text.text = GetPriceItem(itemCurent,1).ToString() + " đ";
        Buy10Text.text = GetPriceItem(itemCurent,10).ToString() + " đ";
        Buy100Text.text = GetPriceItem(itemCurent,100).ToString() + " đ";
    }
    void Buy(int _quantity){
        currentShop.ConfirmTransaction(itemCurent.GetInventoryItem(), _quantity);
    }
    float GetPriceItem(ShopItem _item,int quantity){
        return _item.GetPrice() * quantity;
    }
    public void Close()
    {
        currentShop.colider2D.enabled = true;
        ShopChanged(null);
    }
}