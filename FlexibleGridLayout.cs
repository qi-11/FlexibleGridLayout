using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ��������
/// </summary>
public enum FitType
{
    /// <summary>
    /// ���ȷֲ�
    /// </summary>
    UniForm,
    /// <summary>
    /// ˮƽ����
    /// </summary>
    Horizontal,
    /// <summary>
    /// ��ֱ����
    /// </summary>
    Verticality,
    /// <summary>
    /// �̶���
    /// </summary>
    FixedRow,
    /// <summary>
    /// �̶���
    /// </summary>
    FixedColumn,
}

/// <summary>
/// �������񲼾�
/// </summary>
public class FlexibleGridLayout : LayoutGroup
{
    /// <summary>
    /// �������ͣ�Ĭ�Ͼ��ȷֲ�
    /// </summary>
    [Header("Flexible Grid")]
    public FitType fitType = FitType.UniForm;

    /// <summary>
    /// ��Ԫ����
    /// </summary>
    public Vector2 spacing;

    /// <summary>
    /// ĳ����Ԫ��
    /// </summary>
    RectTransform item;


    /// <summary>
    /// ����ˮƽ����
    /// </summary>
    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        //���Ӷ���������ƽ���������������
        var squarRoot = Mathf.Sqrt(rectChildren.Count);
        var rows = Mathf.CeilToInt(squarRoot);
        var columns = Mathf.CeilToInt(squarRoot);

        if (fitType==FitType.FixedColumn)
        {
            columns = 1;
        }
        else if (fitType==FitType.FixedRow)
        {
            rows = 1;
        }

        if (fitType==FitType.Horizontal || fitType==FitType.FixedColumn)
        {
            rows = Mathf.CeilToInt(rectChildren.Count / (float)columns);
        }

        if (fitType == FitType.Verticality || fitType == FitType.FixedRow)
        {
            rows = Mathf.CeilToInt(rectChildren.Count / (float)rows);
        }

        //����л���Ϊ0  û��Ҫ����
        if (rows == 0 || columns == 0)
        {
            return;
        }

        //�������Ĵ�С
        var spacingWidth = spacing.x / columns * (columns - 1);
        var spacingHeight = spacing.y / rows * (rows - 1);

        //�����ڱ߾��С
        var paddingWidth = (padding.left + padding.right) / columns;
        var paddingHeight = (padding.top + padding.bottom) / rows;

        //���㵥Ԫ���С
        var width = rectTransform.rect.width / columns;
        var height = rectTransform.rect.height / rows;

        //��Ԫ���С ��ȥ �����С ��ȥ �ڱ߾��С
        var cellWidth = width - spacingWidth- paddingWidth;
        var cellHeight = height - spacingHeight- paddingHeight;



        //���㵥Ԫ�����겢���õ�Ԫ��
        for (int i = 0; i < rectChildren.Count; i++)
        {
            //��ȡ������
            var rowIndex = i / columns;
            //��ȡ������
            var columnIndex = i % columns;

            //��������
            var x = (cellWidth+spacing.x) * columnIndex+padding.left;
            var y = (cellHeight + spacing.y)*rowIndex+padding.top;

            item = rectChildren[i];

            SetChildAlongAxis(item, 0, x, cellWidth);
            SetChildAlongAxis(item, 1, y, cellHeight);
        }

    }


    public override void CalculateLayoutInputVertical()
    {

    }

    public override void SetLayoutHorizontal()
    {

    }

    public override void SetLayoutVertical()
    {

    }


}
