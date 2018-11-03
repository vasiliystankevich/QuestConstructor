using System;
using System.Windows.Forms;
using QuestCoreNS;

namespace QuestConstructorNS
{
    public partial class QuestPanel : UserControl
    {
        private Questionnaire questionnaire;
        private Quest quest;
        private int updating;

        public event Action QuestionnaireListChanged = delegate { };
        public event Action Changed = delegate { };

        public QuestPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Строим интерфейс
        /// </summary>
        public void Build(Questionnaire questionnaire, Quest quest)
        {
            this.questionnaire = questionnaire;
            this.quest = quest;

            //выставляем флажок обновления
            updating++;

            //создаем хелпер отрисовки, останавливаем отрисовку
            var helper = new ControlHelper(this);

            //переносим данные из объекта в интерфейс
            tbId.Text = quest.Id;
            tbTitle.Text = quest.Title;
            lbCondition.Text = quest.Condition?.ToString() ?? "Если...";

            //инициализируем комбобокс с типом вопроса
            cbQuestType.DataSource = Enum.GetValues(typeof(QuestType));
            cbQuestType.SelectedItem = quest.QuestType;

            //строим список альтернатив
            //очищаем панель альтернатив
            pnAlternatives.Controls.Clear();
            //создаем контролы для каждой альтернативы
            foreach (var alt in quest)
            {
                //создаем usercontrol для альтренативы
                var pn = new AlternativePanel();
                //строим его
                pn.Build(questionnaire, quest, alt);
                //подписываемся на события
                pn.AlternativeListChanged += () =>
                {
                    Build(questionnaire, quest); //перестриваем интерфейс, если изменился список альтернатив
                    Changed(); //сигнализируем наверх о том, что объект изменился
                };
                pn.Changed += () => Changed(); //сигнализируем наверх о том, что объект изменился
                //добавляем на панель альтернатив
                pnAlternatives.Controls.Add(pn);
            }

            //восстанавливаем отрисовку
            helper.ResumeDrawing();

            //сбрасываем флажок обновления
            updating--;
        }

        /// <summary>
        /// Обновляем объект из интерфеса
        /// </summary>
        private void UpdateObject()
        {
            //если режим обновления интерфейса - не реагируем
            if (updating > 0) return;

            //переносим данные из интерфейса в объект
            quest.Id = tbId.Text;
            quest.Title = tbTitle.Text;
            quest.QuestType = (QuestType)cbQuestType.SelectedItem;

            //сигнализируем наверх о том, что объект изменился
            Changed();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить вопрос?", "Удаление вопроса", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //удаляем вопрос
                new QuestionnaireManipulator().RemoveQuest(questionnaire, quest);
                //сигнализируем наверх о том, что список вопросов поменялся
                QuestionnaireListChanged();
            }
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            //передвигаем вопрос вверх по списку
            new QuestionnaireManipulator().MoveQuest(questionnaire, quest, -1);
            //сигнализируем наверх о том, что список вопросов поменялся
            QuestionnaireListChanged();
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            //передвигаем вопрос вниз по списку
            new QuestionnaireManipulator().MoveQuest(questionnaire, quest, +1);
            //сигнализируем наверх о том, что список вопросов поменялся
            QuestionnaireListChanged();
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            //обновляем объект
            UpdateObject();
        }

        private void btAddAlt_Click(object sender, EventArgs e)
        {
            //добавляем новую альтернативу
            new QuestManipulator().AddNewAlt(quest);
            //перестраиваем интерфейс
            Build(questionnaire, quest);
        }

        private void lbCondition_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //создаем условие, если его еще нет
            if (quest.Condition == null)
                quest.Condition = new Condition();

            //показываем форму редактора условия
            var form = new ConditionForm();
            form.Build(questionnaire, quest.Condition);
            form.Changed += () => Changed();//сигнализируем наверх о том, что объект поменялся
            form.ShowDialog(this);//показываем конструктор условий

            //перестриваем интерфйес
            Build(questionnaire, quest);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            //e.Graphics.DrawLine(Pens.Gray, ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Right, ClientRectangle.Top);
        }
    }
}
