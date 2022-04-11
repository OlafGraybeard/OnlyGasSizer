using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Counters : MonoBehaviour
{
	
	private int cupsGold;
	private int cupsSilver;
	private int cupsBronze;
	
	public TextMeshProUGUI CounterGold;
	public TextMeshProUGUI CounterSilver;
	public TextMeshProUGUI CounterBronze;
	
	void Start()
	{
		CounterBronze.text = "0";
		CounterSilver.text = "0";
		CounterGold.text = "0";
	}
	
	public void resetCounters()
	{
		cupsBronze = 0;
		cupsSilver = 0;
		cupsGold = 0;
		
		CounterBronze.text = "" + cupsBronze;
		CounterSilver.text = "" + cupsSilver;
		CounterGold.text = "" + cupsGold;
	}
	
	public void Insert( string type )
	{
		switch ( type )
		{
			case "bronze":
			cupsBronze++;
			CounterBronze.text = "" + cupsBronze;
			break;
			case "silver":
			cupsSilver++;
			CounterSilver.text = "" + cupsSilver;
			break;
			case "gold":
			cupsGold++;
			CounterGold.text = "" + cupsGold;
			break;
		}
	}
	
	public int[] getCups()
	{
		return new int[] { cupsGold, cupsSilver, cupsBronze };
	}
	
}
