using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.XX
{
    public record SpeakerCountVO(int Content) : ValueObjectBase<int>(Content), IInputLimit<int>
    {
        private const int MinValue = 1;
        private const int MaxValue = 640;

        public static int CurrectValue(int value)
        {
            if (value < MinValue)
            {
                return MinValue;
            }
            else if (value > MaxValue)
            {
                return MaxValue;
            }
            return value;
        }

        public static bool IsValid(int value)
        {
            if (value < MinValue)
            {
                return false;
            }
            else if (value > MaxValue)
            {
                return false;
            }
            return true;
        }

        protected override void Validate()
        {
            if (!IsValid(Content))
            {
                throw new ArgumentException("Invalid Value", nameof(Content));
            }
        }
    }
}
