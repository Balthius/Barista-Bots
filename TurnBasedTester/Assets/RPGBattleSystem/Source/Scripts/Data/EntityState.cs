public class EntityState
{
    public EntityState(EntityData p_data)
    {
        m_data = p_data;
    }

    public EntityData data
    {
        get { return m_data; }
    }

    public int currentHealth
    {
        get { return m_data.stats.health + m_healthTokens; }
    }

    public int currentPower
    {
        get { return m_data.stats.power + m_powerTokens; }
    }

    public int currentDefense
    {
        get { return m_data.stats.defense + m_defenseTokens; }
    }

    public int currentMana
    {
        get { return m_data.stats.mana + m_manaTokens; }
    }

    public int currentResistence
    {
        get { return m_data.stats.resistence + m_resistenceTokens; }
    }

    public int currentSpeed
    {
        get { return m_data.stats.speed + m_speedTokens; }
    }

    public void modifyHealth(int p_health)
    {
        m_healthTokens += p_health;

        if (m_healthTokens < -m_data.stats.health)
        {
            m_healthTokens = -m_data.stats.health;
        }
    }

    public void modifyPower(int p_power)
    {
        m_powerTokens += p_power;

        if (m_powerTokens < -m_data.stats.power)
        {
            m_powerTokens = -m_data.stats.power;
        }
    }

    public void modifyDefense(int p_defense)
    {
        m_defenseTokens += p_defense;

        if (m_defenseTokens < -m_data.stats.defense)
        {
            m_defenseTokens = -m_data.stats.defense;
        }
    }

    public void modifyMana(int p_mana)
    {
        m_manaTokens += p_mana;

        if (m_manaTokens < -m_data.stats.mana)
        {
            m_manaTokens = -m_data.stats.mana;
        }
    }

    public void modifyResistence(int p_resistence)
    {
        m_resistenceTokens += p_resistence;

        if (m_resistenceTokens < -m_data.stats.resistence)
        {
            m_resistenceTokens = -m_data.stats.resistence;
        }
    }


    public void modifySpeed(int p_speed)
    {
        m_speedTokens += p_speed;

        if (m_speedTokens < -m_data.stats.speed)
        {
            m_speedTokens = -m_data.stats.speed;
        }
    }

    protected int m_damageTokens;
    protected int m_healthTokens;
    protected int m_powerTokens;
    protected int m_defenseTokens;
    protected int m_manaTokens;
    protected int m_resistenceTokens;
    protected int m_speedTokens;

    protected EntityData m_data;
}
