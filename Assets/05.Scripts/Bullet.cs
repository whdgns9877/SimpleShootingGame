using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 3; //ź�� �̵� �ӷ�
    //private Rigidbody bulletRigidbody; // �̵��� ����� ������ٵ� ������Ʈ
    private Transform barrelPos;
    private Vector3 bulletDir;

    // Start is called before the first frame update
    void Start()
    {
        InitBullet();
    }
    public void InitBullet()
    {
        //Barrel �̶�� �̸��� ���ӿ�����Ʈ�� transform �� barrelPos�� �Ҵ�
        barrelPos = GameObject.Find("Barrel").transform;

        //���� ������Ʈ���� Rigidbody ������Ʈ�� ã�� bulletRigidbody�� �Ҵ�
        //bulletRigidbody = GetComponent<Rigidbody>();

        //Barrel �̶�� ���ӿ�����Ʈ�� RotateBarrel(���� ������ �Ĵٺ����ϴ� ��ũ��Ʈ) �� ã�� mousePos���� ������
        bulletDir = GameObject.Find("Barrel").GetComponent<RotateBarrel>().mousePos; // bulletDir �� �Ҵ�
        transform.position = barrelPos.position; // ���� ���⿡�� ���� ����
        transform.LookAt(bulletDir); //bulletDir ������ �Ĵٺ���
    }


    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed); //�̹� ������ ���س������� ������ �� ����

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
