using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PathObjects : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<Path> listPaths = new List<Path>();
    private GameObject obj;
    private float movementspeed = 5f;


    public void UnlockPath(CMPath path)
    {
        string pathname = path.Name;
        if (pathname == "PathOne") 
        {
            Path p = listPaths.Find(path => path.pathdata.Name == pathname);
            p.activate();
        }

    }
    public void deactivateAll()
    {
        foreach (Path path in listPaths)
        {
            path.deactivate();
        }
    }

    public void moveObject(string ID)
    {
        
        float horizontalinput = Input.GetAxis("Horizontal");

        if (ID == "1")
        {
            obj = GameObject.Find("WallOne");
            obj.transform.position = obj.transform.position + new Vector3(horizontalinput * movementspeed *Time.deltaTime, 0, 0);
        }
    }
}
