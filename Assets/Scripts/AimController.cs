using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class AimController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera defaultCamera;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private StarterAssetsInputs input;
    [SerializeField] private Camera realCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Animator animator;
    [SerializeField] private VisualEffect visualEffect;
    [SerializeField] private HandIK handIK;
    [SerializeField] private Character player;
    public PowerHolder PowerHolder;
    
    private float _aimBlend = 0;
    private int _hashAimProperty;

    private const string VFXPositionPostfix = "_position";
    private const string VFXRotationPostfix = "_angles";
    private const string VFXScalePostfix = "_scale";

    private void Awake()
    {
        _hashAimProperty = Animator.StringToHash("Aim");
    }

    void Update()
    {
        Aim();
    }


    private static void SetVFXTransformProperty(VisualEffect visualEffect, string propertyName, Transform transform)
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
        var xCenter = Screen.width / 2;
        var yCenter = Screen.height / 2;
        
        if (input.attack)
        {
            var ray = realCamera.ScreenPointToRay(new Vector3(xCenter, yCenter, 0));
            player.Attack(new AttackInstruction()
            {
                direction = ray.direction
            });
        }
        if (!input.aim)
        {
            _aimBlend = Mathf.Lerp(_aimBlend, 0, 10 * Time.deltaTime);
            if (_aimBlend < 0.01f) _aimBlend = 0f;

            animator.SetFloat(_hashAimProperty, _aimBlend);
            animator.SetLayerWeight(1, _aimBlend);
            handIK.weight = _aimBlend;
            visualEffect.SetInt("HP", 0);

            defaultCamera.gameObject.SetActive(true);
            return;
        }

        
        var centerPos = realCamera.ScreenPointToRay(new Vector3(xCenter, yCenter, 0));

        _aimBlend = Mathf.Lerp(_aimBlend, 1, 10 * Time.deltaTime);
        if (_aimBlend < 0.01f) _aimBlend = 0f;

        animator.SetFloat(_hashAimProperty, _aimBlend);
        animator.SetLayerWeight(1, _aimBlend);
        handIK.weight = _aimBlend;
        if (Physics.Raycast(centerPos, out var hit, Mathf.Infinity,
                layerMask))
        {
            if (!hit.collider.gameObject.TryGetComponent(out HitPointsDebug hpDebug))
                return;
            
            if (hpDebug.hp < 0)
                return;
            
            visualEffect.SetInt("HP", hpDebug.hp);
            SetVisualEffectCondition(hpDebug);
            hpDebug.hp--;
            PowerHolder.ChangePower(new PowerColorsChange()
            {
                PowerColors =hpDebug.PowerColors,
                NewValue =PowerHolder[hpDebug.PowerColors]+1,
                Value = PowerHolder[hpDebug.PowerColors]
            });
        }
        else
        {
            visualEffect.SetInt("HP", 0);
        }

        defaultCamera.gameObject.SetActive(false);
    }

    private void SetVisualEffectCondition(HitPointsDebug hpDebug)
    {
        
        SetVFXTransformProperty(visualEffect, "Base", hpDebug.transform);
        visualEffect.SetVector4("BaseColor", hpDebug.CurrentColor);
        visualEffect.SetMesh("TargetMesh", hpDebug.CurrentMesh);
    }
}