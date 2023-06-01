using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField] private GameObject poolingObjectPlayerBullet;

    [SerializeField] private GameObject poolingObjectEnemyBullet;

    [SerializeField] private GameObject poolingObjectEnemy;

    Queue<Bullet> poolingObjectPlayerBulletQueue = new Queue<Bullet>();

    Queue<EnemyBullet> poolingObjectEnemyBulletQueue = new Queue<EnemyBullet>();

    Queue<EnemyController> poolingObjectEnemyQueue = new Queue<EnemyController>();

    private void Awake()
    {
        Instance = this;

        InitBullet(7,10,30);
    }

    private void InitBullet(int initPlayerBulletCount, int initEnemyCount,int initEnemyBulletCount)
    {
        for (int i = 0; i < initPlayerBulletCount; i++) poolingObjectPlayerBulletQueue.Enqueue(CreateNewPlayerBullet());
        for (int i = 0; i < initEnemyBulletCount; i++) poolingObjectEnemyBulletQueue.Enqueue(CreateNewEnemyBullet());
        for (int i = 0; i < initEnemyCount; i++) poolingObjectEnemyQueue.Enqueue(CreateNewEnemy());
    }

    private Bullet CreateNewPlayerBullet()
    {
        //poolingObjectPrefab�� �����ϰ� Bullet ������Ʈ�� ������ �̸� newObj ������ �־��ش�.
        var newObj = Instantiate(poolingObjectPlayerBullet).GetComponent<Bullet>();
        newObj.gameObject.SetActive(false);   //�̸� ��Ȱ��ȭ ���ְ�
        newObj.transform.SetParent(transform);//��ġ���� �θ��� ��ġ ��ObjectPool ������Ʈ�� ��ġ�� ����
        return newObj; //�ش簴ü ��ȯ
    }  

    private EnemyBullet CreateNewEnemyBullet()
    {
        //poolingObjectPrefab�� �����ϰ� Bullet ������Ʈ�� ������ �̸� newObj ������ �־��ش�.
        var newObj = Instantiate(poolingObjectEnemyBullet).GetComponent<EnemyBullet>();
        newObj.gameObject.SetActive(false);   //�̸� ��Ȱ��ȭ ���ְ�
        newObj.transform.SetParent(transform);//��ġ���� �θ��� ��ġ ��ObjectPool ������Ʈ�� ��ġ�� ����
        return newObj; //�ش簴ü ��ȯ
    }
    
    private EnemyController CreateNewEnemy()
    {
        //poolingObjectPrefab�� �����ϰ� Bullet ������Ʈ�� ������ �̸� newObj ������ �־��ش�.
        var newObj = Instantiate(poolingObjectEnemy).GetComponent<EnemyController>();
        newObj.gameObject.SetActive(false);   //�̸� ��Ȱ��ȭ ���ְ�
        newObj.transform.SetParent(transform);//��ġ���� �θ��� ��ġ ��ObjectPool ������Ʈ�� ��ġ�� ����
        return newObj; //�ش簴ü ��ȯ
    }



    public static Bullet GetPlayerBullet()
    {
        if (Instance.poolingObjectPlayerBulletQueue.Count > 0) // ť�� �����ִ� �Ѿ��� ������
        {
            //�� �Լ��� ������ƮǮ�� �־�������� �ʿ信 ���� �ϳ��� �����ִ� �Լ��̴�
            var obj = Instance.poolingObjectPlayerBulletQueue.Dequeue(); //Queue �� �ִ� ������Ʈ�� ������ obj ��� ������ �־��ְ�
            obj.transform.SetParent(null); //�θ�� ����߸��� (�پ������� �������� �����Ŵϱ�)
            obj.gameObject.SetActive(true);//Ȱ��ȭ ������ �Ŀ�
            obj.GetComponent<Bullet>().InitBullet();
            obj.transform.SetParent(GameObject.Find("Bullets").transform);
            return obj; //�̸� ��ȯ
        }
        else //���� ť�� ������ �Ѿ��� ���ٸ�
        {
            var newObj = Instance.CreateNewPlayerBullet(); //���Ӱ� �Ѿ��� ������ְ� 
            newObj.gameObject.SetActive(true); //������� �Ѿ��� �ٷ� Ȱ��ȭ�Ͽ�
            newObj.transform.SetParent(null); //�θ�� ����߸����·�
            newObj.GetComponent<Bullet>().InitBullet();
            newObj.transform.SetParent(GameObject.Find("Bullets").transform);
            return newObj; //��ȯ �״ϱ� ���Ը��ؼ� �������Ѿ� �����ϱ� �����ڸ��� �ٷ� �����ִ½�
        }
    }

    public static EnemyBullet GetEnemyBullet()
    {
        if (Instance.poolingObjectEnemyBulletQueue.Count > 0) // ť�� �����ִ� �Ѿ��� ������
        {
            //�� �Լ��� ������ƮǮ�� �־�������� �ʿ信 ���� �ϳ��� �����ִ� �Լ��̴�
            var obj = Instance.poolingObjectEnemyBulletQueue.Dequeue(); //Queue �� �ִ� ������Ʈ�� ������ obj ��� ������ �־��ְ�
            obj.transform.SetParent(null); //�θ�� ����߸��� (�پ������� �������� �����Ŵϱ�)
            obj.gameObject.SetActive(true);//Ȱ��ȭ ������ �Ŀ�
            obj.transform.SetParent(GameObject.Find("Bullets").transform);
            return obj; //�̸� ��ȯ
        }
        else //���� ť�� ������ �Ѿ��� ���ٸ�
        {
            var newObj = Instance.CreateNewEnemyBullet(); //���Ӱ� �Ѿ��� ������ְ� 
            newObj.gameObject.SetActive(true); //������� �Ѿ��� �ٷ� Ȱ��ȭ�Ͽ�
            newObj.transform.SetParent(null); //�θ�� ����߸����·�
            newObj.transform.SetParent(GameObject.Find("Bullets").transform);
            return newObj; //��ȯ �״ϱ� ���Ը��ؼ� �������Ѿ� �����ϱ� �����ڸ��� �ٷ� �����ִ½�
        }
    }

    public static EnemyController GetEnemy()
    {
        if (Instance.poolingObjectEnemyQueue.Count > 0) // ť�� �����ִ� �Ѿ��� ������
        {
            var obj = Instance.poolingObjectEnemyQueue.Dequeue(); //Queue �� �ִ� ������Ʈ�� ������ obj ��� ������ �־��ְ�
            obj.transform.SetParent(null); //�θ�� ����߸��� (�پ������� �������� �����Ŵϱ�)
            obj.gameObject.SetActive(true);//Ȱ��ȭ ������ �Ŀ�
            obj.transform.SetParent(GameObject.Find("Enemies").transform);
            return obj; //�̸� ��ȯ
        }
        else //���� ť�� ������ �Ѿ��� ���ٸ�
        {
            var newObj = Instance.CreateNewEnemy(); //���Ӱ� �Ѿ��� ������ְ� 
            newObj.gameObject.SetActive(true); //������� �Ѿ��� �ٷ� Ȱ��ȭ�Ͽ�
            newObj.transform.SetParent(null); //�θ�� ����߸����·�
            newObj.transform.SetParent(GameObject.Find("Enemies").transform);
            return newObj; //��ȯ �״ϱ� ���Ը��ؼ� �������Ѿ� �����ϱ� �����ڸ��� �ٷ� �����ִ½�
        }
    }

    public static void ReturnPlayerBullet(Bullet obj)
    {
        //�� �Լ��� ������� �Ѿ��� �ٽ� �����޴°�
        obj.gameObject.SetActive(false); //�Ѿ� ��Ȱ��ȭ ���ְ�
        obj.transform.SetParent(Instance.transform); //�ٽ� ObjectPool ������Ʈ�� ��ġ�� �̵������ְ�
        Instance.poolingObjectPlayerBulletQueue.Enqueue(obj); //ť�� �־���
    }
    
    public static void ReturnEnemyBullet(EnemyBullet obj)
    {
        //�� �Լ��� ������� �Ѿ��� �ٽ� �����޴°�
        obj.gameObject.SetActive(false); //�Ѿ� ��Ȱ��ȭ ���ְ�
        obj.transform.SetParent(Instance.transform); //�ٽ� ObjectPool ������Ʈ�� ��ġ�� �̵������ְ�
        Instance.poolingObjectEnemyBulletQueue.Enqueue(obj); //ť�� �־���
    }
    
    public static void ReturnEnemy(EnemyController obj)
    {
        //�� �Լ��� ������� �Ѿ��� �ٽ� �����޴°�
        obj.gameObject.SetActive(false); //�Ѿ� ��Ȱ��ȭ ���ְ�
        obj.transform.SetParent(Instance.transform); //�ٽ� ObjectPool ������Ʈ�� ��ġ�� �̵������ְ�
        Instance.poolingObjectEnemyQueue.Enqueue(obj); //ť�� �־���
    }
}
