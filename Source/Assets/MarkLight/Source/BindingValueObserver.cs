#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Binding value observer.
    /// </summary>
    public class BindingValueObserver : ValueObserver
    {
        #region Fields

        public List<BindingSource> Sources;
        public ViewFieldData Target;
        public BindingType BindingType;
        public string FormatString;
        public MethodInfo TransformMethod;
        public View ParentView;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes static instance of the class.
        /// </summary>
        public BindingValueObserver()
        {
            Sources = new List<BindingSource>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Notifies the binding value observer that value has changed.
        /// </summary>
        public override void Notify(HashSet<ViewFieldData> callstack)
        {
            try
            {
                base.Notify(callstack);
                bool hasValue;
                string boolTypeName = typeof(bool).Name;

                //Debug.Log(String.Format("Source(s) updated. Updating target field: {0}", Target.ViewFieldPath));
                switch (BindingType)
                {
                    default:
                    case BindingType.SingleBinding:
                        var value = Sources[0].ViewFieldData.GetValue(out hasValue);
                        if (hasValue)
                        {
                            // check if value is to be negated
                            if (Sources[0].NegateValue && Sources[0].ViewFieldData.ViewFieldTypeName == boolTypeName)
                            {
                                value = !((bool)value);
                            }

                            // set value
                            Target.SetValue(value, callstack);
                        }
                        break;

                    case BindingType.MultiBindingTransform:
                        object[] pars = Sources.Count > 0 ? new object[Sources.Count] : null;
                        for (int i = 0; i < pars.Length; ++i)
                        {
                            pars[i] = Sources[i].ViewFieldData.GetValue(out hasValue);

                            // check if value is to be negated
                            if (hasValue && Sources[i].NegateValue && Sources[i].ViewFieldData.ViewFieldTypeName == boolTypeName)
                            {
                                pars[i] = !((bool)pars[i]);
                            }
                        }

                        // set transformed value
                        Target.SetValue(TransformMethod.Invoke(ParentView, pars), callstack);
                        break;

                    case BindingType.MultiBindingFormatString:
                        object[] formatPars = Sources.Count > 0 ? new object[Sources.Count] : null;
                        for (int i = 0; i < formatPars.Length; ++i)
                        {
                            formatPars[i] = Sources[i].ViewFieldData.GetValue(out hasValue);

                            // check if value is to be negated
                            if (hasValue && Sources[i].NegateValue && Sources[i].ViewFieldData.ViewFieldTypeName == boolTypeName)
                            {
                                formatPars[i] = !((bool)formatPars[i]);
                            }
                        }

                        // set format string value
                        Target.SetValue(String.Format(FormatString, formatPars), callstack);
                        break;

                }
            }
            catch (Exception e)
            {
                switch (BindingType)
                {
                    default:
                    case BindingType.SingleBinding:
                        Debug.LogError(String.Format("[MarkLight] Exception thrown when propagating single binding value from source \"{0}.{1}\" to target \"{2}.{3}\": {4}", Sources[0].ViewFieldData.SourceView.ViewTypeName, Sources[0].ViewFieldData.ViewFieldPath, Target.SourceView.ViewTypeName, Target.ViewFieldPath, Utils.GetError(e)));
                        break;

                    case BindingType.MultiBindingTransform:
                    case BindingType.MultiBindingFormatString:
                        StringBuilder sb = new StringBuilder();
                        foreach (var source in Sources)
                        {
                            if (source != Sources[0])
                            {
                                sb.Append(", ");
                            }
                            sb.AppendFormat("{0}.{1}", source.ViewFieldData.SourceView.ViewTypeName, source.ViewFieldData.ViewFieldPath);
                        }

                        Debug.LogError(String.Format("[MarkLight] Exception thrown when propagating single binding value from sources \"{0}\" to target \"{2}.{3}\": {4}", sb.ToString(), Target.SourceView.ViewTypeName, Target.ViewFieldPath, Utils.GetError(e)));
                        break;
                }
            }
        }

        #endregion
    }
}
