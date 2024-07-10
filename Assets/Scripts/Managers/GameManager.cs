using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   public int totalCoins = 0;
    public int coinsToWin = 4;
    public static GameManager Instance;

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
        totalCoins = 0;
        SceneManager.LoadScene(1);
    }

    public void Victory() {
        AudioManager.Instance.Stop();
        AudioManager.Instance.PlaySFX("Victory");
        CanvasManager.Instance.EnableVictoryScreen(true);
    }

    public void GameOver() {
        AudioManager.Instance.Stop();
        AudioManager.Instance.PlaySFX("Death");
        CanvasManager.Instance.EnableGameOverScreen(true);
    }

    public void AddCoin(int value)
    {
        totalCoins += value;
        CanvasManager.Instance.SetCoins(totalCoins);
    }
}
