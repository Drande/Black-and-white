using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(gameObject);
        }
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }
    
    public void BackToMain() {
        SceneManager.LoadScene(0);
    }
    
    public void RestartGame() {
        SceneManager.LoadScene(1);
    }
}
