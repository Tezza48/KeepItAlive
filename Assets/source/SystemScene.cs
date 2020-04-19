using UnityEngine;

public class SystemScene : MonoBehaviour
{
    public Star star;
    public Player player;
    new public Camera camera;
    
    struct PlayerInvGui
    {
        public bool isShowing;
        public Rect windowRect;
    }

    PlayerInvGui playerInvGui = new PlayerInvGui {
        isShowing = false
    };

    // Start is called before the first frame update
    void Start()
    {
        playerInvGui.windowRect.width = 200;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            playerInvGui.isShowing = true;
        }

        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, star.transform.localScale.x * 3, Time.deltaTime);

        camera.transform.position = new Vector3(0, 0, -star.transform.localScale.x);
    }

    private void OnGUI()
    {
        if (playerInvGui.isShowing)
        {
            playerInvGui.windowRect.x = Screen.width - playerInvGui.windowRect.width;
            playerInvGui.windowRect = GUILayout.Window(0, playerInvGui.windowRect, WindowFunction, "Inventory");
        }
    }

    public void WindowFunction(int windowId)
    {
        GUILayout.BeginVertical();

        ElementInventory deltas = new ElementInventory();

        foreach (var elem in player.inventory)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(elem.Key.ToString() + " " + ((int)elem.Key).ToString());
            GUILayout.Label(elem.Value.ToString());

            if (GUILayout.Button("Donate"))
            {
                deltas[elem.Key] = elem.Value;
            }
            GUILayout.EndHorizontal();
        }

        foreach(var delta in deltas)
        {
            player.inventory[delta.Key] = 0;

            star.AddElement(delta.Key, delta.Value);
        }

        GUILayout.EndVertical();

        if (GUILayout.Button("Close"))
        {
            playerInvGui.isShowing = false;
        }
    }
}
