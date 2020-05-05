using System;

[Serializable]
public class GameData 
{
    public bool isGameStartedFirstTime { get; set; }
    public bool isMusicOn { get; set; }
    public int energy { get; set; }
   
    //TIME
    public string datetime { get; set; }
    //METER AND COIN
    public float score { get; set; }
    public int coins { get; set; }
    //ACHIEVEMENTS
    public bool[] achievements { get; set; }
    public bool[] collectedItems { get; set; }

    //STORE
    public int selectedPlayer { get; set; }
    public bool[] maps { get; set; }

    public bool[] players { get; set; }
}
