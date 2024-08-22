using ESAPIX.Constraints;
using System;
using System.Windows.Controls;
using VMS.TPS.Common.Model.API;

namespace ESAPX_StarterUI.ViewModels
{
    public class CTDateConstraint : IConstraint
    {
        public string Name => "CT < 60 days";

        public string FullName => "CT < 60 days";

        public ConstraintResult CanConstrain(PlanningItem pi)
        {
            if(pi.StructureSet.Image != null)
            {
                return new ConstraintResult(this, ResultType.PASSED, "");
            }
            else
            {
                return new ConstraintResult(this, ResultType.NOT_APPLICABLE, "No CT");
            }
        }

        public ConstraintResult Constrain(PlanningItem pi)
        {
            var ctDate = pi.StructureSet.Image.CreationDateTime.Value;
            return ConstrainCTDate(ctDate);
        }

        public ConstraintResult ConstrainCTDate(DateTime ctDate)
        {
            var daysOld = (DateTime.Now - ctDate).TotalDays;
            if(daysOld > 60)
            {
                return new ConstraintResult(this, ResultType.ACTION_LEVEL_3, $"CT is {daysOld} days old.");
            }
            else
            {
                return new ConstraintResult(this, ResultType.PASSED, $"CT is {daysOld} days old.");
            }
        }
    }
}