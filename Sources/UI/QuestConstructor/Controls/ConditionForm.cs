using System;
using System.Windows.Forms;
using QuestCoreNS;

namespace QuestConstructorNS
{
    public partial class ConditionForm : Form
    {
        private Condition condition;
        private Questionnaire questionnaire;

        public event Action Changed = delegate { };

        public ConditionForm()
        {
            InitializeComponent();
        }

        public void Build(Questionnaire questionnaire, Condition condition)
        {
            this.questionnaire = questionnaire;
            this.condition = condition;

            tbExpression.Text = condition.Expression;
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            var expression = tbExpression.Text.Trim();

            //проверяем корректность выражения
            try
            {
                new ConditionCalculator().Check(questionnaire, expression);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            //обновляем доменный объект
            condition.Expression = expression;
            //сигнализируем наверх о том, что объект поменялся
            Changed();
            //закрываем окно
            DialogResult = DialogResult.OK;
        }
    }
}
