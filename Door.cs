using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    float x1;
    float x2;
    float y1;
    float y2;
    GameObject ob;
  
    float xScale = Builder.xScale;
    float yScale = Builder.yScale;
    private void Start()

    {
        GameObject testPrefab = (GameObject)Resources.Load("Door");
        Vector3 coordinates = getCoordinates();
        char rotation = getRotation();
        Quaternion angle = getAngle(rotation);

        GameObject prefabInstance= Instantiate(testPrefab, coordinates, angle);
        ob.transform.position = getCoordinates();
        prefabInstance.transform.parent = ob.transform;


        Bounds meshBounds = prefabInstance.GetComponent<MeshFilter>().mesh.bounds;
        Vector3 scale = getScale(meshBounds,rotation);
        
        prefabInstance.transform.localScale = scale;



       

    }
    private Vector3 getScale(Bounds b,char rotation)
    {
        if (rotation == 'v')
        {
            float yDiff = Mathf.Abs(y1 - y2);
            float meshBound = b.size.x;
            float scale = yDiff / meshBound;
            Vector3 newScale = new Vector3(scale*yScale, 1, 1);
            return newScale;
        }
        // here where you can handle diagonal doors in the future
       
        else
        {
            float xDiff = Mathf.Abs(x1 - x2);
            float meshBound = b.size.x;
            float scale = xDiff / meshBound;
            Vector3 newScale = new Vector3(scale*xScale, 1, 1);
            return newScale;
        }



    }
    private char getRotation()
    { 
        float xDiff = Mathf.Abs(x1 - x2);
        float yDiff = Mathf.Abs(y1 - y2);
        if (xDiff > yDiff)
        {
            return 'h'; 
        }
        if (yDiff > xDiff)
        {
            return 'v';
        }
        return 'n';
    }
    private  Quaternion getAngle(char c)
    {
        switch (c)
        {
            case 'v': return Quaternion.identity;

            case 'h':return Quaternion.Euler(0, 90, 0);
            default: return Quaternion.identity;
        }
    }
    private Vector3 getCoordinates()
    {
        float xCenter = x1+(Mathf.Abs(x2 - x1) / 2);
        float yCenter = y1+(Mathf.Abs(y2 - y1) / 2);
        return new Vector3(yCenter*yScale, 1, xCenter*xScale);
    }
    public void setPoints(float x1, float y1, float x2, float y2)

    {

     
        this.x1 = x1;
        this.x2 = x2 ;
        this.y1 = y1;
        this.y2 = y2 ;



    }

    public void setGameObjectReference(GameObject obj)

    {
        ob = obj;
    }

    }
