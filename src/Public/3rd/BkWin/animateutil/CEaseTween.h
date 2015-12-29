/* -----------------------------------------------------------------------
*  File		:	CEaseTween
*  Author	    :	hexujun
*  Datet   	:	2013/8/3 11:45
*  Brief		:	Tween算法及缓动效果
*
*			Linear：无缓动效果；
*			Quadratic：二次方的缓动（t^2）；
*			Cubic：三次方的缓动（t^3）；
*			Quartic：四次方的缓动（t^4）；
*			Quintic：五次方的缓动（t^5）；
*			Sinusoidal：正弦曲线的缓动（sin(t)）；
*			Exponential：指数曲线的缓动（2^t）；
*			Circular：圆形曲线的缓动（sqrt(1-t^2)）；
*			Elastic：指数衰减的正弦曲线缓动；
*			Back：超过范围的三次方缓动（(s+1)*t^3 - s*t^2）；
*			Bounce：指数衰减的反弹缓动。
*			ps：以上都是自己的烂翻译，希望各位修正。
*
*			每个效果都分三个缓动方式（方法），分别是：
*
*			easeIn：从0开始加速的缓动；
*			easeOut：减速到0的缓动；
*			easeInOut：前半段从0开始加速，后半段减速到0的缓动。
*
*
*			四个参数分别是：
*
*			t：current time（当前时间）；
*			b：beginning value（初始值）；
*			c： change in value（变化量）；
*			d：duration（持续时间）。
*
*参考网址动画效果：http://www.cnblogs.com/bluedream2009/archive/2010/06/19/1760909.html
* ----------------------------------------------------------------------
* ----------------------------------------------------------------------*/

#ifndef __CEaseTween__h__
#define __CEaseTween__h__
//
#include <math.h>

#define BEGIN_NAME_SPACE(namespace_XXXX) namespace namespace_XXXX{
#define END_NAME_SPACE(namespace_XXXX)  }
///////////////////////////////////////////////////////////////////////////

BEGIN_NAME_SPACE(ease_tween)

#define PI	(3.1415926535897932384626433)

/**
* Easing equation function for a simple linear tweening, with no easing.
*
* @param t        Current time (in frames or seconds).
* @param b        Starting value.
* @param c        Change needed in value.
* @param d        Expected easing duration (in frames or seconds).
* @return        The correct value.
*/
template<class T>
class Linear
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) 
	{
		return static_cast<T>( c*t/d + b );
	}
};

/**
* Easing equation function for a quadratic (t^2) easing in: accelerating from zero velocity.
*/
template<class T>
class IQuad
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) 
	{
		t/=d;
		return c*t*t + b;
	}
};

/**
* Easing equation function for a quadratic (t^2) easing out: decelerating to zero velocity.
*/
template<class T>
class OQuad
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d)
	{
		t/=d;
		t*=(t-2);

		return -c *t + b;
	}
};

/**
* Easing equation function for a quadratic (t^2) easing in/out: acceleration until halfway, then deceleration.
*/
template<class T>
class IOQuad
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) 
	{
		T tValue = 0;

		if ((t/=d/2) < 1) 
		{
			tValue = c/2*t*t + b;;
		}
		else
		{
			--t;
			tValue = -c/2 * (t*(t-2) - 1) + b;
		}

		return tValue;
	}
};

/**
* Easing equation function for a quadratic (t^2) easing out/in: deceleration until halfway, then acceleration.
*/
template<class T>
class OIQuad
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) 
	{
		T tValue = 0;

		if (t < d/2) 
			tValue = OQuad<T>::calculate(t*2, b, c/2, d);
		else
			tValue = IQuad<T>::calculate((t*2)-d, b+c/2, c/2, d);

		return tValue;
	}
};

/**
* Easing equation function for a cubic (t^3) easing in: accelerating from zero velocity.
*/
template<class T>
class ICubic
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) 
	{
		t/=d;
		return c*t*t*t + b;
	}
};

