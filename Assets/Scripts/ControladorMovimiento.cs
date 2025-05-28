using System;
using Unity.VisualScripting;
using UnityEngine;

public class ControladorMovimiento : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    private Vector2 direccion = Vector2.down;
    public float velocidad = 5f;

    public KeyCode teclaArriba = KeyCode.W;
    public KeyCode teclaAbajo = KeyCode.S;
    public KeyCode teclaIzquierda = KeyCode.A;
    public KeyCode teclaDerecha = KeyCode.D;


    public RenderizadoAnimacionSprites renderSpritesArriba;
    public RenderizadoAnimacionSprites renderSpritesAbajo;
    public RenderizadoAnimacionSprites renderSpritesIzquierda;
    public RenderizadoAnimacionSprites renderSpritesDerecha;

    public RenderizadoAnimacionSprites renderSpritesMuerte;
    private RenderizadoAnimacionSprites renderSpriteActivo;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        renderSpriteActivo = renderSpritesAbajo; // Inicializar con el sprite de abajo
    }

    private void Update()
    {

        if (Input.GetKey(teclaArriba))
        {
            SetearDireccion(Vector2.up, renderSpritesArriba);
        }
        else if (Input.GetKey(teclaAbajo))
        {
            SetearDireccion(Vector2.down, renderSpritesAbajo);
        }
        else if (Input.GetKey(teclaIzquierda))
        {
            SetearDireccion(Vector2.left, renderSpritesIzquierda);
        }
        else if (Input.GetKey(teclaDerecha))
        {
            SetearDireccion(Vector2.right, renderSpritesDerecha);
        }
        else
        {
            SetearDireccion(Vector2.zero, renderSpriteActivo);
        }

    }


    private void FixedUpdate()
    {
        Vector2 posicion = rb.position;
        Vector2 transladacion = direccion * velocidad * Time.fixedDeltaTime;

        rb.MovePosition(posicion + transladacion * velocidad * Time.fixedDeltaTime);
    }
    private void SetearDireccion(Vector2 nuevaDireccion, RenderizadoAnimacionSprites RenderSprite)
    {
        direccion = nuevaDireccion;

        renderSpritesAbajo.enabled = RenderSprite == renderSpritesAbajo;
        renderSpritesArriba.enabled = RenderSprite == renderSpritesArriba;
        renderSpritesIzquierda.enabled = RenderSprite == renderSpritesIzquierda;
        renderSpritesDerecha.enabled = RenderSprite == renderSpritesDerecha;


        renderSpriteActivo = RenderSprite;
        renderSpriteActivo.idle = direccion == Vector2.zero;

    }


    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            SecuenciaMuerte();
        }
    }

    private void SecuenciaMuerte()
    {
        enabled = false;
        GetComponent<ControladorBomba>().enabled = false;

        renderSpritesAbajo.enabled = false;
        renderSpritesArriba.enabled = false;
        renderSpritesIzquierda.enabled = false;
        renderSpritesDerecha.enabled = false;
        renderSpritesMuerte.enabled = true;


        Invoke(nameof(OnMuerteSecuencia), 1f);
    }

    private void OnMuerteSecuencia()
    {
        gameObject.SetActive(false);
        FindObjectOfType<SupervisorJuego>().checkeoWin();
    }


}
