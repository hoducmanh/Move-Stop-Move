using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UICanvasRef
{
    public UIID ID;
    public UICanvas canvas;
}
public class UIManager : Singleton<UIManager>
{
    public Dictionary<UIID, UICanvas> UIRefDict = new Dictionary<UIID, UICanvas>();
    public Dictionary<UIID, UICanvas> UIDict = new Dictionary<UIID, UICanvas>();
    [NonReorderable] public List<UICanvasRef> UIRefList;
    public Transform UIParentTrans;
    private void Awake()
    {
        foreach (var item in UIRefList)
        {
            if (item.canvas != null)
            {
                UIRefDict.Add(item.ID, item.canvas);
            }
        }
    }
    public UICanvas GetUICanvasByID(UIID id)
    {
        if (!UIDict.ContainsKey(id) || UIDict[id] == null)
        {
            UICanvas canvas = Instantiate(UIRefDict[id], UIParentTrans);
            UIDict[id] = canvas;
        }
        return UIDict[id];
    }
    public T GetUICanvasByID<T>(UIID id) where T : UICanvas
    {
        return GetUICanvasByID(id) as T;
    }
    public UICanvas OpenUI(UIID id)
    {
        UICanvas canvas = GetUICanvasByID(id);
        canvas.OpenCanvas();
        return canvas;
    }
    public T OpenUI<T>(UIID id) where T : UICanvas
    {
        return OpenUI(id) as T;
    }
    public void CloseUI(UIID id)
    {
        if (IsUICanvasOpened(id))
        {
            GetUICanvasByID(id).CloseCanvas();
        }
    }
    public bool IsUICanvasOpened(UIID id)
    {
        return UIDict.ContainsKey(id) && UIDict[id] != null && UIDict[id].gameObject.activeInHierarchy;
    }
}
