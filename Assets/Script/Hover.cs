using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Linq;
using static DroidSelect;

public class Hover : MonoBehaviour {
    // Start is called before the first frame update

    public Tilemap tilemap;
    public GameObject[] rowStarts;
    public GameObject droidHolder;
    public GameObject droidMelee;
    public GameObject droidRanged;
    public GameObject droidTank;
    public GameObject droidAssassin;
    public GameObject droidPendroid;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);            
            Vector3Int cell = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(touch.position));
            int[] validY = {2, 0, -2, -4};
            if(cell != null)
            {
                if(validY.Contains(cell.y))
                {
                    int index = validY.ToList().IndexOf(cell.y);
                    Debug.Log(index);
                    for (int i = 0; i < rowStarts.Length; i++)
                    {
                        if (i == index)
                        {
                            if (DroidSelect.Instance.selectedDroid != Droids.none)
                            {
                                rowStarts[i].SetActive(true);
                                droidHolder.transform.position = rowStarts[i].transform.position;
                            }

                        }
                        else
                        {
                            rowStarts[i].SetActive(false);
                        }
                    }

                    if(touch.phase == TouchPhase.Ended)
                    {
                        for(int i = 0; i < rowStarts.Length; i++)
                        {
                            rowStarts[i].SetActive(false);
                        }


                    }

                }
            }
        }
    }

    private void SelectSprite(DroidSelect.Droids droid, bool active)
    {
        switch (droid)
        {
            case DroidSelect.Droids.Melee:
                droidMelee.SetActive(active);
                break;

            case DroidSelect.Droids.Range:
                droidRanged.SetActive(active);
                break;

            case DroidSelect.Droids.Tank:
                droidTank.SetActive(active);
                break;

            case DroidSelect.Droids.Assassin:
                droidAssassin.SetActive(active);
                break;

            case DroidSelect.Droids.Pendroid:
                droidPendroid.SetActive(active);
                break;

        }
    }
}


// -11
// 2 , -4