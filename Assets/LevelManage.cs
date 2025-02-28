using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManage : MonoBehaviour
{
    public void OpenLevel(int levelID){
        SceneManager.LoadScene(levelID);
    }
}
