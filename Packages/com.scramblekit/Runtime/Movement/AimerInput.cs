using UnityEngine;

namespace ScrambleKit
{
	[RequireComponent(typeof(Aimer))]
	public class AimerInput : MonoBehaviour
	{
		private Aimer aimer;

		void Start()
		{
			aimer = GetComponent<Aimer>();
		}

		void Update()
		{
			aimer.aimingAt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}
}
