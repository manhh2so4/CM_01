using UnityEngine;

public class Death : CoreComponent
{
    private Stats Stats => stats ? stats : core.GetCoreComponent(ref stats);
    private Stats stats;

    public void Die(){
        core.transform.parent.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Stats.Health.OnCurrentValueZero += Die;
    }

    private void OnDisable()
    {
        Stats.Health.OnCurrentValueZero -= Die;
    }
}