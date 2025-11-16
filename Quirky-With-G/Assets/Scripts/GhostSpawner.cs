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

    private Quaternion spawnDir;

    void Start()
    {
        spawnDir =  playerMainCamera.transform.rotation;
    }

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
        float halfArc = 45f;
        float randomAngle = Random.Range(-halfArc + spawnDir.eulerAngles.y, halfArc + spawnDir.eulerAngles.y);

        Quaternion arcRotation = Quaternion.Euler(0f, randomAngle, 0f);
        Vector3 spawnPos = playerMainCamera.position + arcRotation * Vector3.forward * spawnDistance;
        Ghost ghost = ghostPool.Pool.Get();
        ghost.transform.position = spawnPos;
        ghost.targetObj = playerMainCamera.gameObject;
        ghost.loseUIManager = loseUIManager;
    }
}
