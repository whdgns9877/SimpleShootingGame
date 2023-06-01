using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //보스의 현재 상태와 패턴을 나타낼 enum
    public enum BossState { Normal, Berserker }
    public enum BossPattern { one, two, three, BerserkerPattern }

    public BossState bossState;
    public BossPattern bossPattern;

    //보스의 이동속도와 체력
    [SerializeField] float speed = 0;
    [SerializeField] int hp = 0;

    //패턴을 결정할 변수들
    private float decidePatternRate;
    private float timeAfterDecidePattern;

    private void Awake()
    {
        //시작시 상태는 노말, 패턴은 1, 각변수들 설정
        bossState = BossState.Normal;
        bossPattern = BossPattern.one;
        timeAfterDecidePattern = 0;
        decidePatternRate = 3;
    }

    void Update()
    {
        //게임 오버가 아닐때만 연산을 해주고
        if (GameManager.Instance.IsGameOver == false)
        {
            //기본적으로 플레이어를 쳐다보고 그쪽으로 이동하게 해놨다.
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            //시간지남에 따라
            timeAfterDecidePattern += Time.deltaTime;

            if (timeAfterDecidePattern > decidePatternRate && bossState == BossState.Normal)
            {
                DecideNewPattern(); //새로운 패턴을 결정짓는다
            }

            switch (bossPattern) //바뀐 패턴에따라 각자의 로직 함수를 실행한다.
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

            //반피 이상일때는 정상상태 피가 50퍼센트 이하로 내려가면 버서커 상태
            if (hp > 50 && hp < 100) bossState = BossState.Normal;
            else if (hp > 0 && hp < 50) bossState = BossState.Berserker;

            switch (bossState)// 각 상태에 따라 행동
            {
                case BossState.Normal:
                    speed = 100;
                    break;
                case BossState.Berserker:
                    bossPattern = BossPattern.BerserkerPattern;
                    speed = 200;
                    break;
            }

            if (hp <= 0) //피가 0이하가 되면
            {
                GameManager.Instance.EndGame(true); //게임 오버 조건을 승리로 해주고
                Destroy(gameObject); //보스 오브젝트 삭제
            }
        }
    }

    //랜덤하게 3가지 패턴을 실행한다
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

    // 각 패턴에 맞는 함수들 아직 로직은 못짜서 일단 콘솔창 확인만...
    private void PatternOne()
    {
        Debug.Log("보스 패턴 1 실행");
    }

    private void PatternTwo()
    {
        Debug.Log("보스 패턴 2 실행");
    }

    private void PatternThree()
    {
        Debug.Log("보스 패턴 3 실행");
    }

    private void PatternBerserker()
    {
        Debug.Log("보스 패턴 버서커 실행");
    }

    //플레이어 총알에 닿으면 hp를 1씩 깎는다
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet") hp--;
    }
}
