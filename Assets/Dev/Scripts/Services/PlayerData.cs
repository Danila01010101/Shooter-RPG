using UnityEngine;


[CreateAssetMenu (menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
	public GameObject PlayerPrefab;
    public float speed = 10;
    public float gravity = -20f;
    public float _jumpHeight = 2;
    public float runMultiplier = 2;
    [Range(0f, 90f)] public float jumpSlopeLimit = 30f;
    [Range(0f, 90f)] public float originalSlopeLimit = 30f;

    public float jumpMultiplier => Mathf.Sqrt(_jumpHeight * -2f * gravity);

    [Header("Mouse look")]  
    public float mouseSensitivity;
    public float minLookAngle = -85f;
    public float maxLookAngle = 75f;
}