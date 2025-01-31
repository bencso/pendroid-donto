using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidSelect : MonoBehaviour
{
    public static DroidSelect Instance;

    public GameObject droid1;
    public GameObject droid2;

    public enum Droids
    {
        Melee,
        Range,
        Tank,
        Assasin
    }

    public Droids selectedDroid;


    private void Awake()
    {
        Instance = this;
    }

    public void SelectDoid(Droids droid)
    {
        switch (droid)
        {
            case Droids.Melee:
                selectedDroid = Droids.Melee;
                break;

            case Droids.Range:
                selectedDroid = Droids.Range;
                break;

            case Droids.Tank:
                selectedDroid = Droids.Tank;
                break;

            case Droids.Assasin:
                selectedDroid = Droids.Assasin;
                break;

        }
    }
}
