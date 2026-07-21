using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    // ボタンのOnClickに登録する
    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
