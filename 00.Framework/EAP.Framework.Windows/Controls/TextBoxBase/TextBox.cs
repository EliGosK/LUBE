using System;
using System.Globalization; 
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;


namespace EAP.Framework.Windows.Controls
{
	#region BlankText
	/// <summary>
	///   Helper class designed to prevent the Text property from being set to
	///   some default value (ie. textBox1) when the control is added to a form. </summary>
	internal class BlankText : System.Windows.Forms.Design.ControlDesigner 
	{
		/// <summary>
		///   This typically sets the control's Text property.  
		///   Here it does nothing so the Text is left blank. </summary>
        //public override void OnSetComponentDefaults()
        //{
        //}
	}
	#endregion

	#region TextBox
	/// <summary>
	///   Base class for all TextBox classes in this namespace. 
	///   It holds a Behavior object that may be associated to it by a derived class. </summary>
	/// <seealso cref="AlphanumericTextBox" />
	/// <seealso cref="MaskedTextBox" />
	/// <seealso cref="NumericTextBox" />
	/// <seealso cref="IntegerTextBox" />
	/// <seealso cref="CurrencyTextBox" />
	/// <seealso cref="DateTextBox" />
	/// <seealso cref="TimeTextBox" />
	/// <seealso cref="DateTimeTextBox" />
	/// <seealso cref="MultiMaskedTextBox" />
	[Browsable(false)]
	[Designer(typeof(BlankText))]	
	public abstract class TextBoxBase : TextBoxRoot
	{
		/// <summary> The Behavior object associated with this TextBox. </summary>
		protected Behavior m_behavior = null; 	// must be initialized by derived classes
		
		/// <summary>
		///   Initializes a new instance of the TextBox class. </summary>
		/// <remarks>
		///   This constructor is just for convenience for derived classes.  It does nothing. </remarks>
		protected TextBoxBase()
		{
		}

		/// <summary>
		///   Initializes a new instance of the TextBox class by explicitly assigning its Behavior field. </summary>
		/// <param name="behavior">
		///   The <see cref="Behavior" /> object to associate the textbox with. </param>
		/// <remarks>
		///   This constructor provides a way for derived classes to set the internal Behavior object
		///   to something other than the default value (such as <c>null</c>). </remarks>
		protected TextBoxBase(Behavior behavior)
		{
			m_behavior = behavior;
		}

		/// <summary>
		///   Checks if the textbox's text is valid and if not updates it with a valid value. </summary>
		/// <returns>
		///   If the textbox's text is updated (because it wasn't valid), the return value is true; otherwise it is false. </returns>
		/// <remarks>
		///   This method delegates to <see cref="Behavior.UpdateText">Behavior.UpdateText</see>. </remarks>
		public bool UpdateText()
		{			
			return m_behavior.UpdateText();
		}		

		/// <summary>
		///   Gets or sets the flags associated with self's Behavior. </summary>
		/// <remarks>
		///   This property delegates to <see cref="Behavior.Flags">Behavior.Flags</see>. </remarks>
		/// <seealso cref="ModifyFlags" />
		[Category("Behavior")]
		[Description("The flags (on/off attributes) associated with the Behavior.")]
		public int Flags
		{
			get 
			{ 
				return m_behavior.Flags; 
			}
			set
			{
				m_behavior.Flags = value;
			}
		}
		
		/// <summary>
		///   Adds or removes flags from self's Behavior. </summary>
		/// <param name="flags">
		///   The bits to be turned on (ORed) or turned off in the internal flags member. </param>
		/// <param name="addOrRemove">
		///   If true the flags are added, otherwise they're removed. </param>
		/// <remarks>
		///   This method delegates to <see cref="Behavior.ModifyFlags">Behavior.ModifyFlags</see>. </remarks>
		/// <seealso cref="Flags" />
		public void ModifyFlags(int flags, bool addOrRemove)
		{
			m_behavior.ModifyFlags(flags, addOrRemove);
		}

		/// <summary>
		///   Checks if the textbox's value is valid and if not proceeds according to the behavior's <see cref="Flags" />. </summary>
		/// <returns>
		///   If the validation succeeds, the return value is true; otherwise it is false. </returns>
		/// <remarks>
		///   This method delegates to <see cref="Behavior.Validate">Behavior.Validate</see>. </remarks>
		/// <seealso cref="IsValid" />
		public bool Validate()
		{
			return m_behavior.Validate();
		}

		/// <summary>
		///   Checks if the textbox contains a valid value. </summary>
		/// <returns>
		///   If the value is valid, the return value is true; otherwise it is false. </returns>
		/// <remarks>
		///   This method delegates to <see cref="Behavior.IsValid">Behavior.IsValid</see>. </remarks>
		/// <seealso cref="Validate" />
		public bool IsValid()
		{
			return m_behavior.IsValid();
		}

		/// <summary>
		///   Show an error message box. </summary>
		/// <param name="message">
		///   The message to show. </param>
		/// <remarks>
		///   This property delegates to <see cref="Behavior.ShowErrorMessageBox">Behavior.ShowErrorMessageBox</see>. </remarks>
		/// <seealso cref="ShowErrorIcon" />
		/// <seealso cref="ErrorMessage" />
		public void ShowErrorMessageBox(string message)
		{
			m_behavior.ShowErrorMessageBox(message);
		}

		/// <summary>
		///   Show a blinking icon next to the textbox with an error message. </summary>
		/// <param name="message">
		///   The message to show when the cursor is placed over the icon. </param>
		/// <remarks>
		///   This property delegates to <see cref="Behavior.ShowErrorIcon">Behavior.ShowErrorIcon</see>. </remarks>
		/// <seealso cref="ShowErrorMessageBox" />
		/// <seealso cref="ErrorMessage" />
		public void ShowErrorIcon(string message)
		{
			m_behavior.ShowErrorIcon(message);
		}

		/// <summary>
		///   Gets the error message used to notify the user to enter a valid value. </summary>
		/// <remarks>
		///   This property delegates to <see cref="Behavior.ErrorMessage">Behavior.ErrorMessage</see>. </remarks>
		/// <seealso cref="Validate" />
		/// <seealso cref="IsValid" />
		public string ErrorMessage
		{
			get
			{
				return m_behavior.ErrorMessage;
			}
		}
		
	}
	#endregion

	#region AlphanumericTextBox
	/////////////////////////////////////////////////////////////////////////////
	// Alphanumeric textbox
	
