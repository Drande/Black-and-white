using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI coinText;

    void Awake()
    {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void EnableGameOverScreen(bool active) {
        gameOverPanel.SetActive(active);
    }

    public void SetCoins(int value) {
        coinText.text = "Coins: " + value;
    }
}
