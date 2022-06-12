using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{

    private GridManager grid;

    [SerializeField] private GameObject placedObject;

    void Awake()
    {
        grid = FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !(Input.GetKey(KeyCode.LeftShift)))
        {

            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.CompareTag("Building"))
                {
                    //Debug.Log("Collision");
                    PalceObjectAbove(hit.point);
                }
                else if (hit.collider.CompareTag("Environment"))
                {
                    //Debug.Log("No Collision");
                    PlaceObjectNear(hit.point);
                }

            }
        }
        else if (Input.GetKey(KeyCode.LeftShift)) {

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.collider.CompareTag("Building"))
                    {
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
        }
    }

    private void PlaceObjectNear(Vector3 clickPoint)
    {
        Vector3 finalPosition = grid.GetNearestPointOnGrid(clickPoint);

        // used to place a block 1x1 on the surface completely
        finalPosition.y = 0.5f;
        Debug.Log("placing initial");
        Instantiate(placedObject, finalPosition, Quaternion.identity); // this is for object cloning
    }

    private void PalceObjectAbove(Vector3 clickPoint)
    {
        Vector3 finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        finalPosition.y += 0.5f;
        Debug.Log("placing on " + finalPosition.y.ToString());
        Instantiate(placedObject, finalPosition, Quaternion.identity);
    }

}
