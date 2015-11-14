using CozyGod.Game.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyGod.Game.Model;

namespace CozyGod.Game.Raffle
{
    public class RaffleImpl : IRaffle
    {
        float[] m_raffleProbabilityArray;
        private ICardLibrary mCL;
        private ICozyGodEngine m_engine;

        public bool Init(ICozyGodEngine engine)
        {
            bool bRet = false;
            do
            {
                m_engine = engine;
                mCL = engine.GetCardLibrary();
                if (mCL == null)
                {
                    break;
                }
                LoadRaffleProbabilityArray();
                if (m_raffleProbabilityArray == null)
                {
                    break;
                }
                bRet = true;
            } while (false);
            return bRet;
        }

        /// <summary>
        /// 抽卡接口
        /// </summary>
        /// <param name="rank">保底等级</param>
        /// <returns>随机获取一张大于等于保底等级rank值的卡牌</returns>
        public Card Draw(int rank = 0)
        {
            Card cardRet = null;
            Random rd = new Random();
            float _fRd = (float)rd.NextDouble();
            int rankRet = -1;
            for (int i = rank; i < m_raffleProbabilityArray.Length; i++)
            {
                if (_fRd < m_raffleProbabilityArray[i])
                {
                    rankRet = i + 1;
                }
                else
                {
                    _fRd -= m_raffleProbabilityArray[i];
                }
            }

            if (rankRet == -1)
            {
                rankRet = rank;
            }

            if (mCL.Get() != null && mCL.Get().Cards[rankRet] != null)
            {
                int _iRd = rd.Next(0, mCL.Get().Cards[rankRet].Count);
                cardRet = mCL.Get().Cards[rankRet][_iRd];
            }
            return cardRet;
        }

        public Card[] PentaDraw()
        {
            Card [] cardArrayRet = new Card[5];

            cardArrayRet[0] = Draw(1);
            for(int i = 1; i < cardArrayRet.Length; i++)
            {
                cardArrayRet[i] = Draw();
            }


            // shuffle 打乱顺序
            for(int i = 0; i < cardArrayRet.Length; i++)
            {
                Random rd = new Random();
                int _iRdIndex = rd.Next(0, cardArrayRet.Length);

                Card tmp = cardArrayRet[i];
                cardArrayRet[i] = cardArrayRet[_iRdIndex];
                cardArrayRet[_iRdIndex] = tmp;
            }

            return cardArrayRet;
        }

        void LoadRaffleProbabilityArray()
        {
            m_raffleProbabilityArray = new float[4];
            m_raffleProbabilityArray[0] = 0.1f;     //1级1/10
            m_raffleProbabilityArray[1] = 0.02f;    //2级1/50
            m_raffleProbabilityArray[2] = 0.005f;   //3级1/200
            m_raffleProbabilityArray[3] = 0.001f;   //4级1/1000
        }
    }
}
