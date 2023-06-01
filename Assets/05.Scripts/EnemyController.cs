using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum Dir { Right, Left, Up, Down, RightUp, RightDown, LeftUp, LeftDown }
    Dir dir;

    [SerializeField] Rigidbody enemyRigidBody;

    [SerializeField] private float speed = 100f;

    //���� ������ �ñ⸦ ���� ����
    private float decideRateMin = 1f;
    private float decideRateMax = 3f;
    private float decideDirRate;
    private float timeAfterDecideDir;

    //���� ������ ��ġ�� ���� ����
    private Vector3 generatePos;

    private float xSpeed = 0, zSpeed = 0;

    //�÷��̾ �Ĵٺ����ϱ����� ������ ����
    private Transform target;

    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; // �ش� Ÿ���� ������ �÷��̾� �±װ� ���� ���ӿ�����Ʈ�� transform����
        pos = transform.position;
        generatePos.x = Random.Range(-1000, 1000); //��ǥ �����Ҷ� �����κ�
        generatePos.z = Random.Range(-1000, 1000);   //�ȿ��� �����ǰ� ���� ����
        generatePos.y = 0;
        transform.position = generatePos; //�����ϰ� ���� generate ���� ��ġ�� �ش� ��������Ʈ ��ġ ����
        timeAfterDecideDir = 0;
        decideDirRate = Random.Range(decideRateMin, decideRateMax);
        CheckDecide(); //������ ���ϴ� �Լ� ����
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
        if (target != null) transform.LookAt(target); // �� �����Ӹ��� �÷��̾��� ��ġ�� ã�� ������ �Ĵٺ���

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


        timeAfterDecideDir += Time.deltaTime;                          // ���� ������ ������ ��� �÷��ְ�

        if (timeAfterDecideDir > decideDirRate) // �����ϰ� ���� decideDirRate �ð��� ������
        {
            CheckDecide(); //�ٽ� ������ �����ش�.
            decideDirRate = Random.Range(decideRateMin, decideRateMax); //�׸��� decideDirRate �� ���Ӱ� �����ش�.
        }

        DecideDir(); //CheckDecide �Լ��� ���� ���� dir ������ �̵������� �����ش�.

        ////Vector3 �ӵ��� (xSpeed, 0, zSpeed) �� ����
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
