using System;
using System.Windows.Forms;
using Quest.Core.Model;
using Quest.Core.Services;
using Quest.Core.UI;
using Quest.Localizable;

namespace Quest.Controls.QuestConstructor
{
    public partial class ConditionForm : Form, ILocalizableComponents
    {
        public ConditionForm()
        {
            InitializeComponent();
            this.SetCulture("ru-RU");
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

        public void LocalizableComponents()
        {
            Text = I18NEngine.GetString("quest.controls", "questconstructor_conditionform_this_text");
            label1.Text = I18NEngine.GetString("quest.controls", "questconstructor_conditionform_label1_text");
            label2.Text = I18NEngine.GetString("quest.controls", "questconstructor_conditionform_label2_text");
            btOk.Text = I18NEngine.GetString("quest.controls", "questconstructor_conditionform_btok_text");
        }

        private Condition condition;
        private Questionnaire questionnaire;

        public event Action Changed = delegate { };
    }
}
