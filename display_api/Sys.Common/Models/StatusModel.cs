namespace Sys.Common.Models
{
    public class StatusModel
    {
        public StatusModel(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static StatusModel ACTIVE { get { return new StatusModel("ACTIVE"); } }
        public static StatusModel INACTIVE { get { return new StatusModel("INACTIVE"); } }
        public static StatusModel CLOSED { get { return new StatusModel("CLOSED"); } }
        public static StatusModel EXPIRED { get { return new StatusModel("EXPIRED"); } }
        public static StatusModel WAIT { get { return new StatusModel("WAIT"); } }
        public static StatusModel DRAFT { get { return new StatusModel("DRAFT"); } }
        public static StatusModel PUBLISHED { get { return new StatusModel("PUBLISHED"); } }
        public static StatusModel FORCE { get { return new StatusModel("FORCE"); } }
        public static StatusModel WARNING { get { return new StatusModel("WARNING"); } }
        public static StatusModel SELF_HOST { get { return new StatusModel("SELF_HOST"); } }
        public static StatusModel APP_STORE { get { return new StatusModel("APP_STORE"); } }
        public static StatusModel APPLIED { get { return new StatusModel("APPLIED"); } }
    }
}