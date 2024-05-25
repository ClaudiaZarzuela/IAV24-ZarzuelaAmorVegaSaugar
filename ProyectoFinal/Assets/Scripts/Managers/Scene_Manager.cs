using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene_Manager : MonoBehaviour
{
    public void ChangeToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ChangeToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
