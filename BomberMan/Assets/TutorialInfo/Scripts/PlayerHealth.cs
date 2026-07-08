using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("HP")]
    public int maxHP = 3;

    [Header("UI")]
    public TextMeshProUGUI hpText;

    [Header("無敵時間")]
    public float invincibleTime = 1f;

    private int currentHP;
    private bool invincible = false;

    void Start()
    {
        currentHP = maxHP;
        UpdateHPUI();
    }

    public void Damage(int damage)
    {
        // 無敵中はダメージを受けない
        if (invincible)
            return;

        currentHP -= damage;
        UpdateHPUI();
        Debug.Log(gameObject.name + " HP : " + currentHP);

        if (currentHP <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(Invincible());
    }

    IEnumerator Invincible()
    {
        invincible = true;

        yield return new WaitForSeconds(invincibleTime);

        invincible = false;
    }
    void UpdateHPUI()
    {
        if (hpText == null)
            return;

        string heart = "";

        for (int i = 0; i < currentHP; i++)
        {
            heart += "●";
        }

        hpText.text = "HP : " + heart;
    }
    void Die()
    {
        Debug.Log(gameObject.name + " が倒れた");

        Destroy(gameObject);
    }

    public int GetHP()
    {
        return currentHP;
    }
}