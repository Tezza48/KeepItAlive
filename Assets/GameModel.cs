using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public struct GameModel
{
    public static ElementInventory PlayerInventory;
    public static ElementInventory StarInventory;

    private static bool isReady = false;

    public static bool IsReady { get => isReady; }

    private static string Dir { get => Application.persistentDataPath + "\\data\\"; }

    private static string Path { get => Dir + "gameModel.json"; }

    public static void Init()
    {
        if (isReady)
        {
            Debug.LogError("GameModel cannot be initialized multiple times.\n" + System.Environment.StackTrace);
            return;
        }

        ElementMakeup[] player;
        ElementMakeup[] star;

        // Try load from persistent data path
        if (File.Exists(Path))
        {
            var data = File.ReadAllText(Path);

            SGameModel model = JsonUtility.FromJson<SGameModel>(data);

            player = model.player;
            star = model.star;
        } else
        {
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
            }

            // Set to default values (No saved game)
            player = new ElementMakeup[]
            {
            new ElementMakeup { element = Element.Hydrogen, amount = 500 },
            new ElementMakeup { element = Element.Lithium, amount = 50 },
            };

            star = new ElementMakeup[]
            {
                new ElementMakeup { element = Element.Hydrogen, amount = 500 },
                new ElementMakeup { element = Element.Helium, amount = 200 },
            };
        }

        PlayerInventory = new ElementInventory(player);
        StarInventory = new ElementInventory(star);

        isReady = true;
    }

    public static void Save()
    {
        var player = new List<ElementMakeup>();
        var star = new List<ElementMakeup>();

        foreach (var elem in PlayerInventory)
        {
            player.Add(new ElementMakeup
            {
                element = elem.Key,
                amount = elem.Value,
            });
        }

        foreach(var elem in StarInventory)
        {
            star.Add(new ElementMakeup
            {
                element = elem.Key,
                amount = elem.Value,
            });
        }

        var model = new SGameModel
        {
            player = player.ToArray(),
            star = star.ToArray(),
        };

        var data = JsonUtility.ToJson(model);
        File.WriteAllText(Path, data);
    }
}

struct SGameModel
{
    public ElementMakeup[] player;
    public ElementMakeup[] star;
}
