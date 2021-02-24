using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeIndex : MonoBehaviour
{

    public Slider slider;
    public TMPro.TMP_Text textField;

    private void Awake()
    {
        slider.value = Settings.volume;
        Settings.ChangeVolume((int)slider.value);
        textField.text = slider.value.ToString();     
    }

    public void UpdateVolume()
    {
        Settings.ChangeVolume((int)slider.value);
        textField.text = slider.value.ToString();
    }

}
