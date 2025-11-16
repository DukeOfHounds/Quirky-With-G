using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class GhostPool : MonoBehaviour
{
    public Ghost ghostPrefab;
    public GameObject targetObj;

    public int defaultCapacity = 10;
    public int maxSize = 100;

    // Track active ghosts
    private readonly List<Ghost> activeGhosts = new List<Ghost>();

    public IObjectPool<Ghost> Pool { get; private set; }

    void Awake()
    {
        Pool = new ObjectPool<Ghost>(
            CreateGhost,
            OnGetGhost,
            OnReleaseGhost,
            OnDestroyGhost,
            false,
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
        activeGhosts.Add(g);
    }

    private void OnReleaseGhost(Ghost g)
    {
        g.gameObject.SetActive(false);
        activeGhosts.Remove(g);
    }

    private void OnDestroyGhost(Ghost g)
    {
        Destroy(g.gameObject);
    }

    // 🔥 Call this when the player loses
    public void ReleaseAllGhosts()
    {
        // Copy the list because it changes during iteration
        var copy = new List<Ghost>(activeGhosts);

        foreach (Ghost g in copy)
        {
            Pool.Release(g);
        }
    }
}
