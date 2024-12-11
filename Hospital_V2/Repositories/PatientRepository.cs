using Hospital_V2.Models;

namespace Hospital_V2.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);

            _context.SaveChanges();
        }

        public IEnumerable<Patient> GetPatients()
        {
            return _context.Patients.ToList();
        }
    }
}
