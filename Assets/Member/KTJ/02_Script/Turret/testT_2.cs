using UnityEngine;

public class testT_2 : Turret
{
    protected override void OnDelete()
    {
        Debug.Log("ÆÄ±«µÊ");
    }

    protected override void OnSpawn()
    {
        Debug.Log("»ý¼ºµÊ");
    }
}