	/// <summary>
	///   TextBox class which supports the <see cref="AlphanumericBehavior">Alphanumeric</see> behavior. </summary>	
	[Description("TextBox control which supports the Alphanumeric behavior.")]
	public class AlphanumericTextBox : TextBoxBase
	{
		/// <summary>
		///   Initializes a new instance of the AlphanumericTextBox class by assigning its Behavior field
		///   to an instance of <see cref="AlphanumericBehavior" />. </summary>
		public AlphanumericTextBox()
		{
			m_behavior = new AlphanumericBehavior(this);
		}
	
		/// <summary>
		///   Initializes a new instance of the AlphanumericTextBox class by assigning its Behavior field
		///   to an instance of <see cref="AlphanumericBehavior" /> and passing it the characters to consider invalid. </summary>
		/// <param name="invalidChars">
		///   An array of characters that should not be allowed. </param>
		public AlphanumericTextBox(char[] invalidChars)
		{
			m_behavior = new AlphanumericBehavior(this, invalidChars);
		}	

		/// <summary>
		///   Initializes a new instance of the AlphanumericTextBox class by assigning its Behavior field
		///   to an instance of <see cref="AlphanumericBehavior" /> and passing it string of characters to consider invalid. </summary>
		/// <param name="invalidChars">
		///   The set of characters not allowed, concatenated into a string. </param>
		public AlphanumericTextBox(string invalidChars)
		{
			m_behavior = new AlphanumericBehavior(this, invalidChars);
		}	
	
		/// <summary>
		///   Initializes a new instance of the AlphanumericTextBox class by explicitly assigning its Behavior field. </summary>
		/// <param name="behavior">
		///   The <see cref="AlphanumericBehavior" /> object to associate the textbox with. </param>
		public AlphanumericTextBox(AlphanumericBehavior behavior) :
			base(behavior)
		{
		}
		
		/// <summary>
		///   Gets the Behavior object associated with this class. </summary>
		[Browsable(false)]
		public AlphanumericBehavior Behavior
		{
			get 
			{ 
				return (AlphanumericBehavior)m_behavior; 
			}
		}		
		
		/// <summary>
		///   Gets or sets the array of characters considered invalid (not allowed). </summary>
		/// <remarks>
		///   This property delegates to <see cref="AlphanumericBehavior.InvalidChars">AlphanumericBehavior.InvalidChars</see>. </remarks>
		/// <seealso cref="AlphanumericBehavior.InvalidChars" />
		[Category("Behavior")]
		[Description("The array of characters considered invalid (not allowed).")]
		public char[] InvalidChars
		{
			get 
			{ 
				return Behavior.InvalidChars; 
			}
			set 
			{ 
				Behavior.InvalidChars = value;
			}
		}
	}
	#endregion

	#region MaskedTextBox
	/////////////////////////////////////////////////////////////////////////////
	// Masked textbox
	
	/// <summary>
	///   TextBox class which supports the <see cref="MaskedBehavior">Masked</see> behavior. </summary>	
	[Description("TextBox control which supports the Masked behavior.")]
	public class MaskedTextBox : TextBoxBase
	{
		/// <summary>
		///   Initializes a new instance of the MaskedTextBox class by assigning its Behavior field
		///   to an instance of <see cref="MaskedBehavior" />. </summary>
		public MaskedTextBox()
		{
			m_behavior = new MaskedBehavior(this);
		}
	
		/// <summary>
		///   Initializes a new instance of the MaskedTextBox class by assigning its Behavior field
		///   to an instance of <see cref="MaskedBehavior" /> and setting its mask. </summary>
		/// <param name="mask">
		///   The mask string to use for validating and/or formatting the characters entered by the user. 
		///   By default, the <c>#</c> symbol is configured to represent a digit placeholder on the mask. </param>
		public MaskedTextBox(string mask)
		{
			m_behavior = new MaskedBehavior(this, mask);
		}	
	
		/// <summary>
		///   Initializes a new instance of the MaskedTextBox class by explicitly assigning its Behavior field. </summary>
		/// <param name="behavior">
		///   The <see cref="MaskedBehavior" /> object to associate the textbox with. </param>
		public MaskedTextBox(MaskedBehavior behavior) :
			base(behavior)
		{
		}
		
		/// <summary>
		///   Gets the Behavior object associated with this class. </summary>
		[Browsable(false)]
		public MaskedBehavior Behavior
		{
			get 
			{ 
				return (MaskedBehavior)m_behavior; 
			}
		}			

		/// <summary>
		///   Gets or sets the mask -- the string used for validating and/or formatting 
		///   the characters entered by the user.. </summary>
		/// <remarks>
		///   This property delegates to <see cref="MaskedBehavior.Mask">MaskedBehavior.Mask</see>. </remarks>
		/// <seealso cref="MaskedBehavior.Mask" />
		[Category("Behavior")]
		[Description("The string used for formatting the characters entered into the textbox. (# = digit)")]
		public string Mask
		{
			get 
			{ 
				return Behavior.Mask; 
			}
			set 
			{ 
				Behavior.Mask = value;
			}
		}
		
		/// <summary>
		///   Gets the ArrayList of Symbol objects. </summary>
		/// <remarks>
		///   This property delegates to <see cref="MaskedBehavior.Symbols">MaskedBehavior.Symbols</see>. </remarks>
		/// <seealso cref="Mask" />
		/// <seealso cref="MaskedBehavior.Symbol" />
		[Browsable(false)]
		public ArrayList Symbols
		{
			get 
			{ 
				return Behavior.Symbols; 
			}
		}

		/// <summary>
		///   Retrieves the textbox's value without any non-numeric characters. </summary>
		/// <remarks>
		///   This property delegates to <see cref="MaskedBehavior.NumericText">MaskedBehavior.NumericText</see>. </remarks>
		[Browsable(false)]
		public string NumericText
		{
			get 
			{ 
				return Behavior.NumericText;
			}					
		}		
	}
	#endregion

	#region NumericTextBox
	/////////////////////////////////////////////////////////////////////////////
	// Numeric textbox
	
	/// <summary>
	///   TextBox class which supports the <see cref="NumericBehavior">Numeric</see> behavior. </summary>	
	[Description("TextBox control which supports the Numeric behavior.")]
	public class NumericTextBox : TextBoxBase
	{
		/// <summary>
		///   Initializes a new instance of the NumericTextBox class by assigning its Behavior field
		///   to an instance of <see cref="NumericBehavior" />. </summary>
		public NumericTextBox()
		{
			m_behavior = new NumericBehavior(this);
		}

