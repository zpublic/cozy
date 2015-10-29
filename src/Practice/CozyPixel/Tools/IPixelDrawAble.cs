using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel.Tools
{
    public interface IPixelDrawable
    {
        /// <summary>
        /// 默认绘制颜色
        /// </summary>
        Color DefaultDrawColor { get; set; }

        /// <summary>
        /// 绘制到像素块
        /// </summary>
        /// <param name="p">屏幕坐标点</param>
        /// <param name="c">绘制颜色</param>
        /// <returns>成功返回true</returns>
        bool DrawPixel(Point p, Color c);

        /// <summary>
        /// 绘制到屏幕 不保存到像素块
        /// </summary>
        /// <param name="p">屏幕坐标点</param>
        /// <param name="c">绘制颜色</param>
        /// <returns>成功返回true</returns>
        bool FakeDrawPixel(Point p, Color c);

        /// <summary>
        /// 画线到像素块
        /// </summary>
        /// <param name="start">线条起点</param>
        /// <param name="end">线条终点</param>
        /// <param name="c">线条颜色</param>
        /// <returns>成功返回true</returns>
        bool DrawLine(Point start, Point end, Color c);

        /// <summary>
        /// 画线到屏幕 不保存到像素块
        /// </summary>
        /// <param name="start">线条起点</param>
        /// <param name="end">线条终点</param>
        /// <param name="c">绘制颜色</param>
        /// <returns>成功返回true</returns>
        bool FakeDrawLine(Point start, Point end, Color c);

        /// <summary>
        /// 刷新 没有保存到像素块的图像
        /// </summary>
        void UpdateDrawable();
    }
}
