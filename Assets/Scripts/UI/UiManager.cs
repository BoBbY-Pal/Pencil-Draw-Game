using System;
using Frolicode;
using UnityEngine;


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
}