using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Quest.Controls;
using Quest.Controls.QuestConstructor;
using Quest.Core.Helpers;
using Quest.Core.Model;
using Quest.Core.Services;

namespace Quest.Constructor
{
    public partial class MainForm : Form
    {
        private Questionnaire questionnaire = new Questionnaire();
        private bool changed;

        public MainForm()
        {
            InitializeComponent();

            Build();
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
            AskAboutSaveCurrentQuestionnaire();
            var ofd = new OpenFileDialog() {Filter = "Опросник|*.q"};
            if (ofd.ShowDialog() == DialogResult.OK)
                LoadQuestionnaireFromFile(ofd.FileName);
        }

        private void AskAboutSaveCurrentQuestionnaire()
        {
            if (changed)
            {
                if (MessageBox.Show("В текущем опроснике есть несохраненные данные.\r\nВы хотите сохранить опросник?", "Несохраненные изменения", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    btSave.PerformClick();
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog() { Filter = "Опросник|*.q" };
            if (sfd.ShowDialog() == DialogResult.OK)
                SaveQuestionnaireToFile(sfd.FileName);
        }

        private void btAddQuest_Click(object sender, EventArgs e)
        {
            new QuestionnaireManipulator().AddNewQuest(questionnaire);
            changed = true;
            Build();
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            RunQuestionnaire();
        }

        private void btExportCSV_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFolderDialog();
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                var anketas = Directory.GetFiles(ofd.Folder, "*.a").Select(path => SaverLoader.Load<Anketa>(path)).ToList();
                if (anketas.Count == 0)
                {
                    MessageBox.Show("В этой папке не найдены анкеты");
                    return;
                }

                var sfd = new SaveFileDialog() {Filter = "CSV|*.csv"};
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    new Export().ExportToCSV(anketas, sfd.FileName);
                    MessageBox.Show("Экспортировано " + anketas.Count + " анкет");
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AskAboutSaveCurrentQuestionnaire();
        }
    }
}