		/// <summary>
		///   Initializes a new instance of the NumericTextBox class by assigning its Behavior field 
		///   to an instance of <see cref="NumericBehavior" /> and setting the maximum number of digits 
		///   allowed left and right of the decimal point. </summary>
		/// <param name="maxWholeDigits">
		///   The maximum number of digits allowed left of the decimal point.
		///   If it is less than 1, it is set to 1. </param>
		/// <param name="maxDecimalPlaces">
		///   The maximum number of digits allowed right of the decimal point.
		///   If it is less than 0, it is set to 0. </param>
		public NumericTextBox(int maxWholeDigits, int maxDecimalPlaces)
		{
			m_behavior = new NumericBehavior(this, maxWholeDigits, maxDecimalPlaces);
		}

		/// <summary>
		///   Initializes a new instance of the NumericTextBox class by explicitly assigning its Behavior field. </summary>
		/// <param name="behavior">
		///   The <see cref="NumericBehavior" /> object to associate the textbox with. </param>
		public NumericTextBox(NumericBehavior behavior) :
			base(behavior)
		{
		}
		
		/// <summary>
		///   Gets the Behavior object associated with this class. </summary>
		[Browsable(false)]
		public NumericBehavior Behavior
		{
			get 
			{ 
				return (NumericBehavior)m_behavior; 
			}
		}

		/// <summary>
		///   Gets or sets the textbox's Text as a double. </summary>
		/// <remarks>
		///   If the text is empty or cannot be converted to a double, a 0 is returned. </remarks>
		/// <seealso cref="Long" />
		/// <seealso cref="Int" />
		[Browsable(false)]
		public double DoubleValue
		{
			get 
			{ 
				try
				{
					return Convert.ToDouble(Behavior.RealNumericText);
				}
				catch
				{
					return 0;
				}   				
			}			
			set 
			{ 
				Text = value.ToString(); 
			}
		}

        [Browsable(false)]
        public decimal DecimalValue
        {
            get
            {
                try
                {                    
                    return Convert.ToDecimal(Behavior.RealNumericText);
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                Text = value.ToString();
            }
        }

        /// <summary>
        ///   Gets or sets the textbox's Text as an int. </summary>
        /// <remarks>
        ///   If the text empty or cannot be converted to an int, a 0 is returned. </remarks>
        /// <seealso cref="Long" />
        /// <seealso cref="Double" />
        [Browsable(false)]
		public int IntValue
		{
			get 
			{ 
				try
				{
					return Convert.ToInt32(Behavior.RealNumericText);
				}
				catch
				{
					return 0;
				}   	
			}
			set 
			{ 
				Text = value.ToString(); 
			}
		}		

		/// <summary>
		///   Gets or sets the textbox's Text as a long. </summary>
		/// <remarks>
		///   If the text empty or cannot be converted to an long, a 0 is returned. </remarks>
		/// <seealso cref="Int" />
		/// <seealso cref="Double" />
		[Browsable(false)]
		public long LongValue
		{
			get 
			{ 
				try
				{
					return Convert.ToInt64(Behavior.RealNumericText);
				}
				catch
				{
					return 0;
				}   				
			}
			set 
			{ 
				Text = value.ToString(); 
			}
		}		

		/// <summary>
		///   Retrieves the textbox's value without any non-numeric characters. </summary>
		/// <remarks>
		///   This property delegates to <see cref="NumericBehavior.NumericText">NumericBehavior.NumericText</see>. </remarks>
		[Browsable(false)]
		public string NumericText
		{
			get 
			{ 
				return Behavior.NumericText;
			}					
		}		

		/// <summary>
		///   Retrieves the textbox's value without any non-numeric characters,
		///   and with a period for the decimal point and a minus for the negative sign. </summary>
		/// <remarks>
		///   This property delegates to <see cref="NumericBehavior.RealNumericText">NumericBehavior.RealNumericText</see>. </remarks>
		[Browsable(false)]
		public string RealNumericText
		{
			get 
			{ 
				return Behavior.RealNumericText;
			}					
		}		

		/// <summary>
		///   Gets or sets the maximum number of digits allowed left of the decimal point. </summary>
		/// <remarks>
		///   This property delegates to <see cref="NumericBehavior.MaxWholeDigits">NumericBehavior.MaxWholeDigits</see>. </remarks>
		/// <seealso cref="NumericBehavior.MaxWholeDigits" />
		[Category("Behavior")]
		[Description("The maximum number of digits allowed left of the decimal point.")]
		public int MaxWholeDigits
		{
			get 
			{ 
				return Behavior.MaxWholeDigits; 
			}
			set 
			{ 
				Behavior.MaxWholeDigits = value;
			}
		}

		/// <summary>
		///   Gets or sets the maximum number of digits allowed right of the decimal point. </summary>
		/// <remarks>
		///   This property delegates to <see cref="NumericBehavior.MaxDecimalPlaces">NumericBehavior.MaxDecimalPlaces</see>. </remarks>
		/// <seealso cref="NumericBehavior.MaxDecimalPlaces" />
		[Category("Behavior")]
		[Description("The maximum number of digits allowed right of the decimal point.")]
		public int MaxDecimalPlaces
		{
			get 
			{ 
				return Behavior.MaxDecimalPlaces; 
			}
			set 
			{ 
				Behavior.MaxDecimalPlaces = value;
			}
		}

		/// <summary>
		///   Gets or sets whether the value is allowed to be negative or not. </summary>
		/// <remarks>
		///   This property delegates to <see cref="NumericBehavior.AllowNegative">NumericBehavior.AllowNegative</see>. </remarks>
		/// <seealso cref="NumericBehavior.AllowNegative" />
		[Category("Behavior")]
		[Description("Determines whether the value is allowed to be negative or not.")]
		public bool AllowNegative
		{
			get 
			{ 
				return Behavior.AllowNegative; 
			}
			set 
			{ 
				Behavior.AllowNegative = value;
			}
		}

		/// <summary>
		///   Gets or sets the number of digits to place in each group to the left of the decimal point. </summary>
		/// <remarks>
		///   This property delegates to <see cref="NumericBehavior.DigitsInGroup">NumericBehavior.DigitsInGroup</see>. </remarks>
		/// <seealso cref="NumericBehavior.DigitsInGroup" />
		[Category("Behavior")]
		[Description("The number of digits to place in each group to the left of the decimal point.")]
		public int DigitsInGroup
		{
			get 
			{ 
				return Behavior.DigitsInGroup; 
			}
			set 
			{ 
				Behavior.DigitsInGroup = value;
			}
		}

		/// <summary>
		///   Gets or sets the character to use for the decimal point. </summary>
		/// <remarks>
		///   This property delegates to <see cref="NumericBehavior.DecimalPoint">NumericBehavior.DecimalPoint</see>. </remarks>
		/// <seealso cref="NumericBehavior.DecimalPoint" />
		[Category("Behavior")]
		[Description("The character to use for the decimal point.")]
		public char DecimalPoint
		{
			get 
			{ 
				return Behavior.DecimalPoint; 
			}
			set 
			{ 
				Behavior.DecimalPoint = value;
			}
		}

		/// <summary>
		///   Gets or sets the character to use for the group separator. </summary>
		/// <remarks>
		///   This property delegates to <see cref="NumericBehavior.GroupSeparator">NumericBehavior.GroupSeparator</see>. </remarks>
		/// <seealso cref="NumericBehavior.GroupSeparator" />
		[Category("Behavior")]
		[Description("The character to use for the group separator (thousands).")]
		public char GroupSeparator
		{
			get 
			{ 
				return Behavior.GroupSeparator; 
			}
			set 
			{ 
				Behavior.GroupSeparator = value;
			}
		}

		/// <summary>
		///   Gets or sets the character to use for the negative sign. </summary>
		/// <remarks>
		///   This property delegates to <see cref="NumericBehavior.NegativeSign">NumericBehavior.NegativeSign</see>. </remarks>
		/// <seealso cref="NumericBehavior.NegativeSign" />
		[Category("Behavior")]
		[Description("The character to use for the negative sign.")]
		public char NegativeSign
		{
			get 
			{ 
				return Behavior.NegativeSign; 
			}
			set 
			{ 
				Behavior.NegativeSign = value;
			}
		}

		/// <summary>
		///   Gets or sets the text to automatically insert in front of the number, such as a currency symbol. </summary>
		/// <remarks>
		///   This property delegates to <see cref="NumericBehavior.Prefix">NumericBehavior.Prefix</see>. </remarks>
		/// <seealso cref="NumericBehavior.Prefix" />
		[Category("Behavior")]
		[Description("The text to automatically insert in front of the number, such as a currency symbol.")]
		public String Prefix
		{
			get 
			{ 
				return Behavior.Prefix; 
			}
			set 
			{ 
				Behavior.Prefix = value;
			}
		}

		/// <summary>
		///   Gets or sets the minimum value allowed. </summary>
		/// <remarks>
		///   This property delegates to <see cref="NumericBehavior.RangeMin">NumericBehavior.RangeMin</see>. </remarks>
		/// <seealso cref="NumericBehavior.RangeMin" />
		[Category("Behavior")]
		[Description("The minimum value allowed.")]
		public double RangeMin
		{
			get 
			{ 
				return Behavior.RangeMin; 
			}
			set 
			{ 
				Behavior.RangeMin = value;
			}
		}

		/// <summary>
		///   Gets or sets the maximum value allowed. </summary>
		/// <remarks>
		///   This property delegates to <see cref="NumericBehavior.RangeMax">NumericBehavior.RangeMax</see>. </remarks>
		/// <seealso cref="NumericBehavior.RangeMax" />
		[Category("Behavior")]
		[Description("The maximum value allowed.")]
		public double RangeMax
		{
			get 
			{ 
				return Behavior.RangeMax; 
			}
			set 
			{ 
				Behavior.RangeMax = value;
			}
		}

        /// <summary>
        /// 
        /// </summary>
	    public bool AddDecimalAfterMaxWholeDigits
	    {
	        get { return Behavior.AddDecimalAfterMaxWholeDigits; }
            set { Behavior.AddDecimalAfterMaxWholeDigits = value; }
	    }
	}
	#endregion

