public class Player
{
    #region Level

    public Level level;

    private void LoadLevel(NoLimitData lv)
    {
        level.Min = lv.min;
        level.Curr = lv.curr;
        level.Max = lv.max;
    }

    #endregion

    #region Health

    public Health health;

    private void LoadHealth(OneLimitData hp)
    {
        health.Min = hp.min;
        health.Curr = hp.curr;
        health.Max = hp.max;
        health.Limits = hp.limits;
    }

    #endregion

    #region Speed

    public Speed speed;

    private void LoadSpeed(TwoLimitsData sp)
    {
        speed.Min = sp.min;
        speed.Curr = sp.curr;
        speed.Max = sp.max;
        speed.LowerLimits = sp.lowerLimits;
        speed.UpperLimits = sp.upperLimits;
    }

    #endregion

    #region Energy

    public Energy energy;

    private void LoadEnergy(OneLimitData en)
    {
        energy.Min = en.min;
        energy.Curr = en.curr;
        energy.Max = en.max;
        energy.Limits = en.limits;
    }

    #endregion

    #region Frequency

    public Frequency freq;

    private void LoadFreq(TwoLimitsData fq)
    {
        freq.Min = fq.min;
        freq.Curr = fq.curr;
        freq.Max = fq.max;
        freq.LowerLimits = fq.lowerLimits;
        freq.UpperLimits = fq.upperLimits;
    }

    #endregion

    public Player()
    {
        level = new Level();
        health = new Health();
        speed = new Speed();
        energy = new Energy();
        freq = new Frequency();
    }

    public Player(PlayerSO playerConfig)
    {
        level = new Level();
        health = new Health();
        speed = new Speed();
        energy = new Energy();
        freq = new Frequency();

        LoadLevel(playerConfig.level);
        LoadHealth(playerConfig.health);
        LoadEnergy(playerConfig.energy);
        LoadSpeed(playerConfig.speed);
        LoadFreq(playerConfig.frequency);
    }
}
