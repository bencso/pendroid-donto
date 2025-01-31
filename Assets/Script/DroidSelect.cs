using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroidSelect : MonoBehaviour
{
    public static DroidSelect Instance;

    public Image lastSelectedButton;

    public enum Droids
    {
        Melee,
        Range,
        Tank,
        Assassin,
        Pendroid,
        none
    }

    public Droids selectedDroid = Droids.none;


    private void Awake()
    {
        Instance = this;
    }

    public void SelectMelee(Image buttonImage)
    {
        if (selectedDroid == Droids.Melee)
        {
            ResetSelection();
            return;
        }
        else
        {
            ResetSelection();
        }

        selectedDroid = Droids.Melee;
        buttonImage.color = Color.yellow;
        lastSelectedButton = buttonImage;
    }

    public void SelectRange(Image buttonImage)
    {
        if (selectedDroid == Droids.Range)
        {
            ResetSelection();
            return;
        }
        else
        {
            ResetSelection();
        }

        selectedDroid = Droids.Range;
        buttonImage.color = Color.yellow;
        lastSelectedButton = buttonImage;
    }

    public void SelectTank(Image buttonImage)
    {
        if (selectedDroid == Droids.Tank)
        {
            ResetSelection();
            return;
        }
        else
        {
            ResetSelection();
        }

        selectedDroid = Droids.Tank;
        buttonImage.color = Color.yellow;
        lastSelectedButton = buttonImage;
    }

    public void SelectAssassin(Image buttonImage)
    {
        if (selectedDroid == Droids.Assassin)
        {
            ResetSelection();
            return;
        }
        else
        {
            ResetSelection();
        }

        selectedDroid = Droids.Assassin;
        buttonImage.color = Color.yellow;
        lastSelectedButton = buttonImage;
    }

    public void SelectPendroid(Image buttonImage)
    {
        if (selectedDroid == Droids.Pendroid)
        {
            ResetSelection();
            return;
        }
        else
        {
            ResetSelection();
        }

        selectedDroid = Droids.Pendroid;
        buttonImage.color = Color.yellow;
        lastSelectedButton = buttonImage;
    }

    public void ResetSelection()
    {
        selectedDroid = Droids.none;
        if (lastSelectedButton != null) lastSelectedButton.color = Color.white;
        lastSelectedButton = null;
    }
}
