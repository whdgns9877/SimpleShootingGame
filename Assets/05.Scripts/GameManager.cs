using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI���� ���̺귯��
using UnityEngine.SceneManagement; //�� ���� ���� ���̺귯��

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //���� ȭ����� ������Ʈ�� ����

    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject Normal_Level;
    [SerializeField] GameObject Boss_Level;
    [SerializeField] GameObject Boss_Level_Start_Text;
    [SerializeField] GameObject MainCamera;

    public GameObject gameOverText; //���ӿ��� �� Ȱ��ȭ�� �ؽ�Ʈ ���� ������Ʈ
    public GameObject gamewinText; //���ӽ¸� �� Ȱ��ȭ�� �ؽ�Ʈ ���� ������Ʈ
    public Text scoreText; //���ھ ǥ���� �ؽ�Ʈ
    public Text recordText; //�ְ� ����� ǥ���� �ؽ�Ʈ ������Ʈ

    public int Score { get; set; }
    public bool IsGameOver { get; set; } //���ӿ��� ����

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        //���� ���� �ʱ�ȭ
        Score = 0;
        IsGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver == false)
        {
            scoreText.text = "Score : " + Score;
            //���ھ 1000�� �Ѿ�� ���� ������ Ȱ��ȭ ������ ���������̸�
            if(Score >= 30 && Boss_Level.gameObject.activeSelf == false)
            {
                Normal_Level.gameObject.SetActive(false); //�Ϲ� ������ ���ְ�
                Boss_Level_Start_Text.gameObject.SetActive(true); //���� ���� �����Ѵٴ� �ؽ�Ʈ�� �����
                StartCoroutine(BossLevelStart());//�ڷ�ƾ�� �̿��Ͽ�
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //SampleScene ���� �ε�
                SceneManager.LoadScene("GameScene");
                IsGameOver = false;
            }
        }
    }

    IEnumerator BossLevelStart()
    {
        //3�ʸ� ��ٸ���
        yield return new WaitForSeconds(3f);
        Boss_Level_Start_Text.gameObject.SetActive(false); //�ؽ�Ʈ�� �����ְ�
        Boss_Level.gameObject.SetActive(true); //���� ������ ����ش�.
        MainCamera.gameObject.GetComponent<CameraMove>().FindPlayer(); //�÷��̾� ��ġ�� �ٽ� ã�� ī�޶� �Ҵ�
        GameObject.Find("Sounds").transform.Find("BackGroundMusic").gameObject.SetActive(false);
        GameObject.Find("Sounds").transform.Find("BossBackGroundMusic").gameObject.SetActive(true);

    }

    public void StartGame()
    {
        //��ư�� Ŭ���ϸ� �̺�Ʈ�� ���ν�ũ���� �ݾ��ְ�
        mainScreen.gameObject.SetActive(false);
        UI.gameObject.SetActive(true); //UI����ְ�
        Normal_Level.gameObject.SetActive(true);//�Ϲ� ���� ���ְ�
        GameObject.Find("Main Camera").GetComponent<CameraMove>().enabled = true; //���� ī�޶� ��ũ��Ʈ ON
    }

    //���� ������ ���ӿ��� ���·� �����ϴ� �޼ҵ�
    public void EndGame(bool win)
    {
        //���� ���¸� ���ӿ��� ���·� ��ȯ
        IsGameOver = true;
        //���ӿ��� �ؽ�Ʈ ���� ������Ʈ�� Ȱ��ȭ
        gameOverText.SetActive(true);

        if (win == true)
        {
            gamewinText.SetActive(true);
        }
        else gameOverText.SetActive(true);

        //BestScore Ű�� ����� ���������� �ְ� ��� ��������
        int bestScore = PlayerPrefs.GetInt("bestScore");

        //���������� �ְ��Ϻ��� ���� ���� �ð��� �� ũ�ٸ�
        if (Score > bestScore)
        {
            //�ְ� ��� ���� ���� ���� �ð� ������ ����
            bestScore = Score;
            //����� �ְ� ����� BestTime Ű�� ����
            PlayerPrefs.SetInt("bestScore", bestScore);
        }

        //�ְ� ����� recordText �ؽ�Ʈ ������Ʈ�� �̿��� ǥ��
        recordText.text = "Best Score : " + bestScore;
    }
}
