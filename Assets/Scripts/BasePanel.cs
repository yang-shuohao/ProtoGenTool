using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI面板基类
/// </summary>
public class BasePanel : MonoBehaviour
{
    //存储当前面板的所有控件
    private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

    protected virtual void Awake()
    {
        //去除克隆后缀
        transform.name = transform.name.Replace("(Clone)", "");

        //UGUI
        FindChildrenControl<Button>();
        FindChildrenControl<Image>();
        FindChildrenControl<TMP_Text>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<Slider>();
        FindChildrenControl<ScrollRect>();
        FindChildrenControl<TMP_InputField>();
        FindChildrenControl<ToggleGroup>();
    }

    /// <summary>
    /// 找到子对象的对应控件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void FindChildrenControl<T>() where T : UIBehaviour
    {
        T[] controls = this.GetComponentsInChildren<T>(true);
        for (int i = 0; i < controls.Length; ++i)
        {
            string objName = controls[i].gameObject.name;
            if (controlDic.ContainsKey(objName))
            {
                controlDic[objName].Add(controls[i]);
            }
            else
            {
                controlDic.Add(objName, new List<UIBehaviour>() { controls[i] });
            }

            if (controls[i] is Button)
            {
                (controls[i] as Button).onClick.AddListener(() =>
                {
                    OnClick(objName);
                });
            }
            else if (controls[i] is TMP_InputField)
            {
                (controls[i] as TMP_InputField).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName, value);
                });
            }
            else if (controls[i] is Toggle)
            {
                (controls[i] as Toggle).onValueChanged.AddListener((isOn) =>
                {
                    OnValueChanged(objName, isOn);
                });
            }
        }
    }

    /// <summary>
    /// 得到对应名字的对应控件脚本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controlName"></param>
    /// <returns></returns>
    protected T GetControl<T>(string controlName) where T : UIBehaviour
    {
        if (controlDic.ContainsKey(controlName))
        {
            for (int i = 0; i < controlDic[controlName].Count; ++i)
            {
                if (controlDic[controlName][i] is T)
                {
                    return controlDic[controlName][i] as T;
                }
            }
        }

        return null;
    }

    protected virtual void OnClick(string btnName)
    {

    }

    protected virtual void OnValueChanged(string controlName, string value)
    {

    }

    protected virtual void OnValueChanged(string controlName, bool isOn)
    {

    }
}
