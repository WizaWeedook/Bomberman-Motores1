using UnityEngine;

public class Explosion : MonoBehaviour
{
    public RenderizadoAnimacionSprites start;

    public RenderizadoAnimacionSprites middle;
    public RenderizadoAnimacionSprites end;

    public void SetActiveRenderer(RenderizadoAnimacionSprites renderizador)
    {
        start.enabled = renderizador == start;
        middle.enabled = renderizador == middle;
        end.enabled = renderizador == end;
    }

    public void SetDirection(Vector2 direction)
    {

        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);

    }
    
}
