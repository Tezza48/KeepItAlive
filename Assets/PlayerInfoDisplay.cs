using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoDisplay : MonoBehaviour
{
    public Player player;

    public float healthbarHeight;
    public RectTransform healthbar;
    public RectTransform healthBackground;

    // Start is called before the first frame update
    void Start()
    {
        healthBackground.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, healthbarHeight);
        player.onHealthUpdated += handlePlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void handlePlayerHealth(float current, float max)
    {
        Debug.Log("PlayerInfoDisplay");

        var percent = current / max;
        healthbar.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, percent * healthbarHeight);
    }
}
