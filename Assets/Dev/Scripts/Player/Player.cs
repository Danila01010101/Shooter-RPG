using UnityEngine;

public struct Player
{
	public Transform transform;
	public CharacterController characterController;
	public Camera camera;
	public float xRotation;
	public float originalSlopeLimit;
	public float jumpSlopeLimit;
	public float runMultiplier;
	public float jumpMultiplier;
	public float gravity;
	public float speed;
	public float mouseSensivity;
	public float minLookAngle;
	public float maxLookAngle;
}