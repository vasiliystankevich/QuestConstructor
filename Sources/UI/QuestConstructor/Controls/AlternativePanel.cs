using System;
using System.Windows.Forms;
using QuestCoreNS;

namespace QuestConstructorNS
{
    public partial class AlternativePanel : UserControl
    {
        private Quest quest;
        private Alternative alt;
        private Questionnaire questionnaire;
        private int updating;

        public event Action AlternativeListChanged = delegate { };
        public event Action Changed = delegate { };

        public AlternativePanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Строим интерфейс
        /// </summary>
        public void Build(Questionnaire questionnaire, Quest quest, Alternative alt)
        {
            this.quest = quest;
            this.alt = alt;
            this.questionnaire = questionnaire;

            //выставляем флажок обновления
            updating++;

            //переносим данные из объекта в интерфейс
            tbId.Text = alt.Code.ToString();
            tbTitle.Text = alt.Title;
            lbCondition.Text = alt.Condition?.ToString() ?? "Если...";

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
            alt.Code = int.Parse(tbId.Text);
            alt.Title = tbTitle.Text;

            //сигнализируем наверх о том, что объект изменился
            Changed();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить альтернативу?", "Удаление альтернативы", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //удаляем альтернативу
                new QuestManipulator().RemoveAlt(quest, alt);
                //сигнализируем наверх о том, что список поменялся
                AlternativeListChanged();
            }
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            //передвигаем альтернативу вверх по списку
            new QuestManipulator().MoveAlt(quest, alt, -1);
            //сигнализируем наверх о том, что список поменялся
            AlternativeListChanged();
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            //передвигаем альтернативу вниз по списку
            new QuestManipulator().MoveAlt(quest, alt, +1);
            //сигнализируем наверх о том, что список поменялся
            AlternativeListChanged();
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            //обновляем объект
            UpdateObject();
        }

        private void lbCondition_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //создаем условие, если его еще нет
            if (alt.Condition == null)
                alt.Condition = new Condition();

            //показываем форму редактора условия
            var form = new ConditionForm();
            form.Build(questionnaire, alt.Condition);
            form.Changed += () => Changed();//сигнализируем наверх о том, что объект поменялся
            form.ShowDialog(this);//показываем конструктор условий

            //перестриваем интерфйес
            Build(questionnaire, quest, alt);
        }
    }
}
