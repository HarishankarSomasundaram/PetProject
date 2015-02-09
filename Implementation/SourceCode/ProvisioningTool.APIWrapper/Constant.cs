
namespace ServiceAPIWrapper
{
    public static class Constant
    {
        public static readonly string Contact = "Contact";
        public static readonly string Account = "Account";
        public static readonly string SingleConditionQuery = "<queryxml><entity>{0}</entity><query><field>{1}<expression op=\"Contains\">{2}</expression></field></query></queryxml>";
        public static readonly string SubAccountQuery = "<queryxml><entity>{0}</entity><query><condition><field>{1}<expression op=\"Contains\">{2}</expression></field></condition><condition>ParentAccountID<expression op=\"IsNotNull\"></expression></field></condition></query></queryxml>";
    }
}
