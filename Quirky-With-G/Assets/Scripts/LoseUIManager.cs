using UnityEngine;
using UnityEngine.Pool;
public class LoseUIManager : MonoBehaviour
{
    public GameObject loseScreenCanvas;
    public Transform playerCamera;
    public GhostSpawner ghostSpawner;
    public GhostPool ghostPool;

    void Start()
    {
        loseScreenCanvas.SetActive(false);
    }

    public void ShowLoseScreen()
    {
        Debug.Log("Player has lost. Showing lose screen.");
        // Stop spawning
        ghostSpawner.gameObject.SetActive(false);
 
        // 🔥 Release all ghosts
        ghostPool.ReleaseAllGhosts();

        // Position UI
        loseScreenCanvas.transform.position =
            playerCamera.position + playerCamera.forward * 2f;
        loseScreenCanvas.transform.rotation =
            Quaternion.LookRotation(playerCamera.forward);

        loseScreenCanvas.SetActive(true);
    }
}
