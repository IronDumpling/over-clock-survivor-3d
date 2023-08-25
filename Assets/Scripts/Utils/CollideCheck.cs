using UnityEngine;
using UnityEngine.InputSystem.HID;

public class CollideCheck
{
    private Transform _transform;
    private Collider _collider;

    #region Length
    private float _checkRange;
    private float _xRayLength;
    private float _yRayLength;
    #endregion

    #region Positions
    private Vector3 _top;
    private Vector3 _center;
    private Vector3 _bottom;
    #endregion

    #region Directions
    private Vector3 _forward;
    // Get the moving direction
    #endregion

    private Vector3 _closestPoint;
    public Vector3 ClosestPoint => _closestPoint;

    public CollideCheck(Transform transform, float checkRange)
    {
        _transform = transform;
        _checkRange = checkRange;

        _collider = transform.gameObject.GetComponent<Collider>();

        _xRayLength = _collider.bounds.size.x / 2 + _checkRange;
        _yRayLength = _collider.bounds.size.y / 2 + _checkRange;

        _top = new Vector3(_collider.bounds.center.x, _collider.bounds.max.y, _collider.bounds.center.z);
        _center = _collider.bounds.center;
        _bottom = new Vector3(_collider.bounds.center.x, _collider.bounds.min.y, _collider.bounds.center.z);

        _forward = transform.forward;
    }

    public void Update()
    {
        _top = new Vector3(_collider.bounds.center.x, _collider.bounds.max.y, _collider.bounds.center.z);
        _center = _collider.bounds.center;
        _bottom = new Vector3(_collider.bounds.center.x, _collider.bounds.min.y, _collider.bounds.center.z);
        _forward = _transform.forward;
    }

    public bool Ground()
    {
        Collider[] colliders = Physics.OverlapBox(_bottom, new Vector3(_checkRange, 0.05f, _checkRange));

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != _transform.gameObject)
            {
                _closestPoint = collider.ClosestPoint(_transform.position);
                return true;
            }
        }

        return false;
    }

    public bool Ceil()
    {
        Collider[] colliders = Physics.OverlapBox(_top, new Vector3(_checkRange, -0.05f, _checkRange));

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != _transform.gameObject)
            {
                _closestPoint = collider.ClosestPoint(_transform.position);
                return true;
            }
        }

        return false;
    }

    public bool Obstacle()
    {
        if (Physics.Raycast(_center, _forward, out RaycastHit hit, _xRayLength))
        {
            if (hit.collider.gameObject != _transform.gameObject)
            {
                return true;
            }

            _closestPoint = hit.collider.ClosestPoint(_transform.position);
        }

        return false;
    }

    public bool Obstacle(Vector3 direction)
    {
        if (Physics.Raycast(_center, direction, out RaycastHit hit, _xRayLength))
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
        if (!Physics.Raycast(_center, _forward, out RaycastHit _, _xRayLength))
        {
            Vector3 endPoint = _center + _forward * _xRayLength;
            if (!Physics.Raycast(endPoint, Vector3.down, out RaycastHit _, _yRayLength))
            {
                return true;
            }
        }

        return false;
    }

    public bool Edge(Vector3 direction)
    {
        if (!Physics.Raycast(_center, direction, out RaycastHit _, _xRayLength))
        {
            Vector3 endPoint = _center + direction * _xRayLength;
            if (!Physics.Raycast(endPoint, Vector3.down, out RaycastHit _, _yRayLength))
            {
                return true;
            }
        }

        return false;
    }
}