	#region IntegerTextBox
	/////////////////////////////////////////////////////////////////////////////
	// Integer textbox
	
	/// <summary>
	///   TextBox class which supports the <see cref="IntegerBehavior">Integer</see> behavior. </summary>	
	[Description("TextBox control which supports the Integer behavior.")]
	public class IntegerTextBox : NumericTextBox
	{
		/// <summary>
		///   Initializes a new instance of the IntegerTextBox class by assigning its Behavior field
		///   to an instance of <see cref="IntegerBehavior" />. </summary>
		public IntegerTextBox() :
			base(null)
		{
			m_behavior = new IntegerBehavior(this);
		}
	
		/// <summary>
		///   Initializes a new instance of the IntegerTextBox class by assigning its Behavior field 
		///   to an instance of <see cref="IntegerBehavior" /> and setting the maximum number of digits 
		///   allowed left of the decimal point. </summary>
		/// <param name="maxWholeDigits">
		///   The maximum number of digits allowed left of the decimal point.
		///   If it is less than 1, it is set to 1. </param>
		public IntegerTextBox(int maxWholeDigits) :
			base(null)
		{
			m_behavior = new IntegerBehavior(this, maxWholeDigits);
		}	

		/// <summary>
		///   Initializes a new instance of the IntegerTextBox class by explicitly assigning its Behavior field. </summary>
		/// <param name="behavior">
		///   The <see cref="IntegerBehavior" /> object to associate the textbox with. </param>
		public IntegerTextBox(IntegerBehavior behavior) :
			base(behavior)
		{
		}
	}
	#endregion

	#region CurrencyTextBox
	/////////////////////////////////////////////////////////////////////////////
	// Currency textbox
	
	/// <summary>
	///   TextBox class which supports the <see cref="CurrencyBehavior">Currency</see> behavior. </summary>	
	[Description("TextBox control which supports the Currency behavior.")]
	public class CurrencyTextBox : NumericTextBox {
		/// <summary>
		///   Initializes a new instance of the CurrencyTextBox class by assigning its Behavior field
		///   to an instance of <see cref="CurrencyBehavior" />. </summary>
		public CurrencyTextBox() :
			base(null) {
			m_behavior = new CurrencyBehavior(this);
		}	

		/// <summary>
		///   Initializes a new instance of the CurrencyTextBox class by explicitly assigning its Behavior field. </summary>
		/// <param name="behavior">
		///   The <see cref="CurrencyBehavior" /> object to associate the textbox with. </param>
		public CurrencyTextBox(CurrencyBehavior behavior) :
			base(behavior) {
		}

	    public Behavior BEHAVIOR
	    {
            get { return m_behavior; }
	    }
	}
	#endregion

