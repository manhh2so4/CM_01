using Cathei.BakingSheet;
using Cathei.BakingSheet.Unity;

[System.Serializable]
public class BaseSheetContainer : SheetContainerBase{
    public BaseSheetContainer() : base(UnityLogger.Default){}
    public virtual void BakeData(){} 
}