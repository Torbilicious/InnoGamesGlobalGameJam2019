using System;
using UnityEngine;
using UnityEngine.UI;

public partial class LevelTileX : MonoBehaviour
{
    public void OnIsPortalChanged(Toggle change)
    {
        _data.IsPortal = change.isOn;
        SynchronizeData();
    }

    public void OnIsPortalExitChanged(Toggle change)
    {
        _data.IsPortalExit = change.isOn;
        SynchronizeData();
    }

    public void OnPortalTargetChanged(InputField input)
    {
        string[] strParts = input.text.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

        int x = (int)_data.PortalTargetLocation.x, y = (int)_data.PortalTargetLocation.y;
        if (strParts.Length != 2 || !int.TryParse(strParts[0], out x) || !int.TryParse(strParts[1], out y) || FindTileByPosition(x, y) == null || FindTileByPosition(x, y) == this)
        {
            input.textComponent.color = Color.red;
        }
        else
        {
            input.textComponent.color = Color.gray;
        }

        _data.PortalTargetLocation = new Vector2(x, y);
        SynchronizeData();
    }

    public void OnDelete()
    {
        _data = new TileData();
        SynchronizeData();
        Destroy(this.gameObject);
    }
}