	#region DateTextBox
//	/////////////////////////////////////////////////////////////////////////////
//	// Date textbox
//	
//	/// <summary>
//	///   TextBox class which supports the <see cref="DateBehavior">Date</see> behavior. </summary>	
//	[Description("TextBox control which supports the Date behavior.")]
//	public class DateTextBox : TextBox
//	{
//		/// <summary>
//		///   Initializes a new instance of the DateTextBox class by assigning its Behavior field
//		///   to an instance of <see cref="DateBehavior" />. </summary>
//		public DateTextBox()
//		{
//			m_behavior = new DateBehavior(this);
//		}
//	
//		/// <summary>
//		///   Initializes a new instance of the DateTextBox class by explicitly assigning its Behavior field. </summary>
//		/// <param name="behavior">
//		///   The <see cref="DateBehavior" /> object to associate the textbox with. </param>
//		public DateTextBox(DateBehavior behavior) :
//			base(behavior)
//		{
//		}
//
//		/// <summary>
//		///   Gets the Behavior object associated with this class. </summary>
//		[Browsable(false)]
//		public DateBehavior Behavior
//		{
//			get 
//			{ 
//				return (DateBehavior)m_behavior; 
//			}
//		}
//		
//		/// <summary>
//		///   Gets or sets the month on the textbox. </summary>
//		/// <exception cref="ArgumentOutOfRangeException">Setting this property with an invalid month. </exception>
//		/// <remarks>
//		///   This property delegates to <see cref="DateBehavior.Month">DateBehavior.Month</see>. </remarks>
//		/// <seealso cref="Day" />
//		/// <seealso cref="Year" />
//		[Browsable(false)]
//		public int Month
//		{
//			get 
//			{
//				return Behavior.Month;
//			}
//			set
//			{					
//				Behavior.Month = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets the day on the textbox. </summary>
//		/// <exception cref="ArgumentOutOfRangeException">Setting this property with an invalid day. </exception>
//		/// <remarks>
//		///   This property delegates to <see cref="DateBehavior.Day">DateBehavior.Day</see>. </remarks>
//		/// <seealso cref="Month" />
//		/// <seealso cref="Year" />
//		[Browsable(false)]
//		public int Day
//		{
//			get 
//			{
//				return Behavior.Day;
//			}
//			set
//			{					
//				Behavior.Day = value;
//			}
//		}
//		
//		/// <summary>
//		///   Gets or sets the year on the textbox. </summary>
//		/// <exception cref="ArgumentOutOfRangeException">Setting this property with an invalid year. </exception>
//		/// <remarks>
//		///   This property delegates to <see cref="DateBehavior.Year">DateBehavior.Year</see>. </remarks>
//		/// <seealso cref="Month" />
//		/// <seealso cref="Day" />
//		[Browsable(false)]
//		public int Year
//		{
//			get 
//			{
//				return Behavior.Year;
//			}
//			set
//			{					
//				Behavior.Year = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets the month, day, and year on the textbox using a <see cref="DateTime" /> object. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="DateBehavior.Value">DateBehavior.Value</see>. </remarks>
//		/// <seealso cref="Month" />
//		/// <seealso cref="Day" />
//		/// <seealso cref="Year" />
//		[Browsable(false)]
//		public object Value
//		{
//			get 
//			{
//				return Behavior.Value;
//			}
//			set
//			{					
//				Behavior.Value = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets the minimum value allowed. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="DateBehavior.RangeMin">DateBehavior.RangeMin</see>. </remarks>
//		/// <seealso cref="RangeMax" />
//		[Category("Behavior")]
//		[Description("The minimum value allowed.")]
//		public DateTime RangeMin
//		{
//			get 
//			{
//				return Behavior.RangeMin;
//			}
//			set
//			{					
//				Behavior.RangeMin = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets the maximum value allowed. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="DateBehavior.RangeMax">DateBehavior.RangeMax</see>. </remarks>
//		/// <seealso cref="RangeMin" />
//		[Category("Behavior")]
//		[Description("The maximum value allowed.")]
//		public DateTime RangeMax
//		{
//			get 
//			{
//				return Behavior.RangeMax;
//			}
//			set
//			{					
//				Behavior.RangeMax = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets the character used to separate the month, day, and year values of the date. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="DateBehavior.Separator">DateBehavior.Separator</see>. </remarks>
//		[Category("Behavior")]
//		[Description("The character used to separate the month, day, and year values of the date.")]
//		public char Separator
//		{
//			get 
//			{
//				return Behavior.Separator;
//			}
//			set
//			{					
//				Behavior.Separator = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets whether the day should be shown before the month or after it. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="DateBehavior.ShowDayBeforeMonth">DateBehavior.ShowDayBeforeMonth</see>. </remarks>
//		/// <seealso cref="DateBehavior.Flag.DayBeforeMonth" />
//		[Category("Behavior")]
//		[Description("Determines whether the day should be shown before the month or after it.")]
//		public bool ShowDayBeforeMonth
//		{
//			get 
//			{
//				return Behavior.ShowDayBeforeMonth;
//			}
//			set
//			{					
//				Behavior.ShowDayBeforeMonth = value;
//			}
//		}
//
//		/// <summary>
//		///   Sets the month, day, and year on the textbox. </summary>
//		/// <param name="year">
//		///   The year to set. </param>
//		/// <param name="month">
//		///   The month to set. </param>
//		/// <param name="day">
//		///   The day to set. </param>
//		/// <remarks>
//		///   This method delegates to <see cref="DateBehavior.SetDate">DateBehavior.SetDate</see>. </remarks>
//		public void SetDate(int year, int month, int day)
//		{
//			Behavior.SetDate(year, month, day);
//		}		
//	}
	#endregion	

