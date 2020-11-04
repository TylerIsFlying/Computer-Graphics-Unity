using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RendererProp
{
    [SerializeField]
    public Renderer renderer;
    [HideInInspector]
    public MaterialPropertyBlock _propBlock;
    [SerializeField]
    public string matObjectName;
    [HideInInspector]
    public int matObjIndxID;
    public RendererProp(Renderer renderer, MaterialPropertyBlock _propBlock, string matObjectName, int matObjIndxID) 
    {
        this.renderer = renderer;
        this._propBlock = _propBlock;
        this.matObjectName = matObjectName;
        this.matObjIndxID = matObjIndxID;
    }

    public RendererProp()
    {
    }
}
public class MaterialApply : MonoBehaviour
{

    public List<RendererProp> renderersProp;
    public List<MaterialObject> materialObjects;
    private void Awake()
    {
        // setting for all the default ones with a material will get updated its id for the mat
        for(int i = 0; i < renderersProp.Count; i++) 
        {
            renderersProp[i].matObjIndxID = FindMatObject(renderersProp[i].matObjectName);
            renderersProp[i].renderer.material = materialObjects[renderersProp[i].matObjIndxID].material;
        }
    }
    public void AddRenderer(Renderer renderer, string matObjectName)
    {
        RendererProp temp = new RendererProp(renderer, new MaterialPropertyBlock(), matObjectName, FindMatObject(matObjectName));
        renderer.material = materialObjects[temp.matObjIndxID].material;
        renderersProp.Add(temp);
    }
    public void RemoveRenderer(Renderer renderer, string matObjectName)
    {
        foreach(RendererProp rp in renderersProp) 
        {
            if(rp.renderer == renderer && rp.matObjectName == matObjectName) 
            {
                renderersProp.Remove(rp);
                break;
            }
        }
    }
    public void SetRenderer(Renderer renderer, string matObjectName)
    {
        foreach (RendererProp rp in renderersProp)
        {
            if (rp.renderer == renderer)
            {
                rp.matObjectName = matObjectName;
                rp.matObjIndxID = FindMatObject(matObjectName);
                break;
            }
        }
    }
    public bool isAlreadyIn(Renderer renderer, string matObjectName)
    {
        foreach (RendererProp rp in renderersProp)
        {
            if (rp.renderer == renderer && rp.matObjectName == matObjectName)
            {
                return true;
            }
        }
        return false;
    }
    public bool isAlreadyIn(Renderer renderer)
    {
        foreach (RendererProp rp in renderersProp)
        {
            if (rp.renderer == renderer)
            {
                return true;
            }
        }
        return false;
    }
    // Finds the material object via name
    private int FindMatObject(string matObjectName) 
    {
        for(int i = 0; i < materialObjects.Count; i++) 
        {
            if(materialObjects[i].name == matObjectName) 
            {
                return i;
            }
        }
        return -1;
    }
    public void SetPropBlock(Renderer _renderer, MaterialPropertyBlock _propBlock, int matObjIndxID)
    {
        // checking to see if its null if it is send a message telling it that you don't have a renderer
        if (_renderer == null) { Debug.Log("Forgot to put a renderer.");  return; }
        // if it cant find the material object display a error message
        if(matObjIndxID == -1) { Debug.Log("Material Object not found!"); return; }
        // setting the propBlock if its null
        if (_propBlock == null) _propBlock = new MaterialPropertyBlock();
        // gets the property block from the renderer
        _renderer.GetPropertyBlock(_propBlock);
        // Main Texture 
        _propBlock.SetTexture("_MainTex", materialObjects[matObjIndxID].materialProp.m_MainTexture.texture);
        _propBlock.SetColor("_Color", materialObjects[matObjIndxID].materialProp.m_MainTexture.color);
        // Normal Texture
        _propBlock.SetTexture("_BumpMap", materialObjects[matObjIndxID].materialProp.m_NormalMap.texture);
        _propBlock.SetFloat("_BumpScale", materialObjects[matObjIndxID].materialProp.m_NormalMap.value[0]);
        // Metallic Texture
        _propBlock.SetTexture("_MetallicGlossMap", materialObjects[matObjIndxID].materialProp.m_Metallic.texture);
        _propBlock.SetFloat("_Metallic", materialObjects[matObjIndxID].materialProp.m_Metallic.value[0]);
        _propBlock.SetFloat("_Glossiness", materialObjects[matObjIndxID].materialProp.m_Metallic.value[1]);
        // Height Map Texture
        _propBlock.SetTexture("_ParallaxMap", materialObjects[matObjIndxID].materialProp.m_HeightMap.texture);
        _propBlock.SetFloat("_Parallax", materialObjects[matObjIndxID].materialProp.m_HeightMap.value[0]);
        // Occlusion Texture
        _propBlock.SetTexture("_OcclusionMap", materialObjects[matObjIndxID].materialProp.m_Occlusion.texture);
        _propBlock.SetFloat("_OcclusionStrength", materialObjects[matObjIndxID].materialProp.m_Occlusion.value[0]);
        // set the property block
        _renderer.SetPropertyBlock(_propBlock);
    }
    private void Update()
    {
        foreach(RendererProp rp in renderersProp) 
        {
            SetPropBlock(rp.renderer, rp._propBlock, rp.matObjIndxID);
        }
    }
}
