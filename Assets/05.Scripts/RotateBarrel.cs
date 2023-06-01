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
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition); //������ ���� ī�޶�κ��� �����Ͽ� ���콺 ��ġ�� ����
        GroupPlane = new Plane(Vector3.up, new Vector3(0,50,0)); //������ ���߱����Ͽ� Plane ��ü�� ����
        float rayLength = 100f; //������ �Ÿ�
   
        if(GroupPlane.Raycast(cameraRay, out rayLength) //������ �÷��ο� ���߰� �� ������ �Ÿ��� �÷��̾��� ��ġ�� ������� ������ �ִٸ�
            && Vector3.Distance(cameraRay.GetPoint(rayLength), transform.position) > 10f)
        {
            mousePos.x = cameraRay.GetPoint(rayLength).x; //mousePos ������ �ش� ���� �־��ְ�
            mousePos.z = cameraRay.GetPoint(rayLength).z;
            mousePos.y = transform.position.y;
            transform.LookAt(mousePos); //�� ��ġ�� �ٶ����
        }

        if (Input.GetMouseButton(0)) //���콺 ��Ŭ���� �ϰ�
        {
            if (Time.time - oldTime > 0.1f)
            {
                ObjectPool.GetPlayerBullet(); //������ƮǮ�� ����ִ� �Ѿ��� ������ Ȱ��ȭ ��Ų��
                GameObject.Find("Sounds").transform.Find("PlayerShootSound").gameObject.GetComponent<AudioSource>().Play();
                oldTime = Time.time;
            }
        }
    }
}
