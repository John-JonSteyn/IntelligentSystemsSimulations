using UnityEngine;

public class BoneFollower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private HumanBodyBones _targetBone = HumanBodyBones.RightHand;

    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Vector3 _rotationOffset;

    private Transform _boneTransform;
    private MeshRenderer _renderer;

    private void Awake()
    {
        if (_animator == null)
            return;

        _boneTransform = _animator.GetBoneTransform(_targetBone);
        _animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;

        _renderer = GetComponentInChildren<MeshRenderer>();
        if (_renderer == null)
            return;

        gameObject.isStatic = false;
        _renderer.allowOcclusionWhenDynamic = true;
    }

    private void LateUpdate()
    {
        if (_boneTransform == null)
            return;

        var targetPosition = _boneTransform.TransformPoint(_positionOffset);
        var targetRotation = _boneTransform.rotation * Quaternion.Euler(_rotationOffset);
        transform.SetPositionAndRotation(targetPosition, targetRotation);
    }
}
