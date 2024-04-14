using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private GameObject playerObject;
    private Rigidbody rb;
    public float moveSpeed = 5f;

    public bool InitChase;


    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.transform;
        rb = GetComponent<Rigidbody>();

    }
    private void Update()
    {
        if (InitChase)
        {
            ChasePlayer();
        }
    }

    public void ChasePlayer()
    {
        var position = transform.position;
        Vector3 direction = player.position - position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = rotation;

        direction.Normalize();
        position += direction * (moveSpeed * Time.deltaTime);
        transform.position = position;
    }
}
