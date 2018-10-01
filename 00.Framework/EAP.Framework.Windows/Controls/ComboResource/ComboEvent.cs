/** Author: Mr.Fuangwith Sopharath
 *	Date: 05/06/2006
 *  Todo: Event Class
 **/

using System;
using System.Data;
using System.Windows.Forms;

namespace EAP.Framework.Windows.Controls.ComboResource
{
	/// <summary>
	/// Summary description for ComboEvent.
	/// </summary>
	public class ComboEvent
	{
		private DataRow m_drow = null;
		private int m_iSelectedRow;
		private EventType m_eType;
		private KeyPressEventArgs m_keyPressEvent = null;

		public ComboEvent(DataRow drow,int iSelectedRow,EventType eType)
		{
			this.m_drow = drow;
			this.m_iSelectedRow = iSelectedRow;
			this.m_eType = eType;
		}

		public DataRow Row{
			get{
				return this.m_drow;
			}
		}

		public int SelectedRow{
			get{
				return this.m_iSelectedRow;
			}
		}

		public EventType Type{
			get{
				return this.m_eType;
			}
		}

		public KeyPressEventArgs KeyPressEvent{
			get{
				return this.m_keyPressEvent;
			}
			set{
				this.m_keyPressEvent = value;
			}
		}

	}

	public enum EventType
	{
		SELECTED,
	    FORM_HIDED
	}
}
