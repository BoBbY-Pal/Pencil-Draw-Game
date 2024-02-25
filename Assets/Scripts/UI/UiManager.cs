using Frolicode;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UiManager : Singleton<UiManager>
{
    public GameObject levelPassedUI;
    public GameObject levelFailedUI;
    private void Awake()
    {
        
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
        SceneManager.LoadScene("MainMenu");
    }
}