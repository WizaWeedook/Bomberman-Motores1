using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SupervisorJuego : MonoBehaviour
{
    public GameObject[] players;

    public Text[] textosEstadoJugadores;
    private void Start()
    {
        ActualizarEstadosJugadores();
    }

    public void checkeoWin()
    {
        int jugadoresVivos = 0;
        foreach (GameObject player in players)
        {
            if (player.activeSelf)
            {
                jugadoresVivos++;
            }
        }

        ActualizarEstadosJugadores();

        if (jugadoresVivos <= 1)
        {
            Invoke(nameof(NuevaRonda), 3f);
        }
    }

    private void NuevaRonda()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ActualizarEstadosJugadores()
    {
        for (int i = 0; i < players.Length && i < textosEstadoJugadores.Length; i++)
        {
            textosEstadoJugadores[i].text = players[i].activeSelf ? "VIVO" : "MUERTO";
            textosEstadoJugadores[i].color = players[i].activeSelf ? Color.green : Color.red;
        }
    }
}
