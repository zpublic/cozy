using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Engine
{
    public class CozyActionManager
    {
        Dictionary<CozyNode, List<CozyAction>> Targets = new Dictionary<CozyNode, List<CozyAction>>();
        public void AddAction(CozyAction action, CozyNode node, bool paused = false)
        {
            List<CozyAction> elemt = null;
            if(Targets.ContainsKey(node))
            {
                elemt = Targets[node];
            }
            else
            {
                elemt = new List<CozyAction>();
                Targets[node] = elemt;
            }

            elemt.Add(action);

            action.StartWithTarget(node);
        }

        public void RemoveAction(CozyAction action)
        {
            CozyNode target = action.Target;
            if(target != null)
            {
                if(Targets.ContainsKey(target))
                {
                    var elemt = Targets[target];

                    int index = elemt.IndexOf(action);
                    if(index != -1)
                    {
                        elemt.RemoveAt(index);
                    }
                }
            }
        }

        public void RemoveAllActionsWithTarget(CozyNode target)
        {
            if(Targets.ContainsKey(target))
            {
                Targets[target].Clear();
                Targets.Remove(target);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach(var obj in Targets)
            {
                foreach(var act in obj.Value)
                {
                    act.Update(gameTime);
                }
            }
        }
    }
}
