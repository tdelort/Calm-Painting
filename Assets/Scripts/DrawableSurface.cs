using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawableSurface : MonoBehaviour
{
    [SerializeField]
    private Shader _drawShader;

    [SerializeField]
    private Texture2D _brushTex;

    private RenderTexture _paintTex;
    private Material _mat, _drawMat;

    [SerializeField]
    private float startSize = 8f;

    private void Awake() 
    {

        _drawMat = new Material(_drawShader);
        _drawMat.SetColor("_Color", Color.black);
        _drawMat.SetFloat("_SizeX", startSize * transform.localScale.x);
        _drawMat.SetFloat("_SizeY", startSize * transform.localScale.z);
        _drawMat.SetFloat("_Strength", 0.3f);
        _drawMat.SetTexture("_MainTex", Texture2D.whiteTexture);
        _drawMat.SetTexture("_BrushTex", _brushTex);

        _mat = GetComponent<MeshRenderer>().material;
        _paintTex = new RenderTexture(1024, 1024, 0);
        _paintTex.Create();
        _mat.SetTexture("_MainTex", _paintTex);
        
    }

    public void OnHit(Vector2 texCoord)
    {
        Debug.Log("Hit at " + texCoord);
        _drawMat.SetVector("_Coordinates", new Vector4(texCoord.x, texCoord.y, 0, 0));
        RenderTexture tmp = RenderTexture.GetTemporary(_paintTex.width, _paintTex.height, 0);
        Graphics.Blit(_paintTex, tmp);
        Graphics.Blit(tmp, _paintTex, _drawMat);
        RenderTexture.ReleaseTemporary(tmp);
    }
    


    //Setters
    public void SetSize(float newSize)
    {
        _drawMat.SetFloat("_SizeX", newSize * transform.localScale.x);
        _drawMat.SetFloat("_SizeY", newSize * transform.localScale.y);
    }

    public void SetStrength(float newStrength)
    {
        _drawMat.SetFloat("_Strength", newStrength);
    }

    public void SetColor(Color newColor)
    {
        _drawMat.SetColor("_Color", newColor);
    }

    public void SetBrushTexture(Texture2D newBrushTexture)
    {
        _drawMat.SetTexture("_BrushTex", newBrushTexture);
    }
}
