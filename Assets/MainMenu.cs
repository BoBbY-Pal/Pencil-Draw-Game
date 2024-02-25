using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLoopingGame()
    {
        SceneManager.LoadScene("LoopingGame");
    }
    
    public void LoadSequencingGame()
    {
        SceneManager.LoadScene("SequenceGame");
    }
}
