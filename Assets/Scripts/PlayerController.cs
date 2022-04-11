using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
	
	private Rigidbody rigBody;
	
	public SaveLoad sv;
	
	public TimerController timer;
	
	private float limit = 2.6f;
	private float maxSpeed;
	
	public Counters counters;
	
    private bool TouchBegan() { return Input.GetMouseButtonDown(0); }
    private bool TouchEnded() { return Input.GetMouseButtonUp(0); }
    private bool Touch() { return Input.GetMouseButton(0); }
	
	private Vector2 firstPosition;
	private Vector2 deltaPos;
	
	public Animator DeathScreen;
	public Animator WinScreen;
	
	public bool controll = true;
	public Vector2 TouchPos() { return (Vector2)Input.mousePosition; }
	
	public List<GameObject> cars = new List<GameObject>();
	
	
    void Start()
    {
        maxSpeed = RoadGenerator.instance.maxSpeed;
		loadCar();
    }
	
	private void loadCar()
	{
		Vector3 pos = new Vector3( 0, 0.3f, 0 );
		GameObject go = Instantiate(cars[sv.getCar()], pos, Quaternion.identity);
		go.transform.SetParent(transform);
	}
	
    void FixedUpdate()
    {
        Moving();
		if ( timer.time < 1 )
		{
			RoadGenerator.instance.speed = 0;
			controll = false;
			DeathScreen.SetTrigger("Fade");
			timer.ResetTimer();
		}
    }
	
	
	void Moving()
	{
		if ( controll == true )
		{
			float X = deltaPos.x;
			if (EventSystem.current.IsPointerOverGameObject()) return;
			if ( Touch() && RoadGenerator.instance.speed < maxSpeed )
			{
				RoadGenerator.instance.speed += 3 * Time.deltaTime;
			}
			if ( !Touch() && RoadGenerator.instance.speed > 0 )
			{
				RoadGenerator.instance.speed -= 10 * Time.deltaTime;
			}
			if ( RoadGenerator.instance.speed < 0 )
			{
				RoadGenerator.instance.speed = 0;
			}
			
			if ( TouchBegan() )
			{
				timer.StartTimer();
				firstPosition = TouchPos();
			}
			if ( Touch() )
			{
				deltaPos = firstPosition - TouchPos();
				if ( transform.position.x <= limit && transform.position.x >= -limit )
				{
					transform.position += new Vector3( ( -deltaPos.x / 1000 ) * ( RoadGenerator.instance.speed / maxSpeed ), 0, 0 );
				}
				firstPosition = ( firstPosition * 0.95f )+ ( TouchPos() * 0.05f );
			}
			else
			{
				deltaPos = new Vector2(0,0);
			}
			if ( transform.position.x >= limit )
			{
				transform.position += new Vector3( -0.01f, 0, 0 );
				transform.position += new Vector3( -0.01f, 0, 0 );
			}
			if (transform.position.x <= -limit)
			{
				transform.position += new Vector3( 0.01f, 0, 0 );
			}
		}
	}
	
	void OnTriggerEnter(Collider toutch)
    {
        switch ( toutch.gameObject.tag )
		{
			case "barrier":
				died();
			break;
			case "obstacle":
				toutch.gameObject.SetActive(false);
				RoadGenerator.instance.speed -= 1;
			break;
			case "bronze":
				toutch.gameObject.SetActive(false);
				counters.Insert("bronze");
			break;
			case "silver":
				toutch.gameObject.SetActive(false);
				counters.Insert("silver");
			break;
			case "gold":
				toutch.gameObject.SetActive(false);
				counters.Insert("gold");
			break;
			case "finish":
				RoadGenerator.instance.speed = 0;
				controll = false;
				timer.StopTimer();
				sv.cupsCalc(counters.getCups());
				WinScreen.SetTrigger("Fade");
			break;
			default :
			break;
		}
    }
	
	public void Reset()
	{
		transform.position = Vector3.zero;
		transform.rotation = new Quaternion( 0, 0, 0, 0 );
		controll = true;
		
	}
	
	private void died()
	{
		counters.resetCounters();
		RoadGenerator.instance.speed = 0;
		controll = false;
		DeathScreen.SetTrigger("Fade");
		timer.ResetTimer();
	}
	
}
