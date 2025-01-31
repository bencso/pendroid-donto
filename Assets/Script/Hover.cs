using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Linq;
using static DroidSelect;

public class Hover : MonoBehaviour
{
    // Start is called before the first frame update

    public Tilemap tilemap;
    public GameObject[] rowStarts;
    public GameObject droidHolder;
    public GameObject droidMelee;
    public GameObject droidRanged;
    public GameObject droidTank;
    public GameObject droidAssassin;
    public GameObject droidPendroid;

    public DroidManager droidManager;

    void Start()
    {
        Debug.Log(droidHolder.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3Int cell = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(touch.position));
            int[] validY = { 2, 0, -2, -4 };
            if (cell != null)
            {
                if (validY.Contains(cell.y))
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
                                rowStarts[i].transform.position = new Vector3(rowStarts[i].transform.position.x, rowStarts[i].transform.position.y, 0);
                                droidHolder.transform.position = new Vector3(rowStarts[i].transform.position.x, rowStarts[i].transform.position.y+0.5F, 0);
                                SelectSprite(DroidSelect.Instance.selectedDroid, true);
                            }

                        }
                        else
                        {
                            rowStarts[i].SetActive(false);
                        }
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        for (int i = 0; i < rowStarts.Length; i++)
                        {
                            rowStarts[i].SetActive(false);
                        }
                        SelectSprite(DroidSelect.Instance.selectedDroid, false);
                        
                        // Új droid létrehozása a kiválasztott pozícióban
                        GameObject newDroid = null;
                        switch (DroidSelect.Instance.selectedDroid)
                        {
                            case DroidSelect.Droids.Melee:
                                newDroid = Instantiate(droidMelee, droidHolder.transform.position, Quaternion.identity);
                                break;
                            case DroidSelect.Droids.Range:
                                newDroid = Instantiate(droidRanged, droidHolder.transform.position, Quaternion.identity);
                                break;
                            case DroidSelect.Droids.Tank:
                                newDroid = Instantiate(droidTank, droidHolder.transform.position, Quaternion.identity);
                                break;
                            case DroidSelect.Droids.Assassin:
                                newDroid = Instantiate(droidAssassin, droidHolder.transform.position, Quaternion.identity);
                                break;
                            case DroidSelect.Droids.Pendroid:
                                newDroid = Instantiate(droidPendroid, droidHolder.transform.position, Quaternion.identity);
                                break;
                        }
                        
                        if (newDroid != null)
                        {
                            newDroid.SetActive(true);
                            newDroid.AddComponent<DroidManager>();
                            place.Instance.addAttackUnit(cell.x, cell.y, cell.z, DroidSelect.Instance.selectedDroid);
                        }
                    }

                }
            }
        }
        else
        {
            SelectSprite(DroidSelect.Instance.selectedDroid, false);

        }
    }

    private void SelectSprite(DroidSelect.Droids droid, bool active)
    {
        switch (droid)
        {
            case DroidSelect.Droids.Melee:
                droidMelee.transform.position = new Vector3(droidHolder.transform.position.x, droidHolder.transform.position.y, 0);
                droidMelee.SetActive(active);
                droidAssassin.SetActive(false);
                droidRanged.SetActive(false);
                droidTank.SetActive(false);
                droidPendroid.SetActive(false);
                break;

            case DroidSelect.Droids.Range:
                droidRanged.transform.position = new Vector3(droidHolder.transform.position.x, droidHolder.transform.position.y, 0);
                droidRanged.SetActive(active);
                droidMelee.SetActive(false);
                droidAssassin.SetActive(false);
                droidTank.SetActive(false);
                droidPendroid.SetActive(false);
                break;

            case DroidSelect.Droids.Tank:
                droidTank.transform.position = new Vector3(droidHolder.transform.position.x, droidHolder.transform.position.y, 0);
                droidTank.SetActive(active);
                droidMelee.SetActive(false);
                droidAssassin.SetActive(false);
                droidRanged.SetActive(false);
                droidPendroid.SetActive(false);
                break;

            case DroidSelect.Droids.Assassin:
                droidAssassin.transform.position = new Vector3(droidHolder.transform.position.x, droidHolder.transform.position.y, 0);
                droidAssassin.SetActive(active);
                droidMelee.SetActive(false);
                droidRanged.SetActive(false);
                droidTank.SetActive(false);
                droidPendroid.SetActive(false);
                break;

            case DroidSelect.Droids.Pendroid:
                droidPendroid.transform.position = new Vector3(droidHolder.transform.position.x, droidHolder.transform.position.y, 0);
                droidPendroid.SetActive(active);
                droidMelee.SetActive(false);
                droidRanged.SetActive(false);
                droidTank.SetActive(false);
                droidAssassin.SetActive(false);
                break;

        }
    }
}


// -11
// 2 , -4