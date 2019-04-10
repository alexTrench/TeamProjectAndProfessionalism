using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() 
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void switchToMainMenu()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = true;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
