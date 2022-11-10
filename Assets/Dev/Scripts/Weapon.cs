using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] private Transform _pointToShootFrom;
	[SerializeField] private Transform _sleeveOutPosition;
	[SerializeField] private Bullet _bullet;
	[SerializeField] private GameObject _sleeve;

	public void Shoot()
    {
		Instantiate(_bullet, _pointToShootFrom.position, _pointToShootFrom.transform.rotation).Init();
		Instantiate(_sleeve, _sleeveOutPosition.position, _pointToShootFrom.transform.rotation);
	}
}