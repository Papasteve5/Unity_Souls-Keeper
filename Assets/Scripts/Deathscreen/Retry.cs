using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{

    // Loads Game when clicked
    public void Nextscene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("Scene");
        }

    }
}
