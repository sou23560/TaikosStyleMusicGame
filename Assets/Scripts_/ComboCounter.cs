using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // ← これを追加

public class ComboCounter : MonoBehaviour
{
    [SerializeField] int combo = 0;
    int comboBefore = 0;

    [SerializeField] GameObject scoreObject = null;
    TMP_Text scoreText;

    void Start()
    {
        if (scoreObject != null)
            scoreText = scoreObject.GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (combo != comboBefore && scoreText != null)
        {
            comboBefore = combo;
            scoreText.text = "Combo: " + combo;
        }
    }

    public void AddCombo(int amount)
    {
        combo += amount;
    }

    public void ResetCombo()
    {
        combo = 0;
    }
}
