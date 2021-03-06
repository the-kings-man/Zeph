using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Dialog {
    public class DialogSystem {
        /// <summary>
        /// Register a response for a piece of dialog
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static DialogSystemResponse Respond(Classes.DialogResponse response) {
            switch (response.dr_ResponseType) {
                case Enums.DialogResponseType.Close:
                    return new DialogSystemResponse() {
                        response = response,
                        type = response.dr_ResponseType,
                        nextDialog = null,
                        quest = null
                    };
                case Enums.DialogResponseType.NextDialog:
                    return new DialogSystemResponse() {
                        response = response,
                        type = response.dr_ResponseType,
                        nextDialog = Classes.Dialog.Read(response.dr_NextDialog.d_ID),
                        quest = null
                    };
                case Enums.DialogResponseType.ReceiveQuest:
                    return new DialogSystemResponse() {
                        response = response,
                        type = response.dr_ResponseType,
                        nextDialog = null,
                        quest = Classes.Quest.Read(response.dr_Quest.q_ID),   
                    };
                case Enums.DialogResponseType.HandInQuest:
                    return new DialogSystemResponse() {
                        response = response,
                        type = response.dr_ResponseType,
                        nextDialog = null,
                        quest = Classes.Quest.Read(response.dr_Quest.q_ID),
                    };
                default:
                    return null;
            }
        }

        /// <summary>
        /// Get all dialogs the NPC can offer
        /// </summary>
        /// <param name="npc"></param>
        /// <returns></returns>
        public static List<Classes.Dialog> AllDialogForNPC(Classes.NPC npc) {
            var lstD = Classes.Dialog.Read();
            var lstReturn = new List<Classes.Dialog>();

            foreach (var d in lstD) {
                if (d.d_NPC.npc_ID == npc.npc_ID) {
                    lstReturn.Add(d);
                }
            }

            return lstReturn;
        }

        /// <summary>
        /// Get all dialogs the NPC can offer
        /// </summary>
        /// <param name="npc"></param>
        /// <returns></returns>
        public static List<Classes.Dialog> CurrentDialogForNPC(Classes.NPC npc, Classes.Player player) {
            var lstD = Classes.Dialog.Read();
            var lstReturn = new List<Classes.Dialog>();

            foreach (var d in lstD) {
                if (d.d_NPC.npc_ID == npc.npc_ID && d.d_IsInitial) {
                    lstReturn.Add(d);
                }
            }

            return lstReturn;
        }
    }

    /// <summary>
    /// Used to help with managing the after effects of responding to a piece of dialog. Will give the front end of the application the data it requires to do whatever it needs to do.
    /// </summary>
    public class DialogSystemResponse {
        public Classes.DialogResponse response;

        public Enums.DialogResponseType type;
        public Classes.Dialog nextDialog;
        public Classes.Quest quest;
    }
}
