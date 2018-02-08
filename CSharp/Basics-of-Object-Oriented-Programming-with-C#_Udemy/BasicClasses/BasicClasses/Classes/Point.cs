namespace BasicClasses.Classes
{
    class Point
    {
        // Fields:
        public int x;
        public int y;

        private int a;

        // Properties:
        public int X
        {
            // "get" = read, "set" = write.
            get
            {
                return a;
            }

            set
            {
                if(value >= 0)
                {
                    a = value;
                }
                else
                {
                    a = 0;
                }
            }
        }

        // Contructors:
        public Point()
        {
        }

        // Constructor overloading.
        public Point(int x, int y)
        {
            /* The "this" keyword is used when there's a conflict on
             * naming (eg: arguments vs. fields/properties).
             *
             * It referes to a property/field present on the same class.
             */
            this.x = x;
            this.y = y;
        }
    }
}
