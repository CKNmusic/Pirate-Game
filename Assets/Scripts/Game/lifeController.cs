using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifeController : MonoBehaviour
{

    public Slider Life;
    public Slider recharge;
    
    // Start is called before the first frame update
    void Start()
    {
        recharge.value = 5;
    }

    // Update is called once per frame
    void Update()
    {
       // transform.rotation = Quaternion.Euler(0, 0, 0);

        if(recharge.value == 999)
        {
            recharge.gameObject.SetActive(false);
        }
        recharge.value += 1 * Time.deltaTime;
    }

    public void rechargeSlider()
    {
        recharge.value += 0;
        recharge.gameObject.SetActive(true);

    }    
    
    public void Damage(int dmg)
    {
        Life.value -= dmg;
        recharge.gameObject.SetActive(true);

    }
}
