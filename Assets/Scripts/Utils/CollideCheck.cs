using UnityEngine;
using UnityEngine.UIElements;

public class CollideCheck
{
    private Transform _transform;
    private float _checkRange;
    private Vector3 _bottom;
    private Vector3 _forward;

    private Vector3 _closestPoint;
    public Vector3 ClosestPoint => _closestPoint;

    public CollideCheck(Transform transform, float checkRange)
    {
        _transform = transform;
        _checkRange = checkRange;
        _bottom = new Vector3(transform.gameObject.GetComponent<CapsuleCollider>().bounds.center.x, transform.gameObject.GetComponent<CapsuleCollider>().bounds.min.y, transform.gameObject.GetComponent<CapsuleCollider>().bounds.center.z);
        _forward = transform.forward;
    }
    public void Update()
    {
        _bottom = new Vector3(_transform.gameObject.GetComponent<CapsuleCollider>().bounds.center.x, _transform.gameObject.GetComponent<CapsuleCollider>().bounds.min.y, _transform.gameObject.GetComponent<CapsuleCollider>().bounds.center.z);
        _forward = _transform.forward;
    }
    public bool Ground()
    {
        Collider[] colliders = Physics.OverlapBox(_bottom,
        new Vector3(_checkRange, 0.05f, _checkRange));

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != _transform.gameObject)
            {
                Debug.Log($"Who are you? {collider.gameObject.name}");
                _closestPoint = collider.ClosestPoint(_transform.position);
                return true;
            }
        }

        return false;
    }

    public bool Obstacle()
    {
        float rayLength = _transform.localScale.y / 2 + _checkRange;
        if (Physics.Raycast(_bottom, _forward, out RaycastHit hit, rayLength))
        {
            if (hit.collider.gameObject != _transform.gameObject)
            {
                return true;
            }

            _closestPoint = hit.collider.ClosestPoint(_transform.position);
        }

        return false;
    }

    public bool Edge()
    {
        if (Physics.Raycast(_bottom, _forward, out RaycastHit hit1, _checkRange))
        {
            Vector3 endPoint = hit1.point + _forward * _checkRange;

            if (!Physics.Raycast(endPoint, Vector3.down, out RaycastHit _, _checkRange))
            {
                return true;
            }
        }

        return false;
    }
}
