using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [Header("References")]
    public GhostPool ghostPool;            // assign in Inspector
    [SerializeField] 
    private Transform playerMainCamera;    // the playerMainCamera transform
    


    [Header("Spawn Settings")]
    public float spawnInterval = 15f;
    public float spawnDistance = 3f;  // distance in front of the playerMainCamera

    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnGhost();
            timer = spawnInterval;
        }
    }

    public void SpawnGhost()
    {
        // Compute position in front of the playerMainCamera
        Vector3 spawnPos = playerMainCamera.position + playerMainCamera.forward * spawnDistance;

        Ghost ghost = ghostPool.Pool.Get();
        ghost.transform.position = spawnPos;
        ghost.transform.rotation = Quaternion.LookRotation(playerMainCamera.forward);
   
        ghost.targetObj= playerMainCamera.gameObject;
    }
}
