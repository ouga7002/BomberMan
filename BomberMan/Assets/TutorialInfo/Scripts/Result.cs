using TMPro;
using UnityEngine;

public class Result : MonoBehaviour
{
    [Header("Player1")]
    [SerializeField] private TextMeshProUGUI leftResultText;
    [SerializeField] private TextMeshProUGUI leftScoreText;

    [Header("Player2")]
    [SerializeField] private TextMeshProUGUI rightResultText;
    [SerializeField] private TextMeshProUGUI rightScoreText;

    private void Start()
    {
        string winner = PlayerPrefs.GetString("Winner", "");

        Debug.Log("受け取ったWinner：" + winner);

        int player1Score = PlayerPrefs.GetInt("Player1Score", 0);
        int player2Score = PlayerPrefs.GetInt("Player2Score", 0);

        leftScoreText.text = "Score : " + player1Score;
        rightScoreText.text = "Score : " + player2Score;

        if (winner == "1P")
        {
            leftResultText.text = "WIN";
            rightResultText.text = "LOSE";
        }
        else
        {
            leftResultText.text = "LOSE";
            rightResultText.text = "WIN";
        }
    }
}