using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{

    public string[] sceneNames;

    protected override void OnCollide(Collider2D coll)
    {
        base.OnCollide(coll);
        if (collisionWithPlayer)
        {
            GameManager.instance.SaveState();
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            SceneManager.LoadScene(sceneName);

        }
    }
}
