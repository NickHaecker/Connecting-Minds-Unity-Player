using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PathObjects : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<Paths> listPaths = new List<Paths>();
    private GameObject obj;
    private float movementspeed = 5f;


    public void UnlockPath(CMPath path)
    {
        string pathname = path.Name;
        //if (pathname == "PathOne") 
        //{
            Paths p = listPaths.Find(path => path.pathdata.Name == pathname);
            p.activate();
            Debug.Log("Aktivierung von Path " + pathname + " wurde durchgeführt");
        //}

    }
    public void deactivateAll()
    {
        foreach (Paths path in listPaths)
        {
            Debug.Log("Deaktiviere Path wird aufgerufen für "+path.name);
            path.deactivate();
        }
    }

    //public void moveObject(CMPosition position)
    //{
        
        //float horizontalinput = Input.GetAxis("Horizontal");

        //if (ID == "1")
        //{
        //    obj = GameObject.Find("WallOne");
            
            //obj.transform.position = obj.transform.position + new Vector3(horizontalinput * movementspeed *Time.deltaTime, 0, 0);
        //}
        //Paths path = listPaths.Find(p => p.pathdata.Name == po)

    //}
}
