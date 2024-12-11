using Hospital_V2.Models;

namespace Hospital_V2.Repositories
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly ApplicationDbContext _context;

        public ClinicRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Clinic GetClinicById(int clinicId)
        {
            return _context.Clinics.FirstOrDefault(c => c.CID == clinicId);
        }

        public void UpdateClinic(Clinic clinic)
        {
            _context.Clinics.Update(clinic);
            _context.SaveChanges();
        }

        public void AddClinic(Clinic clinic)
        {
            _context.Clinics.Add(clinic);
            _context.SaveChanges();
        }

        public IEnumerable<Clinic> GetClinics()
        {
            return _context.Clinics.ToList();
        }
    }
}
