using HStrong.Saving;
using TMPro;
using UnityEngine;

public class UI_MainMenu : MonoBehaviour {

    [SerializeField] TMP_InputField newGameNameField;

    public void ContinueGame()
    {
        SavingWrapper.Instance.ContinueGame();
    }
    public void NewGame(){
        SavingWrapper.Instance.NewGame(newGameNameField.text);
    }
}