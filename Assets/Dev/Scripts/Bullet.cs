using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    public void Init()
    {

    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }
}