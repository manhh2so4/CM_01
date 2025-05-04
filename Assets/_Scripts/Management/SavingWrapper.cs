using System.Collections;
using System.Collections.Generic;
using HStrong.Saving;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class SavingWrapper : MonoBehaviour {
    public static SavingWrapper Instance; 
    private const string currentSaveKey = "currentSaveName";
    SavingSystem savingSystem;
    [SerializeField] float fadeInTime = 0.1f;
    [SerializeField] float fadeOutTime = 0.1f;
    [SerializeField] SceneField firstFieldBuildIndex;
    [SerializeField] SceneField menuLevelBuildIndex;
    void Awake()
    {
        savingSystem = GetComponent<SavingSystem>();
        if(Instance == null) Instance = this;
        else Destroy(gameObject);

    }

    public void ContinueGame() 
    {
        LoadLastScene().Forget();
    }
    public void NewGame(string saveFile){
        SetCurrentSave(saveFile);
        LoadFirstScene().Forget();
    }
    public void LoadGame(string saveFile){
        SetCurrentSave(saveFile);
        LoadLastScene().Forget();
    }
    public void LoadMenu()
    {
        LoadMenuScene().Forget();
    }
    private void SetCurrentSave(string saveFile)
    {
        PlayerPrefs.SetString(currentSaveKey, saveFile);
    }
    private string GetCurrentSave()
    {
        return "test2";
        //return PlayerPrefs.GetString(currentSaveKey);
    }
    
    async UniTaskVoid LoadLastScene() {
        Fader fader = Fader.Instance;
        await fader.FadeOut(fadeOutTime);
        await savingSystem.LoadLastScene(GetCurrentSave());
        await fader.FadeIn(fadeInTime);
    }
    async UniTaskVoid LoadFirstScene(){
        Fader fader = Fader.Instance;
        await fader.FadeOut(fadeOutTime);
        await SceneManager.LoadSceneAsync(firstFieldBuildIndex);
        await fader.FadeIn(fadeInTime);
    }
    async UniTaskVoid LoadMenuScene()
    {
        Fader fader = Fader.Instance;
        await fader.FadeOut(fadeOutTime);
        await SceneManager.LoadSceneAsync(menuLevelBuildIndex);
        await fader.FadeIn(fadeInTime);
    }

    public static void LoadDataScene(){
        Instance.LoadLastScene().Forget();
    }

    [Button]
    public static void LoadData(){
        Instance.savingSystem.Load(Instance.GetCurrentSave());
    }
    public static IEnumerator LoadToScene(string sceneName){
        yield return Instance.savingSystem.LoadToScene(Instance.GetCurrentSave(),sceneName);
    }

    public static void Save(){
        Instance.savingSystem.Save(Instance.GetCurrentSave());
    }
    public IEnumerable<string> ListSaves()
    {
        return savingSystem.ListSaves();
    }
    
}