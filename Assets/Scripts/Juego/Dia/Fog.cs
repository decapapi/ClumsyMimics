using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public Texture2D fogTexture;
    public SpriteMask spriteMask;

    private Vector2 worldScale;
    private Vector2Int pixelScale;
    public void Awake()
    {
        pixelScale.x = fogTexture.width;
        pixelScale.y = fogTexture.height;
        worldScale.x = pixelScale.x / 100f * transform.localScale.x;
        worldScale.y = pixelScale.y / 100f * transform.localScale.y;
        for (int i = 0; i < pixelScale.x; i++)
        {
            for (int j = 0; j < pixelScale.y; j++)
            {
                fogTexture.SetPixel(i, j, Color.clear);
            }
        }
    }
    private Vector2Int WorldToPixel(Vector2 position)
    {
        Vector2Int pixelPos = Vector2Int.zero;
        float dx = position.x - transform.position.x;
        float dy = position.y - transform.position.y;

        pixelPos.x = Mathf.RoundToInt(.5f * pixelScale.x + dx * worldScale.x * (pixelScale.x / worldScale.x));
        pixelPos.y = Mathf.RoundToInt(.5f * pixelScale.y + dy * worldScale.y * (pixelScale.y / worldScale.y));
        return pixelPos;
    }

    public void MakeHole (Vector2 position, float holeRadius)
    {
        Vector2Int pixelPos = WorldToPixel(position);
        int rad = Mathf.RoundToInt(holeRadius * pixelScale.x / worldScale.x);
        int px, nx, py, ny, distance;
        for (int i = 0; i < rad; i++)
        {
            distance = Mathf.RoundToInt(Mathf.Sqrt(rad * rad - i * i));
            for (int j = 0; j < distance; j++)
            {
                if (i * i + j * j < rad * rad)
                {
                    px = Mathf.Clamp(pixelPos.x + i, 0, pixelScale.x);
                    nx = Mathf.Clamp(pixelPos.x - i, 0, pixelScale.x);
                    py = Mathf.Clamp(pixelPos.y + j, 0, pixelScale.y);
                    ny = Mathf.Clamp(pixelPos.y - j, 0, pixelScale.y);
                    fogTexture.SetPixel(px, py, Color.black);
                    fogTexture.SetPixel(nx, py, Color.black);
                    fogTexture.SetPixel(px, ny, Color.black);
                    fogTexture.SetPixel(nx, ny, Color.black);
                }
            }
        }
        fogTexture.Apply();
        CreateSprite();
    }
    private void CreateSprite()
    {
        spriteMask.sprite = Sprite.Create(fogTexture, new Rect(0, 0, fogTexture.width, fogTexture.height), Vector2.one * .5f, 100f);
    }
}
