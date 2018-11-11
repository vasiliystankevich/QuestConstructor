using System;
using System.Windows.Forms;
using Quest.Core.Model;
using Quest.Core.Services;

namespace Quest.Controls.Presenters
{
    public interface IConditionForm
    {
        DialogResult Ok();
        event Action Changed;
    }

    public class ConditionForm : IConditionForm
    {
        public ConditionForm(TextBox expression, Questionnaire questionnaire, Condition condition)
        {
            Expression = expression;
            Questionnaire = questionnaire;
            Condition = condition;
            Expression.Text = condition.Expression;
        }

        public DialogResult Ok()
        {
            var expression = Expression.Text.Trim();

            try
            {
                new ConditionCalculator().Check(Questionnaire, expression);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return DialogResult.None;
            }
            Condition.Expression = expression;
            Changed();
            return DialogResult.OK;
        }

        private TextBox Expression { get; }
        private Condition Condition { get; set; }
        private Questionnaire Questionnaire { get; set; }
        public event Action Changed = delegate { };
    }
}
