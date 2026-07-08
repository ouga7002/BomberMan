using UnityEngine;
using TMPro;

public class FadeBlink : MonoBehaviour
{
    [SerializeField] float speed = 2f;

    private TextMeshProUGUI textComponent;
    private Color color;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        color = textComponent.color;
    }

    void Update()
    {
        color.a = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        textComponent.color = color;
    }
}