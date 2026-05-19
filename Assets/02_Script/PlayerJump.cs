using System;
using TMPro;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Animator animator;
    public bool isGrounded;
    public int point = 0;
    public int vidas = 3;

    public TMP_Text puntuacion;
    public GameObject menuPrincipal;
    public GameObject gameOver;

    public AudioSource hurt;

    public AudioSource menu;
    public AudioSource game;
    public AudioSource end;

    void Start()
    {
        Time.timeScale = 0;
        mostrarMenuPrincipal();
        UIManager.instance.MostarMensaje("Vidas: " + vidas, 1);
        UIManager.instance.MostarMensaje("puntos: " + point, 2);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin"))
        {
            point++;
            UIManager.instance.MostarMensaje("puntos: " + point, 2);
            Destroy(other.gameObject); // Destruye la moneda para que desaparezca al cogerla
        }

        if (other.CompareTag("enemy"))
        {
            hurt.Play();
            vidas--;
            UIManager.instance.MostarMensaje("Vidas: " + vidas, 1);
            
            if (vidas <= 0)
            {
                Time.timeScale = 0;  // Detiene el juego
                mostrarGameOver();
            }
        }
    }
    public void mostrarMenuPrincipal()
    {
        Time.timeScale = 0;
        menuPrincipal.SetActive(true);
        gameOver.SetActive(false);
        menu.Play();
        game.Stop();
        end.Stop();
    }
    public void mostrarGameOver()
    {
        Time.timeScale = 0;
        menuPrincipal.SetActive(false);
        gameOver.SetActive(true);
        puntuacion.text = "Puntuación Final: " + point.ToString();
        end.Play();
        menu.Stop();
        game.Stop();
    }
    public void EmpezarJuego()
    {
        point = 0;
        vidas = 3;
        Time.timeScale = 1; // Reanuda el juego
        menuPrincipal.SetActive(false);
        gameOver.SetActive(false);
        UIManager.instance.MostarMensaje("Vidas: " + vidas, 1);
        UIManager.instance.MostarMensaje("puntos: " + point, 2);
        GameObject.FindWithTag("GameController").GetComponent<spawnEnemy>().speed = 2.0f;
        UIManager.instance.tiempoTranscurrido = 0f; // Reinicia el cronómetro
        game.Play();
        menu.Stop(); 
        end.Stop();
    }
    public void SalirJuego()
    {
        Application.Quit();
    }
}
