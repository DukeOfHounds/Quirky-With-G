using UnityEngine;
using UnityEngine.Pool;

public class Ghost : MonoBehaviour
{
    public GameObject targetObj;
    public float moveSpeed = 0.15f;
    public float speedUpFactor = 0.01f;
    public float speedRadius = 0.5f;

    private IObjectPool<Ghost> pool;

    public void Init(IObjectPool<Ghost> poolRef, GameObject target)
    {
        pool = poolRef;
        targetObj = target;
    }

    public void Despawn()
    {
        pool.Release(this);
    }

    public void EncounterPlayer()
    {
        // Logic for when the ghost encounters the player
        Despawn();
    }

    public void Death()
    {
        // Logic for when the ghost dies
        Despawn();
    }


    void Update()
    {
        if (targetObj == null) return;

        transform.LookAt(targetObj.transform);
        transform.position += transform.forward *
            (Time.deltaTime * (moveSpeed + ((transform.position - targetObj.transform.position).magnitude - speedRadius) * speedUpFactor));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObj)
        {
            EncounterPlayer();
        }
        else if (other.CompareTag("Fist"))
        {
            Death();
        }
    }
}
