using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CounterColectable : MonoBehaviour
{
    public TextMeshProUGUI itemText;
    public GameObject gameOverPanel;
    private int _itemCount = 0;
    public int finalCounter;
    
    // Start is called before the first frame update
    void Start()
    {   
        gameOverPanel.SetActive(false);
        UpdateUI();
        Time.timeScale = 1;
    }

    void Update()
    {
        if (_itemCount == finalCounter)
        {
            OpenGameOverPanel();
            print("Juego Terminado");
        }
    }
    public void AddItem()
    {
        _itemCount++;
        UpdateUI();
    }
    private void UpdateUI()
    {
        itemText.text = _itemCount.ToString();
    }

    private void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
