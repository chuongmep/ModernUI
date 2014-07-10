/*******************************************************************
* ��Ȩ���У� ���������пƼ����޹�˾
* �ļ����ƣ� SelectionSequence.cs
* ��   �ߣ� chenzhifen
* �������ڣ� 2014-05-11 10:02
* �ļ��汾�� 1.0.0.0
* �޸�ʱ�䣺             �޸��ˣ�                �޸����ݣ�
*******************************************************************/

namespace Genew.ModernUI.ExtendedToolkit
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