using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core {
    public class QuestProgressResult {
        public List<QuestProgressObjectiveResult> objectiveResults = new List<QuestProgressObjectiveResult>();
        public List<Classes.PlayerQuest> questsFinished = new List<Classes.PlayerQuest>();
        public bool progressed = false;
    }
}
