using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBallController : MonoBehaviour
{
    Animator anim;
    bool Explode;

    public bool isPlayer;
    // Update is called once per frame

    private void Start()
    {
        //definir animator para o objeto
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        StartCoroutine(remove());
        //assim que for instanciado, irá para frente até ser interrompido
        if (!Explode)
        {
            transform.Translate(0, 5 * Time.deltaTime,0);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "wall")
        {
            Explode = true;
            anim.SetBool("Colision", true);
        }
        if (isPlayer)
        {
        if(collision.gameObject.tag == "Enemy")
        {
            Explode = true;
            anim.SetBool("Colision", true);
            collision.gameObject.GetComponent<EnemyController>().Dano();
        }
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                Explode = true;
                anim.SetBool("Colision", true);
                collision.gameObject.GetComponent<PlayerController>().Dano();
            }
        }
    }


    IEnumerator remove()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    public void removeBall()
    {
        Destroy(gameObject);
    }
}
