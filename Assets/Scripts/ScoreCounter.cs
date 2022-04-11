using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
	
	public TextMeshProUGUI CounterGold;
	public TextMeshProUGUI CounterSilver;
	public TextMeshProUGUI CounterBronze;
	
	public SaveLoad sv;
	
	private int[] cups;
	
    // Start is called before the first frame update
    void Start()
    {
        CounterGold.text = "0";
		CounterSilver.text = "0";
		CounterBronze.text = "0";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		cups = sv.getCups();
        CounterGold.text = "" + cups[0];
		CounterSilver.text = "" + cups[1];
		CounterBronze.text = "" + cups[2];
    }
}
