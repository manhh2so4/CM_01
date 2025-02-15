using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public enum DestinationIdentifier
    {
        None,A, B, C, D, E
    }
    [Header("Spawn TO")]
    public DestinationIdentifier CurrentGate;
    [SerializeField] private SceneField _sceneToLoad;

    [SerializeField] DestinationIdentifier destination;
    
    [SerializeField] Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Transition());
        }
    }
    private IEnumerator Transition()
    {


        DontDestroyOnLoad(gameObject);
        yield return SceneManager.LoadSceneAsync(_sceneToLoad);

        
        Portal otherPortal = GetOtherPortal();
        UpdatePlayer(otherPortal);

        Destroy(gameObject);
    }
    private void UpdatePlayer(Portal otherPortal)
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = otherPortal.spawnPoint.position;
        player.transform.rotation = otherPortal.spawnPoint.rotation;
    }
    private Portal GetOtherPortal()
    {
        foreach (Portal portal in FindObjectsOfType<Portal>())
        {
            return portal;
        }
        return null;
    }



}