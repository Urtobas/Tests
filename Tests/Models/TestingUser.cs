using Microsoft.AspNetCore.Identity;

namespace Tests.Models
{
    public class TestingUser: IdentityUser
    {
        public string AliasName { get; set; }
        public virtual List<TestResult> TestResultsId { get; set; }
    }
}
