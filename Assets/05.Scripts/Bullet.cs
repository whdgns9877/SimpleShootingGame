using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 3; //탄알 이동 속력
    //private Rigidbody bulletRigidbody; // 이동에 사용할 리지드바디 컴포넌트
    private Transform barrelPos;
    private Vector3 bulletDir;

    // Start is called before the first frame update
    void Start()
    {
        InitBullet();
    }
    public void InitBullet()
    {
        //Barrel 이라는 이름의 게임오브젝트의 transform 을 barrelPos에 할당
        barrelPos = GameObject.Find("Barrel").transform;

        //게임 오브젝트에서 Rigidbody 컴포넌트를 찾아 bulletRigidbody에 할당
        //bulletRigidbody = GetComponent<Rigidbody>();

        //Barrel 이라는 게임오브젝트의 RotateBarrel(포신 돌리고 쳐다보게하는 스크립트) 를 찾아 mousePos값을 가져와
        bulletDir = GameObject.Find("Barrel").GetComponent<RotateBarrel>().mousePos; // bulletDir 에 할당
        transform.position = barrelPos.position; // 포신 방향에서 최초 생성
        transform.LookAt(bulletDir); //bulletDir 방향을 쳐다봐라
    }


    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed); //이미 방향은 정해놓았으니 앞으로 쭉 가라

        Vector3 pos = transform.position;
        if (transform.position.x < -1000 
            || transform.position.x > 1000
            || transform.position.z < -1000
            || transform.position.z > 1000) ObjectPool.ReturnPlayerBullet(this);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyController enemyController = other.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                enemyController.Die();
                GameManager.Instance.Score += 10;
                ObjectPool.ReturnPlayerBullet(this);
            }
        }

        if(other.tag == "Boss")
        {
            GameObject.Find("Sounds").transform.Find("BosHitSound").gameObject.GetComponent<AudioSource>().Play();
            GameManager.Instance.Score += 100;
            ObjectPool.ReturnPlayerBullet(this);
        }
    }
}
