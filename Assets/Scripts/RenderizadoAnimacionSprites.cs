using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderizadoAnimacionSprites : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    public Sprite idleSprite;
    public Sprite[] AnimationSprites;


    public float tiempoEntreFrames = 0.25f;
    private int frameActual;

    public bool loop = true;
    public bool idle = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }
    
    private void OnDisable()
    {
        spriteRenderer.enabled = false;

    }

    private void Start()
    {
        InvokeRepeating(nameof(NextFrame),tiempoEntreFrames, tiempoEntreFrames);

    }

    private void NextFrame()
    {

        frameActual++;
        if (loop && frameActual >= AnimationSprites.Length)
        {
            frameActual = 0;
        }

        if (idle)
        {
            spriteRenderer.sprite = idleSprite; ;
        }
        else if (frameActual >= 0 && frameActual < AnimationSprites.Length)
        {
            spriteRenderer.sprite = AnimationSprites[frameActual];
        }
    }
}