/**
* Easing equation function for a cubic (t^3) easing out: decelerating from zero velocity.
*/
template<class T>
class OCubic
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d)
	{
		t=(t/d)-1;

		return c*(t*t*t + 1) + b;
	}
};

/**
* Easing equation function for a cubic (t^3) easing in/out: acceleration until halfway, then deceleration.
*/
template<class T>
class IOCubic
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) 
	{
		T tValue = 0;

		if ((t/=d/2) < 1) 
			tValue = c/2*t*t*t + b;
		else
		{
			t-=2;
			tValue =  c/2*(t*t*t + 2) + b;
		}
		
		return tValue;
	}
};

/**
* Easing equation function for a cubic (t^3) easing out/in: deceleration until halfway, then acceleration.
*/
template<class T>
class OICubic
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d)
	{
		T tValue = 0;

		if (t < d/2) 
			tValue = OCubic<T>::calculate(t*2, b, c/2, d);
		else
			tValue = ICubic<T>::calculate((t*2)-d, b+c/2, c/2, d);

		return tValue;
	}
};

/**
* Easing equation function for a quartic (t^4) easing in: accelerating from zero velocity.
*/
template<class T>
class IQuart
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) 
	{
		t/=d;
		return c*t*t*t*t + b;
	}
};

/**
* Easing equation function for a quartic (t^4) easing out: decelerating from zero velocity.
*/
template<class T>
class OQuart
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) 
	{
		t=(t/d)-1;

		return -c * (t*t*t*t - 1) + b;
	}
};

/**
* Easing equation function for a quartic (t^4) easing in/out: acceleration until halfway, then deceleration.
*/
template<class T>
class IOQuart
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) 
	{
		T tValue = 0;

		if ((t/=d/2) < 1) 
		{
			tValue = c/2*t*t*t*t + b;
		}
		else
		{
			t-=2;
			tValue = -c/2 * (t*t*t*t - 2) + b;
		}
		
		return tValue;
	}
};

/**
* Easing equation function for a quartic (t^4) easing out/in: deceleration until halfway, then acceleration.
*/
template<class T>
class OIQuart
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d)
	{
		T tValue = 0;

		if (t < d/2) 
			tValue = OQuart<T>::calculate(t*2, b, c/2, d);
		else
			tValue =  IQuart<T>::calculate((t*2)-d, b+c/2, c/2, d);

		return tValue;
	}
};

/**
* Easing equation function for a quintic (t^5) easing in: accelerating from zero velocity.
*/
template<class T>
class IQuint
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) 
	{
		t/=d;
		return c*t*t*t*t*t + b;
	}
};

/**
* Easing equation function for a quintic (t^5) easing out: decelerating from zero velocity.
*/
template<class T>
class OQuint
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) 
	{
		t=(t/d)-1;

		return c*(t*t*t*t*t + 1) + b;
	}
};

/**
* Easing equation function for a quintic (t^5) easing in/out: acceleration until halfway, then deceleration.
*/
template<class T>
class IOQuint
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d)
	{
		T tValue = 0;

		if ((t/=d/2) < 1) 
		{
			tValue =  c/2*t*t*t*t*t + b;
		}
		else
		{
			t-=2;
			tValue = c/2*(t*t*t*t*t + 2) + b;
		}
		
		return tValue;
	}
};

/**
* Easing equation function for a quintic (t^5) easing out/in: deceleration until halfway, then acceleration.
*/
template<class T>
class OIQuint
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d)
	{
		T tValue = 0;

		if (t < d/2) 
			tValue = OQuint<T>::calculate(t*2, b, c/2, d);
		else
			tValue = IQuint<T>::calculate((t*2)-d, b+c/2, c/2, d);

		return tValue;
	}
};

/**
* Easing equation function for a sinusoidal (sin(t)) easing in: accelerating from zero velocity.
*/
template<class T>
class ISine
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) 
	{
		return -c * cosf(t/d * (PI/2)) + c + b;
	}
};

