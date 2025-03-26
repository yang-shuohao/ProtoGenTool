using UnityEngine;
using UnityEngine.UI;

public class ProtoTreeRoot : BasePanel
{
    private Image imgBg;
    private Image imgArrow;
    private Image imgFolder;

    protected override void Awake()
    {
        base.Awake();

        imgBg = GetControl<Image>("ProtoTreeRoot");
        imgArrow = GetControl<Image>("imgArrow");
        imgFolder = GetControl<Image>("imgFolder");
    }
}
