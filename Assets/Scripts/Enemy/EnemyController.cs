using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public AudioSource SFX;
    public AudioClip hit;
    public AudioClip Death;


    public int Life = 3;
    public int mySkin;
    public Sprite[] Skin;
    public GameObject bulletPrefab; // Prefab da bala a ser disparada

    public GameObject BulletShootLocation;
    public bool isChaser;

    public Transform player;
    private Rigidbody2D rb;
    int shootTimes;
    Animator anim;
    public lifeController lc;
    bool canShoot;

    public SpriteRenderer enemySprite;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Pega o Componente NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        //Variaveis setadas como False para Não utilizar os eixos Y Baseado em 3 dimensões
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        player = GameObject.Find("PlayerTarget").transform;

    }

    // Update is called once per frame
    void Update()
    {
        //Sempre verificar qual skin está definida na variavel mySkin e usar o componente Sprite Renderer para definir essa skin no jogador.
        enemySprite.sprite = Skin[mySkin];


        if (Life != 0)
        {
            var playerLife = player.parent.gameObject.GetComponent<PlayerController>().PlayerLife;
            if (!isChaser && canShoot && shootTimes == 0 && playerLife != 0)
            {

                StartCoroutine(shoot());
                shootTimes += 1;
            }

            if (isChaser)
            {
                /*transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                Vector3 direction = player.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb.rotation = angle;
                direction.Normalize();
                movement = direction;*/

                agent.stoppingDistance = 0;
                agent.speed = 0.33f;
                agent.SetDestination(player.transform.position);
                canShoot = false;

                //transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                Vector3 direction = player.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb.rotation = angle;
                direction.Normalize();

            }
            else
            {
                agent.stoppingDistance = 2;

                if (Vector2.Distance(transform.position, player.position) < 5)
                {
                    Vector3 direction = player.position - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    rb.rotation = angle;
                    direction.Normalize();
                    agent.speed = 0.22f;

                    canShoot = true;


                }
                else if (Vector2.Distance(transform.position, player.position) > 5)
                {
                    agent.speed = 0.33f;
                    agent.SetDestination(player.transform.position);
                    canShoot = false;

                    //transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                    Vector3 direction = player.position - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    rb.rotation = angle;
                    direction.Normalize();
                }
            }
        }
        else
        {
            anim.SetBool("death", true);
        }
    }

     IEnumerator shoot()
    {
        lc.recharge.value = 0;
        Instantiate(bulletPrefab, BulletShootLocation.transform.position, BulletShootLocation.transform.rotation);
        yield return new WaitForSeconds(5f);
        shootTimes = 0;


    }


    public void Dano()
    {
        Sound("Hit");
        lc.Damage(1);
        Life -= 1;
        mySkin += 1;
        PlayerController.points += 10;
    }
    



    public void destroy()
    {
        Destroy(gameObject);
    }

    public void Sound(string s)
    {
        if (s == "Hit")
        {
            SFX.pitch = Random.Range(0.7f, 1.4f);
            SFX.PlayOneShot(hit);

        }
        else if (s == "Death")
        {
            SFX.pitch = Random.Range(0.7f, 1.4f);
            SFX.PlayOneShot(Death);
        }
    }
}
