using System;
using System.Collections;

namespace EAP.Framework.Windows.Forms
{

    public class FindOperator {
        // End
        public enum Operator { StartWith, Like, EqualTo, NotEqualTo, Greater, Less, GreaterEqual, LessEqual, Between }
        private Operator m_operator;

        private static string m_strStartWith = " x% ";
        private static string m_strBetween = " x...y ";
        private static string m_strEqualTo = " = ";
        private static string m_strGreater = " > ";
        private static string m_strGreaterEqual = " >= ";
        private static string m_strLike = " % ";
        private static string m_strNotEqualTo = " <> ";
        private static string m_strLess = " < ";
        private static string m_strLessEqual = " <= ";


        // End Add
        public FindOperator(Operator oper) {
            m_operator = oper;
        }
        public static bool operator ==(FindOperator a, FindOperator b) {
            return a.m_operator == b.m_operator;
        }
        public static bool operator !=(FindOperator a, FindOperator b) {
            return a.m_operator != b.m_operator;
        }
        public override bool Equals(object obj) {
            return this.m_operator == ((FindOperator)obj).m_operator;
        }
        //public static bool operator ==(FindOperator a, FindOperator b)
        //{
        //    return a.Name == b.Name;
        //}
        //public static bool operator !=(FindOperator a, FindOperator b)
        //{
        //    return a.Name != b.Name;
        //}
        //public override bool Equals(object obj)
        //{
        //    return this.Name == ((FindOperator)obj).Name;
        //}
        public override int GetHashCode() {
            return base.GetHashCode();
        }
        public override string ToString() {
            //return this.m_operator.ToString();           
            return this.Name;
        }

        public string Name {
            get { return this.m_operator.ToString(); }
        }
        public string Symbol {
            get {
                switch (m_operator) {
                    case Operator.StartWith: return " like ";
                    case Operator.Between: return " between ";
                    case Operator.EqualTo: return " = ";
                    case Operator.Greater: return " > ";
                    case Operator.GreaterEqual: return " >= ";
                    case Operator.Less: return " < ";
                    case Operator.LessEqual: return " <= ";
                    case Operator.Like: return " like ";
                    case Operator.NotEqualTo: return " <> ";
                    default: return string.Empty;
                }
            }
        }
        public static Operator GetOperator(string strOperator) {
            if (strOperator == m_strBetween) {
                return Operator.Between;
            }
            if (strOperator == m_strEqualTo) {
                return Operator.EqualTo;
            }
            if (strOperator == m_strGreater) {
                return Operator.Greater;
            }
            if (strOperator == m_strGreaterEqual) {
                return Operator.GreaterEqual;
            }
            if (strOperator == m_strLess) {
                return Operator.Less;
            }
            if (strOperator == m_strLessEqual) {
                return Operator.LessEqual;
            }
            if (strOperator == m_strLike) {
                return Operator.Like;
            }
            if (strOperator == m_strNotEqualTo) {
                return Operator.NotEqualTo;
            }
            if (strOperator == m_strStartWith) {
                return Operator.StartWith;
            }
            return Operator.EqualTo;
        }
        public string Display {
            get {
                switch (m_operator) {
                    case Operator.StartWith: return m_strStartWith;
                    case Operator.Between: return m_strBetween;
                    case Operator.EqualTo: return m_strEqualTo;
                    case Operator.Greater: return m_strGreater;
                    case Operator.GreaterEqual: return m_strGreaterEqual;
                    case Operator.Less: return m_strLess;
                    case Operator.LessEqual: return m_strLessEqual;
                    case Operator.Like: return m_strLike;
                    case Operator.NotEqualTo: return m_strNotEqualTo;
                    default: return string.Empty;
                }
            }
        }

        public static FindOperator StartWith { get { return new FindOperator(Operator.StartWith); } }
        public static FindOperator Like { get { return new FindOperator(Operator.Like); } }
        public static FindOperator EqualTo { get { return new FindOperator(Operator.EqualTo); } }
        public static FindOperator NotEqualTo { get { return new FindOperator(Operator.NotEqualTo); } }
        public static FindOperator Greater { get { return new FindOperator(Operator.Greater); } }
        public static FindOperator Less { get { return new FindOperator(Operator.Less); } }
        public static FindOperator GreaterEqual { get { return new FindOperator(Operator.GreaterEqual); } }
        public static FindOperator LessEqual { get { return new FindOperator(Operator.LessEqual); } }
        public static FindOperator Between { get { return new FindOperator(Operator.Between); } }
        public static FindOperator[] All {
            get {
                return new FindOperator[]{
							FindOperator.Like,
                            FindOperator.StartWith,
							FindOperator.EqualTo,
							FindOperator.NotEqualTo,
							FindOperator.Greater,
							FindOperator.GreaterEqual,
							FindOperator.Less,
							FindOperator.LessEqual,
							FindOperator.Between
                };
            }
        }

        //Add by Wirachai T. 2007/06/21
        public static FindOperator[] Text {
            get {
                return new FindOperator[]{
							FindOperator.Like,
							FindOperator.StartWith,
                            FindOperator.EqualTo,
							FindOperator.NotEqualTo,
							//FindOperator.Greater,
							//FindOperator.GreaterEqual,
							//FindOperator.Less,
							//FindOperator.LessEqual,
							//FindOperator.Between
                };
            }
        }
        public static FindOperator[] Number {
            get {
                return new FindOperator[]{
							FindOperator.EqualTo,							
					        //FindOperator.Between,		
                            FindOperator.Greater,
							FindOperator.GreaterEqual,
							FindOperator.Less,
							FindOperator.LessEqual
							};
            }
        }
        public static FindOperator[] Combo {
            get {
                return new FindOperator[]{
                            FindOperator.EqualTo};
            }
        }
        public static FindOperator[] DateTime {
            get {
                return new FindOperator[]{
							FindOperator.Between,
							FindOperator.EqualTo,
							FindOperator.Greater,
							FindOperator.GreaterEqual,
							FindOperator.Less,
							FindOperator.LessEqual,
			                FindOperator.NotEqualTo				
                            };
            }
        }
        //---------------------------------------------        
    }

    public class FindOperatorCollection : CollectionBase {

        FindOperator this[int index] {
            get { return (FindOperator)List[index]; }
            set { List[index] = value; }
        }

        public int Add(FindOperator value) {
            return List.Add(value);
        }

        public int IndexOf(FindOperator value) {
            return List.IndexOf(value);
        }

        public void Insert(int index, FindOperator value) {
            List.Insert(index, value);
        }

        public void Remove(FindOperator value) {
            List.Remove(value);
        }

        public bool Contains(FindOperator value) {
            return List.Contains(value);
        }
    }
}
