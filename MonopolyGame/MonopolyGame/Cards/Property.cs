namespace MonopolyGame
{
    public class Property
    {
        private int _color;
        private PropertyType _propertyType;

        public Property(int color, PropertyType propertyType)
        {
            _color = color;
            _propertyType = propertyType;
        }

        public int Color
        {
            get { return _color; }
        }

        public PropertyType PropertyType
        {
            get { return _propertyType; }
        }
    }
}
