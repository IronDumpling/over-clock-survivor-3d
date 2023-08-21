using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    #region Level

    public Level level;

    #endregion

    #region Health

    public Health health;

    #endregion

    #region Speed

    public Speed speed;

    #endregion

    #region Energy

    public Energy energy;

    #endregion

    #region Frequency

    public Frequency freq;

    #endregion

    public Player()
    {
        level = new Level();
        health = new Health();
        speed = new Speed();
        energy = new Energy();
        freq = new Frequency();
    }
}
