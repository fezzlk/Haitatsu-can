using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button refreshButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        pausePanel.SetActive(false);

        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);
        refreshButton.onClick.AddListener(Refresh);
        quitButton.onClick.AddListener(Quit);
    }

    private void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    private void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    private void Refresh()
    {
        SceneManager.LoadScene("10Title");
        Timer.resetTime(); // タイマーをリセット
    }


    private void Quit()
    {
        SceneManager.LoadScene("30Result");
    }
}