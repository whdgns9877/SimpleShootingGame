using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI관련 라이브러리
using UnityEngine.SceneManagement; //씬 관리 관련 라이브러리

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //게임 화면들을 오브젝트로 정리

    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject Normal_Level;
    [SerializeField] GameObject Boss_Level;
    [SerializeField] GameObject Boss_Level_Start_Text;
    [SerializeField] GameObject MainCamera;

    public GameObject gameOverText; //게임오버 시 활성화할 텍스트 게임 오브젝트
    public GameObject gamewinText; //게임승리 시 활성화할 텍스트 게임 오브젝트
    public Text scoreText; //스코어를 표시할 텍스트
    public Text recordText; //최고 기록을 표시할 텍스트 컴포넌트

    public int Score { get; set; }
    public bool IsGameOver { get; set; } //게임오버 상태

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        //각종 변수 초기화
        Score = 0;
        IsGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver == false)
        {
            scoreText.text = "Score : " + Score;
            //스코어가 1000을 넘어가고 보스 레벨이 활성화 되있지 않은상태이면
            if(Score >= 30 && Boss_Level.gameObject.activeSelf == false)
            {
                Normal_Level.gameObject.SetActive(false); //일반 레벨은 꺼주고
                Boss_Level_Start_Text.gameObject.SetActive(true); //보스 레벨 시작한다는 텍스트를 띄운후
                StartCoroutine(BossLevelStart());//코루틴을 이용하여
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //SampleScene 씬을 로드
                SceneManager.LoadScene("GameScene");
                IsGameOver = false;
            }
        }
    }

    IEnumerator BossLevelStart()
    {
        //3초를 기다리고
        yield return new WaitForSeconds(3f);
        Boss_Level_Start_Text.gameObject.SetActive(false); //텍스트는 내려주고
        Boss_Level.gameObject.SetActive(true); //보스 레벨을 띄워준다.
        MainCamera.gameObject.GetComponent<CameraMove>().FindPlayer(); //플레이어 위치를 다시 찾아 카메라에 할당
        GameObject.Find("Sounds").transform.Find("BackGroundMusic").gameObject.SetActive(false);
        GameObject.Find("Sounds").transform.Find("BossBackGroundMusic").gameObject.SetActive(true);

    }

    public void StartGame()
    {
        //버튼을 클릭하면 이벤트로 메인스크린을 닫아주고
        mainScreen.gameObject.SetActive(false);
        UI.gameObject.SetActive(true); //UI띄워주고
        Normal_Level.gameObject.SetActive(true);//일반 레벨 켜주고
        GameObject.Find("Main Camera").GetComponent<CameraMove>().enabled = true; //메인 카메라 스크립트 ON
    }

    //현재 게임을 게임오버 상태로 변경하는 메소드
    public void EndGame(bool win)
    {
        //현재 상태를 게임오버 상태로 전환
        IsGameOver = true;
        //게임오버 텍스트 게임 오브젝트를 활성화
        gameOverText.SetActive(true);

        if (win == true)
        {
            gamewinText.SetActive(true);
        }
        else gameOverText.SetActive(true);

        //BestScore 키로 저장된 이전까지의 최고 기록 가져오기
        int bestScore = PlayerPrefs.GetInt("bestScore");

        //이전까지의 최고기록보다 현재 생존 시간이 더 크다면
        if (Score > bestScore)
        {
            //최고 기록 값을 현재 생존 시간 값으로 변경
            bestScore = Score;
            //변경된 최고 기록을 BestTime 키로 저장
            PlayerPrefs.SetInt("bestScore", bestScore);
        }

        //최고 기록을 recordText 텍스트 컴포넌트를 이용해 표시
        recordText.text = "Best Score : " + bestScore;
    }
}
