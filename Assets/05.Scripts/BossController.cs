using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //������ ���� ���¿� ������ ��Ÿ�� enum
    public enum BossState { Normal, Berserker }
    public enum BossPattern { one, two, three, BerserkerPattern }

    public BossState bossState;
    public BossPattern bossPattern;

    //������ �̵��ӵ��� ü��
    [SerializeField] float speed = 0;
    [SerializeField] int hp = 0;

    //������ ������ ������
    private float decidePatternRate;
    private float timeAfterDecidePattern;

    private void Awake()
    {
        //���۽� ���´� �븻, ������ 1, �������� ����
        bossState = BossState.Normal;
        bossPattern = BossPattern.one;
        timeAfterDecidePattern = 0;
        decidePatternRate = 3;
    }

    void Update()
    {
        //���� ������ �ƴҶ��� ������ ���ְ�
        if (GameManager.Instance.IsGameOver == false)
        {
            //�⺻������ �÷��̾ �Ĵٺ��� �������� �̵��ϰ� �س���.
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            //�ð������� ����
            timeAfterDecidePattern += Time.deltaTime;

            if (timeAfterDecidePattern > decidePatternRate && bossState == BossState.Normal)
            {
                DecideNewPattern(); //���ο� ������ �������´�
            }

            switch (bossPattern) //�ٲ� ���Ͽ����� ������ ���� �Լ��� �����Ѵ�.
            {
                case BossPattern.one:
                    PatternOne();
                    break;

                case BossPattern.two:
                    PatternTwo();
                    break;

                case BossPattern.three:
                    PatternThree();
                    break;

                case BossPattern.BerserkerPattern:
                    PatternBerserker();
                    break;
            }

            //���� �̻��϶��� ������� �ǰ� 50�ۼ�Ʈ ���Ϸ� �������� ����Ŀ ����
            if (hp > 50 && hp < 100) bossState = BossState.Normal;
            else if (hp > 0 && hp < 50) bossState = BossState.Berserker;

            switch (bossState)// �� ���¿� ���� �ൿ
            {
                case BossState.Normal:
                    speed = 100;
                    break;
                case BossState.Berserker:
                    bossPattern = BossPattern.BerserkerPattern;
                    speed = 200;
                    break;
            }

            if (hp <= 0) //�ǰ� 0���ϰ� �Ǹ�
            {
                GameManager.Instance.EndGame(true); //���� ���� ������ �¸��� ���ְ�
                Destroy(gameObject); //���� ������Ʈ ����
            }
        }
    }

    //�����ϰ� 3���� ������ �����Ѵ�
    private void DecideNewPattern()
    {
        timeAfterDecidePattern = 0;

        switch (Random.Range(0, 3))
        {
            case 0:
                bossPattern = BossPattern.one;
                break;

            case 1:
                bossPattern = BossPattern.two;
                break;

            case 2:
                bossPattern = BossPattern.three;
                break;
        }
    }

    // �� ���Ͽ� �´� �Լ��� ���� ������ ��¥�� �ϴ� �ܼ�â Ȯ�θ�...
    private void PatternOne()
    {
        Debug.Log("���� ���� 1 ����");
    }

    private void PatternTwo()
    {
        Debug.Log("���� ���� 2 ����");
    }

    private void PatternThree()
    {
        Debug.Log("���� ���� 3 ����");
    }

    private void PatternBerserker()
    {
        Debug.Log("���� ���� ����Ŀ ����");
    }

    //�÷��̾� �Ѿ˿� ������ hp�� 1�� ��´�
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet") hp--;
    }
}
