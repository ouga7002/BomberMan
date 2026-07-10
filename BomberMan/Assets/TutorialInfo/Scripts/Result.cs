using TMPro;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;

    void Start()
    {
        string winner = PlayerPrefs.GetString("Winner", "");

        resultText.text = winner + " WIN!";
    }
}