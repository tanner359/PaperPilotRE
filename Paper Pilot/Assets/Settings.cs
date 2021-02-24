using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    [SerializeField] public static int volume = 5;


    public static void ChangeVolume(int v)
    {
        volume = v;
    }

}
