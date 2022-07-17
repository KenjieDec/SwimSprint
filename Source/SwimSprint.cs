using UltimateWater;
using UnityEngine;

public class SwimSprint : Mod
{

    private const string SName = "<color=#0000FF>Swim Sprint : </color>";
    private const string Error = "<color=#ff0000>Swim Sprint Error : </color>";
    private const string Loaded = "<color=#0000FF>Swim Sprint : </color><color=#ffd900>Swim Sprint Mod has been Loaded!</color>";
    private const string UnLoaded = "<color=#0000FF>Swim Sprint : </color><color#ff0000>Swim Sprint Mod has been UnLoaded!</color>";
    private static bool sprinted;
    private static float theSpeed;
    KeyCode SwimSprinto = KeyCode.RightShift;
    SubmersionState player;
    public void Start()
    {
        Debug.Log(Loaded);
    }

    public void OnModUnload()
    {
        Debug.Log(UnLoaded);
    }
    public void Update()
    {
        if (!LoadSceneManager.IsGameSceneLoaded)
        {
            return;
        }
        if (MyInput.GetButtonDown("Sprint"))
        {
            sprinted = !sprinted;
            if (sprinted)
            {
                if (RAPI.GetLocalPlayer().PersonController.controllerType != ControllerType.Water)
                {
                    return;
                }
                
                float sS = 1f;
                sS *= (RAPI.GetLocalPlayer().PlayerEquipment.GetEquipedIndexes().Contains(63) ? 1.4f : 1f);
                if (sS == 1f)
                {
                    sS = 0f;
                }
                theSpeed = RAPI.GetLocalPlayer().PersonController.swimSpeed;
                if (RAPI.GetLocalPlayer().PersonController.swimSpeed > 10)
                {
                    FindObjectOfType<HNotify>().AddNotification(HNotify.NotificationType.normal, "Swim Sprint : You can't actually Sprint when your swimming speed is more than the sprint itself ;-;", 2, HNotify.ErrorSprite).SetCloseDelay(2).SetTextColor(Color.red);
                    return;
                }else if(RAPI.GetLocalPlayer().PersonController.swimSpeed > 2 && RAPI.GetLocalPlayer().PersonController.swimSpeed < 10)
                {
                    RAPI.GetLocalPlayer().PersonController.swimSpeed = 10 + sS;
                    RAPI.GetLocalPlayer().PersonController.swimSpeed = (2 * 5) + sS;
                    if (GameModeValueManager.GetCurrentGameModeValue().nourishmentVariables.foodDecrementRateMultiplier > 0f) {
                        GameModeValueManager.GetCurrentGameModeValue().nourishmentVariables.foodDecrementRateMultiplier = 1.2f;
                    }
                    if (GameModeValueManager.GetCurrentGameModeValue().nourishmentVariables.thirstDecrementRateMultiplier > 0f) {
                        GameModeValueManager.GetCurrentGameModeValue().nourishmentVariables.thirstDecrementRateMultiplier = 1.2f;
                    }
                    RAPI.GetLocalPlayer().Stats.stat_oxygen.SetOxygenLostPerSecond(1.25f);
                    return;
                }
                RAPI.GetLocalPlayer().PersonController.swimSpeed = 10 + sS; 
                if (GameModeValueManager.GetCurrentGameModeValue().nourishmentVariables.foodDecrementRateMultiplier > 0f) {
                    GameModeValueManager.GetCurrentGameModeValue().nourishmentVariables.foodDecrementRateMultiplier = 1.3f;
                }
                if (GameModeValueManager.GetCurrentGameModeValue().nourishmentVariables.thirstDecrementRateMultiplier > 0f) {
                    GameModeValueManager.GetCurrentGameModeValue().nourishmentVariables.thirstDecrementRateMultiplier = 1.3f;
                }
                RAPI.GetLocalPlayer().Stats.stat_oxygen.SetOxygenLostPerSecond(1.5f);
            }
        }else if (MyInput.GetButtonUp("Sprint"))
        {
            sprinted = !sprinted;
            if(theSpeed < 2)
            {
                theSpeed = 2;
            }
            RAPI.GetLocalPlayer().PersonController.swimSpeed = theSpeed;
            if (GameModeValueManager.GetCurrentGameModeValue().nourishmentVariables.foodDecrementRateMultiplier > 0f) {
                GameModeValueManager.GetCurrentGameModeValue().nourishmentVariables.foodDecrementRateMultiplier = 1f;
            }
            if (GameModeValueManager.GetCurrentGameModeValue().nourishmentVariables.thirstDecrementRateMultiplier > 0f) {
                GameModeValueManager.GetCurrentGameModeValue().nourishmentVariables.thirstDecrementRateMultiplier = 1f;
            }
            RAPI.GetLocalPlayer().Stats.stat_oxygen.SetOxygenLostPerSecond(1f);
        }
    }
}
