using System;

namespace GUI4UFramework.Graphics
{
    public class Rectangle : IEquatable<Rectangle>
    {
        /// <summary>
        /// The x position of the rectangle.
        /// </summary>
        private float positionX;

        /// <summary>
        ///  The y position of the rectangle
        /// </summary>
        private float positionY;

        /// <summary>
        /// The width of the rectangle
        /// </summary>
        private float width;

        /// <summary>
        /// The height of the rectangle
        /// </summary>
        private float height;

        /// <summary>
        /// Whether i will raise events when changed
        /// </summary>
        private bool canRaiseEvent;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        /// <param name="positionX">The x position.</param>
        /// <param name="positionY">The y position.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Rectangle(float positionX, float positionY, float width, float height) 
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="size">The size.</param>
        public Rectangle(DVector2 location, DVector2 size) : this(location.X, location.Y, size.X, size.Y)
        {
        }

        /// <summary>
        ///  Create a rectangle from the outside locations
        /// </summary>
        /// <param name="left">The left side of the rectangle</param>
        /// <param name="top">The top side of the rectangle</param>
        /// <param name="right">The right side of the rectangle</param>
        /// <param name="bottom">The bottom side of the rectangle</param>
        /// <returns>A rectangle with give positions</returns>
        public static Rectangle FromLeftTopRightBottom(int left, int top, int right, int bottom)
        {
            return new Rectangle(
                left,
                top,
                right - left,
                bottom - top);
        }

        /// <summary>
        /// Creates a empty rectangle
        /// </summary>
        /// <returns>A empty rectangle</returns>
        public static Rectangle Empty()
        {
            return new Rectangle(0, 0, 0, 0);
        }

        /// <summary>
        /// Occurs when this rectangle is [resizing].
        /// </summary>
        public event EventHandler Resizing;

        /// <summary>
        /// Occurs when this rectangle is [repositioning].
        /// </summary>
        public event EventHandler Repositioning;

        /// <summary>
        /// Gets or sets the x position.
        /// </summary>
        /// <value>
        /// The x position.
        /// </value>
        public float PositionX
        {
            get
            {
                return this.positionX;
            }

            set
            {
                this.positionX = value;
                this.RaiseReposition();
            }
        }

        /// <summary>
        /// Gets or sets the y position.
        /// </summary>
        /// <value>
        /// The y position.
        /// </value>
        public float PositionY
        {
            get
            {
                return this.positionY;
            }
            
            set
            {
                this.positionY = value; 
                this.RaiseReposition();
            }
        }

        /// <summary>
        /// Gets or sets the width of this rectangle.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public float Width
        {
            get
            {
                return this.width;
            }

            set
            {
                this.width = value;
                this.RaiseResize();
            }
        }

        /// <summary>
        /// Gets or sets the height of this rectangle
        /// </summary>
        public float Height
        {
            get
            {
                return this.height;
            }

            set
            {
                this.height = value;
                this.RaiseResize();
            }
        }

        /// <summary>
        ///  Gets the x-coordinate of the upper-left corner of the rectangular region defined
        /// </summary>
        public float Left
        {
            get
            {
                return this.PositionX;
            }
        }

        /// <summary>
        ///  Gets the y-coordinate of the upper-left corner of the rectangular region defined
        /// </summary>
        public float Top
        {
            get
            {
                return this.positionY;
            }
        }
  
        /// <summary>
        ///  Gets the x-coordinate of the lower-right corner of the rectangular region defined 
        /// </summary>
        public float Right
        {
            get 
            {
                return this.positionX + this.width;
            }
        }
 
        /// <summary>
        /// Gets the y-coordinate of the lower-right corner of the rectangular region defined
        /// </summary>
        public float Bottom
        {
            get
            {
                return this.positionY + this.height;
            }
        }

        public bool HasValue
        { 
            get 
            {
                const float Tolerance = 0.00000001f;
                if (Math.Abs(this.PositionX) > Tolerance) return true;
                if (Math.Abs(this.PositionY) > Tolerance) return true;
                if (Math.Abs(this.Width) > Tolerance) return true;
                if (Math.Abs(this.Height) > Tolerance) return true;
                return false;
            } 
        }

        public void SetPosition(float newPositionX, float newPositionY)
        {
            this.canRaiseEvent = false;
            this.positionX = newPositionX;
            this.positionY = newPositionY;
            this.canRaiseEvent = true;
            this.RaiseReposition();
        }

