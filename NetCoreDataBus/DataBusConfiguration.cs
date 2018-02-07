using System.Configuration;

namespace NetCoreDataBus
{

    public class DataBusConfiguration : ConfigurationSection
    {
        private const string HostUrlPropertyName = "hostUrl";
        private const string UserNamePropertyName = "userName";
        private const string PasswordPropertyName = "password";

        public const string ConfigurationSectionName = "dataBusSection";

        private static DataBusConfiguration _current;

        public static DataBusConfiguration Current => _current ?? (_current = ConfigurationManager.GetSection(ConfigurationSectionName) as DataBusConfiguration);

        [ConfigurationProperty(HostUrlPropertyName, DefaultValue = "rabbitmq://localhost/", IsRequired = true)]
        public static string HostUrl
        {
            get => Current == null ? "rabbitmq://localhost/" : (string)Current[HostUrlPropertyName];
            set => Current[HostUrlPropertyName] = value;
        }

        [ConfigurationProperty(UserNamePropertyName, DefaultValue = "guest", IsRequired = true)]
        public static string UserName
        {
            get => Current == null ? "guest" : (string)Current[UserNamePropertyName];
            set => Current[UserNamePropertyName] = value;
        }

        [ConfigurationProperty(PasswordPropertyName, DefaultValue = "guest", IsRequired = true)]
        public static string Password
        {
            get => Current == null ? "guest" : (string)Current[PasswordPropertyName];
            set => Current[PasswordPropertyName] = value;
        }
    }
}