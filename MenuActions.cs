using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void StartGame()
    {
        GameObject.Find("Alien").GetComponent<CharacterManager>().start = true;
        GameObject.Find("StartCanvas").SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
