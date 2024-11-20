
namespace Aime_Prog_POE.Tests
{
    internal class LecturerDashboard
    {
        private IClaimService @object;

        public LecturerDashboard()
        {
        }

        public LecturerDashboard(IClaimService @object)
        {
            this.@object = @object;
        }

        internal decimal CalculateTotalClaim(decimal hoursWorked, decimal hourlyRate)
        {
            throw new NotImplementedException();
        }
    }
}