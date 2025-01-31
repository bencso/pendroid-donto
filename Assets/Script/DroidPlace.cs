using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DroidPlace : MonoBehaviour
{
    public Tilemap tilemap;

    public static DroidPlace Instance;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                Vector2 touchPosition = touch.position;
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.collider != null && hit.collider.CompareTag("Droid"))
                {
                    return;
                }
                
                Vector2 position = Camera.main.ScreenToWorldPoint(touch.position);
                TileBase clickedTile = tilemap.GetTile(tilemap.WorldToCell(position));
                Debug.Log(clickedTile.name);
            }
        }
    }
}
