using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{

    public void OpenLevel(int levelID){
        SceneManager.LoadScene(levelID);
    }
}
