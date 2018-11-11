using System;
using System.Windows.Forms;
using Quest.Controls.Presenters;
using Quest.Core.Model;
using Quest.Core.UI;
using Quest.Localizable;

namespace Quest.Controls.QuestConstructor
{
    public partial class ConditionForm : Form, ILocalizableComponents
    {
        public ConditionForm(Questionnaire questionnaire, Condition condition)
        {
            InitializeComponent();
            LocalizableComponents();
            Presenter = new Presenters.ConditionForm(tbExpression, questionnaire, condition);
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            DialogResult = Presenter.Ok();
        }

        public void LocalizableComponents()
        {
            Text = I18NEngine.GetString("quest.controls", "questconstructor_conditionform_this_text");
            label1.Text = I18NEngine.GetString("quest.controls", "questconstructor_conditionform_label1_text");
            label2.Text = I18NEngine.GetString("quest.controls", "questconstructor_conditionform_label2_text");
            btOk.Text = I18NEngine.GetString("quest.controls", "questconstructor_conditionform_btok_text");
        }

        private IConditionForm Presenter { get; }

        public event Action Changed
        {
            add => Presenter.Changed += value;
            remove => Presenter.Changed -= value;
        }
    }
}
