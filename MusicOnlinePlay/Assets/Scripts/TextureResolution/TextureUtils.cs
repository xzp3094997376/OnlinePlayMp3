﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureUtils 
{   
    // Update is called once per frame
    public static Texture2D ReSetTextureSize(Texture2D tex, int width, int height)
    {
        var rendTex = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
        rendTex.Create();
        Graphics.SetRenderTarget(rendTex);
        GL.PushMatrix();
        GL.Clear(true, true, Color.clear);
        GL.PopMatrix();

        var mat = new Material(Shader.Find("Unlit/Transparent"));
        mat.mainTexture = tex;
        Graphics.SetRenderTarget(rendTex);
        GL.PushMatrix();
        GL.LoadOrtho();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.TexCoord2(0, 0);
        GL.Vertex3(0, 0, 0);
        GL.TexCoord2(0, 1);
        GL.Vertex3(0, 1, 0);
        GL.TexCoord2(1, 1);
        GL.Vertex3(1, 1, 0);
        GL.TexCoord2(1, 0);
        GL.Vertex3(1, 0, 0);
        GL.End();
        GL.PopMatrix();

        Texture2D finalTex = new Texture2D(rendTex.width, rendTex.height, TextureFormat.ARGB32, false);
        RenderTexture.active = rendTex;
        finalTex.ReadPixels(new Rect(0, 0, finalTex.width, finalTex.height), 0, 0);
        finalTex.Apply();
        return finalTex;
    }
}
