using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public Text highScoreText;

    private void Start()
    {
       
        highScoreText.text = $"Best score: {PersistanceManager.persistanceManager.bestScore} by {PersistanceManager.persistanceManager.bestPlayer}";


    }

    public void startClicked()
    {
        

        if (PersistanceManager.persistanceManager.nameIsEntered)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            PersistanceManager.persistanceManager.playerName = "<unknown player>";
            SceneManager.LoadScene(1);
        }
    }

}