	#region TimeTextBox
//	/////////////////////////////////////////////////////////////////////////////
//	// Time textbox
//	
//	/// <summary>
//	///   TextBox class which supports the <see cref="TimeBehavior">Time</see> behavior. </summary>	
//	[Description("TextBox control which supports the Time behavior.")]
//	public class TimeTextBox : TextBox
//	{
//		/// <summary>
//		///   Initializes a new instance of the TimeTextBox class by assigning its Behavior field
//		///   to an instance of <see cref="TimeBehavior" />. </summary>
//		public TimeTextBox()
//		{
//			m_behavior = new TimeBehavior(this);
//		}
//	
//		/// <summary>
//		///   Initializes a new instance of the TimeTextBox class by explicitly assigning its Behavior field. </summary>
//		/// <param name="behavior">
//		///   The <see cref="TimeBehavior" /> object to associate the textbox with. </param>
//		public TimeTextBox(TimeBehavior behavior) :
//			base(behavior)
//		{
//		}
//
//		/// <summary>
//		///   Gets the Behavior object associated with this class. </summary>
//		[Browsable(false)]
//		public TimeBehavior Behavior
//		{
//			get 
//			{ 
//				return (TimeBehavior)m_behavior; 
//			}
//		}			
//	
//		/// <summary>
//		///   Gets or sets the hour on the textbox. </summary>
//		/// <exception cref="ArgumentOutOfRangeException">Setting this property with an invalid hour. </exception>
//		/// <remarks>
//		///   This property delegates to <see cref="TimeBehavior.Hour">TimeBehavior.Hour</see>. </remarks>
//		/// <seealso cref="Minute" />
//		/// <seealso cref="Second" />
//		[Browsable(false)]
//		public int Hour
//		{
//			get
//			{			
//				return Behavior.Hour;
//			}			
//			set 
//			{
//				Behavior.Hour = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets the minute on the textbox. </summary>
//		/// <exception cref="ArgumentOutOfRangeException">Setting this property with an invalid minute. </exception>
//		/// <remarks>
//		///   This property delegates to <see cref="TimeBehavior.Minute">TimeBehavior.Minute</see>. </remarks>
//		/// <seealso cref="Hour" />
//		/// <seealso cref="Second" />
//		[Browsable(false)]
//		public int Minute
//		{
//			get
//			{			
//				return Behavior.Minute;
//			}			
//			set 
//			{
//				Behavior.Minute = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets the second on the textbox. </summary>
//		/// <exception cref="ArgumentOutOfRangeException">Setting this property with an invalid second. </exception>
//		/// <remarks>
//		///   This property delegates to <see cref="TimeBehavior.Second">TimeBehavior.Second</see>. </remarks>
//		/// <seealso cref="Hour" />
//		/// <seealso cref="Minute" />
//		[Browsable(false)]
//		public int Second
//		{
//			get
//			{			
//				return Behavior.Second;
//			}			
//			set 
//			{
//				Behavior.Second = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets AM/PM symbol on the textbox. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="TimeBehavior.AMPM">TimeBehavior.AMPM</see>. </remarks>
//		/// <seealso cref="Hour" />
//		/// <seealso cref="Minute" />
//		/// <seealso cref="Second" />
//		[Browsable(false)]
//		public string AMPM
//		{
//			get
//			{			
//				return Behavior.AMPM;
//			}			
//		}
//
//		/// <summary>
//		///   Gets or sets the hour, minute, and second on the textbox using a <see cref="DateTime" /> object. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="TimeBehavior.Value">TimeBehavior.Value</see>. </remarks>
//		/// <seealso cref="Hour" />
//		/// <seealso cref="Minute" />
//		/// <seealso cref="Second" />
//		[Browsable(false)]
//		public object Value
//		{
//			get
//			{			
//				return Behavior.Value;
//			}			
//			set 
//			{
//				Behavior.Value = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets the minimum value allowed. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="TimeBehavior.RangeMin">TimeBehavior.RangeMin</see>. </remarks>
//		/// <seealso cref="RangeMax" />
//		[Category("Behavior")]
//		[Description("The minimum value allowed.")]
//		public DateTime RangeMin
//		{
//			get
//			{			
//				return Behavior.RangeMin;
//			}			
//			set 
//			{
//				Behavior.RangeMin = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets the maximum value allowed. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="TimeBehavior.RangeMax">TimeBehavior.RangeMax</see>. </remarks>
//		/// <seealso cref="RangeMin" />
//		[Category("Behavior")]
//		[Description("The maximum value allowed.")]
//		public DateTime RangeMax
//		{
//			get
//			{			
//				return Behavior.RangeMax;
//			}			
//			set 
//			{
//				Behavior.RangeMax = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets the character used to separate the hour, minute, and second values of the time. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="TimeBehavior.Separator">TimeBehavior.Separator</see>. </remarks>
//		[Category("Behavior")]
//		[Description("The character used to separate the hour, minute, and second values of the time.")]
//		public char Separator
//		{
//			get
//			{			
//				return Behavior.Separator;
//			}			
//			set 
//			{
//				Behavior.Separator = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets whether the hour should be shown in 24-hour format. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="TimeBehavior.Show24HourFormat">TimeBehavior.Show24HourFormat</see>. </remarks>
//		/// <seealso cref="TimeBehavior.Flag.TwentyFourHourFormat" />
//		[Category("Behavior")]
//		[Description("Determines whether the hour should be shown in 24-hour format.")]
//		public bool Show24HourFormat
//		{
//			get
//			{			
//				return Behavior.Show24HourFormat;
//			}			
//			set 
//			{
//				Behavior.Show24HourFormat = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets whether the seconds should be shown (after the minutes). </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="TimeBehavior.ShowSeconds">TimeBehavior.ShowSeconds</see>. </remarks>
//		/// <seealso cref="TimeBehavior.Flag.WithSeconds" />
//		[Category("Behavior")]
//		[Description("Determines whether the seconds should be shown (after the minutes).")]
//		public bool ShowSeconds
//		{
//			get
//			{			
//				return Behavior.ShowSeconds;
//			}			
//			set 
//			{
//				Behavior.ShowSeconds = value;
//			}
//		}
//
//		/// <summary>
//		///   Sets the hour, minute, and second on the textbox. </summary>
//		/// <param name="hour">
//		///   The hour to set, between 0 and 23. </param>
//		/// <param name="minute">
//		///   The minute to set, between 0 and 59. </param>
//		/// <param name="second">
//		///   The second to set, between 0 and 59. </param>
//		/// <remarks>
//		///   This method delegates to <see cref="TimeBehavior.SetTime">TimeBehavior.SetTime</see>. </remarks>
//		public void SetTime(int hour, int minute, int second)
//		{
//			Behavior.SetTime(hour, minute, second);
//		}
//		
//		/// <summary>
//		///   Sets the hour and minute on the textbox. </summary>
//		/// <param name="hour">
//		///   The hour to set, between 0 and 23. </param>
//		/// <param name="minute">
//		///   The minute to set, between 0 and 59. </param>
//		/// <remarks>
//		///   This method delegates to <see cref="TimeBehavior.SetTime">TimeBehavior.SetTime</see>. </remarks>
//		public void SetTime(int hour, int minute)
//		{
//			Behavior.SetTime(hour, minute);
//		}		
//	}
	#endregion

