using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
	
	public List<GameObject> points = new List<GameObject>();
	
	private Quaternion trgRotation;
	private Quaternion strtRotation;
	
	private Vector3 trgPos;
	private Vector3 strtPosition;
	
	private float speed = 0.5f;
	private float startTime;
	private float journeyLength;
	
	private int selector;
	
    // Start is called before the first frame update
    void Start()
    {
		setParams();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveing();
    }
	
	public void changePos( string str )
	{
		switch ( str )
		{
			case "menu":
			selector = 0;
			break;
			case "shop":
			selector = 1;
			break;
		}
		setParams();
	}
	
	private void moveing()
	{
		
		if ( trgPos != transform.position )
		{
			float distCovered = ( Time.time - startTime ) * speed * 7.5f;
			float fractionOfJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp( strtPosition, trgPos, fractionOfJourney );
		}
		if ( trgRotation != transform.rotation )
		{
			transform.rotation = Quaternion.Lerp(strtRotation, trgRotation, ( Time.time - startTime ) * speed);
		}
	}
	
	private void setParams()
	{
		trgPos = points[selector].transform.position;
		trgRotation = points[selector].transform.rotation;
		strtPosition = transform.position;
		strtRotation = transform.rotation;
		startTime = Time.time;
		journeyLength = Vector3.Distance( strtPosition, trgPos );
	}
	
}
