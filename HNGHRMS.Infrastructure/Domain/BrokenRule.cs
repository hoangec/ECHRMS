namespace HNGHRMS.Infrastructure.Domain
{
    public class BrokenRule
    {
        private string _RelatesToProperty;
        private string _Rule;
		
        public BrokenRule(string RelatesToProperty, string Rule)
        {
            _RelatesToProperty = RelatesToProperty;
            _Rule = Rule;
        }
		
        public string Rule
        {
            get
            {
                return _Rule;
            }
        }
		
        public string RelatesToProperty
        {
            get
            {
                return _RelatesToProperty;
            }
        }
    }
}
