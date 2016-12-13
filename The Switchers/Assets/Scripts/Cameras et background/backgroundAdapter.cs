using UnityEngine;
using System.Collections;

public class backgroundAdapter : MonoBehaviour {

    // garder le ratio d'origine de l'image
    public bool KeepAspectRatio = true;
    // adapter la taille de l'image a chaque Update
    public bool ExecuteOnUpdate = true;
    public bool render = true;

    void Start()
    {
        if(render)
            Resize(KeepAspectRatio);
    }

    void FixedUpdate()
    {
        if (ExecuteOnUpdate && render)
            Resize(KeepAspectRatio);
    }

    // Permet d'adapter l'image a la taille de la camera.
    void Resize(bool keepAspect = false)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 imgScale = new Vector3(1f, 1f, 1f);

        if (keepAspect)
        {
            Vector2 ratio = new Vector2(width / height, height / width);
            if ((worldScreenWidth / width) > (worldScreenHeight / height))
            {
                // plus large que haut
                imgScale.x = worldScreenWidth / width;
                imgScale.y = imgScale.x * ratio.y;
            }
            else
            {
                // plus haut que large
                imgScale.y = worldScreenHeight / height;
                imgScale.x = imgScale.y * ratio.x;
            }
        }
        else
        {
            imgScale.x = worldScreenWidth / width;
            imgScale.y = worldScreenHeight / height;
        }

        // appliquer les changements de taille
        transform.localScale = imgScale;
    }

    // permet d'activer ou non le background en fonction du mode de vue du joueur.
    public void changeUnivers(bool isSpirit)
    {
        gameObject.SetActive(transform.CompareTag("SpiritBackground") == isSpirit);
    }
}