using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Generate", 0, 1); //Generate 라는 이름의 함수를 즉시 1초마다 실행하여준다.
    }

    void Generate()
    {
        if (GameManager.Instance.IsGameOver == false)
        {
            ObjectPool.GetEnemy(); //ObjectPool 클래스의 GetEnemy 함수는 닷지게임과 같은방식으로
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
