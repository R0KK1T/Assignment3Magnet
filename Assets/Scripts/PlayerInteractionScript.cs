using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractionScript : IObject
{
    [SerializeField] private ObjectValue[] values;

    [SerializeField] private GameManager gm;

    private Dictionary<ObjectType, int> objectValues;

    protected override ObjectType Type => ObjectType.PLAYER;

    [Space]
    public MagnetScript magnet;
    
    public CanvasGroup starPanel;

    public CanvasGroup canvasGroup;

    AudioSource audioSource;

    public AudioClip coinSound;
    public MagnetScreenInputScript magnetInput;

    private bool invincible = false;

    private void Start()
    {
        objectValues = new Dictionary<ObjectType, int>();
        foreach (var value in values)
        {
            objectValues.Add(value.obj, value.value);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public override void Hit(ObjectType type)
    {
        Debug.Log(type);

        if ((type == ObjectType.MINE && invincible != true)|| (type == ObjectType.SHRAPNEL && invincible != true))
        {
            GameOver();
            return;
        }

        if (type == ObjectType.STAR)
        {
            StarPower();
            return;
        }

        if (!objectValues.ContainsKey(type))
            return;

        gm.ScoreChange(objectValues[type]);
        audioSource.PlayOneShot(coinSound);
    }

    public override void Initiate(Transform player) { }


    void GameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        GetComponent<CapsuleCollider>().enabled = false;

        magnetInput.enabled = false;
        Time.timeScale = 0;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    private void StarPower() 
    {
        if (invincible != true)
        {
            invincible = true;
            starPanel.alpha = 0.6f;
            magnet.maxDistance = 70;
            //Ends star power after a delay
            MagnetPowerEnd(8);
            StarPowerEnd(10);
        }
    }

    private IEnumerator StarPowerEnd(float time)
    {
        yield return new WaitForSeconds(time);
        starPanel.alpha = 0;
        invincible = false;
    }

    private IEnumerator MagnetPowerEnd(float time)
    {
        yield return new WaitForSeconds(time);
        magnet.maxDistance = 30;
    }



}

[System.Serializable]
struct ObjectValue
{
    public ObjectType obj;
    public int value;
}
