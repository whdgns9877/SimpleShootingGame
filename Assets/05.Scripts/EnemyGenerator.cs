using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Generate", 0, 1); //Generate ��� �̸��� �Լ��� ��� 1�ʸ��� �����Ͽ��ش�.
    }

    void Generate()
    {
        if (GameManager.Instance.IsGameOver == false)
        {
            ObjectPool.GetEnemy(); //ObjectPool Ŭ������ GetEnemy �Լ��� �������Ӱ� �����������
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
