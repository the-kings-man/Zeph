using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {
    /// <summary>
    /// The class which handles the players responses to the dialogResponse
    /// </summary>
    public class DialogResponse : Zeph.Core.Classes.ClassBase<DialogResponse> {
        /// <summary>
        /// GUID of the dialogResponse
        /// </summary>
        public Guid dr_GUID = Guid.Empty;
        /// <summary>
        /// The response of the player to the dialog
        /// </summary>
        public string dr_Response = "";
        /// <summary>
        /// The type of reaction this response has. E.g. Does it close the dialog session? Does it result in a new quest being handed out? Does it result in another response being handed out?
        /// </summary>
        public Enums.DialogResponseType dr_ResponseType = Enums.DialogResponseType.Close;
        /// <summary>
        /// The dialog this response is for
        /// </summary>
        public Dialog dr_Dialog = null;
        /// <summary>
        /// If the <see cref="dr_ResponseType"/> is <see cref="Enums.DialogResponseType.NextDialog"/> this is the dialog to go to next
        /// </summary>
        public Dialog dr_NextDialog = null;
        /// <summary>
        /// If the <see cref="dr_ResponseType"/> is <see cref="Enums.DialogResponseType.ReceiveQuest"/> this is the quest to receive. This is a GUID because the QuestingSystem is separate to the core of Zeph. This allows the basics of Zeph to be removed from the potentially extra things.
        /// </summary>
        public Guid dr_QuestGUID = Guid.Empty;


        #region File Access

        public new static bool Delete(Guid guid) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete("dialogResponse", guid);
        }

        public new static DialogResponse Read(Guid guid) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read("dialogResponse", guid);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<DialogResponse> Read() {
            var lst = new List<DialogResponse>();
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var _lst = db.Read("dialogResponse");
                foreach (var dic in _lst) {
                    var _dic = ReadFromDictionary(dic);
                    if (_dic != null) {
                        lst.Add(_dic);
                    }
                }
            }
            return lst;
        }

        public new static DialogResponse ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                var obj = new DialogResponse();
                obj.dr_GUID = (Guid)dic["dr_GUID"];
                obj.dr_Response = (string)dic["dr_Response"];
                obj.dr_ResponseType = (Enums.DialogResponseType)(int)dic["dr_ResponseType"];
                obj.dr_Dialog = new Dialog() { d_GUID = (Guid)dic["dr_Dialog"] };
                obj.dr_NextDialog = dic["dr_NextDialog"] == null ? null : new Dialog() { d_GUID = (Guid)dic["dr_NextDialog"] };
                obj.dr_QuestGUID = (Guid)dic["dr_QuestGUID"];
                return obj;
            } else {
                return null;
            }
        }

        public new static DialogResponse Save(DialogResponse obj) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                dic["dr_GUID"] = obj.dr_GUID;
                dic["dr_Response"] = obj.dr_Response;
                dic["dr_ResponseType"] = (int)obj.dr_ResponseType;
                dic["dr_Dialog"] = obj.dr_Dialog.d_GUID;
                dic["dr_NextDialog"] = obj.dr_NextDialog == null ? null : (object)obj.dr_NextDialog.d_GUID;
                dic["dr_QuestGUID"] = obj.dr_QuestGUID;
                return ReadFromDictionary(db.Save("dialogResponse", obj.dr_GUID, dic));
            }
        }

        #endregion
    }
}
