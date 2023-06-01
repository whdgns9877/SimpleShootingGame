using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum Dir { Right, Left, Up, Down, RightUp, RightDown, LeftUp, LeftDown }
    Dir dir;

    [SerializeField] Rigidbody enemyRigidBody;

    [SerializeField] private float speed = 100f;

    //적을 생성할 시기를 비교할 변수
    private float decideRateMin = 1f;
    private float decideRateMax = 3f;
    private float decideDirRate;
    private float timeAfterDecideDir;

    //적을 생성할 위치를 담을 변수
    private Vector3 generatePos;

    private float xSpeed = 0, zSpeed = 0;

    //플레이어를 쳐다보게하기위해 선언한 변수
    private Transform target;

    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; // 해당 타겟의 방향은 플레이어 태그가 붙은 게임오브젝트의 transform으로
        pos = transform.position;
        generatePos.x = Random.Range(-1000, 1000); //좌표 설정할때 일정부분
        generatePos.z = Random.Range(-1000, 1000);   //안에서 생성되게 범위 지정
        generatePos.y = 0;
        transform.position = generatePos; //랜덤하게 나온 generate 변수 위치에 해당 적오브젝트 위치 설정
        timeAfterDecideDir = 0;
        decideDirRate = Random.Range(decideRateMin, decideRateMax);
        CheckDecide(); //방향을 정하는 함수 실행
        StartCoroutine(EnemyFire());
    }

    IEnumerator EnemyFire()
    {
        while (gameObject.activeSelf == true)
        {
            yield return new WaitForSeconds(3f);
            var bullet = ObjectPool.GetEnemyBullet();
            bullet.transform.position = transform.position;
            bullet.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameOver == true) StopAllCoroutines();
        if (target != null) transform.LookAt(target); // 매 프레임마다 플레이어의 위치를 찾아 그쪽을 쳐다본다

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


        timeAfterDecideDir += Time.deltaTime;                          // 방향 정해줄 변수를 계속 올려주고

        if (timeAfterDecideDir > decideDirRate) // 랜덤하게 나온 decideDirRate 시간이 지나면
        {
            CheckDecide(); //다시 방향을 정해준다.
            decideDirRate = Random.Range(decideRateMin, decideRateMax); //그리고 decideDirRate 를 새롭게 구해준다.
        }

        DecideDir(); //CheckDecide 함수를 통해 나온 dir 값으로 이동방향을 정해준다.

        ////Vector3 속도를 (xSpeed, 0, zSpeed) 로 생성
        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);
        enemyRigidBody.velocity = newVelocity;
    }

    private void DecideDir()
    {
        switch (dir)
        {
            case Dir.Right:
                break;
            case Dir.Left:
                break;
            case Dir.Up:
                break;
            case Dir.Down:
                break;
            case Dir.RightUp:
                break;
            case Dir.RightDown:
                break;
            case Dir.LeftUp:
                break;
            case Dir.LeftDown:
                break;
        }
    }

    private void CheckDecide()
    {
        timeAfterDecideDir = 0;

        switch (Random.Range(0, 8))
        {
            case 0:
                dir = Dir.Right;
                xSpeed = speed;
                zSpeed = 0;
                break;
            case 1:
                dir = Dir.Left;
                xSpeed = -speed;
                zSpeed = 0;
                break;
            case 2:
                dir = Dir.Up;
                xSpeed = 0;
                zSpeed = speed;
                break;
            case 3:
                dir = Dir.Down;
                xSpeed = 0;
                zSpeed = -speed;
                break;
            case 4:
                dir = Dir.RightUp;
                xSpeed = speed * 0.5f;
                zSpeed = speed * 0.5f;
                break;
            case 5:
                dir = Dir.RightDown;
                xSpeed = speed * 0.5f;
                zSpeed = speed * -0.5f;
                break;
            case 6:
                dir = Dir.LeftUp;
                xSpeed = speed * -0.5f;
                zSpeed = speed * 0.5f;
                break;
            case 7:
                dir = Dir.LeftDown;
                xSpeed = speed * -0.5f;
                zSpeed = speed * -0.5f;
                break;
        }
    }

    public void Die()
    {
        GameObject.Find("Sounds").transform.Find("EnemyDieSound").gameObject.GetComponent<AudioSource>().Play();
        ObjectPool.ReturnEnemy(this);
    }
}
