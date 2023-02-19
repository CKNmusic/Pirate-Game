using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioSource SFX;
    public AudioClip hit;
    public AudioClip Death;

    public static int points;

    public int PlayerLife = 5;
    public int playerSkin;
    public Sprite[] Skin;

    public float BoatSpeed = 0.3f;

    public bool DelayTarget = false;
    public float DelayTargetTime = 0.4f;

    public GameObject BulletShootLocation;
    public GameObject BulletShootLocation2;
    public GameObject BulletShootLocation3;


    public Timer timer;
    public GameObject Bullet;

    Vector3 mousePosition;

    public lifeController lc;

    bool canShoot = true;

    Animator anim;

    public Animator camAnimator;

    public GameObject lostScreen;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Sempre verificar qual skin está definida na variavel playerSkin e usar o componente Sprite Renderer para definir essa skin no jogador.
        GetComponent<SpriteRenderer>().sprite = Skin[playerSkin];

        if (PlayerLife != 0)
        {
            //rotacionar barco em direção ao cursor do mouse
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - transform.position;
            transform.up = direction;




            //pegar o valor das setas para mover o barco
            var acelerar = Input.GetAxis("Vertical");
            //Aqui que a magica acontece
            transform.Translate(0, acelerar / BoatSpeed * Time.deltaTime, 0);

            //Atirar com o botão direito
            if (Input.GetMouseButtonDown(0) && canShoot)
            {
                Shoot();
            }else if (Input.GetMouseButtonDown(1) && canShoot)
            {
                Shoot3();
            }
        }
        else
        {
            anim.SetBool("death", true);
            camAnimator.SetBool("death", true);
        }
    }


    void Shoot()
    {

        //Instantiate(Bullet, BulletShootLocation.transform.position, );


        StartCoroutine(shootTimer());
        canShoot = false;


        //bullet.rigidbody.addforce(gun.forward * bulletspeed);
    }    void Shoot3()
    {

        //Instantiate(Bullet, BulletShootLocation.transform.position, );


        StartCoroutine(shootTimer3());
        canShoot = false;


        //bullet.rigidbody.addforce(gun.forward * bulletspeed);
    }

    IEnumerator shootTimer()
    {
        lc.recharge.value = 0;
        Instantiate(Bullet, BulletShootLocation.transform.position, BulletShootLocation.transform.rotation);
        yield return new WaitForSeconds(3f);
        canShoot = true;

    }    
    
    IEnumerator shootTimer3()
    {
        lc.recharge.value = 0;
        Instantiate(Bullet, BulletShootLocation.transform.position, BulletShootLocation.transform.rotation);
        Instantiate(Bullet, BulletShootLocation2.transform.position, BulletShootLocation2.transform.rotation);
        Instantiate(Bullet, BulletShootLocation3.transform.position, BulletShootLocation3.transform.rotation);
        yield return new WaitForSeconds(3f);
        canShoot = true;

    }

    public void Dano()
    {
        Sound("Hit");
        lc.Damage(1);
        PlayerLife -= 1;
        playerSkin += 1;
    }

    public void destroy()
    {
        OnDestroyBoat();
        //Destroy(gameObject);
    }

    private void OnDestroyBoat()
    {
        timer.enabled = false;
        Time.timeScale = 0;

        lostScreen.SetActive(true);
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


    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(collision);
        if (collision.gameObject.tag == "Enemy" )
        {
            PlayerLife = 0;
        }

        if (collision.gameObject.name == "Island")
        {
            PlayerLife = 0;
        }
    }
}
