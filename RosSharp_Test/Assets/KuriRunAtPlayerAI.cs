using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
	public class KuriRunAtPlayerAI : MonoBehaviour{
		#region members
		public float speed = 1.0f;
		public float speed_max = 1.0f;
		public float speed_min = 0.0f;
		public float speed_increase = 0.1f;
		public float speed_decrease = 0.1f;
		public float speed_increase_time = 0.5f;
		public float speed_decrease_time = 0.5f;
		public float speed_increase_time_max = 1.0f;
		public float speed_decrease_time_max = 1.0f;
		public float speed_increase_time_min = 0.0f;
		public float speed_decrease_time_min = 0.0f;
		Transform _playerT;
		public Transform PlayerT{
			get{
				if(_playerT == null){
					_playerT = Camera.main.transform;
				}
				return _playerT;
			}
		}
		public Collider PlayerCollider{
			get{
				return PlayerT.GetComponent<Collider>();
			}
		}

		#endregion

		#region unity
		void Start(){
			speed = speed_min;
		}

		void FixedUpdate(){
			// if I see the player using a raycast forward, I will run at them
			// else I will spin in circles
			// draw a debug raycast forward
			Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
			if(Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, 10.0f)){
				if(hit.collider == PlayerCollider){
					transform.position += -transform.forward * speed * Time.deltaTime;
					speed += speed_increase * Time.deltaTime;
					if(speed > speed_max){
						speed = speed_max;
					}
				}
				else{
					speed -= speed_decrease * Time.deltaTime;
					if(speed < speed_min){
						speed = speed_min;
					}
				}
			}
			else{
				speed -= speed_decrease * Time.deltaTime;
				if(speed < speed_min){
					speed = speed_min;
				}
			}
		}
		#endregion

		#region public
		#endregion

		#region private
		#endregion
	}
}
