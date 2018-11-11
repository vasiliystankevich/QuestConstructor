using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quest.Controls.QuestConstructor;
using Quest.Controls.QuestInterview;
using Quest.Core.Helpers;
using Quest.Core.Model;
using Quest.Core.Services;
using Quest.Localizable;

namespace Quest.Controls.Presenter
{
    public interface IQuestConstructor
    {
        void Build();
        void btOpen_Click();
        void btSave_Click();
        void btAddQuest_Click();
        void btRun_Click(IWin32Window owner);
        void btExportCSV_Click(IWin32Window owner);
        void MainForm_FormClosing();
    }

    public class QuestConstructor : IQuestConstructor
    {
        public QuestConstructor(FlowLayoutPanel pnMain, ToolStripButton btSave)
        {
            this.pnMain = pnMain;
            this.btSave = btSave;
            changed = false;
            questionnaire = new Questionnaire();
        }

        public void Build()
        {
            var helper = new ControlHelper(pnMain);

            pnMain.Controls.Clear();
            foreach (var quest in questionnaire)
            {
                var pn = new QuestPanel();
                pn.Build(questionnaire, quest);

                pn.QuestionnaireListChanged += () =>
                {
                    changed = true;
                    Build();
                };

                pn.Changed += () =>
                {
                    changed = true;
                    UpdateInterface();
                };

                pnMain.Controls.Add(pn);
            }
            UpdateInterface();

            helper.ResumeDrawing();
        }

        private void UpdateInterface()
        {
            btSave.Enabled = changed;
        }

        private void LoadQuestionnaireFromFile(string filePath)
        {
            questionnaire = SaverLoader.Load<Questionnaire>(filePath);
            changed = false;
            Build();
        }

        private void SaveQuestionnaireToFile(string filePath)
        {
            new QuestionnaireValidator().Validate(questionnaire);
            changed = false;
            SaverLoader.Save(questionnaire, filePath);
            UpdateInterface();
        }

        private void RunQuestionnaire(IWin32Window owner)
        {
            new QuestionnaireValidator().Validate(questionnaire);
            new MainForm(questionnaire).ShowDialog(owner);
        }

        public void btOpen_Click()
        {
            //Опросник
            AskAboutSaveCurrentQuestionnaire();
            var filter = I18NEngine.GetString("quest.constructor", "filedialog_filter_quiz_template");
            var ofd = new OpenFileDialog { Filter = filter };
            if (ofd.ShowDialog() == DialogResult.OK)
                LoadQuestionnaireFromFile(ofd.FileName);
        }

        private void AskAboutSaveCurrentQuestionnaire()
        {
            if (!changed) return;
            var text = I18NEngine.GetString("quest.constructor", "ask_about_save_current_questionnaire_message_text");
            var caption = I18NEngine.GetString("quest.constructor",
                "ask_about_save_current_questionnaire_message_caption");
            if (MessageBox.Show(text, caption, MessageBoxButtons.YesNo) == DialogResult.Yes)
                btSave.PerformClick();
        }

        public void btSave_Click()
        {
            var filter = I18NEngine.GetString("quest.constructor", "filedialog_filter_quiz_template");
            var sfd = new OpenFileDialog { Filter = filter };
            if (sfd.ShowDialog() == DialogResult.OK)
                SaveQuestionnaireToFile(sfd.FileName);
        }

        public void btAddQuest_Click()
        {
            var questTitle = I18NEngine.GetString("quest.core", "models_quest_title");
            QuestionnaireManipulator.AddNewQuest(questionnaire, questTitle);
            changed = true;
            Build();
        }

        public void btRun_Click(IWin32Window owner)
        {
            RunQuestionnaire(owner);
        }

        public void btExportCSV_Click(IWin32Window owner)
        {
            var message = string.Empty;
            var ofd = new OpenFolderDialog();
            if (ofd.ShowDialog(owner) != DialogResult.OK) return;
            var questionnairePattern = I18NEngine.GetString("quest.constructor", "findfiles_questionnaire_pattern");
            var anketas = Directory.GetFiles(ofd.Folder, questionnairePattern).Select(SaverLoader.Load<Anketa>).ToList();
            if (anketas.Count != 0)
            {
                var filter = I18NEngine.GetString("quest.constructor", "filedialog_filter_csv_file_template");
                var sfd = new SaveFileDialog { Filter = filter };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Export.ExportToCsv(anketas, sfd.FileName);
                    var messageTemplate = I18NEngine.GetString("quest.constructor",
                        "btexportcsv_click_find_questionnaire_message_template");
                    message = string.Format(messageTemplate, anketas.Count);
                }
            }
            else
            {
                message = I18NEngine.GetString("quest.constructor",
                    "btexportcsv_click_not_find_questionnaire_message");
            }
            MessageBox.Show(message);
        }

        public void MainForm_FormClosing()
        {
            AskAboutSaveCurrentQuestionnaire();
        }

        private FlowLayoutPanel pnMain { get; }
        private ToolStripButton btSave { get; }
        private bool changed { get; set; }
        private Questionnaire questionnaire { get; set; }
    }
}
