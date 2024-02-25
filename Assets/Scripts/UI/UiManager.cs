using Frolicode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UiManager : Singleton<UiManager>
{
    public Button backButton;
    public GameObject levelPassedUI;
    public GameObject levelFailedUI;
    private void Awake()
    {
        backButton.onClick.AddListener(BackBtnPressed);
    }
    
    private void Start()
    {
        
    }

    public void LevelPassed()
    {
        levelPassedUI.SetActive(true);
    }
    
    public void LevelFailed()
    {
        levelFailedUI.SetActive(true);
    }

    public void BackBtnPressed()
    {
        Debug.Log("Back button pressed");
        SceneManager.LoadScene("MainMenu");
    }

    public void TryAgain()
    {
        Debug.Log("Try again button pressed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}