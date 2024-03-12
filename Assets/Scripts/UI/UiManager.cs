using Frolicode;
using ManagersAndControllers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;


public class UiManager : Singleton<UiManager>
{
    public TextMeshProUGUI levelNumberTxt;
    public Button backButton;
    public GameObject levelPassedUI;
    public GameObject levelFailedUI;
    private void Awake()
    {
        backButton.onClick.AddListener(BackBtnPressed);
    }
    
    public void SetLevelNumber(int num)
    {
        levelNumberTxt.SetText("Level: " + num.ToString());
    }

    public void LevelPassed()
    {
        levelPassedUI.SetActive(true);
    }
    
    public void LevelFailed()
    {
        levelFailedUI.SetActive(true);
    }

    public void LoadNextLevelBtnPressed()
    {
        levelPassedUI.SetActive(false);
        GameManager.Instance.currentLevelIndex++;
        GameManager.Instance.LoadLevel();
    }
    
    public void BackBtnPressed()
    {
        Debug.Log("Back button pressed");
        SceneManager.LoadScene("MainMenu");
    }

    public void TryAgain()
    {
        Debug.Log("Try again button pressed");
        levelFailedUI.SetActive(false);
        levelPassedUI.SetActive(false);
        GameManager.Instance.LoadLevel();
    }
}