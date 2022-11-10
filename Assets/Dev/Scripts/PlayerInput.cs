using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	[SerializeField] private Weapon _weapon;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _weapon.Shoot();
        }
    }
}