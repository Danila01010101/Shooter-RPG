using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Sleeve : MonoBehaviour
{
	[SerializeField] private Rigidbody _rigidbody;

	public void PushOut(Vector3 direction)
    {
        _rigidbody.AddForce(direction, ForceMode.Impulse);
    }
}