	#region DateTimeTextBox
//	/////////////////////////////////////////////////////////////////////////////
//	// DateTime textbox
//	
//	/// <summary>
//	///   TextBox class which supports the <see cref="DateTimeBehavior">DateTime</see> behavior. </summary>	
//	[Description("TextBox control which supports the DateTime behavior.")]
//	public class DateTimeTextBox : TimeTextBox
//	{
//		/// <summary>
//		///   Initializes a new instance of the DateTimeTextBox class by assigning its Behavior field
//		///   to an instance of <see cref="DateTimeBehavior" />. </summary>
//		public DateTimeTextBox() :
//			base(null)
//		{
//			m_behavior = new DateTimeBehavior(this);
//		}
//	
//		/// <summary>
//		///   Initializes a new instance of the DateTimeTextBox class by explicitly assigning its Behavior field. </summary>
//		/// <param name="behavior">
//		///   The <see cref="DateTimeBehavior" /> object to associate the textbox with. </param>
//		public DateTimeTextBox(DateTimeBehavior behavior) :
//			base(behavior)
//		{
//		}
//		
//		/// <summary>
//		///   Gets the Behavior object associated with this class. </summary>
//		[Browsable(false)]
//		public new DateTimeBehavior Behavior
//		{
//			get 
//			{ 
//				return (DateTimeBehavior)m_behavior; 
//			}
//		}			
//
//		/// <summary>
//		///   Gets or sets the month on the textbox. </summary>
//		/// <exception cref="ArgumentOutOfRangeException">Setting this property with an invalid month. </exception>
//		/// <remarks>
//		///   This property delegates to <see cref="DateTimeBehavior.Month">DateTimeBehavior.Month</see>. </remarks>
//		/// <seealso cref="Day" />
//		/// <seealso cref="Year" />
//		[Browsable(false)]
//		public int Month
//		{
//			get 
//			{
//				return Behavior.Month;
//			}
//			set
//			{					
//				Behavior.Month = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets the day on the textbox. </summary>
//		/// <exception cref="ArgumentOutOfRangeException">Setting this property with an invalid day. </exception>
//		/// <remarks>
//		///   This property delegates to <see cref="DateTimeBehavior.Day">DateTimeBehavior.Day</see>. </remarks>
//		/// <seealso cref="Month" />
//		/// <seealso cref="Year" />
//		[Browsable(false)]
//		public int Day
//		{
//			get 
//			{
//				return Behavior.Day;
//			}
//			set
//			{					
//				Behavior.Day = value;
//			}
//		}
//		
//		/// <summary>
//		///   Gets or sets the year on the textbox. </summary>
//		/// <exception cref="ArgumentOutOfRangeException">Setting this property with an invalid year. </exception>
//		/// <remarks>
//		///   This property delegates to <see cref="DateTimeBehavior.Year">DateTimeBehavior.Year</see>. </remarks>
//		/// <seealso cref="Month" />
//		/// <seealso cref="Day" />
//		[Browsable(false)]
//		public int Year
//		{
//			get 
//			{
//				return Behavior.Year;
//			}
//			set
//			{					
//				Behavior.Year = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets the month, day, and year on the textbox using a <see cref="DateTime" /> object. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="DateTimeBehavior.Value">DateTimeBehavior.Value</see>. </remarks>
//		/// <seealso cref="Month" />
//		/// <seealso cref="Day" />
//		/// <seealso cref="Year" />
//		[Browsable(false)]
//		public new object Value
//		{
//			get 
//			{
//				return Behavior.Value;
//			}
//			set
//			{					
//				Behavior.Value = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets the minimum value allowed. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="DateTimeBehavior.RangeMin">DateTimeBehavior.RangeMin</see>. </remarks>
//		/// <seealso cref="RangeMax" />
//		[Category("Behavior")]
//		[Description("The minimum value allowed.")]
//		public new DateTime RangeMin
//		{
//			get 
//			{
//				return Behavior.RangeMin;
//			}
//			set
//			{					
//				Behavior.RangeMin = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets the maximum value allowed. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="DateTimeBehavior.RangeMax">DateTimeBehavior.RangeMax</see>. </remarks>
//		/// <seealso cref="RangeMin" />
//		[Category("Behavior")]
//		[Description("The maximum value allowed.")]
//		public new DateTime RangeMax
//		{
//			get 
//			{
//				return Behavior.RangeMax;
//			}
//			set
//			{					
//				Behavior.RangeMax = value;
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets the character used to separate the month, day, and year values of the date. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="DateTimeBehavior.DateSeparator">DateTimeBehavior.DateSeparator</see>. </remarks>
//		[Category("Behavior")]
//		[Description("The character used to separate the month, day, and year values of the date.")]
//		public char DateSeparator
//		{
//			get 
//			{ 
//				return Behavior.DateSeparator; 
//			}
//			set 
//			{ 
//				Behavior.DateSeparator = value; 
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets the character used to separate the hour, minute, and second values of the time. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="DateTimeBehavior.TimeSeparator">DateTimeBehavior.TimeSeparator</see>. </remarks>
//		[Category("Behavior")]
//		[Description("The character used to separate the hour, minute, and second values of the time.")]
//		public char TimeSeparator
//		{
//			get 
//			{ 
//				return Behavior.TimeSeparator; 
//			}
//			set 
//			{ 
//				Behavior.TimeSeparator = value; 
//			}
//		}
//
//		/// <summary>
//		///   Gets the character used to separate the date or time value. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="DateTimeBehavior.Separator">DateTimeBehavior.Separator</see>. </remarks>
//		[Browsable(false)]
//		private new char Separator
//		{
//			get
//			{
//				return Behavior.Separator; 
//			}
//		}
//
//		/// <summary>
//		///   Gets or sets whether the day should be shown before the month or after it. </summary>
//		/// <remarks>
//		///   This property delegates to <see cref="DateBehavior.ShowDayBeforeMonth">DateBehavior.ShowDayBeforeMonth</see>. </remarks>
//		/// <seealso cref="DateBehavior.Flag.DayBeforeMonth" />
//		[Category("Behavior")]
//		[Description("Determines whether the day should be shown before the month or after it.")]
//		public bool ShowDayBeforeMonth
//		{
//			get 
//			{
//				return Behavior.ShowDayBeforeMonth;
//			}
//			set
//			{					
//				Behavior.ShowDayBeforeMonth = value;
//			}
//		}
//
//		/// <summary>
//		///   Sets the month, day, and year on the textbox. </summary>
//		/// <param name="year">
//		///   The year to set. </param>
//		/// <param name="month">
//		///   The month to set. </param>
//		/// <param name="day">
//		///   The day to set. </param>
//		/// <remarks>
//		///   This method delegates to <see cref="DateTimeBehavior.SetDate">DateTimeBehavior.SetDate</see>. </remarks>
//		public void SetDate(int year, int month, int day)
//		{
//			Behavior.SetDate(year, month, day);
//		}		
//
//		/// <summary>
//		///   Sets the month, day, year, hour, minute, and second on the textbox. </summary>
//		/// <param name="year">
//		///   The year to set. </param>
//		/// <param name="month">
//		///   The month to set. </param>
//		/// <param name="day">
//		///   The day to set. </param>
//		/// <param name="hour">
//		///   The hour to set, between 0 and 23. </param>
//		/// <param name="minute">
//		///   The minute to set, between 0 and 59. </param>
//		/// <remarks>
//		///   This method delegates to <see cref="DateTimeBehavior.SetDateTime">DateTimeBehavior.SetDateTime</see>. </remarks>
//		public void SetDateTime(int year, int month, int day, int hour, int minute)
//		{
//			Behavior.SetDateTime(year, month, day, hour, minute);
//		}
//
//		/// <summary>
//		///   Sets the month, day, year, hour, minute, and second on the textbox. </summary>
//		/// <param name="year">
//		///   The year to set. </param>
//		/// <param name="month">
//		///   The month to set. </param>
//		/// <param name="day">
//		///   The day to set. </param>
//		/// <param name="hour">
//		///   The hour to set, between 0 and 23. </param>
//		/// <param name="minute">
//		///   The minute to set, between 0 and 59. </param>
//		/// <param name="second">
//		///   The second to set, between 0 and 59. </param>
//		/// <remarks>
//		///   This method delegates to <see cref="DateTimeBehavior.SetDateTime">DateTimeBehavior.SetDateTime</see>. </remarks>
//		public void SetDateTime(int year, int month, int day, int hour, int minute, int second)
//		{
//			Behavior.SetDateTime(year, month, day, hour, minute, second);
//		}
//	}
	#endregion

