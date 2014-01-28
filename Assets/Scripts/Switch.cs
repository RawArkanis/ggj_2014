using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
	public GameObject[] targets;
	// Use this for initialization
	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.tag == "CrateBig")
		{
			foreach (GameObject g in targets)
			{
				if(g)
					g.GetComponent<SwitchActivate>().ActivateSwitch();
			}
		}
	}
}
