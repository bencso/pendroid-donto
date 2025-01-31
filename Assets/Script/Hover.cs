using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Linq;

public class Hover : MonoBehaviour {
    // Start is called before the first frame update

    public Tilemap tilemap;

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
                    tilemap.SetTile(new Vector3Int(-11, cell.y, 0), Droidplace.selectedTile);
                }
            }
        }
    }
}


// -11
// 2 , -4