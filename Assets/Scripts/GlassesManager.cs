using UnityEngine;
using System.Collections;

public class GlassesManager : MonoBehaviour
{

    private bool isOpen;
    public Transform openPos;
    public Transform closedPos;
    private Vector3 toPosition;
    public GameObject Glass;
    public float speed;
    private bool movementDone = true;


    // Use this for initialization
    void Start ()
	{
	    isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if (!movementDone)
	    {
            float step = speed * Time.deltaTime;
            Glass.transform.localPosition = Vector3.MoveTowards(Glass.transform.localPosition, toPosition, step);
	        if (Glass.transform.localPosition == toPosition)
	        {
	            movementDone = true;
	        }
        }  
    }

    public void open()
    {
        if (!isOpen)
        {
            toPosition = openPos.localPosition;
            movementDone = false;
            isOpen = true;
        }
    }

    public void close()
    {
        toPosition = closedPos.localPosition;
        movementDone = false;
        isOpen = false;
    }


    
}
