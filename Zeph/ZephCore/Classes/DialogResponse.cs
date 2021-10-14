using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {
    /// <summary>
    /// The class which handles the players responses to the dialogResponse
    /// </summary>
    public class DialogResponse : Zeph.Core.Classes.ClassBase<DialogResponse> {
        const string TABLE = "dialogResponse";

        /// <summary>
        /// ID of the dialogResponse
        /// </summary>
        public int dr_ID = -1;
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
        /// If the <see cref="dr_ResponseType"/> is <see cref="Enums.DialogResponseType.ReceiveQuest"/> this is the quest to receive. This is a ID because the QuestingSystem is separate to the core of Zeph. This allows the basics of Zeph to be removed from the potentially extra things.
        /// </summary>
        public Quest dr_Quest = null;
        /// <summary>
        /// The order this response is to be shown to the player.
        /// </summary>
        public int dr_Order = -1;

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete("dialogResponse", id);
        }

        public new static DialogResponse Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read("dialogResponse", id);
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
                try {
                    var obj = new DialogResponse();
                    obj.dr_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.dr_Response = GeneralOps.ConvertDatabaseField<string>(dic, "dr_Response");
                    obj.dr_ResponseType = (Enums.DialogResponseType)GeneralOps.ConvertDatabaseField<int>(dic, "dr_ResponseType");
                    obj.dr_Dialog = new Dialog() { d_ID = GeneralOps.ConvertDatabaseField<int>(dic, "dr_Dialog") };
                    obj.dr_NextDialog = GeneralOps.IsDatabaseFieldNull(dic, "dr_NextDialog") ? null : new Dialog() { d_ID = GeneralOps.ConvertDatabaseField<int>(dic, "dr_NextDialog") };
                    obj.dr_Quest = GeneralOps.IsDatabaseFieldNull(dic, "dr_Quest") ? null : new Quest() { q_ID = GeneralOps.ConvertDatabaseField<int>(dic, "dr_Quest") };
                    obj.dr_Order = GeneralOps.ConvertDatabaseField<int>(dic, "dr_Order");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("DialogResponse", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public new static DialogResponse Save(DialogResponse obj) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                dic["id"] = obj.dr_ID;
                dic["dr_Response"] = obj.dr_Response;
                dic["dr_ResponseType"] = (int)obj.dr_ResponseType;
                dic["dr_Dialog"] = obj.dr_Dialog.d_ID;
                dic["dr_NextDialog"] = obj.dr_NextDialog == null ? null : (object)obj.dr_NextDialog.d_ID;
                dic["dr_QuestGUID"] = obj.dr_Quest == null ? null : (object)obj.dr_Quest.q_ID;
                dic["dr_Order"] = obj.dr_Order;
                return ReadFromDictionary(db.Save("dialogResponse", obj.dr_ID, dic));
            }
        }

        #endregion
    }
}
