using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Quest.Controls;
using Quest.Controls.QuestConstructor;
using Quest.Core.Helpers;
using Quest.Core.Model;
using Quest.Core.Services;
using Quest.Core.UI;
using Quest.Localizable;

namespace Quest.Constructor
{
    public partial class MainForm : Form, ILocalizableComponents
    {
        public MainForm()
        {
            InitializeComponent();
            Build();
            LocalizableComponents();
        }

        private void Build()
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

        private void RunQuestionnaire()
        {
            new QuestionnaireValidator().Validate(questionnaire);
            new Controls.QuestInterview.MainForm(questionnaire).ShowDialog(this);
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            //Опросник
            AskAboutSaveCurrentQuestionnaire();
            var filter = I18NEngine.GetString("quest.constructor", "filedialog_filter_quiz_template");
            var ofd = new OpenFileDialog {Filter = filter };
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

        private void btSave_Click(object sender, EventArgs e)
        {
            var filter = I18NEngine.GetString("quest.constructor", "filedialog_filter_quiz_template");
            var sfd = new OpenFileDialog { Filter = filter };
            if (sfd.ShowDialog() == DialogResult.OK)
                SaveQuestionnaireToFile(sfd.FileName);
        }

        private void btAddQuest_Click(object sender, EventArgs e)
        {
            var questTitle = I18NEngine.GetString("quest.core", "models_quest_title");
            QuestionnaireManipulator.AddNewQuest(questionnaire, questTitle);
            changed = true;
            Build();
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            RunQuestionnaire();
        }

        private void btExportCSV_Click(object sender, EventArgs e)
        {
            var message = string.Empty;
            var ofd = new OpenFolderDialog();
            if (ofd.ShowDialog(this) != DialogResult.OK) return;
            //var questionnairePattern = I18NEngine.GetString("quest.constructor", "findfiles_questionnaire_pattern");
            //var anketas = Directory.GetFiles(ofd.Folder, questionnairePattern).Select(SaverLoader.Load<Anketa>).ToList();
            //if (anketas.Count != 0)
            //{
            //    var filter = I18NEngine.GetString("quest.constructor", "filedialog_filter_csv_file_template");
            //    var sfd = new SaveFileDialog {Filter = filter};
            //    if (sfd.ShowDialog() == DialogResult.OK)
            //    {
            //        Export.ExportToCsv(anketas, sfd.FileName);
            //        var messageTemplate = I18NEngine.GetString("quest.constructor",
            //            "btexportcsv_click_find_questionnaire_message_template");
            //        message = string.Format(messageTemplate, anketas.Count);
            //    }
            //}
            //else
            //{
            //    message = I18NEngine.GetString("quest.constructor",
            //        "btexportcsv_click_not_find_questionnaire_message");
            //}
            MessageBox.Show(message);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AskAboutSaveCurrentQuestionnaire();
        }

        public void LocalizableComponents()
        {
        }

        Questionnaire questionnaire = new Questionnaire();
        bool changed;

    }
}
