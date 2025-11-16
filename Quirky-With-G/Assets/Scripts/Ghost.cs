using UnityEngine;

public class Ghost : MonoBehaviour
{
    public GameObject targetObj;
    public float moveSpeed = 0.15f;

    public float speedUpFactor = 0.01f;
    public float speedRadius = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetObj.transform, Vector3.up);
        transform.position += transform.forward * (Time.deltaTime * (moveSpeed + ((transform.position - targetObj.transform.position).magnitude - speedRadius) * speedUpFactor));
    }
}
