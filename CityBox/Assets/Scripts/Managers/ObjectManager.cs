/*  
 * Copyright June 2022 Barkın Zorlu 
 * All rights reserved. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ObjectMode
{
    None,
    Add,
    Edit,
    Remove
}

public class ObjectManager : MonoBehaviour
{
    public Camera cam;
    public LifeCycleManager lifeCycleManager = null;
    public BuildingManager buildingManager = null;
    public GameObject selectedObject = null;
    public ObjectMode mode = ObjectMode.None;

    // heirarchy parents
    [Header("Heirarchy Parents")]
    public GameObject residentalParent = null;
    public GameObject commercialParent = null;
    public GameObject uniqueParent = null;

    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;

        if(lifeCycleManager == null)
            GetComponent<LifeCycleManager>(); // on the same GameObject

        if(buildingManager == null)
            GetComponent<BuildingManager>(); // on the same GameObject
    }

    private void Update()
    {
        if (mode == ObjectMode.None)
            return;

        if (Input.GetMouseButtonDown(0)) // should add: is in add mode
            MouseAction();
    }

    private void MouseAction() 
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if(mode == ObjectMode.Add)
            { // colliders will remain as they are not affecting the performance even when there is 500+ buildings (in view so no culling)
                if (selectedObject == null)
                    return;

                Vector3 placingPoint = new Vector3(Mathf.RoundToInt(hit.point.x), Mathf.RoundToInt(hit.point.y), Mathf.RoundToInt(hit.point.z));
                
                if (hit.collider.gameObject.layer == 6)
                {
                    placingPoint += Vector3.up * 0.5f;
                    
                    if (selectedObject.tag == "Residental")
                    {
                        Instantiate(buildingManager.residentalUpper, placingPoint, Quaternion.identity, residentalParent.transform); // make this building upper
                        lifeCycleManager.populationLimit += buildingManager.residencePopulationLimit;
                        buildingManager.residenceCount++;
                    }
                    else if (selectedObject.tag == "Commercial")
                    {
                        Instantiate(buildingManager.commercialUpper, placingPoint, Quaternion.identity, commercialParent.transform); // make this building upper
                        //Jobs.type stuff += limit;
                        buildingManager.commercialCount++;
                    }
                }
                else
                {
                    if(selectedObject.tag == "Residental")
                    {
                        Instantiate(selectedObject, placingPoint, Quaternion.identity, residentalParent.transform);
                        lifeCycleManager.populationLimit += buildingManager.residencePopulationLimit;
                        buildingManager.residenceCount++;
                    }
                    else if (selectedObject.tag == "Commercial")
                    {
                        Instantiate(selectedObject, placingPoint, Quaternion.identity, commercialParent.transform);
                        //lifeCycleManager.job and stuff += buildingManager.residencePopulationLimit;
                        buildingManager.commercialCount++;
                    }
                    else // unique
                    {
                        Instantiate(selectedObject, placingPoint, Quaternion.identity, uniqueParent.transform);
                        //radious make happy and stuff
                        buildingManager.uniqueCount++;
                    }
                }
                    
            }
            else if (mode == ObjectMode.Edit)
            {
                // buildings                            // roads
                if (hit.collider.gameObject.layer == 6 || hit.collider.gameObject.layer == 7) // add future layers here
                    OpenPropertiesMenu(hit.collider.gameObject);

            }
            else if (mode == ObjectMode.Remove)
            {
                if (hit.collider.gameObject.layer == 6) // buildings
                { 
                    if(hit.collider.gameObject.tag == "Residental")
                    {
                        lifeCycleManager.populationLimit -= buildingManager.residencePopulationLimit;
                        buildingManager.residenceCount--;
                    }
                    else if(hit.collider.gameObject.tag == "Commercial")
                    {
                        //lifeCycleManager.job type stuff -= buildingManager.residencePopulationLimit;
                        buildingManager.commercialCount--;
                    }
                    

                    Destroy(hit.collider.gameObject);

                }
                else if(hit.collider.gameObject.layer == 7) // roads
                {
                    Destroy(hit.collider.gameObject);
                    // check for builiding road proximity if too far away destroy the builidng after a time
                }     
            }
        }
    }

    private void OpenPropertiesMenu(GameObject go)
    {

    }
}
