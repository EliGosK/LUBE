using System;


namespace EAP.Framework.Windows.Controls
{

	public abstract class TextBoxRoot: System.Windows.Forms.TextBox {
		public TextBoxRoot(): base() {}

		private bool isActiveOnTextChanged = true;

		protected override void OnLostFocus(System.EventArgs e) {
			isActiveOnTextChanged = false;
			base.Text = base.Text.Trim();
			isActiveOnTextChanged = true;
			base.OnLostFocus(e);
		}

		protected override void OnTextChanged(EventArgs e) {
			if (isActiveOnTextChanged) {
				base.OnTextChanged (e);
			}
		}

	}
}