//Added by arch_jslin
/**
* Easing equation function for a sinusoidal (sin(t)) easing in: going through a sine cycle
*                                                               (back to the original point)
*/
template<class T>
class SineCirc
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d)
	{
		return static_cast<T>(c * (-cosf(t/d * (PI*2))+1)/2 + b);
	}
};

/**
* Easing equation function for a sinusoidal (sin(t)) easing out: decelerating from zero velocity.
*/
template<class T>
class OSine
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d)
	{
		return c * sinf(t/d * (PI/2)) + b;
	}
};

/**
* Easing equation function for a sinusoidal (sin(t)) easing in/out: acceleration until halfway, then deceleration.
*/
template<class T>
class IOSine
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) 
	{
		return -c/2 * (cosf(PI*t/d) - 1) + b;
	}
};

/**
* Easing equation function for a sinusoidal (sin(t)) easing out/in: deceleration until halfway, then acceleration.
*/
template<class T>
class OISine
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) 
	{
		T tValue = 0;

		if (t < d/2) 
			tValue = OSine<T>::calculate(t*2, b, c/2, d);
		else
			tValue = ISine<T>::calculate((t*2)-d, b+c/2, c/2, d);

		return tValue;
	}
};

/**
* Easing equation function for an exponential (2^t) easing in: accelerating from zero velocity.
*/
template<class T>
class IExpo
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) 
	{
		T tValue = 0;

		if (t==0)
			tValue = b;
		else
			tValue = c * pow(2, 10 * (t/d - 1)) + b - c * 0.001f;

		return tValue;
	}
};

/**
* Easing equation function for an exponential (2^t) easing out: decelerating from zero velocity.
*/
template<class T>
class OExpo
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) 
	{
		T tValue = 0;

		if (t == d)
			tValue =  b+c;
		else
			tValue = c * 1.001f * (-pow(2, -10 * t/d) + 1) + b;

		return tValue;
	}
};

/**
* Easing equation function for an exponential (2^t) easing in/out: acceleration until halfway, then deceleration.
*/
template<class T>
class IOExpo
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d)
	{
		if (t==0) 
			return b;
		if (t==d)
			return b+c;
		if ((t/=d/2) < 1) 
			return c/2 * pow(2, 10 * (t - 1)) + b - c * 0.0005f;

		return c/2 * 1.0005f * (-pow(2, -10 * --t) + 2) + b;
	}
};

/**
* Easing equation function for an exponential (2^t) easing out/in: deceleration until halfway, then acceleration.
*/
template<class T>
class OIExpo
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d)
	{
		if (t < d/2) return OExpo<T>::calculate(t*2, b, c/2, d);
		return IExpo<T>::calculate((t*2)-d, b+c/2, c/2, d);
	}
};

/**
* Easing equation function for a circular (sqrt(1-t^2)) easing in: accelerating from zero velocity.
*/
template<class T>
class ICirc
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) {
		t/=d;
		return -c * (sqrt(1 - t*t) - 1) + b;
	}
};

/**
* Easing equation function for a circular (sqrt(1-t^2)) easing out: decelerating from zero velocity.
*/
template<class T>
class OCirc
{
public:

	static T calculate(float t, T const& b, T const& c, float const& d) {
		t=(t/d)-1;
		return c * sqrt(1 - t*t) + b;
	}
};

/**
* Easing equation function for a circular (sqrt(1-t^2)) easing in/out: acceleration until halfway, then deceleration.
*/
template<class T>
class IOCirc
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) {
		if ((t/=d/2) < 1) return -c/2 * (sqrt(1 - t*t) - 1) + b;
		t-=2;
		return c/2 * (sqrt(1 - t*t) + 1) + b;
	}
};

/**
* Easing equation function for a circular (sqrt(1-t^2)) easing out/in: deceleration until halfway, then acceleration.
*/
template<class T>
class OICirc
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) {
		if (t < d/2) return OCirc<T>::calculate(t*2, b, c/2, d);
		return ICirc<T>::calculate((t*2)-d, b+c/2, c/2, d);
	}
};


