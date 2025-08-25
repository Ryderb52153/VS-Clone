using System;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private List<CursorConfig> cursorConfigs = new();
    [SerializeField] private CursorType defaultType = CursorType.Default;

    private  Dictionary<CursorType, CursorConfig> cursorMap = new();

    public void SetDefault() => Apply(CursorType.Default);
    public void SetTarget() => Apply(CursorType.Target);

    private void Awake()
    {
        cursorMap.Clear();
        foreach (var cfg in cursorConfigs)
        {
            if (!cfg.texture) continue;
            cursorMap[cfg.type] = cfg;
        }
    }

    private void Start()
    {
        Apply(defaultType);
        Cursor.visible = true;
    }

    public void Apply(CursorType type)
    {
        if (!cursorMap.TryGetValue(type, out var cfg) || cfg.texture == null)
        {
            Debug.LogWarning($"CursorManager: Missing config for {type}");
            return;
        }

        Vector2 hotspot = cfg.useCenterHotspot
            ? new Vector2(cfg.texture.width * 0.5f, cfg.texture.height * 0.5f)
            : cfg.customHotspot;

        Cursor.SetCursor(cfg.texture, hotspot, cfg.cursorMode);
    }
}

[Serializable]
public class CursorConfig
{
    public CursorType type;
    public Texture2D texture;

    [Header("Hotspot")]
    public bool useCenterHotspot = true;
    public Vector2 customHotspot; // pixel coords from TOP-LEFT

    [Header("Advanced")]
    public CursorMode cursorMode = CursorMode.Auto;
}

public enum CursorType
{
    Default,
    Target,
}