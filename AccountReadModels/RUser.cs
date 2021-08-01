namespace AccountReadModels
{
    public class RUser : AccountBaseReadModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string PhoneNumber { get; set; }

        public string DisplayName
        {
            get
            {
                string displayName = FullName;
                if (string.IsNullOrEmpty(displayName))
                {
                    displayName = Email;
                }

                if (string.IsNullOrEmpty(displayName))
                {
                    displayName = PhoneNumber;
                }

                return displayName;
            }
        }
    }
}