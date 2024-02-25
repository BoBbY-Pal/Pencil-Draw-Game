using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLoopingGame()
    {
        GameData.currentGameMode = GameMode.LOOPING;
        SceneManager.LoadScene("LoopingGame");
    }
    
    public void LoadSequencingGame()
    {
        GameData.currentGameMode = GameMode.SEQUENCE;
        SceneManager.LoadScene("SequenceGame");
    }
}
