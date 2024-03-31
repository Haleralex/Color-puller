using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointsDebug : MonoBehaviour
{
    public int hp = 100;
    public MeshRenderer renderer;
    public Color testColor;
    private int _defaultHP;
    private Material CurMaterial;
    public Color CurrentColor => CurMaterial.GetColor(("_BaseColor"));
    public Mesh CurrentMesh { get; set; }
    public Color TestColor
    {
        get => testColor;
        set => testColor = value;
    }

    private void Awake()
    {
        _defaultHP = hp;
        CurrentMesh = GetComponent<MeshFilter>().mesh;
        CurMaterial = renderer.material;
    }

    private void Update()
    {
        hp = Mathf.Max(0, hp);
        var color = new Color(hp  * testColor.r /_defaultHP, hp * testColor.g/_defaultHP, hp * testColor.b/_defaultHP, hp /_defaultHP);
        CurMaterial.SetColor("_BaseColor", color);
        /*CurMaterial.SetFloat("_Radius",(float)(_defaultHP-hp)/500);*/
    }

    public void SetSpherePosition(Vector3 pos)
    {
        /*CurMaterial.SetVector("_Center",pos);*/
    }
}