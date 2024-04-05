using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private GameObject playerObject;
    private Rigidbody2D rb;
    public float moveSpeed = 5f;

    public bool InitChase;


    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.transform;
        rb = GetComponent<Rigidbody2D>();

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
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));

    }
}
