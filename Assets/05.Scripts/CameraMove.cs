using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Transform playerPos;
    [SerializeField] Vector3 cameraPos;

    [SerializeField] Vector2 center;
    [SerializeField] Vector2 mapSize;

    [SerializeField] float cameraMoveSpeed;

    float height;
    float width;

    private void OnEnable()
    {
        FindPlayer();
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        transform.position = Vector3.Lerp(transform.position, playerPos.position + cameraPos, Time.deltaTime * cameraMoveSpeed); //플레이어의 위치에 offset값을 더해서 따라다니게 만든다

        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float lz = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.z, -lz + center.y, lz + center.y);

        transform.position = new Vector3(clampX, 200, clampY);
    }

    public void FindPlayer()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
}