	#region MultiMaskedTextBox

	/////////////////////////////////////////////////////////////////////////////
	// MultiMasked textbox

	/// <summary>
	///   TextBox class which supports multiple behaviors, based on a mask string. </summary>	
	/// <seealso cref="Mask" />
	[Description("TextBox control which supports multiple behaviors, based on a mask string.")]
	public class MultiMaskedTextBox : TextBoxBase
	{
		// Fields
		private string m_mask = "";

		/// <summary>
		///   Initializes a new instance of the MultiMaskedTextBox class by setting its mask to an
		///   empty string and setting its Behavior field to <see cref="AlphanumericBehavior">Alphanumeric</see>. </summary>
		public MultiMaskedTextBox() : 
			this("")
		{
		}

		/// <summary>
		///   Initializes a new instance of the MultiMaskedTextBox class by setting its mask field and 
		///   corresponding Behavior. </summary>
		/// <param name="mask">
		///   The mask string to use for determining what behavior to associate with this textbox. </param>
		/// <seealso cref="Mask" />
		public MultiMaskedTextBox(string mask)
		{
			Mask = mask;
		}

		/// <summary>
		///   Gets the Behavior object currently associated with this class. </summary>
		/// <remarks>
		///   The actual Behavior class is dependent on the <see cref="Mask" /> assigned to this class. </remarks>
		/// <seealso cref="Mask" />
		[Browsable(false)]
		public Behavior Behavior
		{
			get 
			{ 
				return m_behavior; 
			}
		}			

		/// <summary>
		///   Gets or sets the mask string used to determine which Behavior object to associate with this class. </summary>
		/// <remarks>
		///   The mask string is interpreted as follows:
		///   <list type="bullet">
		///   <item><description>
		///     ##/##/#### ##:##:## = Date and time (with seconds). 
		///     The location of the month and the hour format are retrieved from the user's system. </description></item>
		///   <item><description>
		///     ##/##/#### ##:## = Date and time (without seconds). 
		///     The location of the month and the hour format are retrieved from the user's system. </description></item>
		///   <item><description>
		///     ##/##/#### = Date.  
		///     The location of the month is retrieved from the user's system. </description></item>
		///   <item><description>
		///     ##:##:## = Time (with seconds). 
		///     The location of hour format is retrieved from the user's system. </description></item>
		///   <item><description>
		///     ##:## = Time (without seconds). 
		///     The location of hour format is retrieved from the user's system. </description></item>
		///   <item><description>
		///     If it looks like a numeric value, such as ### or #,###.### (without foreign characters after the first #) 
		///     then it's treated as a number; otherwise it's treated as a masked value (e.g., ###-####). </description></item>
		///   </list></remarks>
		[Category("Behavior")]
		[Description("The string used to determine which Behavior object to associate with this textbox.")]
		public string Mask
		{
			get 
			{ 
				return m_mask; 
			}
			set 
			{ 
				if (m_mask == value && m_behavior != null)
					return;
				
				if (m_behavior != null)
					m_behavior.Dispose();
				Text = "";
				
				m_mask = value; 
				int length = value.Length;

				// If it doesn't have numeric place holders then it's alphanumeric
				int position = value.IndexOf('#');
				if (position < 0)
				{
					m_behavior = new AlphanumericBehavior(this, "");
					return;
				}

				// If it's exactly like the date mask, then it's a date
				if (value == "##/##/#### ##:##:##")
				{
					m_behavior = new DateTimeBehavior(this);
					((DateTimeBehavior)m_behavior).ShowSeconds = true;
					return;
				}

				// If it's exactly like the date mask, then it's a date
				else if (value == "##/##/#### ##:##")
				{
					m_behavior = new DateTimeBehavior(this);
					return;
				}

				// If it's exactly like the date mask, then it's a date
				else if (value == "##/##/####")
				{
					m_behavior = new DateBehavior(this);
					return;
				}

				// If it's exactly like the time mask with seconds, then it's a time
				else if (value == "##:##:##")
				{
					m_behavior = new TimeBehavior(this);
					((TimeBehavior)m_behavior).ShowSeconds = true;
					return;
				}

				// If it's exactly like the time mask, then it's a time
				else if (value == "##:##")
				{
					m_behavior = new TimeBehavior(this);
					return;
				}

				// If after the first numeric placeholder, we don't find any foreign characters,
				// then it's numeric, otherwise it's masked numeric.
				string smallMask = value.Substring(position + 1);
				int smallLength = smallMask.Length;
				
				char decimalPoint = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0];
				char groupSeparator = NumberFormatInfo.CurrentInfo.NumberGroupSeparator[0];

				for (int iPos = 0; iPos < smallLength; iPos++)
				{
					char c = smallMask[iPos];
					if (c != '#' && c != decimalPoint && c != groupSeparator)
					{
						m_behavior = new MaskedBehavior(this, value);
						return;
					}
				}

				// Verify that it ends in a number; otherwise it's a masked numeric
				if (smallLength > 0 && smallMask[smallLength - 1] != '#')
					m_behavior = new MaskedBehavior(this, value);
				else
					m_behavior = new NumericBehavior(this, value);
			}
		}
	}
	#endregion
}
