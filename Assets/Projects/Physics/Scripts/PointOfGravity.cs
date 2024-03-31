using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class PointOfGravity : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera defaultCamera;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Camera realCamera;
    [SerializeField] private StarterAssetsInputs _input;
    [SerializeField] private LayerMask layerMask;
    private List<HitPointsDebug> hitPointsDebugs = new();
    public VisualEffect VisualEffect;
    [SerializeField] private Animator animator;
    private float _aimBlend = 0;

    void Update()
    {
        Aim();
    }

    private const string VFXPositionPostfix = "_position";
    private const string VFXRotationPostfix = "_angles";
    private const string VFXScalePostfix = "_scale";

    public static void SetVFXTransformProperty(VisualEffect visualEffect, string propertyName, Transform transform)
    {
        var position = propertyName + VFXPositionPostfix;
        var angles = propertyName + VFXRotationPostfix;
        var scale = propertyName + VFXScalePostfix;

        visualEffect.SetVector3(position, transform.position);
        visualEffect.SetVector3(angles, transform.eulerAngles);
        visualEffect.SetVector3(scale, transform.localScale);
    }

    private void Aim()
    {
        
        if (!_input.aim)
        {
            _aimBlend = Mathf.Lerp(_aimBlend, 0, 10 * Time.deltaTime);
            if (_aimBlend < 0.01f) _aimBlend = 0f;
            animator.SetFloat(Animator.StringToHash("Aim"), _aimBlend);
            animator.SetLayerWeight(1,_aimBlend);
            VisualEffect.SetInt("HP", 0);
            defaultCamera.gameObject.SetActive(true);
            return;
        }

        var centerPos = realCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        _aimBlend = Mathf.Lerp(_aimBlend, 1, 10 * Time.deltaTime);
        if (_aimBlend < 0.01f) _aimBlend = 0f;
        animator.SetFloat(Animator.StringToHash("Aim"), _aimBlend);
        animator.SetLayerWeight(1,_aimBlend);
        RaycastHit hit;
        if (Physics.Raycast(centerPos, out hit, Mathf.Infinity,
                layerMask))
        {
            if (!hit.collider.gameObject.TryGetComponent(out HitPointsDebug hpDebug))
                return;
            if (hpDebug.hp < 0)
                return;
            VisualEffect.SetInt("HP", hpDebug.hp);
            SetVFXTransformProperty(VisualEffect, "Base", hpDebug.transform);
            VisualEffect.SetVector4("BaseColor", hpDebug.CurrentColor);
            VisualEffect.SetMesh("TargetMesh", hpDebug.CurrentMesh);
            hpDebug.SetSpherePosition(hit.point);
            hpDebug.hp--;
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.Log("Did not Hit");
            VisualEffect.SetInt("HP", 0);
        }

        defaultCamera.gameObject.SetActive(false);
    }
}