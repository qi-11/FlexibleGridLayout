using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 适配类型
/// </summary>
public enum FitType
{
    /// <summary>
    /// 均匀分布
    /// </summary>
    UniForm,
    /// <summary>
    /// 水平优先
    /// </summary>
    Horizontal,
    /// <summary>
    /// 垂直优先
    /// </summary>
    Verticality,
    /// <summary>
    /// 固定行
    /// </summary>
    FixedRow,
    /// <summary>
    /// 固定列
    /// </summary>
    FixedColumn,
}

/// <summary>
/// 灵活的网格布局
/// </summary>
public class FlexibleGridLayout : LayoutGroup
{
    /// <summary>
    /// 适配类型，默认均匀分布
    /// </summary>
    [Header("Flexible Grid")]
    public FitType fitType = FitType.UniForm;

    /// <summary>
    /// 单元格间隔
    /// </summary>
    public Vector2 spacing;

    /// <summary>
    /// 某个单元格
    /// </summary>
    RectTransform item;


    /// <summary>
    /// 计算水平布局
    /// </summary>
    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        //对子对象数量开平方根计算出行列数
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

        //如果行或列为0  没必要继续
        if (rows == 0 || columns == 0)
        {
            return;
        }

        //计算间隔的大小
        var spacingWidth = spacing.x / columns * (columns - 1);
        var spacingHeight = spacing.y / rows * (rows - 1);

        //计算内边距大小
        var paddingWidth = (padding.left + padding.right) / columns;
        var paddingHeight = (padding.top + padding.bottom) / rows;

        //计算单元格大小
        var width = rectTransform.rect.width / columns;
        var height = rectTransform.rect.height / rows;

        //单元格大小 减去 间隔大小 减去 内边距大小
        var cellWidth = width - spacingWidth- paddingWidth;
        var cellHeight = height - spacingHeight- paddingHeight;



        //计算单元格坐标并设置单元格
        for (int i = 0; i < rectChildren.Count; i++)
        {
            //获取行索引
            var rowIndex = i / columns;
            //获取列索引
            var columnIndex = i % columns;

            //计算坐标
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
