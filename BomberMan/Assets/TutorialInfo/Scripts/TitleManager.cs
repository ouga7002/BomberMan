using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    void Update()
    {
        if (Gamepad.current != null && Gamepad.current.aButton.wasPressedThisFrame)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}