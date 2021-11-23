using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public enum WeighCategories { easy = 1, middle, hevy }
        public enum Priorities { reggular, fast, emergency }
        public enum statusOfQ { available, maintenance, delivery }
        public enum stateOfP { Defined, associated, collected, provided }
    }
}