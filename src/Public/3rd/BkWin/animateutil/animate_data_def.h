/* -----------------------------------------------------------------------
 *  File		:	animate_data_def.h
 *  Author	    :	hexujun
 *  Datet   	:	2014/2/18 11:03
 *  Brief		:	
 *
 * ----------------------------------------------------------------------
 * ----------------------------------------------------------------------*/

#ifndef __animate_data_def__h__
#define __animate_data_def__h__
//
///////////////////////////////////////////////////////////////////////////
namespace ease_tween
{

template<class TEaseTween>
class KEaseTweenSimpleAnimateData
{
public:
    KEaseTweenSimpleAnimateData()
    {
        Clear();
    }

    virtual void Clear()
    {
        m_fCurTime		= 0;
        m_fTotalTime	= 0;
        m_fStartValue	= 0;
        m_fChangeValue  = 0;
    }

    float AddTime(float fAddTime)
    {
        float fOldCurrTime = m_fCurTime;

        m_fCurTime += fAddTime;
        if (m_fCurTime > m_fTotalTime)
        {
            m_fCurTime = m_fTotalTime;
        }

        return fOldCurrTime;
    }


    virtual void SetValue(float fTotalTime, float fStartValue, float fChangeValue)
    {
        m_fStartValue  = fStartValue;
        m_fTotalTime   = fTotalTime;
        m_fChangeValue = fChangeValue;
    }

    void SetParam(void* pParam)
    {
        m_pParam = pParam;
    }

    void* GetParam() const
    {
        return m_pParam;
    }

    float Caculate() const
    {
        return TEaseTween::calculate(m_fCurTime, m_fStartValue, m_fChangeValue, m_fTotalTime);
    }

    float Caculate(float fStartValue, float fChangeValue) const
    {
        return TEaseTween::calculate(m_fCurTime, fStartValue, fChangeValue, m_fTotalTime);
    }

    float IsStop() const
    {
        return m_fCurTime >= m_fTotalTime;
    }

    float GetChangeValue() const
    {
        return m_fChangeValue;
    }

    float GetStartValue() const
    {
        return m_fStartValue;
    }

    void SetCurrentTime(float fCur)
    {
        m_fCurTime = fCur;
    }

    float GetCurrentTime() const
    {
        return m_fCurTime;
    }

    float GetTotalTime() const
    {
        return m_fTotalTime;
    }

protected:
    float  m_fCurTime;
    float  m_fTotalTime;
    float  m_fStartValue;
    float  m_fChangeValue;
    void*  m_pParam;
};

template<class T, class TEaseTween >
class KEaseTweenAnimateData
    : public KEaseTweenSimpleAnimateData<TEaseTween>
{
public:

	typedef int (T::*pfun)(void*, float);

	KEaseTweenAnimateData()
	{
	}

	virtual void Clear()
	{
        KEaseTweenSimpleAnimateData<TEaseTween>::Clear();

		m_pObj   = NULL;
		m_pParam = NULL;
		m_pFunc  = NULL;
	}

    virtual void SetValue(float fTotalTime, float fStartValue, float fChangeValue)
    {
        KEaseTweenSimpleAnimateData<TEaseTween>::SetValue(fTotalTime, fStartValue, fChangeValue);

        m_pObj  = NULL;
        m_pFunc = NULL;
    }

	void SetValue(T* pObject, int (T::*f)(void*, float), float fTotalTime, float fStartValue, float fChangeValue)
	{
		m_fStartValue  = fStartValue;
		m_fTotalTime   = fTotalTime;
		m_fChangeValue = fChangeValue;

		m_pObj  = pObject;
		m_pFunc = f;
	}

	int CallFunction(float fValue)
	{
		int nRetCode = -1;

		if (m_pObj && m_pFunc)
		{
			nRetCode = (m_pObj->*m_pFunc)(m_pParam, fValue);
		}

		return nRetCode;
	}

private:
	T*	   m_pObj;
	pfun   m_pFunc;
};

}//end ease_tween 
///////////////////////////////////////////////////////////////////////////
//
#endif // __animate_data_def__h__