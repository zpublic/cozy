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
        int PixelWidth { get; }

        int PixelHeight { get; }

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
        /// 刷新 没有保存到像素块的图像
        /// </summary>
        void UpdateDrawable();

        /// <summary>
        /// 尝试读取坐标颜色
        /// </summary>
        /// <param name="p"></param>
        /// <param name="c"></param>
        /// <returns>失败返回false</returns>
        Color ReadPixel(Point p);

        Point ConvertMapToScene(Point p);
        Point ConvertSceneToMap(Point p);
    }
}
