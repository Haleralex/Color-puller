using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class HitPointsDebug : MonoBehaviour
{
    public int hp = 100;
    private MeshRenderer meshRenderer;
    public Color testColor;
    private int defaultHp;
    private Material curMaterial;
    private const string BASE_COLOR_PROPERTY = "_BaseColor";
    private static readonly int BaseColor = Shader.PropertyToID((BASE_COLOR_PROPERTY));
    public Color CurrentColor => curMaterial.GetColor(BaseColor);
    public Mesh CurrentMesh { get; private set; }

    private void Awake()
    {
        defaultHp = hp;
        meshRenderer = GetComponent<MeshRenderer>();
        CurrentMesh = GetComponent<MeshFilter>().mesh;
        curMaterial = meshRenderer.material;
    }

    private void Update()
    {
        hp = Mathf.Max(0, hp);
        var alpha = hp / defaultHp;
        var color = new Color(hp * testColor.r/ defaultHp, hp * testColor.g/ defaultHp, hp * testColor.b/ defaultHp, alpha);
        curMaterial.SetColor(BaseColor, color);
    }
}