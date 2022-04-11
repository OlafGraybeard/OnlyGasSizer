using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RND = System.Random;

public class Obstacles : MonoBehaviour
{
	
	public List<GameObject> obstacles = new List<GameObject>();
	public List<GameObject> cups = new List<GameObject>();
	
	private Vector3 pos = Vector3.zero;
	private Vector3 Shift = new Vector3( 0, 0, -20 );
	private float variator;
	private RND rnd = new RND();
	private int cont;
	
	// barrier properties //
	private static float b_pos = 2.15f;
	private static float b_step = 1.25f;
	private static float b_space = 10f;
	private static float c_space = 2f;
	
	public void Create( GameObject go, int i)
	{	
		variator = Random.Range(0f, 1f);
		switch ( variator )
		{
			case < 0.2f:
			obstaclesLine_ob( go, i );
			break;
			case < 0.4f:
			obstaclesLine_oob( go, i );
			break;
			case < 0.6f:
			cupspawn( go, i );
			break;
			case > 0.6f:
			obstaclesLine_sb( go, i );
			break;
			default:
			break;
		}
	}
	
	private void obstaclesLine_ob( GameObject go, float row )
	{
		Vector3 CurPos = go.transform.position;
		for( int i = 0; i < 4; i++)
		{
			pos = CurPos + Shift + (new Vector3( 0, 0.3f, b_space * row )) + (new Vector3( b_pos - i * b_step, 0, 0 ));
			GameObject bar = Instantiate(obstacles[rnd.Next(0,1)], pos, Quaternion.identity);
			bar.transform.SetParent(go.transform);
		}
	}
	private void obstaclesLine_oob( GameObject go, float row )
	{
		Vector3 CurPos = go.transform.position;
		
		for( int i = 0; i < 4; i++)
		{			
			pos = CurPos + Shift + (new Vector3( 0, 0, b_space * row )) + (new Vector3( b_pos - i * b_step, 0, 0 ));
			GameObject bar = Instantiate(obstacles[rnd.Next(0,2)], pos, Quaternion.identity);
			bar.transform.SetParent(go.transform);
		}
	}
	public void obstaclesLine_sb( GameObject go, float row )
	{
		Vector3 CurPos = go.transform.position;
		cont = 0;
		
		for( int i = 0; i < 4; i++)
		{
			int rndm = rnd.Next(0,1);
			if ( rndm > 0 && cont < 2 )
			{
				cont++;
				pos = CurPos + Shift + (new Vector3( 0, 0, b_space * row )) + (new Vector3( b_pos - i * b_step, 0, 0 ));
				GameObject bar = Instantiate(obstacles[2], pos, Quaternion.identity);
				bar.transform.SetParent(go.transform);
			}
		}
	}
	public void cupspawn( GameObject go, float row )
	{
		Vector3 CurPos = go.transform.position;
		cont = 0;
		
		for( int i = 0; i < 4; i++)
		{
			float rndm = Random.Range(0f,1f);
			if ( rndm > 0.4 && cont < 2 )
			{
				cont++;
				pos = CurPos + Shift + (new Vector3( 0, 0, b_space * row )) + (new Vector3( b_pos - i * b_step, 0, 0 ));
				GameObject bar = Instantiate(obstacles[2], pos, Quaternion.identity);
				bar.transform.SetParent(go.transform);
			}
			else
			{
				pos = CurPos + Shift + (new Vector3( 0, 0, b_space * row )) + (new Vector3( b_pos - i * b_step, 0, 0 ));
				GameObject bar = Instantiate(cups[rnd.Next(0,2)], pos, Quaternion.identity);
				bar.transform.SetParent(go.transform);
			}
		}
	}
}
