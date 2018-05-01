using UnityEngine;

public class ShipController : MonoBehaviour {

	public float speedScaler;
	public float distanceFromSurface;

	private Vector3 movementVector;
	private Transform shipTransform;
	private Camera mainCamera;
	private Vector3 cursorPointInWorldSpace;
	private Vector3 mouseScreenPosition;
	private Transform rotationRootTransform;
	private Rigidbody shipRigidBody;

	private void Start () {
		shipTransform = transform;
		mainCamera = Camera.main;
		shipRigidBody = GetComponent<Rigidbody>();

		foreach (Transform childTransform in transform) {
			if (childTransform.name.Equals("RotationRoot")) {
				rotationRootTransform = childTransform;
				break;
			}
		}
	}
	
	private void Update () {
		HandleTransformInput();
		HandleHeadPosition();

		Debug.DrawLine(shipTransform.position, cursorPointInWorldSpace, Color.green);
	}

	//private void FixedUpdate() {
	//	HandleRigidBodyTransform();
	//}

	private void HandleTransformInput() {
		movementVector = Vector2.zero;
		movementVector.x = Input.GetAxis("Horizontal");
		movementVector.z = Input.GetAxis("Vertical");

		if (movementVector.x == 0f && movementVector.z == 0f)
			return;

		shipTransform.Translate(movementVector * speedScaler * Time.deltaTime);
	}

	private void HandleHeadPosition() {
		mouseScreenPosition = Input.mousePosition;
		mouseScreenPosition.z = distanceFromSurface;
		cursorPointInWorldSpace = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

		rotationRootTransform.LookAt(cursorPointInWorldSpace);
		//shipTransform.LookAt(cursorPointInWorldSpace);
	}

	//private void HandleRigidBodyTransform() {
	//	shipRigidBody.velocity = movementVector * speedScaler;// * Time.deltaTime;

	//	shipRigidBody.position = new Vector3
	//	(
	//		Mathf.Clamp(shipRigidBody.position.x, boundary.xMin, boundary.xMax),
	//		0.0f,
	//		Mathf.Clamp(shipRigidBody.position.z, boundary.zMin, boundary.zMax)
	//	);
	//}
}
