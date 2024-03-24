using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public Transform playerTarget;
    public float bulletSpeed;
    public Vector3 offset;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTarget.position + offset, bulletSpeed * Time.deltaTime);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().Dead();
            Destroy(gameObject);
        }
    }
}
