using UnityEngine;
using System.Collections;

public class VisionCone : MonoBehaviour 
{
	private bool debug = true;

	public float Distance = 5f;
	public float Angle = 45f;
	public int nbRays;
	public Transform Origin;
	public LayerMask DetectedLayers;

	private GameObject Target;
	public string Function;

	//working vars
	private Vector3 targetDir;
	private Vector3 forward;
	private float startAngle;
	private float subAngle;
	private float currentAngle;
	private float rayCount;
	private RaycastHit hitInfo;

	private Object[] parameters;
	private Ray ray;

	void Awake()
	{
		parameters = new Object[2];
		ray = new Ray();
		Target = GameObject.Find("GameManager");
	}

	void Update () 
	{
		startAngle = Angle / 2f;
		nbRays = nbRays - nbRays % 2 + 1;
		subAngle = Angle / (nbRays - 1);

		currentAngle = startAngle;
		for(int i = 0 ; i < nbRays; i++)
		{
			ray.origin = Origin.position;
			ray.direction = Quaternion.Euler(new Vector3(0f, 0f, currentAngle)) * transform.parent.up;

			if(debug)
			{
				var end = ray.origin + (ray.direction * Distance);
				Debug.DrawLine(ray.origin, end, Color.green);
			}

			if (Physics.Raycast(ray, out hitInfo, Distance, DetectedLayers))
			{
				parameters[0] = this.gameObject;
				parameters[1] = hitInfo.collider.gameObject;
				Target.SendMessage(Function, parameters, SendMessageOptions.DontRequireReceiver);
			}
			currentAngle -= subAngle;
		}
	}
}
