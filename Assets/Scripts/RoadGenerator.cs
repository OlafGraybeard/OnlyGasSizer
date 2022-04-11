using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
	// singletone //
	public static RoadGenerator instance;
	private void Awake() { instance = this; }
	
	// prefabs //
	public List<GameObject> PrefabList = new List<GameObject>();
	
	private GameObject startPart;
	public GameObject FinishLine;
	private GameObject FinishLineObj;
	
	public Obstacles obs;
	
	public int traceDistance = 15;
	public int distance;
	
	// road properties //
	public int partCount = 7;
	public float speed = 1;
	public float maxSpeed = 10;
	public float curSpeed = 0;
	
	private List<GameObject> parts = new List<GameObject>();
	private List<GameObject> activeParts = new List<GameObject>();
	private int selector;
	private bool Moving;
	
    // Start is called before the first frame update
    void Start()
    {
		for ( int i = 0; i < partCount; i++ )
		{
			CreatePart();
		}
        ResetLvl();
		StartLvl();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
	
	private void CreatePart()
    {
		
        Vector3 pos = Vector3.zero;
		
		selector = (selector + 1) % PrefabList.Count;
        GameObject go = Instantiate(PrefabList[selector], pos, Quaternion.identity);
        go.transform.SetParent(transform);
		createObjects(go);
		if ( parts.Count < 1 )
		{
			startPart = go;
		}
        parts.Add(go);
		go.SetActive(false);
		
    }
	
	private void ActivatePart()
	{
		
		distance++;
		
		if ( distance == traceDistance )
		{
			Vector3 pos = activeParts[activeParts.Count - 1].transform.position + new Vector3(0, 0, 40);
			GameObject go = Instantiate(FinishLine, pos, Quaternion.identity);
			go.transform.SetParent(transform);
			FinishLineObj = go;
			go.SetActive(true);
			activeParts.Add(go);
		}
		else
		{
			Vector3 pos = Vector3.zero;
			int Select;
			GameObject part = startPart;
			if (activeParts.Count > 0)
			{
				pos = activeParts[activeParts.Count - 1].transform.position + new Vector3(0, 0, 40);
				Select = Random.Range( 0, parts.Count - 1 );
				part = parts[Select];
			}
			part.transform.position = pos;
			part.SetActive(true);
			
			foreach(Transform child in part.GetComponentsInChildren<Transform>()) {
				child.gameObject.SetActive(true);
			}
			
			activeParts.Add(part);
			parts.Remove(part);
		}
	}
	
	private void createObjects( GameObject go )
	{
		if ( parts.Count > 0 )
		{
			for ( int i = 0; i < 4; i++ )
			{
				obs.Create( go, i );
			}
		}
	}
	
	private void Move()
	{
		
		if( activeParts.Count < partCount )
		{
			ActivatePart();
		}
		
		if (speed < 1) { return; }
		
		
		transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
		
		if (activeParts[0].transform.position.z < -25)
        {
            activeParts[0].SetActive(false);
            parts.Add(activeParts[0]);
            activeParts.RemoveAt(0);
			ActivatePart();
        }
	}
	
	public void StartLvl()
	{
		speed = curSpeed;
	}
	public void PauseLvl()
	{
		curSpeed = speed;
		speed = 0;
	}
	
	public void ResetLvl()
	{
		speed = 0;
		if ( distance >= traceDistance )
		{
			Debug.Log("Fuck finish");
			activeParts.Remove(FinishLineObj);	
			Destroy(FinishLineObj);
		}
		distance = 0;
		while (activeParts.Count > 0)
        {
            parts.Add(activeParts[0]);
			activeParts[0].SetActive(false);
            activeParts.RemoveAt(0);
        }
	}
	
}
