using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveLoad : MonoBehaviour
{	

	private Save sv = new Save();

	void Start()
	{
		load();
	}

	public void save()
	{
		PlayerPrefs.SetString("SAVE", JsonUtility.ToJson(sv));
	}
	public void load()
	{
		if( !PlayerPrefs.HasKey("SAVE"));
		else
		{
			sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SAVE"));
		}
	}
	public void cupsCalc( int[] cups )
	{
		sv.cupsGold += cups[0];
		sv.cupsSilver += cups[1];
		sv.cupsBronze += cups[2];
		save();
	}
	public void setCar( int id )
	{
		sv.CarId = id;
		save();
	}
	public int getCar()
	{
		return sv.CarId;
	}
	public int[] getCups()
	{
		return new int[]{ sv.cupsGold, sv.cupsSilver, sv.cupsBronze };
	}
	
}

[Serializable]
public class Save
{
	
    public int cupsGold;
    public int cupsSilver;
    public int cupsBronze;
	
	public int CarId;
	
	public int[] cars = { 0 };
	
}