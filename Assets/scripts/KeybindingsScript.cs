using UnityEngine;
using UnityEngine.SceneManagement;

public class KeybindingsScript : MonoBehaviour
{
    void Update()
    {
        if(Input.anyKey)
        {
            SceneManager.LoadScene("Stage1");
        }
    }
}