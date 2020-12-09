using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    private Image image;
    public Sprite[] spritesIdle;
    public Sprite[] spritesRun;
    private float elapsedTime;
    private bool isOn = false;
    private int currentFrame;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 1.0f / 14.0f)
        {
            if (currentFrame >= 0 && (currentFrame < spritesIdle.Length && !isOn || currentFrame < spritesRun.Length && isOn))
            {
                if (!isOn && currentFrame < spritesIdle.Length)
                {
                    image.sprite = spritesIdle[currentFrame];
                    currentFrame++;
                }
                else if (isOn && currentFrame < spritesRun.Length)
                {
                    image.sprite = spritesRun[currentFrame];
                    currentFrame++;
                }
            }
            else
            {
                currentFrame = 0;
            }
            elapsedTime = 0.0f;
        }
    }
    public void IsOn()
    {
        this.isOn = !this.isOn ;
    }
}
