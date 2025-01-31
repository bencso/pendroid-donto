using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidPlace : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                    Debug.Log("Touched: " + hit.collider.gameObject.name);
                if (hit.collider != null)
                {
                }
            }
        }
    }
}
