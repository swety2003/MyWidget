using MyWidgets.SDK.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using MyWidgets.SDK.Core.Card;

namespace MyWidgets.SDK.Extensions
{

    public static class ICardExt
    {
        /// <summary>
        /// 查找父控件
        /// </summary>
        /// <typeparam name="T">父控件的类型</typeparam>
        /// <param name="obj">要找的是obj的父控件</param>
        /// <param name="name">想找的父控件的Name属性</param>
        /// <returns>目标父控件</returns>
        public static T GetParentObject<T>(DependencyObject obj) where T : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);

            while (parent != null)
            {
                if (parent is T)
                {
                    return (T)parent;
                }

                // 在上一级父控件中没有找到指定名字的控件，就再往上一级找
                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }


        public static CardControl GetCardControl(this ICard card)
        {
            var c = GetParentObject<CardControl>(card as DependencyObject);
            return c;
        }

        public static CardWindow GetCardWindow(this ICard card)
        {

            var win = GetParentObject<CardWindow>(card as DependencyObject);
            return win;
        }
    }

}
