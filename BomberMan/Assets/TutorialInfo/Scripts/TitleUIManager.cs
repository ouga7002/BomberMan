using UnityEngine;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private GameObject creditsPanel;

    // クレジットを開く
    public void OpenCredits()
    {
        titlePanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    // クレジットを閉じる
    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        titlePanel.SetActive(true);
    }
}