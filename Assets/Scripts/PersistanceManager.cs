using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PersistanceManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static PersistanceManager persistanceManager;
    public GameObject gameManager;
    public Text nameInputText;
    public int bestScore;
    public string playerName;
    public string bestPlayer;
    public bool nameIsEntered;
   

    void Awake()
    {
        LoadScore();

        if (persistanceManager != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            persistanceManager = this;
            DontDestroyOnLoad(gameObject);

            
        }
       
    }
    private void Start()
    {
        

        if (GameObject.Find("InputFieldText") != null)
        {
            nameInputText = GameObject.Find("InputFieldText").GetComponent<Text>();
        }

    }

    public void SetPlayerName()
    {
        if (!string.IsNullOrEmpty(nameInputText.text.ToString()))
        {
        playerName = nameInputText.text.ToString();
        nameIsEntered = true;
        }
        else
        {
            playerName = "<unknown player>";
        }

        Debug.Log(playerName);
        
    }
    
    
    
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "main") 
       
        {
            
         gameManager = GameObject.Find("MainManager");

            if(gameManager.GetComponent<MainManager>().bestPoints > bestScore)
            {
                bestScore = gameManager.GetComponent<MainManager>().bestPoints;
            }
        
            if (gameManager.GetComponent<MainManager>().m_GameOver && gameManager.GetComponent<MainManager>().notYetSaved)
            {
                SaveScore();
                gameManager.GetComponent<MainManager>().notYetSaved = false;
            }
        }

       

    }

    [System.Serializable]
    public class SaveData
    {
        public int savedScore;
        public string savedPlayername;
    }


    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/BrickBreakerSaveFile";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.savedScore;
            bestPlayer = data.savedPlayername;
    Debug.Log("Loaded from: " + Application.persistentDataPath);
            Debug.Log(bestScore);
        }

    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.savedScore = bestScore;
        data.savedPlayername = bestPlayer;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/BrickBreakerSaveFile", json);
        Debug.Log("Saved to: " + Application.persistentDataPath);

        
    }

}
