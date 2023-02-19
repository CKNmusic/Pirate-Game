using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float totalTime = 180;
    public float spawnTimer; 
    public Text timer;
    public Text points;
    public GameObject winScreen;

    public Transform[] spawns;

    public GameObject[] enemies;


    private void Start()
    {
       var enemy = Random.Range(0, enemies.Length);
        var location = Random.Range(0, spawns.Length);
        Instantiate(enemies[enemy], spawns[location].transform.position, spawns[location].transform.rotation);
        StartCoroutine(Spawn());
        PlayerController.points += Mathf.RoundToInt(totalTime);

    }
    private void Update()
    {
        //diminuir valor por segundo
        totalTime -= Time.deltaTime;
        UpdateLevelTimer(totalTime);
        points.text = PlayerController.points.ToString();


    }

    public void UpdateLevelTimer(float totalSeconds)
    {
        //separar minutos e segundos
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.RoundToInt(totalSeconds % 60f);

        string formatedSeconds = seconds.ToString();

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }

        //Printar o valor no texto da GUI
        timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");

        if(totalTime <= 0 )
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);

        }
    }

    IEnumerator Spawn()
    {
        var enemy = Random.Range(0, enemies.Length);
        var location = Random.Range(0, spawns.Length);
        yield return new WaitForSeconds(spawnTimer);
        Instantiate(enemies[enemy], spawns[location].transform.position, spawns[location].transform.rotation);

        StartCoroutine(Spawn());
    }
}
