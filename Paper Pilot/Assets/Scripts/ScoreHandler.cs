using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    public TMPro.TMP_Text textField;

    private void Update()
    {
        textField.text = "Score: " + PlayerStats.score.ToString();
    }

}
