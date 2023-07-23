/*  
 * Copyright June 2022 Barkın Zorlu 
 * All rights reserved. 
 */

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
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
    public RoadManager roadManager = null;
    public GameObject selectedObject = null;
    public ObjectMode mode = ObjectMode.None;

    #region Road Variables
    [Header("Road Variables")]
    public GameObject roadPreviewPrefab = null;
    private GameObject roadPreview = null;
    public List<Vector3> vehicleRoadNodes = new List<Vector3>(); // change to private later
    public List<GameObject> vehicleRoads = new List<GameObject>(); // change to private later
    public Vector3[] roadCreationPoints = new Vector3[2];
    private bool initilized = false;
    private bool empty = true;
    #endregion

    public GameObject singleGrid = null;
    private List<GameObject> grids = new List<GameObject>();

    // heirarchy parents
    [Header("Heirarchy Parents")]
    public GameObject residentalParent = null;
    public GameObject commercialParent = null;
    public GameObject uniqueParent = null;
    public GameObject vehicleRoadParent = null;

    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;

        //if (singleGrid == null)
        //    singleGrid = new GameObject("Grid", PrimitiveType.Quad);

        if(lifeCycleManager == null)
            GetComponent<LifeCycleManager>(); // on the same GameObject

        if(buildingManager == null)
            GetComponent<BuildingManager>(); // on the same GameObject

        if(roadManager == null)
            GetComponent<RoadManager>(); // on the same GameObject

    }

    private void Update()
    {
        if (mode == ObjectMode.None)
            return;

        if (Input.GetMouseButtonDown(0)) // this action wont trigger if mode is None
            MouseAction();

        if (selectedObject.layer == 7) // road
        {
            if (Input.GetKeyDown(KeyCode.LeftControl)) // canceling of road addition
            {
                selectedObject = null;
                mode = ObjectMode.None;
                empty = true;
                initilized = false;
                ToggleGrids();
                if(roadPreview != null)
                    Destroy(roadPreview);
                return;
            }

            PreviewRoad(roadManager.vehicleRoadType);
        }
            

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

                Vector3 placingPoint = new Vector3(Mathf.RoundToInt(hit.point.x), Mathf.RoundToInt(hit.point.y), Mathf.RoundToInt(hit.point.z)); // should add where roads are

                if (hit.collider.gameObject.layer == 6) // buildings
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
                else if (hit.collider.gameObject.layer == 8) // grids
                { 
                    
                }
                else if (hit.collider.gameObject.layer == 7) // roads
                { // cannot put building or other roads ontop of other roads
                    return;
                }
                else // everything else
                {
                    if (selectedObject.tag == "Residental")
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
                    else if (selectedObject.tag == "Unique")// unique
                    {
                        Instantiate(selectedObject, placingPoint, Quaternion.identity, uniqueParent.transform);
                        //radious make happy and stuff
                        buildingManager.uniqueCount++;
                    }
                    else // roads
                    {
                        PlaceRoad(placingPoint, roadManager.vehicleRoadType);
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
                    Destroy(hit.collider.transform.parent.gameObject);
                    // check for builiding road proximity if too far away destroy the builidng after a time
                }     
            }
        }
    }

    private void PlaceRoad(Vector3 point, VehicleRoadType vehicleRoadType)
    {
        float roadWidth = 2f;
        if (empty)
        {
            roadCreationPoints[0] = point;
            empty = false;
            initilized = true;

            switch (vehicleRoadType)
            {
                case VehicleRoadType.TwoWay: roadWidth = 2; break;
                case VehicleRoadType.OneWay: roadWidth = 2; break;
                case VehicleRoadType.Asymetric: roadWidth = 2; break;
                // other cases cannot have grids
            }
        }
        else if (initilized)
        {
            roadCreationPoints[1] = point;
            // adding of roads between points

            // direction
            Vector3 direction = roadCreationPoints[1] - roadCreationPoints[0];
            direction.Normalize();

            float distance = Vector3.Distance(roadCreationPoints[0], roadCreationPoints[1]);
            if (distance < 0)
                distance *= -1;

            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            
            Vector3 position = roadCreationPoints[0];
            GameObject road = Instantiate(selectedObject, position, rotation, vehicleRoadParent.transform);
            road.transform.position += distance / 2f * direction ;
            road.transform.localScale = new Vector3(1, 1, distance);
            vehicleRoads.Add(road);

            Vector3 rightGridPosition = road.transform.position - distance / 2f * direction + road.transform.right * roadWidth;
            Vector3 leftGridPosition = road.transform.position - distance / 2f * direction + road.transform.right * -roadWidth;

            // adding grids
            for (int i = 0; i < (int)distance; i++)
            {
                // left side
                GameObject leftInner = Instantiate(singleGrid, leftGridPosition, rotation);
                leftInner.transform.Rotate(new Vector3(90, 0, 0));
                leftInner.transform.parent = road.transform;
                grids.Add(leftInner);

                // outer left side
                GameObject leftOuter = Instantiate(singleGrid, leftGridPosition - road.transform.right, rotation);
                leftOuter.transform.Rotate(new Vector3(90, 0, 0));
                leftOuter.transform.parent = road.transform;
                grids.Add(leftOuter);

                leftGridPosition += road.transform.forward;

                // right side
                GameObject rightInner = Instantiate(singleGrid, rightGridPosition, rotation);
                rightInner.transform.Rotate(new Vector3(90, 0, 0));
                rightInner.transform.parent = road.transform;
                grids.Add(rightInner);

                // outer right side
                GameObject rightOuter = Instantiate(singleGrid, rightGridPosition + road.transform.right, rotation);
                rightOuter.transform.Rotate(new Vector3(90, 0, 0));
                rightOuter.transform.parent = road.transform;
                grids.Add(rightOuter);

                rightGridPosition += road.transform.forward;
            }

            vehicleRoadNodes.Add(roadCreationPoints[1]);
            roadCreationPoints[0] = roadCreationPoints[1];
        }
            
    }

    public void ToggleGrids()
    {
        if (mode != ObjectMode.None)
        {
            foreach (var grid in grids)
                grid.SetActive(true);
        }
        else
        {
            foreach (var grid in grids)
                grid.SetActive(false);
        }
    }

    private void PreviewRoad(VehicleRoadType vehicleRoadType)
    {
        if (roadPreview == null && roadCreationPoints[1] != Vector3.zero)
        {
            float roadWidth = 3f;
            switch (vehicleRoadType)
            {
                case VehicleRoadType.TwoWay: roadWidth = 3; break;
                case VehicleRoadType.OneWay: roadWidth = 3; break;
                case VehicleRoadType.Asymetric: roadWidth = 3; break;
                    // other cases cannot have grids
            }

            roadPreview = Instantiate(roadPreviewPrefab, roadCreationPoints[1], Quaternion.identity);
            roadPreview.transform.localScale = new Vector3(roadWidth, roadPreviewPrefab.transform.localScale.y, roadPreviewPrefab.transform.localScale.z);
        }
            

        if (initilized && roadCreationPoints[1] != Vector3.zero)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                Vector3 placingPoint = new Vector3(Mathf.RoundToInt(hit.point.x), Mathf.RoundToInt(hit.point.y), Mathf.RoundToInt(hit.point.z));

                Vector3 direction = placingPoint - roadCreationPoints[1];
                direction.Normalize();

                float distance = Vector3.Distance(placingPoint, roadCreationPoints[1]);
                if (distance < 0)
                    distance *= -1;

                Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

                roadPreview.transform.position = direction * distance / 2 + roadCreationPoints[1];
                roadPreview.transform.localScale = new Vector3(roadPreview.transform.localScale.x, roadPreview.transform.localScale.y, distance);
                roadPreview.transform.rotation = rotation;
            }
        }
    }

    private void OpenPropertiesMenu(GameObject go)
    {

    }
}
