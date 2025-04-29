using UnityEngine;

public class UI_PauseMenu : MonoBehaviour {
    private void OnEnable() {
        Player player = PlayerManager.GetPlayer();
        if(player == null) return;
        Time.timeScale = 0;
        player.enabled = false;
    }

    private void OnDisable() {
        Time.timeScale = 1;
        PlayerManager.GetPlayer().enabled = true;
    }
    public void SaveGame(){
        SavingWrapper.Save();
    }
    public void SaveAndQuit(){
        gameObject.SetActive(false);
        SavingWrapper.Save();
        SavingWrapper.Instance.LoadMenu();
    }
}