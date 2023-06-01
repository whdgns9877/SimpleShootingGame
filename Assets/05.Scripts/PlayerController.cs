using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 8f;

    // Update is called once per frame
    void FixedUpdate()
    {
        //수평, 수직축의 입력값을 감지하여 저장.
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        //실제 이동 속도를 입력값과 이동 속력을 사용해 결정
        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        ////Vector3 속도를 (xSpeed, 0, zSpeed) 로 생성
        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);

        transform.position += newVelocity * speed * Time.deltaTime;

        Vector3 pos = transform.position;
        if (transform.position.x < -1000)
        {
            pos.x = -1000;
            transform.position = pos;
        }
        if (transform.position.x > 1000)
        {
            pos.x = 1000;
            transform.position = pos;
        }
        if (transform.position.z < -1000)
        {
            pos.z = -1000;
            transform.position = pos;
        }
        if (transform.position.z > 1000)
        {
            pos.z = 1000;
            transform.position = pos;
        }
    }

    public void Die()
    {
        GameObject.Find("Sounds").transform.Find("PlayerDieSound").gameObject.GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);
        GameManager.Instance.EndGame(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Boss")
        {
            Die();
        }
    }
}
