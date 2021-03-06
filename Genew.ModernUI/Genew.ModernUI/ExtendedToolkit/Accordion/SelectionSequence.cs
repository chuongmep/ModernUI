/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： SelectionSequence.cs
* 作   者： chenzhifen
* 创建日期： 2014-05-11 10:02
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

namespace ModernUI.ExtendedToolkit
{
    /// <summary>
    ///     Determines the order in which visual states are set.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
    public enum SelectionSequence
    {
        /// <summary>
        ///     Collapses are set before expansions.
        /// </summary>
        CollapseBeforeExpand,

        /// <summary>
        ///     No delays, all states are set immediately.
        /// </summary>
        Simultaneous
    }
}