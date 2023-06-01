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
        //poolingObjectPrefab을 생성하고 Bullet 컴포넌트를 가져와 이를 newObj 변수에 넣어준다.
        var newObj = Instantiate(poolingObjectPlayerBullet).GetComponent<Bullet>();
        newObj.gameObject.SetActive(false);   //이를 비활성화 해주고
        newObj.transform.SetParent(transform);//위치값을 부모의 위치 즉ObjectPool 오브젝트의 위치로 설정
        return newObj; //해당객체 반환
    }  

    private EnemyBullet CreateNewEnemyBullet()
    {
        //poolingObjectPrefab을 생성하고 Bullet 컴포넌트를 가져와 이를 newObj 변수에 넣어준다.
        var newObj = Instantiate(poolingObjectEnemyBullet).GetComponent<EnemyBullet>();
        newObj.gameObject.SetActive(false);   //이를 비활성화 해주고
        newObj.transform.SetParent(transform);//위치값을 부모의 위치 즉ObjectPool 오브젝트의 위치로 설정
        return newObj; //해당객체 반환
    }
    
    private EnemyController CreateNewEnemy()
    {
        //poolingObjectPrefab을 생성하고 Bullet 컴포넌트를 가져와 이를 newObj 변수에 넣어준다.
        var newObj = Instantiate(poolingObjectEnemy).GetComponent<EnemyController>();
        newObj.gameObject.SetActive(false);   //이를 비활성화 해주고
        newObj.transform.SetParent(transform);//위치값을 부모의 위치 즉ObjectPool 오브젝트의 위치로 설정
        return newObj; //해당객체 반환
    }



    public static Bullet GetPlayerBullet()
    {
        if (Instance.poolingObjectPlayerBulletQueue.Count > 0) // 큐에 남아있는 총알이 있으면
        {
            //이 함수는 오브젝트풀에 넣어놓은것을 필요에 의해 하나씩 꺼내주는 함수이다
            var obj = Instance.poolingObjectPlayerBulletQueue.Dequeue(); //Queue 에 있는 오브젝트를 꺼내서 obj 라는 변수에 넣어주고
            obj.transform.SetParent(null); //부모와 떨어뜨리고 (붙어있으면 움직이지 않을거니까)
            obj.gameObject.SetActive(true);//활성화 시켜준 후에
            obj.GetComponent<Bullet>().InitBullet();
            obj.transform.SetParent(GameObject.Find("Bullets").transform);
            return obj; //이를 반환
        }
        else //만약 큐에 꺼내줄 총알이 없다면
        {
            var newObj = Instance.CreateNewPlayerBullet(); //새롭게 총알을 만들어주고 
            newObj.gameObject.SetActive(true); //만들어준 총알을 바로 활성화하여
            newObj.transform.SetParent(null); //부모와 떨어뜨린상태로
            newObj.GetComponent<Bullet>().InitBullet();
            newObj.transform.SetParent(GameObject.Find("Bullets").transform);
            return newObj; //반환 그니까 쉽게말해서 꺼내줄총알 없으니까 만들자마자 바로 꺼내주는식
        }
    }

    public static EnemyBullet GetEnemyBullet()
    {
        if (Instance.poolingObjectEnemyBulletQueue.Count > 0) // 큐에 남아있는 총알이 있으면
        {
            //이 함수는 오브젝트풀에 넣어놓은것을 필요에 의해 하나씩 꺼내주는 함수이다
            var obj = Instance.poolingObjectEnemyBulletQueue.Dequeue(); //Queue 에 있는 오브젝트를 꺼내서 obj 라는 변수에 넣어주고
            obj.transform.SetParent(null); //부모와 떨어뜨리고 (붙어있으면 움직이지 않을거니까)
            obj.gameObject.SetActive(true);//활성화 시켜준 후에
            obj.transform.SetParent(GameObject.Find("Bullets").transform);
            return obj; //이를 반환
        }
        else //만약 큐에 꺼내줄 총알이 없다면
        {
            var newObj = Instance.CreateNewEnemyBullet(); //새롭게 총알을 만들어주고 
            newObj.gameObject.SetActive(true); //만들어준 총알을 바로 활성화하여
            newObj.transform.SetParent(null); //부모와 떨어뜨린상태로
            newObj.transform.SetParent(GameObject.Find("Bullets").transform);
            return newObj; //반환 그니까 쉽게말해서 꺼내줄총알 없으니까 만들자마자 바로 꺼내주는식
        }
    }

    public static EnemyController GetEnemy()
    {
        if (Instance.poolingObjectEnemyQueue.Count > 0) // 큐에 남아있는 총알이 있으면
        {
            var obj = Instance.poolingObjectEnemyQueue.Dequeue(); //Queue 에 있는 오브젝트를 꺼내서 obj 라는 변수에 넣어주고
            obj.transform.SetParent(null); //부모와 떨어뜨리고 (붙어있으면 움직이지 않을거니까)
            obj.gameObject.SetActive(true);//활성화 시켜준 후에
            obj.transform.SetParent(GameObject.Find("Enemies").transform);
            return obj; //이를 반환
        }
        else //만약 큐에 꺼내줄 총알이 없다면
        {
            var newObj = Instance.CreateNewEnemy(); //새롭게 총알을 만들어주고 
            newObj.gameObject.SetActive(true); //만들어준 총알을 바로 활성화하여
            newObj.transform.SetParent(null); //부모와 떨어뜨린상태로
            newObj.transform.SetParent(GameObject.Find("Enemies").transform);
            return newObj; //반환 그니까 쉽게말해서 꺼내줄총알 없으니까 만들자마자 바로 꺼내주는식
        }
    }

    public static void ReturnPlayerBullet(Bullet obj)
    {
        //이 함수는 꺼내줬던 총알을 다시 돌려받는것
        obj.gameObject.SetActive(false); //총알 비활성화 해주고
        obj.transform.SetParent(Instance.transform); //다시 ObjectPool 오브젝트의 위치로 이동시켜주고
        Instance.poolingObjectPlayerBulletQueue.Enqueue(obj); //큐에 넣어줌
    }
    
    public static void ReturnEnemyBullet(EnemyBullet obj)
    {
        //이 함수는 꺼내줬던 총알을 다시 돌려받는것
        obj.gameObject.SetActive(false); //총알 비활성화 해주고
        obj.transform.SetParent(Instance.transform); //다시 ObjectPool 오브젝트의 위치로 이동시켜주고
        Instance.poolingObjectEnemyBulletQueue.Enqueue(obj); //큐에 넣어줌
    }
    
    public static void ReturnEnemy(EnemyController obj)
    {
        //이 함수는 꺼내줬던 총알을 다시 돌려받는것
        obj.gameObject.SetActive(false); //총알 비활성화 해주고
        obj.transform.SetParent(Instance.transform); //다시 ObjectPool 오브젝트의 위치로 이동시켜주고
        Instance.poolingObjectEnemyQueue.Enqueue(obj); //큐에 넣어줌
    }
}
