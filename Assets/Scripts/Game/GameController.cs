using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //dura��o do jogo em segundos. IMPORTANTE: no codigo do counter esse numero � convertido em minutos
    public Timer gameDuration;

    //op��o a ser escolhida no menu de configura��o, sendo 1 minuto, 2 minutos e 3 minutos
    int gameDurationProps = 2;

    //descri��o que aparecer� escrita no menu de configura��o
    string gameDurationDesc;

    //Componente de texto da descri��o do tempo de jogo
    public Text gameDurationDescText;    
    


    //op��o a ser escolhida no menu de configura��o, sendo 2, 5 e 8 segundos;
    int spawnDurationProps = 2;

    //descri��o que aparecer� escrita no menu de configura��o
    string spawnDurationDesc;

    //Componente de texto da descri��o do tempo de jogo
    public Text spawnDurationDescText;

    void Start()
    {
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        //sempre atualizar o componente do texto com a informa��o
        gameDurationDescText.text = gameDurationDesc;


        //n�o permitir que o valor seja maior do que 2 nem menor do que 0
        if (gameDurationProps > 2)
        {
            gameDurationProps = 0;
        } else if (gameDurationProps < 0)
        {
            gameDurationProps = 2;
        }

        //definir as descri��es e a dira��o correta do jogo com base na op��o 
        switch (gameDurationProps)
        {
            case 2:
                gameDurationDesc = "3 minutos";
                gameDuration.totalTime = 180;
                break;
            case 1:
                gameDurationDesc = "2 minutos";
                gameDuration.totalTime = 120;
                break;
            default:
                gameDurationDesc = "1 minuto";
                gameDuration.totalTime = 60;
                break;
        }



        //sempre atualizar o componente do texto com a informa��o
        spawnDurationDescText.text = spawnDurationDesc;


        //n�o permitir que o valor seja maior do que 2 nem menor do que 0
        if (spawnDurationProps > 2)
        {
            spawnDurationProps = 0;
        }
        else if (spawnDurationProps < 0)
        {
            spawnDurationProps = 2;
        }

        //definir as descri��es e a dira��o correta do jogo com base na op��o 
        switch (spawnDurationProps)
        {
            case 2:
                spawnDurationDesc = "8 segundos";
                gameDuration.spawnTimer = 8;
                break;
            case 1:
                spawnDurationDesc = "5 segundos";
                gameDuration.spawnTimer = 5;
                break;
            default:
                spawnDurationDesc = "2 segundos";
                gameDuration.spawnTimer = 2;
                break;
        }
    }



    //Codigos linkados aos bot�es para adicionar ou remover o valor
    public void Add()
    {
        gameDurationProps += 1;
    }

    public void Remove()
    {
        gameDurationProps -= 1;
    }


        public void AddSpawn()
    {
        spawnDurationProps += 1;
    }

    public void RemoveSpawn()
    {
        spawnDurationProps -= 1;
    }


    public void reloadScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } 
}
