using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Link.Card
{
    public class Control : MonoBehaviour
    {
        [SerializeField] Card[] cards;

        public void OnInit()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider != null)
                {
                    Debug.Log(": " + hit.collider.name);
                }
            }
        }
    }
}
