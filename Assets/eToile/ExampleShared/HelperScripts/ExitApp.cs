using UnityEngine;

public class ExitApp : MonoBehaviour
{
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            print("Quit app");
        }
    }
}
