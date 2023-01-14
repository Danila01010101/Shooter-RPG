using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] private Transform _pointToShootFrom;
	[SerializeField] private Transform _gunEndPoint;
	[SerializeField] private Transform _sleeveOutPosition;
	[SerializeField] private Bullet _bullet;
	[SerializeField] private GameObject _sleeve;
	[SerializeField] private GameObject _trail;
	[SerializeField] private Vector3 _sleevePushDirection = new Vector3(1, 1, 0);
    [SerializeField] private GameObject _dustEffect;
    [SerializeField] private LayerMask _target;

    public void Shoot(Vector3 shootPosition, Vector3 shootDirection)
    {
        RaycastHit hit;

		if (Physics.Raycast(shootPosition, shootDirection, out hit, Mathf.Infinity, _target.value))
		{
			Instantiate(_dustEffect, hit.point, Quaternion.FromToRotation(hit.point, shootPosition));
        }
        TrailRenderer trail = Instantiate(_trail, _gunEndPoint.transform.position, _trail.transform.rotation).GetComponent<TrailRenderer>();
        StartCoroutine(SpawnTrail(trail, hit));

  //      if (Vector3.Distance(_gunEndPoint.transform.position, hit.point) > 1.75f)
		//{
		//	TrailRenderer trail = Instantiate(_trail, _gunEndPoint.transform.position, _trail.transform.rotation).GetComponent<TrailRenderer>();
		//	StartCoroutine(SpawnTrail(trail, hit));
		//}
    }

    public void Shoot()
    {
		Vector3 shootDirection = (_pointToShootFrom.position - _gunEndPoint.position).normalized;

		Shoot(_pointToShootFrom.transform.position, shootDirection);
		//Instantiate(_sleeve, _sleeveOutPosition.position, _pointToShootFrom.transform.rotation).GetComponent<Rigidbody>().AddForce(transform.position + _sleevePushDirection * 4, ForceMode.VelocityChange);
	}

	private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
	{
		float time = 0;

		Vector3 startPosition = trail.transform.position;

		while (time < 1f)
		{
			trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
			time += Time.deltaTime / trail.time;

			yield return null;
		}

		Destroy(trail.gameObject);
	}
}