using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop : MonoBehaviour
{
	
	public List<GameObject> cars = new List<GameObject>();
	public SaveLoad sv;
	
	private GameObject CurrentCar;
	private int CurrentCarId;
	
	private void Start()
	{
		CurrentCarId = sv.getCar();
		spawnCar();
	}
	
	private void spawnCar()
	{
		if(CurrentCar != null)
		{
			Destroy(CurrentCar);
		}
		GameObject go = Instantiate(cars[CurrentCarId], transform.position, Quaternion.identity);
        go.transform.SetParent(transform);
		CurrentCar = go;
	}
	
	public void Next()
	{
		if ( CurrentCarId < cars.Count - 1 )
		{
			CurrentCarId++;
		}
		spawnCar();
		sv.setCar(CurrentCarId);
	}
	public void Previus()
	{
		if ( CurrentCarId > 0 )
		{
			CurrentCarId--;
		}
		spawnCar();
		sv.setCar(CurrentCarId);
	}
	
}
