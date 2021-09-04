using System;
using System.Collections.Generic;
using System.Text;
using Zeph.Core.Classes;

namespace Zeph.QuestingSystem.Classes {
    /// <summary>
    /// To be honest, I think this is overkill for what it is. This could just very easily be done using the normal DialogResponse class. But I've done some work on it so I'm leaving it here in case I need it.. But I probs won't.
    /// Also think I missed out on something crucial to C#... https://stackoverflow.com/questions/12565736/convert-base-class-to-derived-class
    /// </summary>
    //public class DialogResponseQuest : DialogResponse {
    //    public Quest dr_Quest = null;

    //    public new static DialogResponseQuest Read(Guid guid) {
    //        var dr = DialogResponse.Read(guid);

    //    }

    //    public new static List<DialogResponseQuest> Read() {
    //        var listDR = DialogResponse.Read();
    //        var listDRQ = new List<DialogResponseQuest>();
    //        foreach (var dr in listDR) {
    //            var drq = (DialogResponseQuest)dr;
    //            drq.dr_Quest = dr.dr_QuestGUID == Guid.Empty ? null : new Quest() { q_GUID = dr.dr_QuestGUID };
    //            listDRQ.Add(drq);
    //        }
    //        return listDRQ;
    //    }

    //    public static new DialogResponseQuest ReadFromDictionary(Dictionary<string, object> dic) {
    //        var dr = DialogResponse.ReadFromDictionary(dic);

    //        var obj = (DialogResponseQuest)dr;
    //        obj.dr_Quest = dr.dr_QuestGUID == Guid.Empty ? null : new Quest() { q_GUID = dr.dr_QuestGUID };
    //        return obj;
    //    }

    //    public static DialogResponseQuest Save(DialogResponseQuest obj) {
    //        var dr = (DialogResponse)obj;
    //        dr.dr_QuestGUID = obj.dr_Quest == null ? Guid.Empty : obj.dr_Quest.q_GUID;

    //        var _dr = DialogResponse.Save(dr);
    //        var _obj = (DialogResponseQuest)dr;
    //        _obj.dr_Quest = dr.dr_QuestGUID == Guid.Empty ? null : new Quest() { q_GUID = _dr.dr_QuestGUID };
    //        return _obj;
    //    }

    //    public static explicit operator DialogResponseQuest(DialogResponse dr) {
    //        var obj = (DialogResponseQuest)dr;
    //        obj.dr_Quest = dr.dr_QuestGUID == Guid.Empty ? null : new Quest() { q_GUID = dr.dr_QuestGUID };
    //        return obj;
    //    }
    //}
}
