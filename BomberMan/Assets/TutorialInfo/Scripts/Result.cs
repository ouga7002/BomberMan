using TMPro;
using UnityEngine;

public class Result: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerText;

    [SerializeField] private TextMeshProUGUI leftResultText;
    [SerializeField] private TextMeshProUGUI rightResultText;

    private void Start()
    {
        string winner = PlayerPrefs.GetString("Winner", "");

        winnerText.text = winner + " WIN!";

        if (winner == "Player1")
        {
            leftResultText.text = "WIN";
            rightResultText.text = "LOSE";
        }
        else if (winner == "Player2")
        {
            leftResultText.text = "LOSE";
            rightResultText.text = "WIN";
        }
    }
}