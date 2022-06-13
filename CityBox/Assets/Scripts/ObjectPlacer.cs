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
    {   // object placement
        if (Input.GetMouseButtonDown(0) && !(Input.GetKey(KeyCode.LeftShift)))
        {

            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Building"))
                {
                    PlaceObjectAbove(hit.point);
                }
                else
                {
                    PlaceObject(hit.point);
                }
                
            }

        } // Object deletion
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

    private void PlaceObject(Vector3 clickPoint)
    {
        Vector3 finalPosition = grid.GetNearestPointOnGrid(clickPoint);

        // 4.5 because the ray should start below surface which is 5
        finalPosition.y = 4.5f;

        RaycastHit hit;
        
        // check to see if the palce you are trying to fill occupied
        if (Physics.Raycast(finalPosition, Vector3.up, out hit, 1f)){
            
            Debug.Log("Did Hit");
            finalPosition.y += 2f;
            Instantiate(placedObject, finalPosition, Quaternion.identity);

        }
        else
        {
            finalPosition.y = 5.5f;
            Debug.Log("placing initial");
            Instantiate(placedObject, finalPosition, Quaternion.identity); // this is for object cloning
        }

    }

    private void PlaceObjectAbove(Vector3 clickPoint)
    {

        Vector3 finalPosition = grid.GetNearestPointOnGrid(clickPoint);

        finalPosition.y += 0.5f;

        RaycastHit hit;

        // check to see if the palce you are trying to fill occupied
        if (Physics.Raycast(finalPosition, Vector3.up, out hit, 1f))
        {
            Debug.Log("not able to place"); // in the future show/tell the player they cannot
        }
        else
        {
            Instantiate(placedObject, finalPosition, Quaternion.identity);
        }
    }

}