//helper function
template <class T>
inline float length(T const& t) { return abs(t); }
//template <>
//inline float length<vec2>(vec2 const& t) { return t.getLength(); }
//template <>
//inline float length<vec3>(vec3 const& t) { return t.getLength(); }

/**
* Easing equation function for an elastic (exponentially decaying sine wave) easing in: accelerating from zero velocity.
* @param a        Amplitude.
* @param p        Period.
*/
template<class T>
class IElastic
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d,
		T a = T(), float p=.0f) //additional param.
	{
		if(t==0) 
			return b;  
		if((t/=d)==1) 
			return b+c;  

		if(p==.0f) 
			p=d*.3;
		float s = .0f;
		float la = length(a);
		float lc = length(c); 
		bool sign = ((c/a) >= T()) ? true : false;

		if( la==.0f || la < lc ) 
		{
			a=c; s=p/4;
		}
		else 
		{
			s = p / (2*PI) * asinf (lc/la * (sign?1:-1));
		}

		t-=1;
		return -(a*pow(2,10*t) * sinf( (t*d-s)*(2*PI)/p )) + b;
	}
};

/**
* Easing equation function for an elastic (exponentially decaying sine wave) easing out: decelerating from zero velocity.
* @param a        Amplitude.
* @param p        Period.
*/
template<class T>
class OElastic
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d, T a = T(), float p=.0f) //additional param.
	{
		if(t==0) 
			return b; 

		if((t/=d)==1) 
			return b+c; 

		if(p==.0f) p=d*.3;

		float s = .0f;
		float la = length(a);
		float lc = length(c); 
		bool sign = ((c/a) >= T()) ? true : false;

		if( la==.0f || la < lc ) 
		{
			a=c; 
			s=p/4; 
		}
		else
		{
			s = p / (2*PI) * asinf (lc/la * (sign?1:-1));
		}

		return (a*pow(2,-10*t) * sinf( (t*d-s)*(2*PI)/p ) + c + b);
	}
};

/**
* EEasing equation function for an elastic (exponentially decaying sine wave) easing in/out: acceleration until halfway, then deceleration.
* @param a        Amplitude.
* @param p        Period.
*/
template<class T>
class IOElastic
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d, T a = T(), float p=.0f) //additional param.
	{
		if (t==0) 
			return b;  

		if ((t/=d/2)==2)
			return b+c; 

		if(p==.0f) 
			p=d*(.3*1.5);

		float s = .0f;
		float la = length(a);
		float lc = length(c);
		bool sign = ((c/a) >= T()) ? true : false;

		if ( length(a)==.0f || length(a) < length(c) )
		{ 
			a=c; s=p/4; 
		}
		else 
		{
			s = p/(2*PI) * asinf (lc/la * (sign?1:-1));
		}

		t-=1;
		if (t < 1) 
			return -.5*(a*pow(2,10*t) * sinf( (t*d-s)*(2*PI) / p )) + b;

		return a*pow(2,-10*t) * sinf( (t*d-s)*(2*PI)/p )*.5 + c + b;
	}
};

/**
* Easing equation function for an elastic (exponentially decaying sine wave) easing out/in: deceleration until halfway, then acceleration.
* @param a        Amplitude.
* @param p        Period.
*/
template<class T>
class OIElastic
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d, T const& a = T(), float const& p=.0f) //additional param.
	{
		if (t < d/2) 
			return OElastic<T>::calculate(t*2, b, c/2, d, a, p);

		return IElastic<T>::calculate((t*2)-d, b+c/2, c/2, d, a, p);
	}
};

/**
* Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in: accelerating from zero velocity.
* @param s        Overshoot ammount: higher s means greater overshoot (0 produces cubic easing with no overshoot,
*              and the default value of 1.70158 produces an overshoot of 10 percent).
*/
template<class T>
class IBack
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d, float s=.0f) //additional param.
	{
		if (s==.0f) 
			s = 1.70158f;

		t/=d;
		return c*t*t*((s+1)*t - s) + b;
	}
};

