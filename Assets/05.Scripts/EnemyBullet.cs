using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 1; //ź�� �̵� �ӷ�aaaaaaaaa

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed); //�̹� ������ ���س������� ������ �� ����

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
