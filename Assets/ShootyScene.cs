using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyScene : MonoBehaviour
{
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        // ! This should be initialized in a loading state.
        if (!GameModel.IsReady)
        {
            GameModel.Init();
        }

        player.onHealthUpdated += Player_onHealthUpdated;
    }

    public void Save()
    {
        GameModel.Save();
    }

    private void Player_onHealthUpdated(float current, float max)
    {
        Debug.Log("Shooty Scene");

        if (current <= 0)
        {
            Debug.Log("Player Died");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
