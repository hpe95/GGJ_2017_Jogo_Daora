using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableObjects : MonoBehaviour {
	public AllPossiblePickups.Pickups whatToOpen;

	public bool TryToUse(ref List<AllPossiblePickups.Pickups> pickups){
		bool result = false;

		foreach (AllPossiblePickups.Pickups pickup in pickups) {
			if (pickup == whatToOpen) {
				result = true;
				break;
			}
		}
		if (result) {
			pickups.Remove (whatToOpen);
			Destroy (gameObject);
		}
		return result;
	}
}
