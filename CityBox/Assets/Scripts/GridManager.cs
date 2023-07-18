/*  
 * Copyright 2023 Barkın Zorlu 
 * All rights reserved.
 * 
 * This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License. 
 * To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/4.0/ or send a letter to Creative Commons, PO Box 1866, Mountain View, CA 94042, USA.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private float size = 1f;

    //public float Size { get { return size; } }

    //public List<Vector3> occupied = new List<int>();

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        float xCount = Mathf.Round(position.x / size);
        float yCount = Mathf.Round(position.y / size);
        float zCount = Mathf.Round(position.z / size);

        Vector3 result = new Vector3 (xCount*size, yCount*size, zCount*size); 

        result += transform.position;

        return result;
    }


    private void OnDrawGizmos()
    {
        if(size > 0)
        {
            Gizmos.color = Color.blue;

            for (float x = 0; x < 50; x += size)
            {
                for (float z = 0; z < 50; z += size)
                {
                    var point = GetNearestPointOnGrid(new Vector3(x, 0f + 5, z));
                    Gizmos.DrawSphere(point, 0.1f);
                }

            }

        }
        
    }

}
