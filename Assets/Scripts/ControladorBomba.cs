using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;


public class ControladorBomba : MonoBehaviour
{

    [Header("bomba")]
    public KeyCode teclaBomba = KeyCode.Space;
    public GameObject prefabBomba;
    public float TiempoExplosionBomba = 3f;
    public int maxBombas = 1;
    private int bombasRestantes;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask LayerExplosion;
    public float duracionExplosion = 1f;
    public int radioExplosion = 1;

    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public Destructible destructiblePrefab;


    private void OnEnable()
    {
        bombasRestantes = maxBombas;

    }

    private void Update()
    {
        if (Input.GetKeyDown(teclaBomba) && bombasRestantes > 0)
        {
            StartCoroutine(CrearBomba());
        }
    }

    private IEnumerator CrearBomba()
    {
        Vector2 posicion = transform.position;
        posicion.x = Mathf.Round(posicion.x);
        posicion.y = Mathf.Round(posicion.y);

        GameObject bomba = Instantiate(prefabBomba, posicion, Quaternion.identity);
        bombasRestantes--;

        yield return new WaitForSeconds(TiempoExplosionBomba);

        posicion = bomba.transform.position;
        posicion.x = Mathf.Round(posicion.x);
        posicion.y = Mathf.Round(posicion.y);

        Explosion explosion = Instantiate(explosionPrefab, posicion, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);

        Destroy(explosion.gameObject, duracionExplosion);
        Explotar(posicion, Vector2.up, radioExplosion);
        Explotar(posicion, Vector2.down, radioExplosion);
        Explotar(posicion, Vector2.left, radioExplosion);
        Explotar(posicion, Vector2.right, radioExplosion);

        Destroy(bomba);
        bombasRestantes++;
    }

    private void OnTriggerExit2D(Collider2D Otro)
    {
        if (Otro.gameObject.layer == LayerMask.NameToLayer("Bomba"))
        {
            Otro.isTrigger = false;
        }
    }

    private void Explotar(Vector2 posicion, Vector2 direccion, int largo)
    {
        if (largo <= 0) return;

        posicion += direccion;


        if (Physics2D.OverlapBox(posicion, Vector2.one / 2f, 0f, LayerExplosion))
        {
            limpiarDestructibles(posicion);
            return;
        }

        Explosion explosion = Instantiate(explosionPrefab, posicion, Quaternion.identity);
        explosion.SetActiveRenderer(largo > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direccion);
        Destroy(explosion.gameObject, duracionExplosion);

        Explotar(posicion, direccion, largo - 1);
    }
    
    private void limpiarDestructibles(Vector2 posicion)
    {
        Vector3Int celdaPosicion = destructibleTiles.WorldToCell(posicion);
        TileBase tile = destructibleTiles.GetTile(celdaPosicion);
        if (tile != null)
        {
            Instantiate(destructiblePrefab, posicion, Quaternion.identity);
            destructibleTiles.SetTile(celdaPosicion, null);
        }
    }

}