        public void SetSize(float newWidth, float newHeight)
        {
            this.canRaiseEvent = false;
            this.width = newWidth;
            this.height = newHeight;
            this.canRaiseEvent = true;
            this.RaiseResize();
        }

        public void SetSize(DVector2 size)
        {
            this.SetSize(size.X, size.Y);
        }

        public void SetPositionSize(float newPositionX, float newPositionY, float newWidth, float newHeight)
        {
            this.SetPosition(newPositionX,newPositionY);
            this.SetSize(newWidth,newHeight);
        }

        private void RaiseResize()
        {
            if (this.canRaiseEvent == false) return;

            if (this.Resizing != null)
                this.Resizing(this, null);
        }

        private void RaiseReposition()
        {
            if (this.canRaiseEvent == false) return;

            if (this.Repositioning != null)
                this.Repositioning(this, null);
        }

        /// <summary>
        /// Tests whether two objects have equal location and size.
        /// </summary>
        /// <param name="left">the rectangle to check from</param>
        /// <param name="right">the rectangle to check to</param>
        /// <returns>true if equal</returns>
        public static bool operator ==(Rectangle left, Rectangle right) 
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)left == null) || ((object)right == null))
            {
                return false;
            }
            const float Tolerance = 0.001f;
            return (Math.Abs(left.PositionX - right.PositionX) < Tolerance
                    && Math.Abs(left.PositionY - right.PositionY) < Tolerance
                    && Math.Abs(left.Width - right.Width) < Tolerance
                    && Math.Abs(left.Height - right.Height) < Tolerance);
        }

        /// <summary>
        /// Tests whether two objects differ in location or size.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true if not equal</returns>
        public static bool operator !=(Rectangle left, Rectangle right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return string.Format("Loc:|{0}|{1}|  Size:|{2}|{3}|", this.positionX, this.positionY, this.width, this.height);
        }

        /// <summary>
        /// Create a clone of this rectangle with the same values,
        /// </summary>
        /// <returns>A exact duplicate rectangle</returns>
        public Rectangle Clone()
        {
            var rectangle = new Rectangle(this.positionX, this.positionY, this.width, this.height);
            return rectangle;
        }

        public bool Equals(Rectangle other)
        {
            if (other == null)
            {
                return false;
            }

            const float Tolerance = 0.0001f;

            return 
            !(Math.Abs(other.Width - this.Width) > Tolerance ||
              Math.Abs(other.Height - this.Height) > Tolerance ||
              Math.Abs(other.PositionX - this.PositionX) > Tolerance ||
              Math.Abs(other.PositionY - this.PositionY) > Tolerance);
        }
        
        public override bool Equals(object obj) {
            if (!(obj is Rectangle))
                return false;
  
            var comp = (Rectangle)obj;
  
            return
                (comp.PositionX.Equals(this.PositionX) &&
                 comp.PositionY.Equals(this.PositionY) &&
                 comp.Width.Equals(this.Width) &&
                 comp.Height.Equals(this.Height));
        }
        public override int GetHashCode() 
        {
            return (int)((uint)this.PositionX ^
                        (((uint)this.PositionY << 13) | ((uint)this.PositionY >> 19)) ^
                        (((uint)this.Width << 26) | ((uint)this.Width >> 6)) ^
                        (((uint)this.Height << 7) | ((uint)this.Height >> 25)));
        }

        /// <summary>
        /// Creates a Rectangle that represents the intersection between this Rectangle and the other.
        /// </summary>
        /// <param name="rect"></param>
        public void Intersect(Rectangle rect)
        {
            var result = Intersect(rect, this);

            this.PositionX = result.PositionX;
            this.PositionY = result.PositionY;
            this.Width = result.Width;
            this.Height = result.Height;
        }

        /// <summary>
        /// Creates a rectangle that represents the intersection between a and b. If there is no intersection, null is returned.
        /// </summary>
        /// <param name="rectangleA"></param>
        /// <param name="rectangleB"></param>
        /// <returns></returns>
        public static Rectangle Intersect(Rectangle rectangleA, Rectangle rectangleB) 
        {
#if DEBUG
            if (rectangleA == null)
                throw new ArgumentNullException("rectangleA");

            if (rectangleB == null)
                throw new ArgumentNullException("rectangleB");
#endif
            
            var x1 = Math.Max(rectangleA.PositionX, rectangleB.PositionX);
            var x2 = Math.Min(rectangleA.PositionX + rectangleA.Width, rectangleB.PositionX + rectangleB.Width);
            var y1 = Math.Max(rectangleA.PositionY, rectangleB.PositionY);
            var y2 = Math.Min(rectangleA.PositionY + rectangleA.Height, rectangleB.PositionY + rectangleB.Height);
  
            if (x2 >= x1
                && y2 >= y1) {
 
                return new Rectangle(x1, y1, x2 - x1, y2 - y1);
            }
            return Empty();
        }
        /// <summary>
        /// Determines if this rectangle intersects with the other.
        /// </summary>
        /// <param name="rectangle">the rectangle to check if its inside</param>
        /// <returns>true if it intersects</returns>
        public bool IntersectsWith(Rectangle rectangle)
        {
#if DEBUG
            if (rectangle == null)
                throw new ArgumentNullException("rectangle");
#endif

            return (rectangle.PositionX < this.PositionX + this.Width) &&
            (this.PositionX < (rectangle.PositionX + rectangle.Width)) &&
            (rectangle.PositionY < this.PositionY + this.Height) &&
            (this.PositionY < rectangle.PositionY + rectangle.Height);
        }

        /// <summary>
        /// Determines if the specified point is contained within the rectangular region defined by this
        /// </summary>
        /// <param name="toCheckX"></param>
        /// <param name="toCheckY"></param>
        /// <returns>true if it has the specified point inside</returns>
        public bool Contains(int toCheckX, int toCheckY)
        {
            return
                this.PositionX <= toCheckX &&
                toCheckX < this.PositionX + this.Width &&
                this.PositionY <= toCheckY &&
                toCheckY < this.PositionY + this.Height;
        }
        /// <summary>
        /// Determines if the rectangular region represented by the given rectangle is entirely contained within the rectangular region represented
        /// </summary>
        /// <returns>True if it does fit in me</returns>
        public bool Contains(Rectangle rectangle)
        {
#if DEBUG
            if (rectangle == null)
                throw new ArgumentNullException("rectangle");
#endif

            return (this.PositionX <= rectangle.PositionX) &&
                   ((rectangle.PositionX + rectangle.Width) <= (this.PositionX + this.Width)) &&
                   (this.PositionY <= rectangle.PositionY) &&
                   ((rectangle.PositionY + rectangle.Height) <= (this.PositionY + this.Height));
        }

        /// <summary>
        /// Creates a rectangle that represents the union between a and b.
        /// </summary>
        /// <param name="rectangleA"></param>
        /// <param name="rectangleB"></param>
        /// <returns></returns>
        public static Rectangle Union(Rectangle rectangleA, Rectangle rectangleB) 
        {
#if DEBUG
            if (rectangleA == null)
                throw new ArgumentNullException("rectangleA");

            if (rectangleB == null)
                throw new ArgumentNullException("rectangleB");
#endif

            var x1 = Math.Min(rectangleA.PositionX, rectangleB.PositionX);
            var x2 = Math.Max(rectangleA.PositionX + rectangleA.Width, rectangleB.PositionX + rectangleB.Width);
            var y1 = Math.Min(rectangleA.PositionY, rectangleB.PositionY);
            var y2 = Math.Max(rectangleA.PositionY + rectangleA.Height, rectangleB.PositionY + rectangleB.Height);
 
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        /// <summary>
        /// Inflates this by the specified amount.
        /// </summary>
        /// <param name="inflateWidth"></param>
        /// <param name="inflateHeight"></param>
        public void Inflate(int inflateWidth, int inflateHeight)
        {
            this.PositionX -= inflateWidth;
            this.PositionY -= inflateHeight;
            this.Width += 2 * inflateWidth;
            this.Height += 2 * inflateHeight;
        }
  
        /// <summary>
        /// Inflates given rectangle by the specified amount.
        /// </summary>
        public static Rectangle Inflate(Rectangle rectangle, int amountX, int amountY) 
        {
#if DEBUG
            if (rectangle == null)
                throw new ArgumentNullException("rectangle");
#endif

            var r = rectangle;
            r.Inflate(amountX, amountY);
            return r;
        }

        /// <summary>
        /// Adjusts the location of this rectangle by the specified amount.
        /// </summary>
        /// <param name="amountX"></param>
        /// <param name="amountY"></param>
        public void Offset(int amountX, int amountY)
        {
            this.PositionX += amountX;
            this.PositionY += amountY;
        }

        public void ShiftHorizontal(float shift)
        {
            this.positionX = this.positionX + shift;
        }

        public void ShiftVertical(float shift)
        {
            this.positionY = this.positionY + shift;
        }
    }
}
