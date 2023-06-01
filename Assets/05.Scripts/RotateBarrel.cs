using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBarrel : MonoBehaviour
{
    Plane GroupPlane;
    public Vector3 mousePos;
    Ray cameraRay;

    float oldTime = 0;
    // Update is called once per frame
    void Update()
    {
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition); //광선을 메인 카메라로부터 시작하여 마우스 위치로 생성
        GroupPlane = new Plane(Vector3.up, new Vector3(0,50,0)); //광선을 맞추기위하여 Plane 객체를 생성
        float rayLength = 100f; //광선의 거리
   
        if(GroupPlane.Raycast(cameraRay, out rayLength) //광선을 플레인에 맞추고 그 광선의 거리가 플레이어의 위치와 어느정도 떨어져 있다면
            && Vector3.Distance(cameraRay.GetPoint(rayLength), transform.position) > 10f)
        {
            mousePos.x = cameraRay.GetPoint(rayLength).x; //mousePos 변수에 해당 지점 넣어주고
            mousePos.z = cameraRay.GetPoint(rayLength).z;
            mousePos.y = transform.position.y;
            transform.LookAt(mousePos); //그 위치를 바라봐라
        }

        if (Input.GetMouseButton(0)) //마우스 좌클릭을 하고
        {
            if (Time.time - oldTime > 0.1f)
            {
                ObjectPool.GetPlayerBullet(); //오브젝트풀에 들어있는 총알을 꺼내서 활성화 시킨다
                GameObject.Find("Sounds").transform.Find("PlayerShootSound").gameObject.GetComponent<AudioSource>().Play();
                oldTime = Time.time;
            }
        }
    }
}
