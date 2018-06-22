using UnityEngine;

public class Util {

    public static void SetLayerRecursively(GameObject _obj, int newLayer)
    {
        if(_obj == null)
        {
            return;
        }
        _obj.layer = newLayer;

        foreach (Transform _child in _obj.transform)
        {
            if (_child == null)
                continue;


            SetLayerRecursively(_child.gameObject, newLayer);
        }
    }
}
