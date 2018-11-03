using System;
using System.Windows.Forms;
using Quest.Core.Model;
using Quest.Core.Services;

namespace Quest.Controls.QuestConstructor
{
    public partial class ConditionForm : Form
    {
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

            try
            {
                new ConditionCalculator().Check(questionnaire, expression);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            condition.Expression = expression;
            Changed();
            DialogResult = DialogResult.OK;
        }

        private Condition condition;
        private Questionnaire questionnaire;

        public event Action Changed = delegate { };
    }
}
