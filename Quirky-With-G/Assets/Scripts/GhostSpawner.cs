using UnityEngine;
using UnityEngine.Pool;
public class GhostSpawner : MonoBehaviour
{
    public GhostPool ghostPool;
    public Transform playerMainCamera;
    
    public LoseUIManager loseUIManager;

    public float spawnInterval = 15f;
    public float spawnDistance = 3f;
    private float timer;

    void Update()
    {
        if (!gameObject.activeSelf) return; // (optional safeguard)

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnGhost();
            timer = spawnInterval;
        }
    }

    public void SpawnGhost()
    {
        float halfArc = 60;
        float randomAngle = Random.Range(-halfArc, halfArc);

        Quaternion arcRotation = Quaternion.Euler(0f, randomAngle, 0f);
        Vector3 spawnDir = arcRotation * playerMainCamera.forward;
        Vector3 spawnPos = playerMainCamera.position + spawnDir * spawnDistance;

        Ghost ghost = ghostPool.Pool.Get();
        ghost.transform.position = spawnPos;
        ghost.transform.rotation = Quaternion.LookRotation(playerMainCamera.position - spawnPos);
        ghost.targetObj = playerMainCamera.gameObject;
        ghost.loseUIManager = loseUIManager;
    }
}
