/*******************************************************************
* ��Ȩ���У� ���������пƼ����޹�˾
* �ļ����ƣ� AccordionSelectionMode.cs
* ��   �ߣ� chenzhifen
* �������ڣ� 2014-05-11 10:02
* �ļ��汾�� 1.0.0.0
* �޸�ʱ�䣺             �޸��ˣ�                �޸����ݣ�
*******************************************************************/

namespace Genew.ModernUI.ExtendedToolkit
{
    /// <summary>
    ///     Defines the minimum and maximum number of selected items allowed in an Accordion control.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
    public enum AccordionSelectionMode
    {
        /// <summary>
        ///     Exactly one item must be selected in the Accordion.
        /// </summary>
        One,

        /// <summary>
        ///     At least one item must be selected in the Accordion.
        /// </summary>
        OneOrMore,

        /// <summary>
        ///     No more than one item can be selected in the accordion.
        /// </summary>
        ZeroOrOne,

        /// <summary>
        ///     Any number of  items can be selected in the Accordion.
        /// </summary>
        ZeroOrMore
    }
}