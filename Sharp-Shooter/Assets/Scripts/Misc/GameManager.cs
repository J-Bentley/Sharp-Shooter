using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool isPaused;

    [SerializeField] GameObject levelCompleteText;
    [SerializeField] GameObject pauseContainer;
    [SerializeField] GameObject menuContainer;
    [SerializeField] GameObject optionsContainer;
    [SerializeField] GameObject controlsContainer;
    [SerializeField] GameObject hudContainer;
    [SerializeField] int nextLevelDelay;
    [SerializeField] StarterAssetsInputs starterAssetsInputs;

    int enemiesRemaining = 0;

    public void TogglePause()
    {
        if (isPaused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    public void Back() {
        optionsContainer.SetActive(false);
        pauseContainer.SetActive(true);
    }

    public void Options() {
        pauseContainer.SetActive(false);
        optionsContainer.SetActive(true);
    }

    public void MenuBack() {
        controlsContainer.SetActive(false);
        menuContainer.SetActive(true);
    }

    public void Controls() {
        menuContainer.SetActive(false);
        controlsContainer.SetActive(true);
    }

    void Pause() {
        hudContainer.SetActive(false);
        pauseContainer.SetActive(true);
        starterAssetsInputs.SetCursorState(false);
        AudioListener.pause = true;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Unpause() {
        hudContainer.SetActive(true);
        pauseContainer.SetActive(false);
        starterAssetsInputs.SetCursorState(true);
        AudioListener.pause = false;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void AdjustEnemiesRemaining(int amount) {
        enemiesRemaining += amount;

        if (enemiesRemaining <= 0)
        {
            levelCompleteText.SetActive(true);
            Invoke("NextLevel", nextLevelDelay);
        }
    }

    public void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetLevelButton() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitButton() {
        Debug.Log("Cannot quit in the editor");
        Application.Quit();
    }
}
