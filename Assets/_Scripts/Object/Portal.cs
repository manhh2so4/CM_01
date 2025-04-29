using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    [Header("Spawn TO")]
    [SerializeField] public SceneField _sceneToLoad;
    [SerializeField] Transform spawnPoint;
    Vector2 startPosition;
    float speed = 10f;
    float height = .05f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           NextMap().Forget();
        }
    }
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        float newX = startPosition.x + Mathf.Sin(Time.time * speed) * height;
        transform.localPosition = new Vector2(newX, startPosition.y);
    }
    private async UniTaskVoid NextMap()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        DontDestroyOnLoad( gameObject );

        await Fader.Instance.FadeOut(0.2f);

        SavingWrapper.Save();
        await SavingWrapper.LoadToScene(_sceneToLoad);
        SavingWrapper.LoadData();

        Portal otherPortal = GetOtherPortal(currentScene);
        if( otherPortal == null ){
            Debug.LogError("otherPortal is null");
            await UniTask.Yield();
        }
        UpdatePlayer( otherPortal );

        SavingWrapper.Save();

        await Fader.Instance.FadeIn(0.2f);
        Destroy(gameObject);
    }
    private void UpdatePlayer(Portal otherPortal)
    {
        GameObject player = PlayerManager.GetPlayer().gameObject;
        player.transform.position = otherPortal.spawnPoint.position;
    }
    private Portal GetOtherPortal( string currentScene )
    {
        foreach (Portal portal in FindObjectsOfType<Portal>())
        {
            if(portal._sceneToLoad == currentScene) return portal;
        }
        return null;
    }

}