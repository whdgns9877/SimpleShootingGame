using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 1; //탄알 이동 속력aaaaaaaaa

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed); //이미 방향은 정해놓았으니 앞으로 쭉 가라

        Vector3 pos = transform.position;
        if (transform.position.x < -1000
            || transform.position.x > 1000
            || transform.position.z < -1000
            || transform.position.z > 1000) ObjectPool.ReturnEnemyBullet(this);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.Die();
                ObjectPool.ReturnEnemyBullet(this);
            }
        }
    }
}
