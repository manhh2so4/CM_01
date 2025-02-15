using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapManager : MonoBehaviour {
    public static SceneSwapManager Instance;
    private Portal.DestinationIdentifier _destination;
    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static void SwapSceneFormDoorUse(SceneField myScene, Portal.DestinationIdentifier destination)
    {
        //Instance.StartCoroutine(Instance.FadeOutThenChangeScene(myScene,destination));
    }
    // private IEnumerator FadeOutThenChangeScene(SceneField myScene,Portal.DestinationIdentifier destination = Portal.DestinationIdentifier.None)
    // {
    //     _destination = destination;
    //     SceneManager.LoadScene(myScene);

    // }

}