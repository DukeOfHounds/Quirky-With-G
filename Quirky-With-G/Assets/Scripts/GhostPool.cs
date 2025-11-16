using UnityEngine;
using UnityEngine.Pool;

public class GhostPool : MonoBehaviour
{
    public Ghost ghostPrefab;
    public GameObject targetObj;

    public int defaultCapacity = 10;
    public int maxSize = 100;

    public IObjectPool<Ghost> Pool { get; private set; }

    void Awake()
    {
        Pool = new ObjectPool<Ghost>(
            CreateGhost,
            OnGetGhost,
            OnReleaseGhost,
            OnDestroyGhost,
            collectionCheck: false,
            defaultCapacity,
            maxSize
        );
    }

    private Ghost CreateGhost()
    {
        Ghost g = Instantiate(ghostPrefab);
        g.Init(Pool, targetObj);
        return g;
    }

    private void OnGetGhost(Ghost g)
    {
        g.gameObject.SetActive(true);
    }

    private void OnReleaseGhost(Ghost g)
    {
        g.gameObject.SetActive(false);
    }

    private void OnDestroyGhost(Ghost g)
    {
        Destroy(g.gameObject);
    }
}
