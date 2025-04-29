using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rgbMain, rgbDifficulty, rgbReturn;
    private float speed = -600f;

    /**
     * This function is called when the Play button is triggered, it opens the Options Scene
     */
    public void Singleplayer()
    {
        speed = speed * -1;
        StartCoroutine(MenuBounceRight(rgbMain, false));
        StartCoroutine(MenuGoDown(rgbDifficulty, true));
    }

    public void Classic()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    
    public void Return()
    {
        speed = speed * -1;
        StartCoroutine(MenuGoDown(rgbDifficulty, false));
        StartCoroutine(MenuBounceRight(rgbMain, true));
    }

    private IEnumerator MenuBounceRight(Rigidbody2D rgb, bool returnTrue)
    {
        if (returnTrue)
        {
            yield return new WaitForSeconds(0.97f);
        }

        rgb.velocity = new Vector2(-speed * 0.3f, 0);
        yield return new WaitForSeconds(0.1f);

        rgb.velocity = new Vector2(speed, 0);
        yield return new WaitForSeconds(1.3f);

        rgb.velocity = Vector2.zero;

        rgb.velocity = new Vector2(-speed * 0.2f, 0);
        yield return new WaitForSeconds(0.1f);

        rgb.velocity = Vector2.zero;
    }

    private IEnumerator MenuGoDown(Rigidbody2D rgb, bool returnTrue)
    {
        if (returnTrue)
        {
            yield return new WaitForSeconds(1.4f);
        }

        rgb.velocity = new Vector2(0, speed * 0.3f);
        rgbReturn.velocity = new Vector2(0, speed * 0.3f);
        yield return new WaitForSeconds(0.1f);

        rgb.velocity = new Vector2(0, -speed);
        rgbReturn.velocity = new Vector2(0, -speed);
        yield return new WaitForSeconds(1.14f);

        rgb.velocity = Vector2.zero;
        rgbReturn.velocity = Vector2.zero;

        rgb.velocity = new Vector2(0, speed * 0.2f);
        rgbReturn.velocity = new Vector2(0, speed * 0.2f);
        yield return new WaitForSeconds(0.1f);

        rgb.velocity = Vector2.zero;
        rgbReturn.velocity = Vector2.zero;
    }
    /**
     * This function is called when the Exit button is triggered
     */
    public void Salir(){

        Debug.Log("Leaving...");
        Application.Quit();
    }
}
