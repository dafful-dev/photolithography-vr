using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuData : MonoBehaviour
{
    private Stack<MenuKey> MenuStack = new();

    public static event Action OnMenuChange;

    public void Awake()
    {
        ResetMenu();
    }

    public MenuKey GetActiveMenu()
    {
        return MenuStack.Peek();
    }

    public void ResetMenu()
    {
        MenuStack.Clear();
        MenuStack.Push(MenuKey.Start);
    }

    public void AddMenu(MenuKey menuKey)
    {
        MenuStack.Push(menuKey);
        OnMenuChange?.Invoke();
    }

    public void RemoveMenu()
    {
        if (MenuStack.Count == 1) return;
        MenuStack.Pop();
        OnMenuChange?.Invoke();
    }
}
