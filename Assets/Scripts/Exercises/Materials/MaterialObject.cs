using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public struct TextureRangeValueProps 
{
    [SerializeField]
    public Texture texture;
    [SerializeField]
    [Range(0.0f,1.0f)]
    public float[] value;
}
[Serializable]
public struct TextureValueProps
{
    [SerializeField]
    public Texture texture;
    [SerializeField]
    public float[] value;
}
[Serializable]
public struct TextureColorProps
{
    [SerializeField]
    public Texture texture;
    [SerializeField]
    public Color color;
}
[Serializable]
public struct MaterialProps 
{
    [SerializeField]
    public TextureColorProps m_MainTexture;
    [SerializeField]
    public TextureValueProps m_NormalMap;
    [SerializeField]
    public TextureRangeValueProps m_Metallic, m_HeightMap, m_Occlusion;
}
[CreateAssetMenu(fileName = "Material Object", menuName = "Material Object/Material Object Template", order = 1)]
public class MaterialObject : ScriptableObject
{
    public string name;
    public MaterialProps materialProp;
    public Material material;
}
