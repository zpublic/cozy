using CozyMind.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyMind.TestData
{
    class TestData1
    {
        private MindChart chart = new MindChart()
        {
            Title = new MindText()
            {
                Context = "TestData1"
            },
            Root = new MindRoot()
            {
                Surface = new MindNodeSurface(),
                Text = new MindNodeText()
                {
                    Context = "MindRoot",
                    Size = 12
                },
                Childs = new List<MindNode>()
                {
                    new MindNode()
                    {
                        Surface = new MindNodeSurface(),
                        Text = new MindNodeText()
                        {
                            Context = "MindNode1",
                            Size = 10
                        },
                    },
                    new MindNode()
                    {
                        Surface = new MindNodeSurface(),
                        Text = new MindNodeText()
                        {
                            Context = "MindNode2",
                            Size = 10
                        },
                    }
                }
            }
        };
        public MindChart Chart
        {
            get { return chart; }
        }
    }
}
