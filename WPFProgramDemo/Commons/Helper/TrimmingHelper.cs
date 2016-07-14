/***********************************************************************   
* Copyright(c) 2016-2050 ligl
* CLR 版本: 4.0.30319.42000   
* 文 件 名：TrimmingHelper   
* 创 建 人：ligl   
* 创建日期：2016/7/14 21:05:16   
* 修 改 人：ligl   
* 修改日期：   
* 备注描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Commons.Helper
{
    /// <summary>
    /// 计算长度是否超出文本宽度的帮助类
    /// </summary>
    public class TrimmingHelper
    {

        public static string Trim(string source, string suffix, double width, Typeface face, double fontsize)
        {
            return Trim(source, suffix, null, width, face, fontsize);
        }

        public static string Trim(string source, string suffix, string endNoTrimSource, double width, Typeface face, double fontsize)
        {

            if (face != null)
            {
                //real display max width.
                double realWidth = width;

                //try to get GlyphTypeface.
                GlyphTypeface glyphTypeface;
                face.TryGetGlyphTypeface(out glyphTypeface);

                if (glyphTypeface != null)
                {
                    //calculate end string 's display width.
                    if (!string.IsNullOrEmpty(endNoTrimSource))
                    {
                        double notrimWidth = 0;
                        foreach (char c in endNoTrimSource)
                        {
                            ushort w;
                            glyphTypeface.CharacterToGlyphMap.TryGetValue(c, out w);
                            notrimWidth += glyphTypeface.AdvanceWidths[w] * fontsize;
                        }

                        realWidth = width - notrimWidth;
                    }

                    //calculate source 's screen width
                    double sourceWidth = 0;
                    if (!string.IsNullOrEmpty(source))
                    {
                        foreach (char c in source)
                        {
                            ushort w;
                            glyphTypeface.CharacterToGlyphMap.TryGetValue(c, out w);
                            sourceWidth += glyphTypeface.AdvanceWidths[w] * fontsize;
                        }
                    }

                    //don't need to trim.
                    if (sourceWidth <= realWidth) return source + endNoTrimSource;

                    //calculate suffix's display width
                    double suffixWidth = 0;
                    if (!string.IsNullOrEmpty(suffix))
                    {
                        foreach (char c in suffix)
                        {
                            ushort w;
                            glyphTypeface.CharacterToGlyphMap.TryGetValue(c, out w);
                            suffixWidth += glyphTypeface.AdvanceWidths[w] * fontsize;
                        }
                    }

                    realWidth = realWidth - suffixWidth;

                    if (realWidth > 0)
                    {
                        sourceWidth = 0;
                        string trimStr = string.Empty;
                        foreach (char c in source)
                        {
                            ushort w;
                            glyphTypeface.CharacterToGlyphMap.TryGetValue(c, out w);

                            double cWidth = glyphTypeface.AdvanceWidths[w] * fontsize;

                            if ((sourceWidth + cWidth) > realWidth) return trimStr + suffix + endNoTrimSource;
                            trimStr += c;
                            sourceWidth += cWidth;
                        }
                    }
                    else
                    {
                        if (width > suffixWidth) return suffix;
                        else return "...";
                    }
                }
            }

            return source + endNoTrimSource;
        }

        public static string Trim(string source, string suffix, string endNoTrimSource, double width, Typeface face, double fontsize, ref bool ShowTip)
        {

            if (face != null)
            {
                //real display max width.
                double realWidth = width;

                //try to get GlyphTypeface.
                GlyphTypeface glyphTypeface;
                face.TryGetGlyphTypeface(out glyphTypeface);

                if (glyphTypeface != null)
                {
                    //calculate end string 's display width.
                    if (!string.IsNullOrEmpty(endNoTrimSource))
                    {
                        double notrimWidth = 0;
                        foreach (char c in endNoTrimSource)
                        {
                            ushort w;
                            glyphTypeface.CharacterToGlyphMap.TryGetValue(c, out w);
                            notrimWidth += glyphTypeface.AdvanceWidths[w] * fontsize;
                        }

                        realWidth = width - notrimWidth;
                    }

                    //calculate source 's screen width
                    double sourceWidth = 0;
                    if (!string.IsNullOrEmpty(source))
                    {
                        foreach (char c in source)
                        {
                            ushort w;
                            glyphTypeface.CharacterToGlyphMap.TryGetValue(c, out w);
                            sourceWidth += glyphTypeface.AdvanceWidths[w] * fontsize;
                        }
                    }

                    //don't need to trim.
                    if (sourceWidth <= realWidth) return source + endNoTrimSource;

                    //calculate suffix's display width
                    double suffixWidth = 0;
                    if (!string.IsNullOrEmpty(suffix))
                    {
                        foreach (char c in suffix)
                        {
                            ushort w;
                            glyphTypeface.CharacterToGlyphMap.TryGetValue(c, out w);
                            suffixWidth += glyphTypeface.AdvanceWidths[w] * fontsize;
                        }
                    }

                    realWidth = realWidth - suffixWidth;

                    if (realWidth > 0)
                    {
                        sourceWidth = 0;
                        string trimStr = string.Empty;
                        foreach (char c in source)
                        {
                            ushort w;
                            glyphTypeface.CharacterToGlyphMap.TryGetValue(c, out w);

                            double cWidth = glyphTypeface.AdvanceWidths[w] * fontsize;

                            if ((sourceWidth + cWidth) > realWidth)
                            {
                                ShowTip = true;
                                return trimStr + suffix + endNoTrimSource;
                            }
                            trimStr += c;
                            sourceWidth += cWidth;
                        }
                    }
                    else
                    {
                        ShowTip = true;
                        if (width > suffixWidth) return suffix;
                        else return "...";

                    }
                }
            }
            ShowTip = false;
            return source + endNoTrimSource;
        }

        public static bool IsTrim(string source, string suffix, double width, Typeface face, double fontsize)
        {
            if (face != null)
            {
                //real display max width.
                double realWidth = width;

                //try to get GlyphTypeface.
                GlyphTypeface glyphTypeface;
                face.TryGetGlyphTypeface(out glyphTypeface);

                if (glyphTypeface != null)
                {
                    //calculate source 's screen width
                    double sourceWidth = 0;
                    if (!string.IsNullOrEmpty(source))
                    {
                        foreach (char c in source)
                        {
                            ushort w;
                            glyphTypeface.CharacterToGlyphMap.TryGetValue(c, out w);
                            sourceWidth += glyphTypeface.AdvanceWidths[w] * fontsize;
                        }
                    }

                    //don't need to trim.
                    if (sourceWidth <= realWidth) return false;

                    //calculate suffix's display width
                    double suffixWidth = 0;
                    if (!string.IsNullOrEmpty(suffix))
                    {
                        foreach (char c in suffix)
                        {
                            ushort w;
                            glyphTypeface.CharacterToGlyphMap.TryGetValue(c, out w);
                            suffixWidth += glyphTypeface.AdvanceWidths[w] * fontsize;
                        }
                    }

                    realWidth = realWidth - suffixWidth;

                    if (realWidth > 0)
                    {
                        sourceWidth = 0;
                        string trimStr = string.Empty;
                        foreach (char c in source)
                        {
                            ushort w;
                            glyphTypeface.CharacterToGlyphMap.TryGetValue(c, out w);

                            double cWidth = glyphTypeface.AdvanceWidths[w] * fontsize;

                            if ((sourceWidth + cWidth) > realWidth) return true;
                            trimStr += c;
                            sourceWidth += cWidth;
                        }
                    }
                    else
                    {
                        //if (width > suffixWidth) return true;
                        //else return "...";
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