/**
* Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out: decelerating from zero velocity.
* @param s        Overshoot ammount: higher s means greater overshoot (0 produces cubic easing with no overshoot,
*              and the default value of 1.70158 produces an overshoot of 10 percent).
*/
template<class T>
class OBack
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d, float s=.0f) //additional param.
	{
		if (s==.0f) 
			s = 1.70158f;

		t=(t/d)-1;
		return c*(t*t*((s+1)*t + s) + 1) + b;
	}
};

/**
* Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in/out: acceleration until halfway, then deceleration.
* @param s        Overshoot ammount: higher s means greater overshoot (0 produces cubic easing with no overshoot,
*              and the default value of 1.70158 produces an overshoot of 10 percent).
*/
template<class T>
class IOBack
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d, float s=.0f) //additional param.
	{
		T tVaule = 0;

		if (s==.0f)
			s = 1.70158f;

		s*=1.525;

		if ((t/=d/2) < 1)
		{
			tVaule = c/2*(t*t*((s+1)*t - s)) + b;
		}
		else
		{
			t-=2;
			tVaule = c/2*(t*t*((s+1)*t + s) + 2) + b;
		}

		return tVaule;
	}
};

/**
*  Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out/in: deceleration until halfway, then acceleration.
*/
template<class T>
class OIBack
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d, float s=.0f) //additional param.
	{
		if (t < d/2) return OBack<T>::calculate(t*2, b, c/2, d, s);
		return IBack<T>::calculate((t*2)-d, b+c/2, c/2, d, s);
	}
};

/**
* Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out: decelerating from zero velocity.
*/
template<class T>
class OBounce
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) {
		if ((t/=d) < (1/2.75f)) 
		{
			return c*(7.5625f*t*t) + b;
		} 
		else if (t < (2/2.75f))
		{
			t-=(1.5f/2.75f);
			return c*(7.5625f*t*t + .75f) + b;
		} 
		else if (t < (2.5f/2.75f))
		{
			t-=(2.25f/2.75f);
			return c*(7.5625f*t*t + .9375f) + b;
		} 
		else 
		{
			t-=(2.625f/2.75f);
			return c*(7.5625f*t*t + .984375f) + b;
		}
	}
};

/**
* Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in: accelerating from zero velocity.
*/
template<class T>
class IBounce
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d)
	{
		return c - OBounce<T>::calculate(d-t, T(), c, d) + b;
	}
};

/**
* Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in/out: acceleration until halfway, then deceleration.
*/
template<class T>
class IOBounce
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d) 
	{
		T tValue = 0;

		if (t < d/2) 
			tValue = IBounce<T>::calculate(t*2, T(), c, d) * .5 + b;
		else
			tValue = OBounce<T>::calculate(t*2-d, T(), c, d) * .5 + c*.5 + b;
		
		return  tValue;
	}
};

/**
* Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out/in: deceleration until halfway, then acceleration.
*/
template<class T>
class OIBounce
{
public:
	static T calculate(float t, T const& b, T const& c, float const& d)
	{
		T tValue = 0;

		if (t < d/2) 
			tValue = OBounce<T>::calculate(t*2, b, c/2, d);
		else
			tValue = IBounce<T>::calculate((t*2)-d, b+c/2, c/2, d);

		return tValue;
	}
};

// 抛物线
template<class T>
class Parabola
{
public:

	// 抛物线运行,a加速度
	// b 起始点位置
	// c 相对起始的位移（正负都可以）
	// hof_t 到达最高点时时间
	static T calculate(float t, T const& b, T const& c, float const& d, float a)
	{
		//垂直公式 v * t - (1/2) * a * t * t + b= s

		T tValue = 0;
		if (a == 0) a = 0.05;

		a /= 2;

		float fSpeed = (c + a * d * d) / d;
		tValue = (fSpeed - a * t) * t + b;

		return tValue;
	}
};

END_NAME_SPACE(ease_tween)
///////////////////////////////////////////////////////////////////////////
//
#endif // __CEaseTween__h__