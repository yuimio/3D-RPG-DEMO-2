using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    TextMeshProUGUI levelText;
    Image healthSlider;
    Image expSlider;

     void Awake()
    {
        levelText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        healthSlider = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        expSlider = transform.GetChild(1).GetChild(0).GetComponent<Image>();
    }
     void Update()
    {
        levelText.text = "Level   " + GameManagers.Instance.playerStats.characterData.currentLevel.ToString("00");
        UpdateHealth();
        UpdateExp();
    }

    void UpdateHealth()
    {
        float sliderPercent = (float)GameManagers.Instance.playerStats.CurrentHealth / GameManagers.Instance.playerStats.MaxHealth;
        healthSlider.fillAmount = sliderPercent;
    }

    void UpdateExp()
    {
        float sliderPercent = (float)GameManagers.Instance.playerStats.characterData.currentExp / GameManagers.Instance.playerStats.characterData.baseExp;
        expSlider.fillAmount = sliderPercent;
    }
}
